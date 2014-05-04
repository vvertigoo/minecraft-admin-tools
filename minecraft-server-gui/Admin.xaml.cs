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
        public StreamWriter ServerInput;
        private AdminSettings _adminSettings;

        public Admin()
        {
            InitializeComponent();
            Properties.Settings.Default.UpdateMacrosNamesMethod = UpdateMacros;
            UpdateMacros();
        }

        public void UpdateMacros()
        {
            ButtonMacro1.Content = Properties.Settings.Default.macro_1_name;
            ButtonMacro2.Content = Properties.Settings.Default.macro_2_name;
            ButtonMacro3.Content = Properties.Settings.Default.macro_3_name;
            ButtonMacro4.Content = Properties.Settings.Default.macro_4_name;
            ButtonMacro5.Content = Properties.Settings.Default.macro_5_name;
            ButtonMacro6.Content = Properties.Settings.Default.macro_6_name;
            ButtonMacro7.Content = Properties.Settings.Default.macro_7_name;
            ButtonMacro8.Content = Properties.Settings.Default.macro_8_name;
            ButtonMacro9.Content = Properties.Settings.Default.macro_9_name;
            ButtonMacro10.Content = Properties.Settings.Default.macro_10_name;
            ButtonMacro11.Content = Properties.Settings.Default.macro_11_name;
            ButtonMacro12.Content = Properties.Settings.Default.macro_12_name;
            ButtonMacro13.Content = Properties.Settings.Default.macro_13_name;
            ButtonMacro14.Content = Properties.Settings.Default.macro_14_name;
            ButtonMacro15.Content = Properties.Settings.Default.macro_15_name;
        }

        private void Button_Admin_Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Admin_Enter_Click(object sender, RoutedEventArgs e)
        {
            ServerInput.WriteLine(TextboxInput.Text);
            TextboxInput.Text = "";
        }

        private void Button_Admin_Settings_Click(object sender, RoutedEventArgs e)
        {
            if (!Properties.Settings.Default.IsAdminSettingsWindowActive)
            {
                _adminSettings = new AdminSettings {ShowInTaskbar = false, Owner = this};
                Properties.Settings.Default.IsAdminSettingsWindowActive = true;
                _adminSettings.ShowDialog();
            }
            else if (_adminSettings.IsInitialized) _adminSettings.Activate();
        }

        private void PlaceMacro(string macro)
        {
            TextboxInput.Text += macro;
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
