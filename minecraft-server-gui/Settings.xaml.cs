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
        string _newFileName;
        bool _javaFilenameChanged;

        SettingsServer _settingsServer;
        SettingsRemote _settingsRemote;

        public Settings()
        {
            InitializeComponent();
            TextboxJavaPath.Text = Properties.Settings.Default.java_path;
            TextboxRamMin.Text = Properties.Settings.Default.RAM_Min;
            TextboxRamMax.Text = Properties.Settings.Default.RAM_Max;
            _javaFilenameChanged = false;
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
                _newFileName = dlg.FileName;
                TextboxJavaPath.Text = _newFileName;
                _javaFilenameChanged = true;
            }
        }

        private void Button_Settings_Save_Click(object sender, RoutedEventArgs e)
        {
            if(_javaFilenameChanged) Properties.Settings.Default.java_path = _newFileName;
            Properties.Settings.Default.RAM_Min = TextboxRamMin.Text;
            Properties.Settings.Default.RAM_Max = TextboxRamMax.Text;
            Properties.Settings.Default.Save();
        }

        private void Button_Settings_Server_Click(object sender, RoutedEventArgs e)
        {
            if (!Properties.Settings.Default.IsSettingsServerWindowActive)
            {
                _settingsServer = new SettingsServer {Owner = this, ShowInTaskbar = false};
                Properties.Settings.Default.IsSettingsServerWindowActive = true;
                _settingsServer.ShowDialog();
            }
            else _settingsServer.Activate();
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
                _settingsRemote = new SettingsRemote {Owner = this, ShowInTaskbar = false};
                Properties.Settings.Default.IsSettingsRemoteWindowActive = true;
                _settingsRemote.ShowDialog();
            }
            else _settingsRemote.Activate();
        }
    }
}
