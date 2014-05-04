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
        private readonly Process SERVER;
        private StreamWriter SERVER_INPUT;
        private StreamReader SERVER_OUTPUT;
        private Thread t;
        private Admin admin;
        private Players players;
        private Settings settings;
        private byte playersOnline;
        private string maxPlayers;
        private Config conf;
        
        public bool IsServerShutdown;

        public MainWindow()
        {
            InitializeComponent();
            Properties.Settings.Default.IsAdminWindowActive = false;
            Properties.Settings.Default.IsPlayerWindowActive = false;
            Properties.Settings.Default.IsServerRunning = false;
            IsServerShutdown = false;
            SERVER = new Process();
            Textbox_Log.TextWrapping = TextWrapping.Wrap;
            Textbox_Log.AcceptsReturn = true;
            Textbox_Log.IsReadOnly = true;
            Button_Admin.IsEnabled = false;
            Button_Players.IsEnabled = false;
            Button_Send.IsEnabled = false;
            playersOnline = 0;
            Properties.Settings.Default.ReadMaxPlayers();
            maxPlayers = Properties.Settings.Default.max_players;
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
            Button_Start.IsEnabled = false;
            Status_Text.Content = "Запускаем...";

            if (File.Exists(Properties.Settings.Default.java_path) && Properties.Settings.Default.java_path.Contains("java.exe") && File.Exists("minecraft_server.jar"))
            {
                SERVER.StartInfo.FileName = Properties.Settings.Default.java_path;
                SERVER.StartInfo.Arguments = "-Xmx" + Properties.Settings.Default.RAM_Max + "M -Xms" + Properties.Settings.Default.RAM_Min + "M -jar minecraft_server.jar nogui";
                SERVER.StartInfo.CreateNoWindow = true;
                SERVER.StartInfo.UseShellExecute = false;
                SERVER.StartInfo.RedirectStandardInput = true;
                SERVER.StartInfo.RedirectStandardOutput = true;
                Status_ProgressBar.Value = 3.0;

                if (SERVER.Start())
                {
                    Status_ProgressBar.Value = 5.0;
                    SERVER_INPUT = SERVER.StandardInput;
                    SERVER_OUTPUT = SERVER.StandardOutput;

                    Status_ProgressBar.Value = 10.0;
                    t = new Thread(GetLogMessage) {Name = "Server output reader"};
                    t.Start();

                    Status_ProgressBar.Value = 15.0;
                    maxPlayers = Properties.Settings.Default.max_players;
                }
            }
            else 
            {
                Status_Text.Content = "Сервер выключен.";
                MessageBox.Show("Не могу найти файлы java.exe и/или minecraft_server.exe.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                Button_Start.IsEnabled = true;
            }
        }

        private void Button_Stop_Click(object sender, RoutedEventArgs e)
        {
            Button_Stop.IsEnabled = false;
            Button_Admin.IsEnabled = false;
            Button_Players.IsEnabled = false;
            Button_Send.IsEnabled = false;
            if (SERVER.Responding)
            {
                SERVER_INPUT.WriteLine("/stop");
            }
            else SERVER.Kill();
            IsServerShutdown = true;
        }

        private void Button_Settings_Click(object sender, RoutedEventArgs e)
        {
            if (!Properties.Settings.Default.IsSettingsWindowActive)
            {
                settings = new Settings {Owner = this, ShowInTaskbar = false};
                Properties.Settings.Default.IsSettingsWindowActive = true;
                settings.ShowDialog();
            }
            else settings.Activate();
        }

        private void Button_Send_Click(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.IsServerRunning)
            {
                SERVER_INPUT.WriteLine("/say " + Textbox_Send.Text);
                Textbox_Log.ScrollToEnd();
            }
            Textbox_Send.Text = "";
        }

        private void Textbox_Send_GotFocus(object sender, RoutedEventArgs e)
        {
            Button_Send.IsDefault = true;
        }

        private void Textbox_Send_LostFocus(object sender, RoutedEventArgs e)
        {
            Button_Send.IsDefault = false;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (Properties.Settings.Default.IsServerRunning)
            {
                t.Abort();
                if (SERVER.Responding)
                {
                    SERVER_INPUT.WriteLine("/stop");
                }
                else SERVER.Kill();
            }
        }

        private void Button_Admin_Click(object sender, RoutedEventArgs e)
        {
            if (!Properties.Settings.Default.IsAdminWindowActive)
            {
                admin = new Admin
                {
                    SERVER_INPUT = SERVER_INPUT,
                    Left = Left + Width,
                    Top = Top,
                    Owner = this,
                    ShowInTaskbar = false
                };
                Properties.Settings.Default.IsAdminWindowActive = true;
                admin.Show();
            }
            else admin.Activate();
        }

        private delegate void Invoker(string arg);

        private void PrintLogMessage(string arg)
        {
            if (!Properties.Settings.Default.IsServerRunning)
            {
                if (arg.Contains("Starting minecraft server version")) Status_ProgressBar.Value = 25.0;
                else if (arg.Contains("Loading properties")) Status_ProgressBar.Value = 45.0;
                else if (arg.Contains("Default game type")) Status_ProgressBar.Value = 65.0;
                else if (arg.Contains("Generating keypair")) Status_ProgressBar.Value = 75.0;
                else if (arg.Contains("Preparing level")) Status_ProgressBar.Value = 85.0;
                else if (arg.Contains("Preparing start region")) Status_ProgressBar.Value = 90.0;
                else if (arg.Contains("Preparing spawn area")) Status_ProgressBar.Value = 95.0;
                else if (arg.Contains("Done"))
                {
                    conf = new Config(SERVER_INPUT);
                    Status_ProgressBar.Value = 0.0;
                    Button_Stop.IsEnabled = true;
                    Properties.Settings.Default.IsServerRunning = true;
                    Status_Text.Content = "Сервер работает.";
                    UpdatePlayersOnline();
                    Button_Admin.IsEnabled = true;
                    Button_Players.IsEnabled = true;
                    Button_Send.IsEnabled = true;
                }
            }
            else if (IsServerShutdown)
            {
                Status_Text.Content = "Выключаем...";
                Status_ProgressBar.Value = 50.0;
                ShutownServer();
            }
            else
            {
                if(arg.Contains("joined the game"))
                {
                    string nickname = arg.Substring(33);
                    nickname = nickname.Substring(0, nickname.Length - 16);
                    Properties.Settings.Default.PlayerNames.Add(nickname);
                    conf.JoinAct(nickname);
                    if (Properties.Settings.Default.IsPlayerWindowActive) players.List_Players.Items.Refresh();
                    playersOnline += 1;
                    UpdatePlayersOnline();
                }
                else if (arg.Contains("left the game"))
                {
                    string nickname = arg.Substring(33);
                    nickname = nickname.Substring(0, nickname.Length - 14);
                    Properties.Settings.Default.PlayerNames.Remove(nickname);
                    if(Properties.Settings.Default.IsPlayerWindowActive) players.List_Players.Items.Refresh();
                    playersOnline -= 1;
                    UpdatePlayersOnline();
                }
                else if (arg.Contains("Time ran backwards") || arg.Contains("Can't keep up"))
                {
                    SERVER_INPUT.WriteLine("/say There's was a lag on server side.");
                }

            }

            Textbox_Log.ScrollToEnd();
            Textbox_Log.Text += arg + "\n";
        }

        private void GetLogMessage()
        {
            while (true)
            {
                if (!Dispatcher.CheckAccess())
                {
                    Dispatcher.Invoke(new Invoker(PrintLogMessage), SERVER_OUTPUT.ReadLine());
                }
                else
                {
                    PrintLogMessage(SERVER_OUTPUT.ReadLine());
                }
            }
            // ReSharper disable once FunctionNeverReturns
        }

        private void ShutownServer()
        {
            while (true)
            {
                if (SERVER.HasExited)
                {
                    break;
                }
            }
            Button_Start.IsEnabled = true;
            Properties.Settings.Default.IsServerRunning = false;
            IsServerShutdown = false;
            t.Abort();
            Status_ProgressBar.Value = 0.0;
            Status_Text.Content = "Сервер выключен.";
            UpdatePlayersOnline();
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.IsAdminWindowActive)
            {
                admin.Left = Left + Width;
                admin.Top = Top;
            }
            if (Properties.Settings.Default.IsPlayerWindowActive)
            {
                players.Left = Left;
                players.Top = Top + Height;
            }
        }

        private void UpdatePlayersOnline()
        {
            if (Properties.Settings.Default.IsServerRunning) Status_Players_Online.Content = "Игроков онлайн: " + Convert.ToString(playersOnline) + "/" + maxPlayers;
            else Status_Players_Online.Content = "Игроков онлайн: -";
        }

        private void Button_Players_Click(object sender, RoutedEventArgs e)
        {
            if (!Properties.Settings.Default.IsPlayerWindowActive)
            {
                players = new Players
                {
                    Left = Left,
                    Top = Top + Height,
                    Owner = this,
                    ShowInTaskbar = false
                };
                Properties.Settings.Default.IsPlayerWindowActive = true;
                players.SERVER_INPUT = SERVER_INPUT;
                players.Show();
            }
            else players.Activate();
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
