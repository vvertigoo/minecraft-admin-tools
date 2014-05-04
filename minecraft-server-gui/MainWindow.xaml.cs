using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows;
using System.Diagnostics;

namespace minecraft_server_gui
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly Process _server;
        private StreamWriter _serverInput;
        private StreamReader _serverOutput;
        private Thread _t;
        private Admin _admin;
        private Players _players;
        private Settings _settings;
        private byte _playersOnline;
        private string _maxPlayers;
        private Config _conf;
        
        public bool IsServerShutdown;

        public MainWindow()
        {
            InitializeComponent();
            Properties.Settings.Default.IsAdminWindowActive = false;
            Properties.Settings.Default.IsPlayerWindowActive = false;
            Properties.Settings.Default.IsServerRunning = false;
            IsServerShutdown = false;
            _server = new Process();
            TextboxLog.TextWrapping = TextWrapping.Wrap;
            TextboxLog.AcceptsReturn = true;
            TextboxLog.IsReadOnly = true;
            ButtonAdmin.IsEnabled = false;
            ButtonPlayers.IsEnabled = false;
            ButtonSend.IsEnabled = false;
            _playersOnline = 0;
            Properties.Settings.Default.ReadMaxPlayers();
            _maxPlayers = Properties.Settings.Default.max_players;
            UpdatePlayersOnline();
        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.IsServerRunning)
            {
                MessageBox.Show("Нельзя выйти из программы при работающем сервере.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Application.Current.Shutdown();
            }
        }

        private void Button_Start_Click(object sender, RoutedEventArgs e)
        {
            ButtonStart.IsEnabled = false;
            StatusText.Content = "Запускаем...";

            if (File.Exists(Properties.Settings.Default.java_path) && Properties.Settings.Default.java_path.Contains("java.exe") && File.Exists("minecraft_server.jar"))
            {
                _server.StartInfo.FileName = Properties.Settings.Default.java_path;
                _server.StartInfo.Arguments = "-Xmx" + Properties.Settings.Default.RAM_Max + "M -Xms" + Properties.Settings.Default.RAM_Min + "M -jar minecraft_server.jar nogui";
                _server.StartInfo.CreateNoWindow = true;
                _server.StartInfo.UseShellExecute = false;
                _server.StartInfo.RedirectStandardInput = true;
                _server.StartInfo.RedirectStandardOutput = true;
                StatusProgressBar.Value = 3.0;

                if (_server.Start())
                {
                    StatusProgressBar.Value = 5.0;
                    _serverInput = _server.StandardInput;
                    _serverOutput = _server.StandardOutput;

                    StatusProgressBar.Value = 10.0;
                    _t = new Thread(GetLogMessage) {Name = "Server output reader"};
                    _t.Start();

                    StatusProgressBar.Value = 15.0;
                    _maxPlayers = Properties.Settings.Default.max_players;
                }
            }
            else 
            {
                StatusText.Content = "Сервер выключен.";
                MessageBox.Show("Не могу найти файлы java.exe и/или minecraft_server.exe.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                ButtonStart.IsEnabled = true;
            }
        }

        private void Button_Stop_Click(object sender, RoutedEventArgs e)
        {
            ButtonStop.IsEnabled = false;
            ButtonAdmin.IsEnabled = false;
            ButtonPlayers.IsEnabled = false;
            ButtonSend.IsEnabled = false;
            if (_server.Responding)
            {
                _serverInput.WriteLine("/stop");
            }
            else _server.Kill();
            IsServerShutdown = true;
        }

        private void Button_Settings_Click(object sender, RoutedEventArgs e)
        {
            if (!Properties.Settings.Default.IsSettingsWindowActive)
            {
                _settings = new Settings {Owner = this, ShowInTaskbar = false};
                Properties.Settings.Default.IsSettingsWindowActive = true;
                _settings.ShowDialog();
            }
            else _settings.Activate();
        }

        private void Button_Send_Click(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.IsServerRunning)
            {
                _serverInput.WriteLine("/say " + TextboxSend.Text);
                TextboxLog.ScrollToEnd();
            }
            TextboxSend.Text = "";
        }

        private void Textbox_Send_GotFocus(object sender, RoutedEventArgs e)
        {
            ButtonSend.IsDefault = true;
        }

        private void Textbox_Send_LostFocus(object sender, RoutedEventArgs e)
        {
            ButtonSend.IsDefault = false;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (Properties.Settings.Default.IsServerRunning)
            {
                _t.Abort();
                if (_server.Responding)
                {
                    _serverInput.WriteLine("/stop");
                }
                else _server.Kill();
            }
        }

        private void Button_Admin_Click(object sender, RoutedEventArgs e)
        {
            if (!Properties.Settings.Default.IsAdminWindowActive)
            {
                _admin = new Admin
                {
                    ServerInput = _serverInput,
                    Left = Left + Width,
                    Top = Top,
                    Owner = this,
                    ShowInTaskbar = false
                };
                Properties.Settings.Default.IsAdminWindowActive = true;
                _admin.Show();
            }
            else _admin.Activate();
        }

        private delegate void Invoker(string arg);

        private void PrintLogMessage(string arg)
        {
            if (!Properties.Settings.Default.IsServerRunning)
            {
                if (arg.Contains("Starting minecraft server version")) StatusProgressBar.Value = 25.0;
                else if (arg.Contains("Loading properties")) StatusProgressBar.Value = 45.0;
                else if (arg.Contains("Default game type")) StatusProgressBar.Value = 65.0;
                else if (arg.Contains("Generating keypair")) StatusProgressBar.Value = 75.0;
                else if (arg.Contains("Preparing level")) StatusProgressBar.Value = 85.0;
                else if (arg.Contains("Preparing start region")) StatusProgressBar.Value = 90.0;
                else if (arg.Contains("Preparing spawn area")) StatusProgressBar.Value = 95.0;
                else if (arg.Contains("Done"))
                {
                    _conf = new Config(_serverInput);
                    StatusProgressBar.Value = 0.0;
                    ButtonStop.IsEnabled = true;
                    Properties.Settings.Default.IsServerRunning = true;
                    StatusText.Content = "Сервер работает.";
                    UpdatePlayersOnline();
                    ButtonAdmin.IsEnabled = true;
                    ButtonPlayers.IsEnabled = true;
                    ButtonSend.IsEnabled = true;
                }
            }
            else if (IsServerShutdown)
            {
                StatusText.Content = "Выключаем...";
                StatusProgressBar.Value = 50.0;
                ShutownServer();
            }
            else
            {
                if(arg.Contains("joined the game"))
                {
                    string nickname = arg.Substring(33);
                    nickname = nickname.Substring(0, nickname.Length - 16);
                    Properties.Settings.Default.PlayerNames.Add(nickname);
                    _conf.JoinAct(nickname);
                    if (Properties.Settings.Default.IsPlayerWindowActive) _players.ListPlayers.Items.Refresh();
                    _playersOnline += 1;
                    UpdatePlayersOnline();
                }
                else if (arg.Contains("left the game"))
                {
                    string nickname = arg.Substring(33);
                    nickname = nickname.Substring(0, nickname.Length - 14);
                    Properties.Settings.Default.PlayerNames.Remove(nickname);
                    if(Properties.Settings.Default.IsPlayerWindowActive) _players.ListPlayers.Items.Refresh();
                    _playersOnline -= 1;
                    UpdatePlayersOnline();
                }
                else if (arg.Contains("Time ran backwards") || arg.Contains("Can't keep up"))
                {
                    _serverInput.WriteLine("/say There's was a lag on server side.");
                }

            }

            TextboxLog.ScrollToEnd();
            TextboxLog.Text += arg + "\n";
        }

        private void GetLogMessage()
        {
            while (true)
            {
                if (!Dispatcher.CheckAccess())
                {
                    Dispatcher.Invoke(new Invoker(PrintLogMessage), _serverOutput.ReadLine());
                }
                else
                {
                    PrintLogMessage(_serverOutput.ReadLine());
                }
            }
            // ReSharper disable once FunctionNeverReturns
        }

        private void ShutownServer()
        {
            while (true)
            {
                if (_server.HasExited)
                {
                    break;
                }
            }
            ButtonStart.IsEnabled = true;
            Properties.Settings.Default.IsServerRunning = false;
            IsServerShutdown = false;
            _t.Abort();
            StatusProgressBar.Value = 0.0;
            StatusText.Content = "Сервер выключен.";
            UpdatePlayersOnline();
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.IsAdminWindowActive)
            {
                _admin.Left = Left + Width;
                _admin.Top = Top;
            }
            if (Properties.Settings.Default.IsPlayerWindowActive)
            {
                _players.Left = Left;
                _players.Top = Top + Height;
            }
        }

        private void UpdatePlayersOnline()
        {
            if (Properties.Settings.Default.IsServerRunning) StatusPlayersOnline.Content = "Игроков онлайн: " + Convert.ToString(_playersOnline) + "/" + _maxPlayers;
            else StatusPlayersOnline.Content = "Игроков онлайн: -";
        }

        private void Button_Players_Click(object sender, RoutedEventArgs e)
        {
            if (!Properties.Settings.Default.IsPlayerWindowActive)
            {
                _players = new Players
                {
                    Left = Left,
                    Top = Top + Height,
                    Owner = this,
                    ShowInTaskbar = false
                };
                Properties.Settings.Default.IsPlayerWindowActive = true;
                _players.ServerInput = _serverInput;
                _players.Show();
            }
            else _players.Activate();
        }

        /*private void ConnectMessage(string playerName)
        {
            if (File.Exists("connectmessages.conf"))
            {
                StreamReader connectMessageConfig = File.OpenText("connectmessages.conf");
                string data;
                while (!connectMessageConfig.EndOfStream)
                {
                    data = connectMessageConfig.ReadLine();
                    if (!data.StartsWith("###"))
                    {
                        //SERVER_INPUT.WriteLine("/tell " + playerName + " " + data);
                        SERVER_INPUT.WriteLine("/tellraw " + playerName + " {text:\"[SERVER] " + data + "\",color:green,bold:true}");
                    }
                }
                connectMessageConfig.Close();
            }
            else
            {
                StreamWriter connectMessageConfig = File.CreateText("connectmessages.conf");
                connectMessageConfig.WriteLine("### <- This is comment line");
                connectMessageConfig.WriteLine("###");
                connectMessageConfig.WriteLine("###");
                connectMessageConfig.WriteLine("### In this file you can write any text, which will be displayed to connected players.");
                connectMessageConfig.Close();
            }
        }*/
    }
}
