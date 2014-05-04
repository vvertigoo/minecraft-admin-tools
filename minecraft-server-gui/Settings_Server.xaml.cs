using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace minecraft_server_gui
{
    /// <summary>
    /// Логика взаимодействия для Settings_Server.xaml
    /// </summary>
    public partial class SettingsServer
    {
        public SettingsServer()
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
                if (data.Contains("generator-settings")) TextboxGeneratorSettings.Text = data.Substring("generator-settings=".Length);
                else if (data.Contains("op-permission-level")) TextboxOpPermissionLevel.Text = data.Substring("op-permission-level=".Length);
                else if (data.Contains("allow-nether")) TextboxAllowNether.Text = data.Substring("allow-nether=".Length);
                else if (data.Contains("level-name")) TextboxLevelName.Text = data.Substring("level-name=".Length);
                else if (data.Contains("enable-query")) TextboxEnableQuery.Text = data.Substring("enable-query=".Length);
                else if (data.Contains("allow-flight")) TextboxAllowFlight.Text = data.Substring("allow-flight=".Length);
                else if (data.Contains("announce-player-achievements")) TextboxAnnouncePlayerAchievements.Text = data.Substring("announce-player-achievements=".Length);
                else if (data.Contains("server-port")) TextboxServerPort.Text = data.Substring("server-port=".Length);
                else if (data.Contains("level-type")) TextboxLevelType.Text = data.Substring("level-type=".Length);
                else if (data.Contains("enable-rcon")) TextboxEnableRcon.Text = data.Substring("enable-rcon=".Length);
                else if (data.Contains("force-gamemode")) TextboxForceGamemode.Text = data.Substring("force-gamemode=".Length);
                else if (data.Contains("level-seed")) TextboxLevelSeed.Text = data.Substring("level-seed=".Length);
                else if (data.Contains("server-ip")) TextboxServerIp.Text = data.Substring("server-ip=".Length);
                else if (data.Contains("max-build-height")) TextboxMaxBuildHeight.Text = data.Substring("max-build-height=".Length);
                else if (data.Contains("spawn-npcs")) TextboxSpawnNpcs.Text = data.Substring("spawn-npcs=".Length);
                else if (data.Contains("white-list")) TextboxWhiteList.Text = data.Substring("white-list=".Length);
                else if (data.Contains("spawn-animals")) TextboxSpawnAnimals.Text = data.Substring("spawn-animals=".Length);
                else if (data.Contains("snooper-enabled")) TextboxSnooperEnabled.Text = data.Substring("snooper-enabled=".Length);
                else if (data.Contains("hardcore")) TextboxHardcore.Text = data.Substring("hardcore=".Length);
                else if (data.Contains("online-mode")) TextboxOnlineMode.Text = data.Substring("online-mode=".Length);
                else if (data.Contains("resource-pack")) TextboxResourcePack.Text = data.Substring("resource-pack=".Length);
                else if (data.Contains("pvp")) TextboxPvp.Text = data.Substring("pvp=".Length);
                else if (data.Contains("difficulty")) TextboxDifficulty.Text = data.Substring("difficulty=".Length);
                else if (data.Contains("enable-command-block")) TextboxEnableCommandBlock.Text = data.Substring("enable-command-block=".Length);
                else if (data.Contains("player-idle-timeout")) TextboxPlayerIdleTimeout.Text = data.Substring("player-idle-timeout=".Length);
                else if (data.Contains("gamemode")) TextboxGamemode.Text = data.Substring("gamemode=".Length);
                else if (data.Contains("max-players")) TextboxMaxPlayers.Text = data.Substring("max-players=".Length);
                else if (data.Contains("spawn-monsters")) TextboxSpawnMonsters.Text = data.Substring("spawn-monsters=".Length);
                else if (data.Contains("view-distance")) TextboxViewDistance.Text = data.Substring("view-distance=".Length);
                else if (data.Contains("generate-structures")) TextboxGenerateStructures.Text = data.Substring("generate-structures=".Length);
                else if (data.Contains("spawn-protection")) TextboxSpawnProtection.Text = data.Substring("spawn-protection=".Length);
                else if (data.Contains("motd")) TextboxMotd.Text = data.Substring("motd=".Length);
            }
            serverCfgReader.Close();
        }

        private void WriteServerCfg()
        {
            File.Delete("server.properties");
            StreamWriter serverCfgWriter = File.CreateText("server.properties");
            serverCfgWriter.WriteLine("generator-settings=" + TextboxGeneratorSettings.Text);
            serverCfgWriter.WriteLine("op-permission-level=" + TextboxOpPermissionLevel.Text);
            serverCfgWriter.WriteLine("allow-nether=" + TextboxAllowNether.Text);
            serverCfgWriter.WriteLine("level-name=" + TextboxLevelName.Text);
            serverCfgWriter.WriteLine("enable-query=" + TextboxEnableQuery.Text);
            serverCfgWriter.WriteLine("allow-flight=" + TextboxAllowFlight.Text);
            serverCfgWriter.WriteLine("announce-player-achievements=" + TextboxAnnouncePlayerAchievements.Text);
            serverCfgWriter.WriteLine("server-port=" + TextboxServerPort.Text);
            serverCfgWriter.WriteLine("level-type=" + TextboxLevelType.Text);
            serverCfgWriter.WriteLine("enable-rcon=" + TextboxEnableRcon.Text);
            serverCfgWriter.WriteLine("force-gamemode=" + TextboxForceGamemode.Text);
            serverCfgWriter.WriteLine("level-seed=" + TextboxLevelSeed.Text);
            serverCfgWriter.WriteLine("server-ip=" + TextboxServerIp.Text);
            serverCfgWriter.WriteLine("max-build-height=" + TextboxMaxBuildHeight.Text);
            serverCfgWriter.WriteLine("spawn-npcs=" + TextboxSpawnNpcs.Text);
            serverCfgWriter.WriteLine("white-list=" + TextboxWhiteList.Text);
            serverCfgWriter.WriteLine("spawn-animals=" + TextboxSpawnAnimals.Text);
            serverCfgWriter.WriteLine("snooper-enabled=" + TextboxSnooperEnabled.Text);
            serverCfgWriter.WriteLine("hardcore=" + TextboxHardcore.Text);
            serverCfgWriter.WriteLine("online-mode=" + TextboxOnlineMode.Text);
            serverCfgWriter.WriteLine("resource-pack=" + TextboxResourcePack.Text);
            serverCfgWriter.WriteLine("pvp=" + TextboxPvp.Text);
            serverCfgWriter.WriteLine("difficulty=" + TextboxDifficulty.Text);
            serverCfgWriter.WriteLine("enable-command-block=" + TextboxEnableCommandBlock.Text);
            serverCfgWriter.WriteLine("player-idle-timeout=" + TextboxPlayerIdleTimeout.Text);
            serverCfgWriter.WriteLine("gamemode=" + TextboxGamemode.Text);
            serverCfgWriter.WriteLine("max-players=" + TextboxMaxPlayers.Text);
            serverCfgWriter.WriteLine("spawn-monsters=" + TextboxSpawnMonsters.Text);
            serverCfgWriter.WriteLine("view-distance=" + TextboxViewDistance.Text);
            serverCfgWriter.WriteLine("generate-structures=" + TextboxGenerateStructures.Text);
            serverCfgWriter.WriteLine("spawn-protection=" + TextboxSpawnProtection.Text);
            serverCfgWriter.WriteLine("motd=" + TextboxMotd.Text);
            serverCfgWriter.Close();
            Properties.Settings.Default.max_players = TextboxMaxPlayers.Text;
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
