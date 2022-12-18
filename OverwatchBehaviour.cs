using PlayerRoles;
using PlayerRoles.Spectating;
using PluginAPI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NWAPI_Overwatch
{
    public class OverwatchBehaviour : MonoBehaviour
    {
        private bool Active = false;
        private Player Player;
        private SpectatorRole Role;
        private float Timer = 0;
        private Player PreviousSpectated = null;

        public void OnEnable()
        {
            Player = Player.Get(gameObject);
            Role = Player.ReferenceHub.roleManager.CurrentRole as SpectatorRole;
            Active = true;
        }

        public void Update()
        {
            if (!Active)
                return;
            if (Player.Role != RoleTypeId.Spectator)
            {
                Plugin.RemoveOverwatch(Player);
                Active = false;
                return;
            }
            Timer += Time.deltaTime;
            if (Timer > 0.5f)
            {
                Timer = 0;
                Player spectatedPlayer = Player.Get(Role.SyncedSpectatedNetId);
                if (PreviousSpectated != spectatedPlayer)
                {
                    PreviousSpectated = spectatedPlayer;
                    Player.SendConsoleMessage($"\nPlayer Id: {spectatedPlayer.PlayerId}\nName: {spectatedPlayer.Nickname}\nUser ID: {spectatedPlayer.UserId}");
                }
                Player.ReceiveHint($"\n\n\n\n\n\nPlayer Id: {spectatedPlayer.PlayerId}\nName: {spectatedPlayer.Nickname}\nUser ID: {spectatedPlayer.UserId}", 1f);
            }
        }

        public void Destroy()
        {
            Active = false;
            DestroyImmediate(this);
        }
    }
}
