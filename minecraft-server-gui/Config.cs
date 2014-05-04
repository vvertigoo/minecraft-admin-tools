using System.IO;
using System.Timers;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace minecraft_server_gui
{
    public delegate void HandleFunc(ItemType type, string parameter);

    public class Config
    {
        private readonly List<ConfigItemBase> _onJoin;
        private readonly List<ConfigItemTimed> _timed;
        private readonly StreamWriter _serverInput;

        private ConfigState _configState;

        public Config(StreamWriter serverInput)
        {
            _serverInput = serverInput;
            _onJoin = new List<ConfigItemBase>();
            _timed = new List<ConfigItemTimed>();

            if (File.Exists("minecraft-server-gui.conf"))
            {
                StreamReader cfgReader = File.OpenText("minecraft-server-gui.conf");
                while (!cfgReader.EndOfStream)
                {
                    ParseConfigLine(cfgReader.ReadLine());
                }
                cfgReader.Close();
            }
            else
            {
                StreamWriter cfgWriter = File.CreateText("minecraft-server-gui.conf");
                cfgWriter.WriteLine("### <- this is comment line");
                cfgWriter.WriteLine("###");
                cfgWriter.WriteLine("### Now we have two options - console input and backup world.");
                cfgWriter.WriteLine("### Here is sample");
                cfgWriter.WriteLine("");
                cfgWriter.WriteLine("[onjoin]");
                cfgWriter.WriteLine("CONSOLE /tellraw %playername% {text:\"[SERVER] Sometext\",color:green,bold:true}");
                cfgWriter.WriteLine("");
                cfgWriter.WriteLine("[timed]");
                cfgWriter.WriteLine("### Please notice, that time should be set in minutes");
                cfgWriter.WriteLine("1 BACKUP");
                cfgWriter.WriteLine("2 CONSOLE /say Sometext");
                cfgWriter.Close();
            }
        }

        private void ParseConfigLine(string input)
        {
            if (!input.StartsWith("###"))
            {
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
                    if (_configState == ConfigState.OnJoin)
                    {
                        string data = input.Substring("CONSOLE ".Length);
                        _onJoin.Add(new ConfigItemBase(ItemType.Console, data));
                    }
                }
                else if (input.StartsWith("BACKUP"))
                {
                    BackupWorld();
                }
                else if (input.Contains("CONSOLE"))
                {
                    int i;
                    string[] subs = Regex.Split(input, " CONSOLE ");
                    int.TryParse(subs[0], out i);
                    string command = subs[1];
                    _timed.Add(new ConfigItemTimed(ItemType.Backup, command, i, Handler));
                }
                else if (input.Contains("BACKUP"))
                {
                    int i;
                    string[] subs = Regex.Split(input, " BACKUP");
                    int.TryParse(subs[0], out i);
                    _timed.Add(new ConfigItemTimed(ItemType.Backup, " ", i, Handler));
                }

            }
        }

        private void BackupWorld()
        {

        }

        public void Handler(ItemType type, string parameter)
        {
            _serverInput.WriteLine("/say DEBUG: Here something happened");
        }

        public void JoinAct(string playername)
        {
            _serverInput.WriteLine("/say DEBUG: Someone joined");
        }
    }

    public class ConfigItemBase
    {
        /// <summary>
        /// Тип елемента конфигурации
        /// </summary>
        protected ItemType Type { get; set; }
        /// <summary>
        /// Параметр елемента
        /// </summary>
        protected string Parameter { get; set; }

        public ConfigItemBase(ItemType type, string parameter)
        {
            Type = type;
            Parameter = parameter;
        }
    }

    public class ConfigItemTimed : ConfigItemBase
    {
        protected Timer Timer;
        readonly HandleFunc _func;

        public ConfigItemTimed(ItemType type, string parameter, int delay, HandleFunc func)
            :base (type, parameter)
        {
            Timer = new Timer();
            _func = func;
            Timer.Elapsed += Handler;
            Timer.Interval = delay * 60000;
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
