//using CommandSystem;
//using HarmonyLib;
//using PlayerRoles;
//using PluginAPI.Core;
//using RemoteAdmin;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace NWAPI_Overwatch
//{
//    [HarmonyPatch(typeof(CommandSystem.Commands.RemoteAdmin.OverwatchCommand), nameof(CommandSystem.Commands.RemoteAdmin.OverwatchCommand.Execute))]
//    static class OverwatchCommandPatch
//    {
//        public static bool Prefix(ArraySegment<string> arguments, ICommandSender sender, out string response)
//        {
//            if (!(sender is PlayerCommandSender plrSender))
//            {
//                response = "Not a player";
//                return false;
//            }
//            Player player = Player.Get(plrSender.ReferenceHub);
//            if (Plugin.IsInOverwatch(player))
//            {
//                Plugin.RemoveOverwatch(player);
//                response = "No longer in overwatch";
//                return true;
//            }
//            else
//            {
//                player.Role = RoleTypeId.Spectator;
//                Plugin.AddOverwatch(player);
//                response = "Now in overwatch";
//                return true;
//            }
//        }
//    }
//}
