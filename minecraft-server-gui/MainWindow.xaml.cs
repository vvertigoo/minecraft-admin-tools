using System;
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
        System.Diagnostics.Process SERVER = new Process();
        System.IO.StreamWriter SERVER_INPUT;
        System.IO.StreamReader SERVER_OUTPUT;
        
        public bool ServerIsRunning = false;

        public MainWindow()
        {
            InitializeComponent();
            Textbox_Log.TextWrapping = TextWrapping.Wrap;
            Textbox_Log.AcceptsReturn = true;
            Textbox_Log.IsReadOnly = true;
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
            Status_ProgressBar.Value = 0.0;
            SERVER.StartInfo.Arguments = "-Xmx1024M -Xms1024M -jar minecraft_server.jar nogui";
            SERVER.StartInfo.CreateNoWindow = true;
            SERVER.StartInfo.UseShellExecute = false;
            SERVER.StartInfo.RedirectStandardInput = true;
            SERVER.StartInfo.RedirectStandardOutput = true;
            Status_ProgressBar.Value = 5.0;

            if (SERVER.Start())
            {
                SERVER_INPUT = SERVER.StandardInput;
                SERVER_OUTPUT = SERVER.StandardOutput;
                Status_ProgressBar.Value = 100.0;

                Button_Stop.IsEnabled = true;
                ServerIsRunning = true;
                Status_Text.Content = "Сервер работает.";
                Status_ProgressBar.Value = 0.0;
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
            Button_Start.IsEnabled = true;
            ServerIsRunning = false;
            Status_Text.Content = "Сервер выключен.";
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
                if (SERVER.Responding)
                {
                    SERVER_INPUT.WriteLine("/stop");
                }
                else SERVER.Kill();
            }
        }

        private void Button_Admin_Click(object sender, RoutedEventArgs e)
        {
            Admin admin = new Admin();
            admin.Show();
        }
    }
}
