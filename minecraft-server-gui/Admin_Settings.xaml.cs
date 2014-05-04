using System;
using System.Windows;
using System.Windows.Controls;

namespace minecraft_server_gui
{
    /// <summary>
    /// Логика взаимодействия для Admin_Settings.xaml
    /// </summary>
    public partial class AdminSettings
    {
        private readonly string[] _macroNameTemp = new string[15];
        private readonly string[] _macroValueName = new string[15];

        private int _comboSelected;

        public AdminSettings()
        {
            InitializeComponent();
            _comboSelected = 1;

            #region
            _macroNameTemp[0] = Properties.Settings.Default.macro_1_name;
            _macroValueName[0] = Properties.Settings.Default.macro_1_value;
            _macroNameTemp[1] = Properties.Settings.Default.macro_2_name;
            _macroValueName[1] = Properties.Settings.Default.macro_2_value;
            _macroNameTemp[2] = Properties.Settings.Default.macro_3_name;
            _macroValueName[2] = Properties.Settings.Default.macro_3_value;
            _macroNameTemp[3] = Properties.Settings.Default.macro_4_name;
            _macroValueName[3] = Properties.Settings.Default.macro_4_value;
            _macroNameTemp[4] = Properties.Settings.Default.macro_5_name;
            _macroValueName[4] = Properties.Settings.Default.macro_5_value;
            _macroNameTemp[5] = Properties.Settings.Default.macro_6_name;
            _macroValueName[5] = Properties.Settings.Default.macro_6_value;
            _macroNameTemp[6] = Properties.Settings.Default.macro_7_name;
            _macroValueName[6] = Properties.Settings.Default.macro_7_value;
            _macroNameTemp[7] = Properties.Settings.Default.macro_8_name;
            _macroValueName[7] = Properties.Settings.Default.macro_8_value;
            _macroNameTemp[8] = Properties.Settings.Default.macro_9_name;
            _macroValueName[8] = Properties.Settings.Default.macro_9_value;
            _macroNameTemp[9] = Properties.Settings.Default.macro_10_name;
            _macroValueName[9] = Properties.Settings.Default.macro_10_value;
            _macroNameTemp[10] = Properties.Settings.Default.macro_11_name;
            _macroValueName[10] = Properties.Settings.Default.macro_11_value;
            _macroNameTemp[11] = Properties.Settings.Default.macro_12_name;
            _macroValueName[11] = Properties.Settings.Default.macro_12_value;
            _macroNameTemp[12] = Properties.Settings.Default.macro_13_name;
            _macroValueName[12] = Properties.Settings.Default.macro_13_value;
            _macroNameTemp[13] = Properties.Settings.Default.macro_14_name;
            _macroValueName[13] = Properties.Settings.Default.macro_14_value;
            _macroNameTemp[14] = Properties.Settings.Default.macro_15_name;
            _macroValueName[14] = Properties.Settings.Default.macro_15_value;
            #endregion

            TextBoxName.Text = _macroNameTemp[0];
            TextBoxCommand.Text = _macroValueName[0];
        }

        private void Combo_Admin_Settings_MacroSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(IsInitialized)
            {
                switch (ComboMacroSelect.SelectedIndex)
                {
                    case 0:
                        _comboSelected = 1;
                        break;
                    case 1:
                        _comboSelected = 2;
                        break;
                    case 2:
                        _comboSelected = 3;
                        break;
                    case 3:
                        _comboSelected = 4;
                        break;
                    case 4:
                        _comboSelected = 5;
                        break;
                    case 5:
                        _comboSelected = 6;
                        break;
                    case 6:
                        _comboSelected = 7;
                        break;
                    case 7:
                        _comboSelected = 8;
                        break;
                    case 8:
                        _comboSelected = 9;
                        break;
                    case 9:
                        _comboSelected = 10;
                        break;
                    case 10:
                        _comboSelected = 11;
                        break;
                    case 11:
                        _comboSelected = 12;
                        break;
                    case 12:
                        _comboSelected = 13;
                        break;
                    case 13:
                        _comboSelected = 14;
                        break;
                    case 14:
                        _comboSelected = 15;
                        break;
                }
                TextBoxName.Text = _macroNameTemp[_comboSelected-1];
                TextBoxCommand.Text = _macroValueName[_comboSelected-1];
            }
        }

        private void Button_Admin_Settings_Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Admin_Settings_Save_Click(object sender, RoutedEventArgs e)
        {
            _macroNameTemp[_comboSelected - 1] = TextBoxName.Text;
            _macroValueName[_comboSelected - 1] = TextBoxCommand.Text;

            Properties.Settings.Default.macro_1_name = _macroNameTemp[0];
            Properties.Settings.Default.macro_1_value = _macroValueName[0];
            Properties.Settings.Default.macro_2_name = _macroNameTemp[1];
            Properties.Settings.Default.macro_2_value = _macroValueName[1];
            Properties.Settings.Default.macro_3_name = _macroNameTemp[2];
            Properties.Settings.Default.macro_3_value = _macroValueName[2];
            Properties.Settings.Default.macro_4_name = _macroNameTemp[3];
            Properties.Settings.Default.macro_4_value = _macroValueName[3];
            Properties.Settings.Default.macro_5_name = _macroNameTemp[4];
            Properties.Settings.Default.macro_5_value = _macroValueName[4];
            Properties.Settings.Default.macro_6_name = _macroNameTemp[5];
            Properties.Settings.Default.macro_6_value = _macroValueName[5];
            Properties.Settings.Default.macro_7_name = _macroNameTemp[6];
            Properties.Settings.Default.macro_7_value = _macroValueName[6];
            Properties.Settings.Default.macro_8_name = _macroNameTemp[7];
            Properties.Settings.Default.macro_8_value = _macroValueName[7];
            Properties.Settings.Default.macro_9_name = _macroNameTemp[8];
            Properties.Settings.Default.macro_9_value = _macroValueName[8];
            Properties.Settings.Default.macro_10_name = _macroNameTemp[9];
            Properties.Settings.Default.macro_10_value = _macroValueName[9];
            Properties.Settings.Default.macro_11_name = _macroNameTemp[10];
            Properties.Settings.Default.macro_11_value = _macroValueName[10];
            Properties.Settings.Default.macro_12_name = _macroNameTemp[11];
            Properties.Settings.Default.macro_12_value = _macroValueName[11];
            Properties.Settings.Default.macro_13_name = _macroNameTemp[12];
            Properties.Settings.Default.macro_13_value = _macroValueName[12];
            Properties.Settings.Default.macro_14_name = _macroNameTemp[13];
            Properties.Settings.Default.macro_14_value = _macroValueName[13];
            Properties.Settings.Default.macro_15_name = _macroNameTemp[14];
            Properties.Settings.Default.macro_15_value = _macroValueName[14];

            Properties.Settings.Default.Save();
        }

        private void Window_Admin_Settings_Closed(object sender, EventArgs e)
        {
            Properties.Settings.Default.IsAdminSettingsWindowActive = false;
            Owner.Activate();
        }
    }
}
