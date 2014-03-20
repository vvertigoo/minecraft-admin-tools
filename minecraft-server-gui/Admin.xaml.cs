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
    delegate void UpdateMacrosNames();
    /// <summary>
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        public System.IO.StreamWriter SERVER_INPUT;
        private Admin_Settings admin_settings;

        public Admin()
        {
            InitializeComponent();
            Properties.Settings.Default.UpdateMacrosNamesMethod = UpdateMacros;
            UpdateMacros();
        }

        public void UpdateMacros()
        {
            Button_Admin_Macro1.ContentStringFormat = Properties.Settings.Default.macro_1_name;
            Button_Admin_Macro2.ContentStringFormat = Properties.Settings.Default.macro_2_name;
            Button_Admin_Macro3.ContentStringFormat = Properties.Settings.Default.macro_3_name;
            Button_Admin_Macro4.ContentStringFormat = Properties.Settings.Default.macro_4_name;
            Button_Admin_Macro5.ContentStringFormat = Properties.Settings.Default.macro_5_name;
            Button_Admin_Macro6.ContentStringFormat = Properties.Settings.Default.macro_6_name;
            Button_Admin_Macro7.ContentStringFormat = Properties.Settings.Default.macro_7_name;
            Button_Admin_Macro8.ContentStringFormat = Properties.Settings.Default.macro_8_name;
            Button_Admin_Macro9.ContentStringFormat = Properties.Settings.Default.macro_9_name;
            Button_Admin_Macro10.ContentStringFormat = Properties.Settings.Default.macro_10_name;
            Button_Admin_Macro11.ContentStringFormat = Properties.Settings.Default.macro_11_name;
            Button_Admin_Macro12.ContentStringFormat = Properties.Settings.Default.macro_12_name;
            Button_Admin_Macro13.ContentStringFormat = Properties.Settings.Default.macro_13_name;
            Button_Admin_Macro14.ContentStringFormat = Properties.Settings.Default.macro_14_name;
            Button_Admin_Macro15.ContentStringFormat = Properties.Settings.Default.macro_15_name;
        }

        private void Button_Admin_Close_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.IsAdminConsoleActive = false;
            this.Close();
        }

        private void Button_Admin_Enter_Click(object sender, RoutedEventArgs e)
        {
            SERVER_INPUT.WriteLine(Textbox_Admin_Input.Text);
            Textbox_Admin_Input.Text = "";
        }

        private void Button_Admin_Settings_Click(object sender, RoutedEventArgs e)
        {
            admin_settings = new Admin_Settings();
            admin_settings.Show();
        }
    }
}
