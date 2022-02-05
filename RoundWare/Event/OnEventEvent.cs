using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoundWare.Event
{
    public interface OnEventEvent
    {
        bool OnEvent(EventData photonEvent, Photon.Realtime.Player Sender);
    }
}
