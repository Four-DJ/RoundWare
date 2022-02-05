using RoundWare.Event;
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
    [Menu("Player")]
    internal class Player : BaseMenu, OnUpdateEvent
    {
        bool fly, godMode, infAmo;

        public Player(string text) : base(text)
        {
            Main.Instance.onUpdateEvents.Add(this);
        }

        [Toggle("Fly")]
        public void Fly(bool state)
        {
            fly = state;
            if (state)
            {
                PlayerHelper.LocalPlayer().data.playerVel.enabled = false;
            }
            else
            {
                PlayerHelper.LocalPlayer().data.playerVel.enabled = true;
            }
        }

        [Toggle("GodMode")]
        public void GodMode(bool state)
        {
            godMode = state;
        }

        [Toggle("InfAmo")]
        public void InfAmo(bool state)
        {
            infAmo = state;
        }

        public void Update()
        {
            if (fly)
            {
                Transform transform = PlayerHelper.LocalPlayer().transform;

                if (Input.GetKey(KeyCode.W))
                {
                    PlayerHelper.LocalPlayer().transform.position += transform.transform.up * 10 * Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.S))
                {
                    PlayerHelper.LocalPlayer().transform.position -= transform.transform.up * 10 * Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    PlayerHelper.LocalPlayer().transform.position -= transform.transform.right * 10 * Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    PlayerHelper.LocalPlayer().transform.position += transform.transform.right * 10 * Time.deltaTime;
                }

                PlayerHelper.LocalPlayer().data.isGrounded = true;
            }

            if (godMode)
            {
                PlayerHelper.LocalPlayer().data.health = float.PositiveInfinity;
            }

            if (infAmo)
            {
                if (Input.GetKey(KeyCode.Mouse0))
                    PlayerHelper.LocalPlayer().data.weaponHandler.gun.Attack(0, true, 1f, 1f, false);
            }
        }
    }
}
