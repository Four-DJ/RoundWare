using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoundWare.Event
{
    public interface OnRPCEvent
    {
        bool OnRPC(Photon.Realtime.Player sender, string methodName, params object[] parameters);
    }
}
