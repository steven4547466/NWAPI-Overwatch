using AdminToys;
using CustomPlayerEffects;
using InventorySystem.Items;
using InventorySystem.Items.Firearms;
using InventorySystem.Items.Radio;
using InventorySystem.Items.Usables;
using LiteNetLib;
using MapGeneration.Distributors;
using PlayerRoles;
using PlayerStatsSystem;
using PluginAPI.Core;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using PluginAPI.Events;
using System;
using System.Collections.Generic;
using CommandSystem;
using RemoteAdmin;
using HarmonyLib;
using System.Linq;

namespace NWAPI_Overwatch
{
    public class Plugin
    {
        public static Plugin Singleton { get; private set; }
        public static Harmony Harmony { get; private set; }

        public static List<Player> OverwatchPlayers { get; private set; } = new List<Player>();

        [PluginPriority(LoadPriority.Highest)]
        [PluginEntryPoint("Lmao NW \"Removed\" Overwatch", "1.0.0", "Fixing NW's mistakes since 2020", "Steven4547466")] // Is this too much?
        void LoadPlugin()
        {
            Singleton = this;
            Harmony = new Harmony($"nw-overwatch-{DateTime.Now.Ticks}");
            Harmony.PatchAll();
        }

        [PluginUnload]
        void UnloadPlugin()
        {
            Harmony.UnpatchAll(Harmony.Id);
            Harmony = null;
        }

        public static void AddOverwatch(Player player)
        {
            OverwatchPlayers.Add(player);
            player.GameObject.AddComponent<OverwatchBehaviour>();
        }

        public static void RemoveOverwatch(Player player)
        {
            OverwatchPlayers.Remove(player);
            if (player.GameObject.TryGetComponent(out OverwatchBehaviour behaviour))
            {
                behaviour.Destroy();
            }
        }

        public static bool IsInOverwatch(Player player)
        {
            return OverwatchPlayers.Contains(player);
        }

        [PluginEvent(ServerEventType.RoundRestart)]
        public void RestartingRound()
        {
            foreach(Player player in OverwatchPlayers)
            {
                
                if (player.GameObject.TryGetComponent(out OverwatchBehaviour behaviour))
                {
                    behaviour.Destroy();
                }
            }
            OverwatchPlayers.Clear();
        }
    }

    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class OverwatchCommand : ICommand
    {
        public string Command { get; } = "nwow"; // overwatch is a taken command, but doesn't work

        public string[] Aliases { get; } = new string[] { };

        public string Description { get; } = "Enable/disable overwatch.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!(sender is PlayerCommandSender plrSender))
            {
                response = "Not a player";
                return false;
            }
            Player player = Player.Get(plrSender.ReferenceHub);
            if (Plugin.IsInOverwatch(player))
            {
                Plugin.RemoveOverwatch(player);
                response = "No longer in overwatch";
                return true;
            }
            else
            {
                player.Role = RoleTypeId.Spectator;
                Plugin.AddOverwatch(player);
                response = "Now in overwatch";
                return true;
            }
        }
    }
}
