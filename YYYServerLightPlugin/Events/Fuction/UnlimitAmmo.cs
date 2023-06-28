using System;
using MEC;
using PlayerRoles;
using PluginAPI.Core;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;

namespace YYYServerLightPlugin.Events.Fuction
{
    public class UnlimitAmmo
    {
        [PluginEvent(ServerEventType.PlayerReloadWeapon)]
        void PlayerChangeRole(Player player, InventorySystem.Items.Firearms.Firearm firearm)
        {
            if (firearm.ItemTypeId != ItemType.ParticleDisruptor)
            {
                try
                {
                    player.SetAmmo(firearm.AmmoType, firearm.AmmoManagerModule.MaxAmmo);
                }
                catch
                {
                
                }
            }
        }
    }
}