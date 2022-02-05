using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoundWare.SDK
{
    internal class PlayerHelper
    {
        private static Player _LocalPlayer;

        public static List<Player> Players => PlayerManager.instance.players;
        public static Player LocalPlayer()
        {
            if(_LocalPlayer == null)
            {
                _LocalPlayer = PhotonHelper.GetPlayerWithActorID(PhotonHelper.LoadBalancingClient.LocalPlayer.ActorNumber);
            }
            return _LocalPlayer;
        }
    }
}
