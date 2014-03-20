﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace minecraft_server_gui
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private System.Diagnostics.Process SERVER;
        private System.IO.StreamWriter SERVER_INPUT;
        private System.IO.StreamReader SERVER_OUTPUT;
        private Thread t;
        private Admin admin;
        private Players players;
        private byte playersOnline;
        
        public bool ServerIsRunning;
        public bool ServerShutdown;

        public MainWindow()
        {
            InitializeComponent();
            Properties.Settings.Default.IsAdminConsoleActive = false;
            Properties.Settings.Default.IsPlayerWindowActive = false;
            ServerIsRunning = false;
            ServerShutdown = false;
            SERVER = new Process();
            Textbox_Log.TextWrapping = TextWrapping.Wrap;
            Textbox_Log.AcceptsReturn = true;
            Textbox_Log.IsReadOnly = true;
            playersOnline = 0;
            UpdatePlayersOnline();
        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            if (ServerIsRunning)
            {
                System.Windows.MessageBox.Show("Нельзя выйти из программы при работающем сервере.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                t = new Thread(new ThreadStart(GetLogMessage));
                t.Name = "Server output reader";
                t.Start();

                Status_ProgressBar.Value = 15.0;
            }
        }

        private void Button_Stop_Click(object sender, RoutedEventArgs e)
        {
            Button_Stop.IsEnabled = false;
            if (SERVER.Responding)
            {
                SERVER_INPUT.WriteLine("/stop");
            }
            else SERVER.Kill();
            ServerShutdown = true;
        }

        private void Button_Settings_Click(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings();
            settings.Show();
        }

        private void Button_Send_Click(object sender, RoutedEventArgs e)
        {
            if (ServerIsRunning)
            {
                SERVER_INPUT.WriteLine("/tellraw @a {text:\"[ADMIN] " + Textbox_Send.Text + "\",color:green,bold:true}");
                Textbox_Log.ScrollToEnd();
                Textbox_Log.Text += "[ADMIN] " + Textbox_Send.Text + "\n";
            }
            else
            {
                Textbox_Log.ScrollToEnd();
                Textbox_Log.Text += "Сначала запустите сервер)))\n";
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ServerIsRunning)
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
            if (ServerIsRunning)
            {
                admin = new Admin();
                admin.SERVER_INPUT = SERVER_INPUT;
                admin.Left = this.Left + this.Width;
                admin.Top = this.Top;
                admin.Owner = this;
                Properties.Settings.Default.IsAdminConsoleActive = true;
                admin.Show();
            }
            else
            {
                System.Windows.MessageBox.Show("Нельзя зайти в админ. консоль при выключенном сервере.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private delegate void Invoker(string arg);

        private void PrintLogMessage(string arg)
        {
            if (!ServerIsRunning)
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
                    Status_ProgressBar.Value = 0.0;
                    Button_Stop.IsEnabled = true;
                    ServerIsRunning = true;
                    Status_Text.Content = "Сервер работает.";
                }
            }
            else if (ServerShutdown)
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
            ServerIsRunning = false;
            ServerShutdown = false;
            t.Abort();
            Status_ProgressBar.Value = 0.0;
            Status_Text.Content = "Сервер выключен.";
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.IsAdminConsoleActive)
            {
                admin.Left = this.Left + this.Width;
                admin.Top = this.Top;
            }
            if (Properties.Settings.Default.IsPlayerWindowActive)
            {
                players.Left = this.Left;
                players.Top = this.Top + this.Height;
            }
        }

        private void UpdatePlayersOnline()
        {
            Status_Players_Online.Content = "Игроков онлайн: " + Convert.ToString(playersOnline);
        }

        private void Button_Players_Click(object sender, RoutedEventArgs e)
        {
            if (ServerIsRunning)
            {
                players = new Players();
                players.Left = this.Left;
                players.Top = this.Top + this.Height;
                players.Owner = this;
                Properties.Settings.Default.IsPlayerWindowActive = true;
                players.Show();
            }
            else
            {
                System.Windows.MessageBox.Show("Игроки при выключенном сервере? Ты серьёзно?", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
