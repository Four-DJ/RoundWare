using ModLoader.Attributes;
using ModLoader.Modules;
using RoundWare.Event;
using RoundWare.Modules;
using RoundWare.SDK;
using RoundWare.SDK.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RoundWare
{
	[ModuleInfo("RoundWare", "1.0", "Four_DJ", ConsoleColor.Yellow)]
	public class Main : Mod
	{
		public static Main Instance;
		public Settings settings;

		public List<OnEventEvent> onEventEvents = new List<OnEventEvent>();
		public List<OnRPCEvent> onRPCEvents = new List<OnRPCEvent>();
		public List<OnUpdateEvent> onUpdateEvents = new List<OnUpdateEvent>();

		List<BaseMenu> menus = new List<BaseMenu>();
		BaseMenu selectedMenu = null;

		void Start()
		{
			if (!File.Exists("Config.json"))
			{
				File.Create("Config.json").Close();
				settings = new Settings();
				settings.fakeName = "HackerMan";
				settings.r = 0;
				settings.g = 0;
				settings.b = 0;
				settings.a = 0.75f;
				File.WriteAllText("Config.json", settings.SaveToString());
			}

			settings = (Settings)JsonUtility.FromJson(File.ReadAllText("Config.json"), typeof(Settings));

			Instance = this;
			Task.Run(() => Patches.Init());

			Type[] types = Assembly.GetExecutingAssembly().GetTypes();
			for(int i = 0; i < types.Length; i++)
            {
				MenuAttribute menuInfo = types[i].GetCustomAttribute<MenuAttribute>();
				if (menuInfo != null)
                {
					BaseMenu menu = Activator.CreateInstance(types[i], new object[] {menuInfo.text}) as BaseMenu;
					menus.Add(menu);
				}
            }
			
		}

        void Update()
        {
			foreach(OnUpdateEvent @event in onUpdateEvents)
				@event.Update();
        }

		void OnGUI()
		{
			GUI.skin.window.normal.background = Render.MakeTex(2, 2, new UnityEngine.Color(settings.r, settings.g, settings.b, settings.a));
			GUI.skin.window.focused.background = Render.MakeTex(2, 2, new UnityEngine.Color(settings.r, settings.g, settings.b, settings.a));
			GUI.skin.window.onNormal.background = Render.MakeTex(2, 2, new UnityEngine.Color(settings.r, settings.g, settings.b, settings.a));
			GUI.skin.box.normal.background = Render.MakeTex(2, 2, new UnityEngine.Color(settings.r, settings.g, settings.b, 0));
			new GUIStyle(GUI.skin.box);
			if (selectedMenu == null)
			{
				GUILayout.Window(0, new Rect(25,25, 250f, 35f * menus.Count), new GUI.WindowFunction(this.RenderMainMenu), $"<b><color=white>RoundsWare</color></b>", new GUILayoutOption[0]);
            }
            else
            {
				GUILayout.Window(1, new Rect(25, 25, 250f, 35f * selectedMenu.buttons.Count + 35f * selectedMenu.toggles.Count + 35f), new GUI.WindowFunction(this.RenderMainMenu), $"<b><color=white>{selectedMenu.text}</color></b>", new GUILayoutOption[0]);
			}
		}

		private void RenderMainMenu(int id)
		{
			if (id == 0)
			{
				foreach (BaseMenu menu in menus)
				{
					if (GUILayout.Button($"<b><color=white>{menu.text}</color></b>", new GUILayoutOption[0]))
					{
						selectedMenu = menu;
					}
				}
            }
            else
            {
				selectedMenu.OnGUI();
				if (GUILayout.Button($"<b><color=white>Back</color></b>", new GUILayoutOption[0]))
				{
					selectedMenu = null;
				}
			}
		}

		void OnApplicationQuit()
		{
			File.WriteAllText("Config.json", JsonUtility.ToJson(settings, false));
		}
	}
}