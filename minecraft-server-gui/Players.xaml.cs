using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;


namespace minecraft_server_gui
{
    /// <summary>
    /// Логика взаимодействия для Players.xaml
    /// </summary>
    public partial class Players
    {
        public StreamWriter SERVER_INPUT;

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
            Close();
        }

        private void Button_Players_Kick_Click(object sender, RoutedEventArgs e)
        {
            SERVER_INPUT.WriteLine("/kick " + List_Players.SelectedValue);
        }

        private void Button_Players_Ban_Click(object sender, RoutedEventArgs e)
        {
            SERVER_INPUT.WriteLine("/ban " + List_Players.SelectedValue);
        }

        private void Button_Players_Message_Send_Click(object sender, RoutedEventArgs e)
        {
            SERVER_INPUT.WriteLine("/tell " + List_Players.SelectedValue + " " + Textbox_Players_Message.Text);
            Textbox_Players_Message.Text = "";
        }

        private void List_Players_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Button_Players_Ban.IsEnabled = true;
            Button_Players_Kick.IsEnabled = true;
            Button_Players_Message_Send.IsEnabled = true;
        }

        private void Window_Players_Closed(object sender, EventArgs e)
        {
            Properties.Settings.Default.IsPlayerWindowActive = false;
            Owner.Activate();
        }
    }
}
