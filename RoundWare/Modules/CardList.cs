using RoundWare.SDK;
using RoundWare.SDK.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RoundWare.Modules
{
    [Menu("CradList")]
    internal class CardList : BaseMenu
    {
        Vector2 scrollPosition;

        public CardList(string text) : base(text)
        {
        }

        public override void OnGUI()
        {
            CardInfo[] cards = CardChoice.instance.cards;
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(250f), GUILayout.Height(350f));
            for (int i = 0; i < cards.Length; i++)
            {
                if (GUILayout.Button($"<i><color=white>{cards[i].cardName}</color></i>", new GUILayoutOption[0]))
                {
                    GameObject gameObject = CardChoice.instance.AddCard(cards[i]);
                    gameObject.GetComponentInChildren<CardVisuals>().firstValueToSet = true;
                    gameObject.transform.root.GetComponentInChildren<ApplyCardStats>().Pick(PlayerHelper.LocalPlayer().teamID, false, PickerType.Team);
                    GameObject.Destroy(gameObject);
                }
            }
            GUILayout.EndScrollView();
        }
    }
}
