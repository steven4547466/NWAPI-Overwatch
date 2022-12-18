using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using PlayerRoles;
using PluginAPI.Core;

namespace NWAPI_Overwatch
{
    [HarmonyPatch(typeof(PlayerRoleManager), nameof(PlayerRoleManager.ServerSetRole))]
    static class ChageRolePatch
    {
        public static bool Prefix(PlayerRoleManager __instance, RoleTypeId newRole, RoleChangeReason reason)
        {
            Player player = Player.Get(__instance.Hub);
            if (player != null && Plugin.IsInOverwatch(player))
            {
                return false;
            }
            return true;
        }
    }
}
