using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace minecraft_server_gui
{
    /// <summary>
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        string newFileName;
        bool java_filename_changed;

        Settings_Server settings_server;

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
            this.Close();
        }

        private void Button_Settings_More_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "java"; // Default file name
            dlg.DefaultExt = ".exe"; // Default file extension
            dlg.Filter = "Application (.exe)|*.exe"; // Filter files by extension 

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

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
                settings_server = new Settings_Server();
                settings_server.Owner = this;
                settings_server.ShowInTaskbar = false;
                Properties.Settings.Default.IsSettingsServerWindowActive = true;
                settings_server.ShowDialog();
            }
            else settings_server.Activate();
        }

        private void Window_Settings_Closed(object sender, EventArgs e)
        {
            Properties.Settings.Default.IsSettingsWindowActive = false;
            this.Owner.Activate();
        }
    }
}
