using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoundWare.SDK
{
    internal class PhotonHelper
    {
        public static Dictionary<int, Photon.Realtime.Player> PlayerList => Room.Players;
        public static Room Room => LoadBalancingClient.CurrentRoom;
        public static LoadBalancingClient LoadBalancingClient => PhotonNetwork.NetworkingClient;

		public static Player GetPlayerWithActorID(int actorID)
		{
			for (int i = 0; i < PlayerManager.instance.players.Count; i++)
			{
				if (PlayerManager.instance.players[i].data.view.OwnerActorNr == actorID)
				{
					return PlayerManager.instance.players[i];
				}
			}
			return null;
		}

		public static void RPC(string methodName, RpcTarget target, params object[] parameters)
        {
			PhotonView.Find(0).RPC(methodName, target, parameters);
        }

		public static void OpRaiseEvent(byte eventCode, object customEventContent, RaiseEventOptions raiseEventOptions, SendOptions sendOptions)
		{
			LoadBalancingClient.OpRaiseEvent(eventCode, customEventContent, raiseEventOptions, sendOptions);
		}
	}
}
