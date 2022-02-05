using ExitGames.Client.Photon;
using RoundWare.Event;
using RoundWare.SDK.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoundWare.Modules
{
    [Menu("Logger")]
    internal class Logger : BaseMenu, OnEventEvent, OnRPCEvent
    {
        public Logger(string text) : base(text)
        {
            if (Main.Instance.settings.eventLogger)
                EventLogger(true);
            if (Main.Instance.settings.rpcLogger)
                RPCLogger(true);
        }

        [Toggle("EventLogger")]
        public void EventLogger(bool state)
        {
            Main.Instance.settings.eventLogger = state;
            if (state)
                Main.Instance.onEventEvents.Add(this);
            else
                Main.Instance.onEventEvents.Remove(this);
        }

        [Toggle("RPCLogger")]
        public void RPCLogger(bool state)
        {
            Main.Instance.settings.rpcLogger = state;
            if (state)
                Main.Instance.onRPCEvents.Add(this);
            else
                Main.Instance.onRPCEvents.Remove(this);
        }

        public bool OnEvent(EventData photonEvent, Photon.Realtime.Player Sender)
        {
            Main.Instance.Log("OnEvent", $"{Sender.NickName} sended {photonEvent.Code} {photonEvent.CustomData}", ConsoleColor.Cyan);
            return true;
        }

        public bool OnRPC(Photon.Realtime.Player sender, string methodName, params object[] parameters)
        {
            string objects = "\n";
            for (int i = 0; i < parameters.Length; i++)
                objects += $"{parameters}\n";
            Main.Instance.Log("OnRPC",$"{sender.NickName} sended {methodName} {objects}",ConsoleColor.Cyan);
            return true;
        }
    }
}
