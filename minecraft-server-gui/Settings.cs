using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

// ReSharper disable once CheckNamespace
namespace minecraft_server_gui.Properties {
    
    
    // Этот класс позволяет обрабатывать определенные события в классе параметров:
    //  Событие SettingChanging возникает перед изменением значения параметра.
    //  Событие PropertyChanged возникает после изменения значения параметра.
    //  Событие SettingsLoaded возникает после загрузки значений параметров.
    //  Событие SettingsSaving возникает перед сохранением значений параметров.
    internal sealed partial class Settings {

        public UpdateMacrosNames UpdateMacrosNamesMethod;
        public bool IsAdminWindowActive;
        public bool IsAdminSettingsWindowActive;
        public bool IsPlayerWindowActive;
        public bool IsSettingsWindowActive;
        public bool IsSettingsServerWindowActive;
        public bool IsSettingsRemoteWindowActive;
        public bool IsServerRunning;
        public List<string> PlayerNames;
        public string WorldName;
        
        public Settings() {
            // // Для добавления обработчиков событий для сохранения и изменения параметров раскомментируйте приведенные ниже строки:
            //
            // this.SettingChanging += this.SettingChangingEventHandler;
            //
            SettingsSaving += SettingsSavingEventHandler;
            IsAdminWindowActive = false;
            IsAdminSettingsWindowActive = false;
            IsPlayerWindowActive = false;
            IsSettingsWindowActive = false;
            IsSettingsServerWindowActive = false;
            IsSettingsRemoteWindowActive = false;
            IsServerRunning = false;
            PlayerNames = new List<string>();
        }
        
        private void SettingsSavingEventHandler(object sender, CancelEventArgs e) {
            if (IsAdminWindowActive)
            {
                UpdateMacrosNamesMethod();
            }
        }

        public void ReadMaxPlayers()
        {
            if (File.Exists("server.properties"))
            {
                var serverCfgReader = File.OpenText("server.properties");
                while (!serverCfgReader.EndOfStream)
                {
                    var data = serverCfgReader.ReadLine();
                    if (data == null || !data.Contains("max-players")) continue;
                    max_players = data.Substring("max-players=".Length);
                    break;
                }
                serverCfgReader.Close();
            }
            else
            {
                var serverCfgWriter = File.CreateText("server.properties");
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
                max_players = "20";
            }
        }
    }
}
