using System;
using System.Windows;
using Microsoft.Win32;

namespace minecraft_server_gui
{
    /// <summary>
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings
    {
        string newFileName;
        bool java_filename_changed;

        Settings_Server settings_server;
        Settings_Remote settings_remote;

        public Settings()
        {
            InitializeComponent();
            Textbox_Settings_JavaPath.Text = Properties.Settings.Default.java_path;
            Textbox_Settings_RAM_Min.Text = Properties.Settings.Default.RAM_Min;
            Textbox_Settings_RAM_Max.Text = Properties.Settings.Default.RAM_Max;
            java_filename_changed = false;
        }

        private void Button_Settings_Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Settings_More_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog
            {
                FileName = "java",
                DefaultExt = ".exe",
                Filter = "Application (.exe)|*.exe"
            };

            // Show open file dialog box
            bool? result = dlg.ShowDialog();

            // Process open file dialog box results 
            if (result == true)
            {
                // Open document 
                newFileName = dlg.FileName;
                Textbox_Settings_JavaPath.Text = newFileName;
                java_filename_changed = true;
            }
        }

        private void Button_Settings_Save_Click(object sender, RoutedEventArgs e)
        {
            if(java_filename_changed) Properties.Settings.Default.java_path = newFileName;
            Properties.Settings.Default.RAM_Min = Textbox_Settings_RAM_Min.Text;
            Properties.Settings.Default.RAM_Max = Textbox_Settings_RAM_Max.Text;
            Properties.Settings.Default.Save();
        }

        private void Button_Settings_Server_Click(object sender, RoutedEventArgs e)
        {
            if (!Properties.Settings.Default.IsSettingsServerWindowActive)
            {
                settings_server = new Settings_Server {Owner = this, ShowInTaskbar = false};
                Properties.Settings.Default.IsSettingsServerWindowActive = true;
                settings_server.ShowDialog();
            }
            else settings_server.Activate();
        }

        private void Window_Settings_Closed(object sender, EventArgs e)
        {
            Properties.Settings.Default.IsSettingsWindowActive = false;
            Owner.Activate();
        }

        private void Button_Settings_Remote_Click(object sender, RoutedEventArgs e)
        {
            if (!Properties.Settings.Default.IsSettingsRemoteWindowActive)
            {
                settings_remote = new Settings_Remote {Owner = this, ShowInTaskbar = false};
                Properties.Settings.Default.IsSettingsRemoteWindowActive = true;
                settings_remote.ShowDialog();
            }
            else settings_remote.Activate();
        }
    }
}
