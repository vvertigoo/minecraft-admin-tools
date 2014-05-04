using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace minecraft_server_gui
{
    /// <summary>
    /// Логика взаимодействия для Settings_Server.xaml
    /// </summary>
    public partial class Settings_Server
    {
        public Settings_Server()
        {
            InitializeComponent();
            if (File.Exists("server.properties")) ReadServerCfg();
            else
            {
                CreateNewCfg();
                ReadServerCfg();
            }
        }

        private void ReadServerCfg()
        {
            StreamReader serverCfgReader = File.OpenText("server.properties");
            while (!serverCfgReader.EndOfStream)
            {
                string data = serverCfgReader.ReadLine();
                if (data.Contains("generator-settings")) textbox_generator_settings.Text = data.Substring("generator-settings=".Length);
                else if (data.Contains("op-permission-level")) textbox_op_permission_level.Text = data.Substring("op-permission-level=".Length);
                else if (data.Contains("allow-nether")) textbox_allow_nether.Text = data.Substring("allow-nether=".Length);
                else if (data.Contains("level-name")) textbox_level_name.Text = data.Substring("level-name=".Length);
                else if (data.Contains("enable-query")) textbox_enable_query.Text = data.Substring("enable-query=".Length);
                else if (data.Contains("allow-flight")) textbox_allow_flight.Text = data.Substring("allow-flight=".Length);
                else if (data.Contains("announce-player-achievements")) textbox_announce_player_achievements.Text = data.Substring("announce-player-achievements=".Length);
                else if (data.Contains("server-port")) textbox_server_port.Text = data.Substring("server-port=".Length);
                else if (data.Contains("level-type")) textbox_level_type.Text = data.Substring("level-type=".Length);
                else if (data.Contains("enable-rcon")) textbox_enable_rcon.Text = data.Substring("enable-rcon=".Length);
                else if (data.Contains("force-gamemode")) textbox_force_gamemode.Text = data.Substring("force-gamemode=".Length);
                else if (data.Contains("level-seed")) textbox_level_seed.Text = data.Substring("level-seed=".Length);
                else if (data.Contains("server-ip")) textbox_server_ip.Text = data.Substring("server-ip=".Length);
                else if (data.Contains("max-build-height")) textbox_max_build_height.Text = data.Substring("max-build-height=".Length);
                else if (data.Contains("spawn-npcs")) textbox_spawn_npcs.Text = data.Substring("spawn-npcs=".Length);
                else if (data.Contains("white-list")) textbox_white_list.Text = data.Substring("white-list=".Length);
                else if (data.Contains("spawn-animals")) textbox_spawn_animals.Text = data.Substring("spawn-animals=".Length);
                else if (data.Contains("snooper-enabled")) textbox_snooper_enabled.Text = data.Substring("snooper-enabled=".Length);
                else if (data.Contains("hardcore")) textbox_hardcore.Text = data.Substring("hardcore=".Length);
                else if (data.Contains("online-mode")) textbox_online_mode.Text = data.Substring("online-mode=".Length);
                else if (data.Contains("resource-pack")) textbox_resource_pack.Text = data.Substring("resource-pack=".Length);
                else if (data.Contains("pvp")) textbox_pvp.Text = data.Substring("pvp=".Length);
                else if (data.Contains("difficulty")) textbox_difficulty.Text = data.Substring("difficulty=".Length);
                else if (data.Contains("enable-command-block")) textbox_enable_command_block.Text = data.Substring("enable-command-block=".Length);
                else if (data.Contains("player-idle-timeout")) textbox_player_idle_timeout.Text = data.Substring("player-idle-timeout=".Length);
                else if (data.Contains("gamemode")) textbox_gamemode.Text = data.Substring("gamemode=".Length);
                else if (data.Contains("max-players")) textbox_max_players.Text = data.Substring("max-players=".Length);
                else if (data.Contains("spawn-monsters")) textbox_spawn_monsters.Text = data.Substring("spawn-monsters=".Length);
                else if (data.Contains("view-distance")) textbox_view_distance.Text = data.Substring("view-distance=".Length);
                else if (data.Contains("generate-structures")) textbox_generate_structures.Text = data.Substring("generate-structures=".Length);
                else if (data.Contains("spawn-protection")) textbox_spawn_protection.Text = data.Substring("spawn-protection=".Length);
                else if (data.Contains("motd")) textbox_motd.Text = data.Substring("motd=".Length);
            }
            serverCfgReader.Close();
        }

        private void WriteServerCfg()
        {
            File.Delete("server.properties");
            StreamWriter serverCfgWriter = File.CreateText("server.properties");
            serverCfgWriter.WriteLine("generator-settings=" + textbox_generator_settings.Text);
            serverCfgWriter.WriteLine("op-permission-level=" + textbox_op_permission_level.Text);
            serverCfgWriter.WriteLine("allow-nether=" + textbox_allow_nether.Text);
            serverCfgWriter.WriteLine("level-name=" + textbox_level_name.Text);
            serverCfgWriter.WriteLine("enable-query=" + textbox_enable_query.Text);
            serverCfgWriter.WriteLine("allow-flight=" + textbox_allow_flight.Text);
            serverCfgWriter.WriteLine("announce-player-achievements=" + textbox_announce_player_achievements.Text);
            serverCfgWriter.WriteLine("server-port=" + textbox_server_port.Text);
            serverCfgWriter.WriteLine("level-type=" + textbox_level_type.Text);
            serverCfgWriter.WriteLine("enable-rcon=" + textbox_enable_rcon.Text);
            serverCfgWriter.WriteLine("force-gamemode=" + textbox_force_gamemode.Text);
            serverCfgWriter.WriteLine("level-seed=" + textbox_level_seed.Text);
            serverCfgWriter.WriteLine("server-ip=" + textbox_server_ip.Text);
            serverCfgWriter.WriteLine("max-build-height=" + textbox_max_build_height.Text);
            serverCfgWriter.WriteLine("spawn-npcs=" + textbox_spawn_npcs.Text);
            serverCfgWriter.WriteLine("white-list=" + textbox_white_list.Text);
            serverCfgWriter.WriteLine("spawn-animals=" + textbox_spawn_animals.Text);
            serverCfgWriter.WriteLine("snooper-enabled=" + textbox_snooper_enabled.Text);
            serverCfgWriter.WriteLine("hardcore=" + textbox_hardcore.Text);
            serverCfgWriter.WriteLine("online-mode=" + textbox_online_mode.Text);
            serverCfgWriter.WriteLine("resource-pack=" + textbox_resource_pack.Text);
            serverCfgWriter.WriteLine("pvp=" + textbox_pvp.Text);
            serverCfgWriter.WriteLine("difficulty=" + textbox_difficulty.Text);
            serverCfgWriter.WriteLine("enable-command-block=" + textbox_enable_command_block.Text);
            serverCfgWriter.WriteLine("player-idle-timeout=" + textbox_player_idle_timeout.Text);
            serverCfgWriter.WriteLine("gamemode=" + textbox_gamemode.Text);
            serverCfgWriter.WriteLine("max-players=" + textbox_max_players.Text);
            serverCfgWriter.WriteLine("spawn-monsters=" + textbox_spawn_monsters.Text);
            serverCfgWriter.WriteLine("view-distance=" + textbox_view_distance.Text);
            serverCfgWriter.WriteLine("generate-structures=" + textbox_generate_structures.Text);
            serverCfgWriter.WriteLine("spawn-protection=" + textbox_spawn_protection.Text);
            serverCfgWriter.WriteLine("motd=" + textbox_motd.Text);
            serverCfgWriter.Close();
            Properties.Settings.Default.max_players = textbox_max_players.Text;
        }

        private void CreateNewCfg()
        {
            StreamWriter serverCfgWriter = File.CreateText("server.properties");
            serverCfgWriter.WriteLine("generator-settings=");
            serverCfgWriter.WriteLine("op-permission-level=4");
            serverCfgWriter.WriteLine("allow-nether=true");
            serverCfgWriter.WriteLine("level-name=world");
            serverCfgWriter.WriteLine("enable-query=false");
            serverCfgWriter.WriteLine("allow-flight=false");
            serverCfgWriter.WriteLine("announce-player-achievements=false");
            serverCfgWriter.WriteLine("server-port=25565");
            serverCfgWriter.WriteLine("level-type=DEFAULT");
            serverCfgWriter.WriteLine("enable-rcon=false");
            serverCfgWriter.WriteLine("force-gamemode=false");
            serverCfgWriter.WriteLine("level-seed=");
            serverCfgWriter.WriteLine("server-ip=");
            serverCfgWriter.WriteLine("max-build-height=256");
            serverCfgWriter.WriteLine("spawn-npcs=true");
            serverCfgWriter.WriteLine("white-list=false");
            serverCfgWriter.WriteLine("spawn-animals=true");
            serverCfgWriter.WriteLine("snooper-enabled=false");
            serverCfgWriter.WriteLine("hardcore=false");
            serverCfgWriter.WriteLine("online-mode=false");
            serverCfgWriter.WriteLine("resource-pack=");
            serverCfgWriter.WriteLine("pvp=true");
            serverCfgWriter.WriteLine("difficulty=1");
            serverCfgWriter.WriteLine("enable-command-block=false");
            serverCfgWriter.WriteLine("player-idle-timeout=0");
            serverCfgWriter.WriteLine("gamemode=0");
            serverCfgWriter.WriteLine("max-players=20");
            serverCfgWriter.WriteLine("spawn-monsters=true");
            serverCfgWriter.WriteLine("view-distance=10");
            serverCfgWriter.WriteLine("generate-structures=true");
            serverCfgWriter.WriteLine("spawn-protection=16");
            serverCfgWriter.WriteLine("motd=A Minecraft Server");
            serverCfgWriter.Close();
        }

        private void Settings_Server_Window_Closed(object sender, EventArgs e)
        {
            Properties.Settings.Default.IsSettingsServerWindowActive = false;
            Owner.Activate();
        }

        private void Button_Server_Settings_Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Settings_Server_Help_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("http://minecraft-ru.gamepedia.com/%D0%A1%D0%BE%D0%B7%D0%B4%D0%B0%D0%BD%D0%B8%D0%B5_%D0%B8_%D0%BD%D0%B0%D1%81%D1%82%D1%80%D0%BE%D0%B9%D0%BA%D0%B0_%D1%81%D0%B5%D1%80%D0%B2%D0%B5%D1%80%D0%B0#.D0.9E.D0.BF.D0.B8.D1.81.D0.B0.D0.BD.D0.B8.D0.B5_.D0.BF.D0.B0.D1.80.D0.B0.D0.BC.D0.B5.D1.82.D1.80.D0.BE.D0.B2");
        }

        private void Button_Settings_Server_Save_Click(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.IsServerRunning) MessageBox.Show("Всё сохранено, но для применения настроек необходимо перезапустить сервер.", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
            WriteServerCfg();
        }
    }
}
