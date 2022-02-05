using ExitGames.Client.Photon;
using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;
using RoundWare.Event;
using RoundWare.SDK;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RoundWare
{
    internal class Patches
    {
        private static readonly HarmonyLib.Harmony Instance = new HarmonyLib.Harmony("RoundWare");
        private static string newHWID = "";
        private static int patchedMethodCount = 0;

        private static void Patch(MethodInfo originalMethod, MethodInfo patchMethod)
        {
            Instance.Patch(originalMethod, new HarmonyMethod(patchMethod));
            patchedMethodCount++;
        }

        public static void Init()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            try
            {
                Patch(typeof(SystemInfo).GetProperty("deviceUniqueIdentifier").GetGetMethod(), AccessTools.Method(typeof(Patches), nameof(FakeHWID)));
                Main.Instance.Log("Patch", "Analystics", ConsoleColor.DarkMagenta, ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                Main.Instance.Log("Patch", $"Analystics\n{ex}", ConsoleColor.DarkMagenta, ConsoleColor.Red);
            }
            try
            {
                Patch(typeof(LoadBalancingClient).GetMethod("OnEvent"), AccessTools.Method(typeof(Patches), nameof(OnEvent)));

                Main.Instance.Log("Patch", "Networking", ConsoleColor.DarkMagenta, ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                Main.Instance.Log("Patch", $"Networking\n{ex}", ConsoleColor.DarkMagenta, ConsoleColor.Red);
            }
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            Main.Instance.Log("Patch", $"Patched {patchedMethodCount} Methods in {ts.TotalMilliseconds}ms", ConsoleColor.DarkMagenta, ConsoleColor.Green);
        }

        private static bool OnEvent(EventData __0)
        {
            Photon.Realtime.Player player = PhotonHelper.Room.GetPlayer(__0.Sender);

            if (__0.Code == 200)
                if (!OnRPC(__0.CustomData as Hashtable, player))
                    return false;
            foreach (OnEventEvent @event in Main.Instance.onEventEvents)
                if (!@event.OnEvent(__0, player))
                    return false;
            return true;
        }

        private static bool OnRPC(Hashtable rpcData, Photon.Realtime.Player sender)
        {
            string text;
            if (rpcData.ContainsKey(5))
            {
                int num3 = (int)((byte)rpcData[5]);
                if (num3 > PhotonNetwork.PhotonServerSettings.RpcList.Count - 1)
                {
                    return false;
                }
                text = PhotonNetwork.PhotonServerSettings.RpcList[num3];
            }
            else
            {
                text = (string)rpcData[3];
            }
            object[] array = null;
            if (rpcData.ContainsKey(4))
            {
                array = (object[])rpcData[4];
            }
            foreach(OnRPCEvent @event in Main.Instance.onRPCEvents)
                if(!@event.OnRPC(sender,text,array))
                    return false;
            return true;
        }

        private static bool FakeHWID(ref string __result)
        {
            if (Patches.newHWID == "")
            {
                Patches.newHWID = KeyedHashAlgorithm.Create().ComputeHash(Encoding.UTF8.GetBytes(string.Format("{0}A-{1}{2}-{3}{4}-{5}{6}-3C-1F", new object[]
                {
                    new System.Random().Next(0, 9),
                    new System.Random().Next(0, 9),
                    new System.Random().Next(0, 9),
                    new System.Random().Next(0, 9),
                    new System.Random().Next(0, 9),
                    new System.Random().Next(0, 9),
                    new System.Random().Next(0, 9)
                }))).Select(delegate (byte x)
                {
                    byte b = x;
                    return b.ToString("x2");
                }).Aggregate((string x, string y) => x + y);
                Main.Instance.Log("HWID", $"new {Patches.newHWID}", ConsoleColor.DarkMagenta, ConsoleColor.Green);
            }
            __result = Patches.newHWID;
            return true;
        }
    }
}
