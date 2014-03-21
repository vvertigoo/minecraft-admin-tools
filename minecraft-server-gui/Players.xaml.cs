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
        public System.IO.StreamWriter SERVER_INPUT;

        public Players()
        {
            InitializeComponent();
            List_Players.SelectionMode = SelectionMode.Single;
            List_Players.ItemsSource = Properties.Settings.Default.PlayerNames;
            Button_Players_Ban.IsEnabled = false;
            Button_Players_Kick.IsEnabled = false;
            Button_Players_Message_Send.IsEnabled = false;

        }

        private void Button_Players_Close_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.IsPlayerWindowActive = false;
            this.Close();
        }

        private void Button_Players_Kick_Click(object sender, RoutedEventArgs e)
        {
            SERVER_INPUT.WriteLine("/kick " + List_Players.SelectedValue.ToString());
        }

        private void Button_Players_Ban_Click(object sender, RoutedEventArgs e)
        {
            SERVER_INPUT.WriteLine("/ban " + List_Players.SelectedValue.ToString());
        }

        private void Button_Players_Message_Send_Click(object sender, RoutedEventArgs e)
        {
            SERVER_INPUT.WriteLine("/tell " + List_Players.SelectedValue.ToString() + " " + Textbox_Players_Message.Text);
            Textbox_Players_Message.Text = "";
        }

        private void List_Players_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Button_Players_Ban.IsEnabled = true;
            Button_Players_Kick.IsEnabled = true;
            Button_Players_Message_Send.IsEnabled = true;
        }
    }
}
