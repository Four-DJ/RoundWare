using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RoundWare.SDK.UI
{
    internal class ButtonAttribute : Attribute
    {
        public string text;
        public MethodInfo method;

        public ButtonAttribute(string text)
        {
            this.text = text;
        }
    }
}
