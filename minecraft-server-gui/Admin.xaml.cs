using System;
using System.IO;
using System.Windows;

namespace minecraft_server_gui
{
    delegate void UpdateMacrosNames();
    /// <summary>
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin
    {
        public StreamWriter SERVER_INPUT;
        private Admin_Settings admin_settings;

        public Admin()
        {
            InitializeComponent();
            Properties.Settings.Default.UpdateMacrosNamesMethod = UpdateMacros;
            UpdateMacros();
        }

        public void UpdateMacros()
        {
            Button_Admin_Macro1.Content = Properties.Settings.Default.macro_1_name;
            Button_Admin_Macro2.Content = Properties.Settings.Default.macro_2_name;
            Button_Admin_Macro3.Content = Properties.Settings.Default.macro_3_name;
            Button_Admin_Macro4.Content = Properties.Settings.Default.macro_4_name;
            Button_Admin_Macro5.Content = Properties.Settings.Default.macro_5_name;
            Button_Admin_Macro6.Content = Properties.Settings.Default.macro_6_name;
            Button_Admin_Macro7.Content = Properties.Settings.Default.macro_7_name;
            Button_Admin_Macro8.Content = Properties.Settings.Default.macro_8_name;
            Button_Admin_Macro9.Content = Properties.Settings.Default.macro_9_name;
            Button_Admin_Macro10.Content = Properties.Settings.Default.macro_10_name;
            Button_Admin_Macro11.Content = Properties.Settings.Default.macro_11_name;
            Button_Admin_Macro12.Content = Properties.Settings.Default.macro_12_name;
            Button_Admin_Macro13.Content = Properties.Settings.Default.macro_13_name;
            Button_Admin_Macro14.Content = Properties.Settings.Default.macro_14_name;
            Button_Admin_Macro15.Content = Properties.Settings.Default.macro_15_name;
        }

        private void Button_Admin_Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Admin_Enter_Click(object sender, RoutedEventArgs e)
        {
            SERVER_INPUT.WriteLine(Textbox_Admin_Input.Text);
            Textbox_Admin_Input.Text = "";
        }

        private void Button_Admin_Settings_Click(object sender, RoutedEventArgs e)
        {
            if (!Properties.Settings.Default.IsAdminSettingsWindowActive)
            {
                admin_settings = new Admin_Settings {ShowInTaskbar = false, Owner = this};
                Properties.Settings.Default.IsAdminSettingsWindowActive = true;
                admin_settings.ShowDialog();
            }
            else if (admin_settings.IsInitialized) admin_settings.Activate();
        }

        private void PlaceMacro(string macro)
        {
            Textbox_Admin_Input.Text += macro;
        }

        #region
        private void Button_Admin_Macro1_Click(object sender, RoutedEventArgs e)
        {
            PlaceMacro(Properties.Settings.Default.macro_1_value);
        }

        private void Button_Admin_Macro2_Click(object sender, RoutedEventArgs e)
        {
            PlaceMacro(Properties.Settings.Default.macro_2_value);
        }

        private void Button_Admin_Macro3_Click(object sender, RoutedEventArgs e)
        {
            PlaceMacro(Properties.Settings.Default.macro_3_value);
        }

        private void Button_Admin_Macro4_Click(object sender, RoutedEventArgs e)
        {
            PlaceMacro(Properties.Settings.Default.macro_4_value);
        }

        private void Button_Admin_Macro5_Click(object sender, RoutedEventArgs e)
        {
            PlaceMacro(Properties.Settings.Default.macro_5_value);
        }

        private void Button_Admin_Macro6_Click(object sender, RoutedEventArgs e)
        {
            PlaceMacro(Properties.Settings.Default.macro_6_value);
        }

        private void Button_Admin_Macro7_Click(object sender, RoutedEventArgs e)
        {
            PlaceMacro(Properties.Settings.Default.macro_7_value);
        }

        private void Button_Admin_Macro8_Click(object sender, RoutedEventArgs e)
        {
            PlaceMacro(Properties.Settings.Default.macro_8_value);
        }

        private void Button_Admin_Macro9_Click(object sender, RoutedEventArgs e)
        {
            PlaceMacro(Properties.Settings.Default.macro_9_value);
        }

        private void Button_Admin_Macro10_Click(object sender, RoutedEventArgs e)
        {
            PlaceMacro(Properties.Settings.Default.macro_10_value);
        }

        private void Button_Admin_Macro11_Click(object sender, RoutedEventArgs e)
        {
            PlaceMacro(Properties.Settings.Default.macro_11_value);
        }

        private void Button_Admin_Macro12_Click(object sender, RoutedEventArgs e)
        {
            PlaceMacro(Properties.Settings.Default.macro_12_value);
        }

        private void Button_Admin_Macro13_Click(object sender, RoutedEventArgs e)
        {
            PlaceMacro(Properties.Settings.Default.macro_13_value);
        }

        private void Button_Admin_Macro14_Click(object sender, RoutedEventArgs e)
        {
            PlaceMacro(Properties.Settings.Default.macro_14_value);
        }

        private void Button_Admin_Macro15_Click(object sender, RoutedEventArgs e)
        {
            PlaceMacro(Properties.Settings.Default.macro_15_value);
        }
        #endregion

        private void Window_Admin_Closed(object sender, EventArgs e)
        {
            Properties.Settings.Default.IsAdminWindowActive = false;
            Owner.Activate();
        }
    }
}
