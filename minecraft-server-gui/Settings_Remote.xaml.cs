using System;

namespace minecraft_server_gui
{
    /// <summary>
    /// Логика взаимодействия для Settings_Remote.xaml
    /// </summary>
    public partial class Settings_Remote
    {
        public Settings_Remote()
        {
            InitializeComponent();
        }

        private void Window_Settings_Remote_Closed(object sender, EventArgs e)
        {
            Properties.Settings.Default.IsSettingsRemoteWindowActive = false;
            Owner.Activate();
        }
    }
}
