using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RoundWare.SDK
{

    [System.Serializable]
    public class Settings
    {
        public float r, g, b, a;
        public bool eventLogger;
        public bool rpcLogger;
        public bool nameSpoofer;
        public string fakeName;
        
        public string SaveToString()
        {
            return JsonUtility.ToJson(this);
        }
    }
}
