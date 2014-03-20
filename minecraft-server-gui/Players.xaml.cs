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
    /// Логика взаимодействия для Players.xaml
    /// </summary>
    public partial class Players : Window
    {
        public Players()
        {
            InitializeComponent();
            List_Players.SelectionMode = SelectionMode.Single;
            List_Players.ItemsSource = Properties.Settings.Default.PlayerNames;
        }

        private void Button_players_close_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.IsPlayerWindowActive = false;
            this.Close();
        }
    }
}
