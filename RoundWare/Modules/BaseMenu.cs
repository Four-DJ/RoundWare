using RoundWare.SDK.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RoundWare.Modules
{
    internal class BaseMenu
    {
        public string text;
        public List<ButtonAttribute> buttons = new List<ButtonAttribute>();
        public List<ToggleAttribute> toggles = new List<ToggleAttribute>();

        public BaseMenu(string text)
        {
            this.text = text;
            MethodInfo[] methods = this.GetType().GetMethods();
            for(int i = 0; i < methods.Length; i++)
            {
                ButtonAttribute button = methods[i].GetCustomAttribute<ButtonAttribute>();
                if (button != null)
                {
                    button.method = methods[i];
                    buttons.Add(button);
                }
                else
                {
                    ToggleAttribute toggle = methods[i].GetCustomAttribute<ToggleAttribute>();
                    if (toggle != null)
                    {
                        toggle.method = methods[i];
                        toggles.Add(toggle);
                    }
                }
            }
        }

        public virtual void OnGUI()
        {
            foreach (ToggleAttribute toggle in toggles)
            {
                if (GUILayout.Button($"<i><color={(toggle.toggled ? "green" : "red")}>{toggle.text}</color></i>", new GUILayoutOption[0]))
                {
                    toggle.toggled = !toggle.toggled;
                    toggle.method.Invoke(this , new object[] { toggle.toggled });
                    Main.Instance.Log(this.GetType().Name, $"Toggled {toggle.text} {toggle.toggled}", ConsoleColor.Yellow);
                }
            }
            foreach (ButtonAttribute button in buttons)
            {
                if (GUILayout.Button($"<i><color=white>{button.text}</color></i>", new GUILayoutOption[0]))
                {
                    button.method.Invoke(this, null);
                }
            }
        }
    }
}
