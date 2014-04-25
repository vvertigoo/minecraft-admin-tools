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
    /// Логика взаимодействия для Settings_Remote.xaml
    /// </summary>
    public partial class Settings_Remote : Window
    {
        public Settings_Remote()
        {
            InitializeComponent();
        }

        private void Window_Settings_Remote_Closed(object sender, EventArgs e)
        {
            Properties.Settings.Default.IsSettingsRemoteWindowActive = false;
            this.Owner.Activate();
        }
    }
}
