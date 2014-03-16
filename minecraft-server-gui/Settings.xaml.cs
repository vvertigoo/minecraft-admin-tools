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

        public Settings()
        {
            InitializeComponent();
            Textbox_Settings_JavaPath.Text = Properties.Settings.Default.java_path;
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
            }
        }

        private void Button_Settings_Save_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.java_path = newFileName;
            Properties.Settings.Default.Save();
        }
    }
}
