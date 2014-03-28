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
    /// Логика взаимодействия для Admin_Settings.xaml
    /// </summary>
    public partial class Admin_Settings : Window
    {
        private string[] macro_name_temp = new string[15];
        private string[] macro_value_name = new string[15];

        private int ComboSelected;

        public Admin_Settings()
        {
            InitializeComponent();
            ComboSelected = 1;

            #region
            macro_name_temp[0] = Properties.Settings.Default.macro_1_name;
            macro_value_name[0] = Properties.Settings.Default.macro_1_value;
            macro_name_temp[1] = Properties.Settings.Default.macro_2_name;
            macro_value_name[1] = Properties.Settings.Default.macro_2_value;
            macro_name_temp[2] = Properties.Settings.Default.macro_3_name;
            macro_value_name[2] = Properties.Settings.Default.macro_3_value;
            macro_name_temp[3] = Properties.Settings.Default.macro_4_name;
            macro_value_name[3] = Properties.Settings.Default.macro_4_value;
            macro_name_temp[4] = Properties.Settings.Default.macro_5_name;
            macro_value_name[4] = Properties.Settings.Default.macro_5_value;
            macro_name_temp[5] = Properties.Settings.Default.macro_6_name;
            macro_value_name[5] = Properties.Settings.Default.macro_6_value;
            macro_name_temp[6] = Properties.Settings.Default.macro_7_name;
            macro_value_name[6] = Properties.Settings.Default.macro_7_value;
            macro_name_temp[7] = Properties.Settings.Default.macro_8_name;
            macro_value_name[7] = Properties.Settings.Default.macro_8_value;
            macro_name_temp[8] = Properties.Settings.Default.macro_9_name;
            macro_value_name[8] = Properties.Settings.Default.macro_9_value;
            macro_name_temp[9] = Properties.Settings.Default.macro_10_name;
            macro_value_name[9] = Properties.Settings.Default.macro_10_value;
            macro_name_temp[10] = Properties.Settings.Default.macro_11_name;
            macro_value_name[10] = Properties.Settings.Default.macro_11_value;
            macro_name_temp[11] = Properties.Settings.Default.macro_12_name;
            macro_value_name[11] = Properties.Settings.Default.macro_12_value;
            macro_name_temp[12] = Properties.Settings.Default.macro_13_name;
            macro_value_name[12] = Properties.Settings.Default.macro_13_value;
            macro_name_temp[13] = Properties.Settings.Default.macro_14_name;
            macro_value_name[13] = Properties.Settings.Default.macro_14_value;
            macro_name_temp[14] = Properties.Settings.Default.macro_15_name;
            macro_value_name[14] = Properties.Settings.Default.macro_15_value;
            #endregion

            TextBox_Admin_Settings_Name.Text = macro_name_temp[0];
            TextBox_Admin_Settings_Command.Text = macro_value_name[0];
        }

        private void Combo_Admin_Settings_MacroSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(this.IsInitialized)
            {
                switch (Combo_Admin_Settings_MacroSelect.SelectedIndex)
                {
                    case 0:
                        ComboSelected = 1;
                        break;
                    case 1:
                        ComboSelected = 2;
                        break;
                    case 2:
                        ComboSelected = 3;
                        break;
                    case 3:
                        ComboSelected = 4;
                        break;
                    case 4:
                        ComboSelected = 5;
                        break;
                    case 5:
                        ComboSelected = 6;
                        break;
                    case 6:
                        ComboSelected = 7;
                        break;
                    case 7:
                        ComboSelected = 8;
                        break;
                    case 8:
                        ComboSelected = 9;
                        break;
                    case 9:
                        ComboSelected = 10;
                        break;
                    case 10:
                        ComboSelected = 11;
                        break;
                    case 11:
                        ComboSelected = 12;
                        break;
                    case 12:
                        ComboSelected = 13;
                        break;
                    case 13:
                        ComboSelected = 14;
                        break;
                    case 14:
                        ComboSelected = 15;
                        break;
                }
                TextBox_Admin_Settings_Name.Text = macro_name_temp[ComboSelected-1];
                TextBox_Admin_Settings_Command.Text = macro_value_name[ComboSelected-1];
            }
        }

        private void Button_Admin_Settings_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Admin_Settings_Save_Click(object sender, RoutedEventArgs e)
        {
            macro_name_temp[ComboSelected - 1] = TextBox_Admin_Settings_Name.Text;
            macro_value_name[ComboSelected - 1] = TextBox_Admin_Settings_Command.Text;

            Properties.Settings.Default.macro_1_name = macro_name_temp[0];
            Properties.Settings.Default.macro_1_value = macro_value_name[0];
            Properties.Settings.Default.macro_2_name = macro_name_temp[1];
            Properties.Settings.Default.macro_2_value = macro_value_name[1];
            Properties.Settings.Default.macro_3_name = macro_name_temp[2];
            Properties.Settings.Default.macro_3_value = macro_value_name[2];
            Properties.Settings.Default.macro_4_name = macro_name_temp[3];
            Properties.Settings.Default.macro_4_value = macro_value_name[3];
            Properties.Settings.Default.macro_5_name = macro_name_temp[4];
            Properties.Settings.Default.macro_5_value = macro_value_name[4];
            Properties.Settings.Default.macro_6_name = macro_name_temp[5];
            Properties.Settings.Default.macro_6_value = macro_value_name[5];
            Properties.Settings.Default.macro_7_name = macro_name_temp[6];
            Properties.Settings.Default.macro_7_value = macro_value_name[6];
            Properties.Settings.Default.macro_8_name = macro_name_temp[7];
            Properties.Settings.Default.macro_8_value = macro_value_name[7];
            Properties.Settings.Default.macro_9_name = macro_name_temp[8];
            Properties.Settings.Default.macro_9_value = macro_value_name[8];
            Properties.Settings.Default.macro_10_name = macro_name_temp[9];
            Properties.Settings.Default.macro_10_value = macro_value_name[9];
            Properties.Settings.Default.macro_11_name = macro_name_temp[10];
            Properties.Settings.Default.macro_11_value = macro_value_name[10];
            Properties.Settings.Default.macro_12_name = macro_name_temp[11];
            Properties.Settings.Default.macro_12_value = macro_value_name[11];
            Properties.Settings.Default.macro_13_name = macro_name_temp[12];
            Properties.Settings.Default.macro_13_value = macro_value_name[12];
            Properties.Settings.Default.macro_14_name = macro_name_temp[13];
            Properties.Settings.Default.macro_14_value = macro_value_name[13];
            Properties.Settings.Default.macro_15_name = macro_name_temp[14];
            Properties.Settings.Default.macro_15_value = macro_value_name[14];

            Properties.Settings.Default.Save();
        }

        private void Window_Admin_Settings_Closed(object sender, EventArgs e)
        {
            Properties.Settings.Default.IsAdminSettingsWindowActive = false;
            this.Owner.Activate();
        }
    }
}
