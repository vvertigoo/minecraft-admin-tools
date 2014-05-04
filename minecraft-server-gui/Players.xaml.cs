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
        public StreamWriter ServerInput;

        public Players()
        {
            InitializeComponent();
            ListPlayers.SelectionMode = SelectionMode.Single;
            ListPlayers.ItemsSource = Properties.Settings.Default.PlayerNames;
            ButtonPlayersBan.IsEnabled = false;
            ButtonPlayersKick.IsEnabled = false;
            ButtonPlayersMessageSend.IsEnabled = false;

        }

        private void Button_Players_Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Players_Kick_Click(object sender, RoutedEventArgs e)
        {
            ServerInput.WriteLine("/kick " + ListPlayers.SelectedValue);
        }

        private void Button_Players_Ban_Click(object sender, RoutedEventArgs e)
        {
            ServerInput.WriteLine("/ban " + ListPlayers.SelectedValue);
        }

        private void Button_Players_Message_Send_Click(object sender, RoutedEventArgs e)
        {
            ServerInput.WriteLine("/tell " + ListPlayers.SelectedValue + " " + TextboxPlayersMessage.Text);
            TextboxPlayersMessage.Text = "";
        }

        private void List_Players_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtonPlayersBan.IsEnabled = true;
            ButtonPlayersKick.IsEnabled = true;
            ButtonPlayersMessageSend.IsEnabled = true;
        }

        private void Window_Players_Closed(object sender, EventArgs e)
        {
            Properties.Settings.Default.IsPlayerWindowActive = false;
            Owner.Activate();
        }
    }
}
