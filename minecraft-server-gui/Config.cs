using System.IO;
using System.Timers;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace minecraft_server_gui
{
    public delegate void HandleFunc(ItemType type, string parameter);

    public class Config
    {
        private readonly List<ConfigItemBase> OnJoin;
        private readonly List<ConfigItemTimed> Timed;
        private readonly StreamWriter SERVER_INPUT;

        private ConfigState configState;

        public Config(StreamWriter server_input)
        {
            SERVER_INPUT = server_input;
            OnJoin = new List<ConfigItemBase>();
            Timed = new List<ConfigItemTimed>();

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
                    configState = ConfigState.OnJoin;
                }
                else if (input.StartsWith("[timed]"))
                {
                    configState = ConfigState.Timed;
                }
                else if (input.StartsWith("CONSOLE"))
                {
                    if (configState == ConfigState.OnJoin)
                    {
                        string data = input.Substring("CONSOLE ".Length);
                        OnJoin.Add(new ConfigItemBase(ItemType.Console, data));
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
                    Timed.Add(new ConfigItemTimed(ItemType.Backup, command, i, Handler));
                }
                else if (input.Contains("BACKUP"))
                {
                    int i;
                    string[] subs = Regex.Split(input, " BACKUP");
                    int.TryParse(subs[0], out i);
                    Timed.Add(new ConfigItemTimed(ItemType.Backup, " ", i, Handler));
                }

            }
        }

        private void BackupWorld()
        {

        }

        public void Handler(ItemType type, string parameter)
        {
            SERVER_INPUT.WriteLine("/say DEBUG: Here something happened");
        }

        public void JoinAct(string playername)
        {
            SERVER_INPUT.WriteLine("/say DEBUG: Someone joined");
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
        protected Timer timer;
        readonly HandleFunc Func;

        public ConfigItemTimed(ItemType type, string parameter, int delay, HandleFunc func)
            :base (type, parameter)
        {
            timer = new Timer();
            Func = func;
            timer.Elapsed += Handler;
            timer.Interval = delay * 60000;
        }

        private void Handler(object source, ElapsedEventArgs e)
        {
            Func(Type, Parameter);
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
