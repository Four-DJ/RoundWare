using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoundWare.SDK.UI
{
    internal class MenuAttribute : Attribute
    {
        public string text;

        public MenuAttribute(string text)
        {
            this.text = text;
        }
    }
}
