using System.IO;
using System.IO.Compression;
using System.Timers;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace minecraft_server_gui
{
    public delegate void HandleFunc(ItemType type, string parameter);

    public class Config
    {
        private readonly List<ConfigItemBase> _onJoin;
        private readonly List<ConfigItemTimed> _timed;
        private bool _isBackupNeeded;
        private string _worldName;
        public StreamWriter ServerInput { get; set; }

        private ConfigState _configState;

        public Config()
        {
            _onJoin = new List<ConfigItemBase>();
            _timed = new List<ConfigItemTimed>();
            _isBackupNeeded = false;
        }

        ~Config()
        {
            _onJoin.Clear();
            _timed.Clear();
        }

        public void StartConfig()
        {
            if (File.Exists("minecraft-server-gui.conf"))
            {
                _worldName = ReadWorldName();
                var cfgReader = File.OpenText("minecraft-server-gui.conf");
                while (!cfgReader.EndOfStream)
                {
                    ParseConfigLine(cfgReader.ReadLine());
                }
                cfgReader.Close();
            }
            else
            {
                var cfgWriter = File.CreateText("minecraft-server-gui.conf");
                cfgWriter.WriteLine("### <- this is comment line");
                cfgWriter.WriteLine("###");
                cfgWriter.WriteLine("### Now we have only one option - console input.");
                cfgWriter.WriteLine("### Here is sample");
                cfgWriter.WriteLine("");
                cfgWriter.WriteLine("[onjoin]");
                cfgWriter.WriteLine("CONSOLE /tellraw %playername% {text:\"[SERVER] Sometext\",color:green,bold:true}");
                cfgWriter.WriteLine("");
                cfgWriter.WriteLine("[timed]");
                cfgWriter.WriteLine("### Please notice, that time should be set in minutes!");
                cfgWriter.WriteLine("2 CONSOLE /say Sometext");
                cfgWriter.Close();
            }
        }

        public void StopConfig()
        {
            _onJoin.Clear();
            foreach (var item in _timed)
            {
                item.StopTimer();
            }
            _timed.Clear();
        }

        private void ParseConfigLine(string input)
        {
            if (input.StartsWith("###")) return;
            if (input.StartsWith("[onjoin]"))
            {
                _configState = ConfigState.OnJoin;
            }
            else if (input.StartsWith("[timed]"))
            {
                _configState = ConfigState.Timed;
            }
            else if (input.StartsWith("CONSOLE"))
            {
                if (_configState != ConfigState.OnJoin) return;
                var data = input.Substring("CONSOLE ".Length);
                _onJoin.Add(new ConfigItemBase(ItemType.Console, data));
            }
            else if (input.StartsWith("BACKUP"))
            {
                BackupWorld();
            }
            else if (input.Contains("CONSOLE"))
            {
                int i;
                var subs = Regex.Split(input, " CONSOLE ");
                int.TryParse(subs[0], out i);
                var command = subs[1];
                _timed.Add(new ConfigItemTimed(ItemType.Console, command, i, Handler));
            }
            else if (input.Contains("BACKUP"))
            {
                int i;
                var subs = Regex.Split(input, " BACKUP");
                int.TryParse(subs[0], out i);
                _timed.Add(new ConfigItemTimed(ItemType.Backup, " ", i, Handler));
            }
        }

        private void BackupWorld()
        {
            ServerInput.WriteLine("/say Starting backup...");
            _isBackupNeeded = true;
            ServerInput.WriteLine("/save-off");
            ServerInput.WriteLine("/save-all");
        }

        public void WorldSaved()
        {
            if (!_isBackupNeeded) return;
            string path = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            if (!Directory.Exists(path + @"\backup"))
            {
                Directory.CreateDirectory(path + @"\Backup");
            }
            ZipFile.CreateFromDirectory(path + @"\" + _worldName + @"\", path + @"\backup\backup_" + _worldName + ".zip");
            ServerInput.WriteLine("/save-on");
            ServerInput.WriteLine("/say Backup complete!");
            _isBackupNeeded = false;
        }

        public void Handler(ItemType type, string parameter)
        {
            switch (type)
            {
                case ItemType.Backup:
                    BackupWorld();
                    break;
                case ItemType.Console:
                    ServerInput.WriteLine(parameter);
                    break;
            }
        }

        public void JoinAct(string playername)
        {
            if (_onJoin.Count <= 0) return;
            foreach (var message in _onJoin)
            {
                switch (message.Type)
                {
                    case ItemType.Backup:
                        BackupWorld();
                        break;
                    case ItemType.Console:
                        var text = message.Parameter.Replace("%playername%", playername);
                        ServerInput.WriteLine(text);
                        break;
                }
            }
        }

        private string ReadWorldName()
        {
            if (File.Exists("server.properties"))
            {
                var serverCfgReader = File.OpenText("server.properties");
                while (!serverCfgReader.EndOfStream)
                {
                    var data = serverCfgReader.ReadLine();
                    if (data == null || !data.Contains("level-name")) continue;
                    return data.Substring("level-name=".Length);
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
                return "world";
            }
            return null;
        }
    }

    public class ConfigItemBase
    {
        /// <summary>
        /// Тип елемента конфигурации
        /// </summary>
        public ItemType Type { get; set; }
        /// <summary>
        /// Параметр елемента
        /// </summary>
        public string Parameter { get; set; }

        public ConfigItemBase(ItemType type, string parameter)
        {
            Type = type;
            Parameter = parameter;
        }
    }

    public class ConfigItemTimed : ConfigItemBase
    {
        protected System.Timers.Timer Timer;
        private readonly HandleFunc _func;

        public ConfigItemTimed(ItemType type, string parameter, int delay, HandleFunc func)
            :base (type, parameter)
        {
            Timer = new System.Timers.Timer();
            _func = func;
            Timer.Elapsed += Handler;
            Timer.Interval = delay * 60000;
            Timer.Start();
        }

        public void StopTimer()
        {
            Timer.Stop();
            Timer.Close();
        }

        private void Handler(object source, ElapsedEventArgs e)
        {
            _func(Type, Parameter);
        }
    }

    public enum ItemType
    {
        Console = 1,
        Backup = 2,
    }

    public enum ConfigState
    {
        OnJoin = 1,
        Timed = 2,
    }
}
