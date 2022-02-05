using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RoundWare.SDK.UI
{
    internal class ToggleAttribute : Attribute
    {
        public string text;
        public bool toggled;
        public MethodInfo method;

        public ToggleAttribute(string text)
        {
            this.text = text;
        }
    }
}
