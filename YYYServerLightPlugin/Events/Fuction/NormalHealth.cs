using MEC;
using PlayerRoles;
using PluginAPI.Core;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;

namespace YYYServerLightPlugin.Events.Fuction
{
    public class NormalHealth
    {
        [PluginEvent(ServerEventType.PlayerChangeRole)]
        void PlayerChangeRole(Player player, PlayerRoles.PlayerRoleBase oldRole, PlayerRoles.RoleTypeId newRole, PlayerRoles.RoleChangeReason changeReason)
        {
            switch (newRole)
            {
                case RoleTypeId.ClassD:
                    Timing.CallDelayed(0.1f, () =>
                    {
                        player.Health = 100;
                    });
                    break;
                case RoleTypeId.Scientist:
                    Timing.CallDelayed(0.1f, () =>
                    {
                        player.Health = 100;
                    });
                    break;
                case RoleTypeId.FacilityGuard:
                    Timing.CallDelayed(0.1f, () =>
                    {
                        player.Health = 100;
                        player.SetAmmo(ItemType.Ammo556x45,0);
                        player.SetAmmo(ItemType.Ammo762x39,0);
                        player.SetAmmo(ItemType.Ammo9x19,105);
                    });
                    break;
                case RoleTypeId.NtfCaptain:
                    Timing.CallDelayed(0.1f, () =>
                    {
                        player.Health = 100;
                        player.SetAmmo(ItemType.Ammo556x45,120);
                        player.SetAmmo(ItemType.Ammo762x39,0);
                        player.SetAmmo(ItemType.Ammo9x19,100);
                    });
                    break;
                case RoleTypeId.NtfPrivate:
                    Timing.CallDelayed(0.1f, () =>
                    {
                        player.Health = 100;
                        player.SetAmmo(ItemType.Ammo556x45,40);
                        player.SetAmmo(ItemType.Ammo762x39,0);
                        player.SetAmmo(ItemType.Ammo9x19,100);
                    });
                    break;
                case RoleTypeId.NtfSergeant:
                    Timing.CallDelayed(0.1f, () =>
                    {
                        player.Health = 100;
                        player.SetAmmo(ItemType.Ammo556x45,80);
                        player.SetAmmo(ItemType.Ammo762x39,0);
                        player.SetAmmo(ItemType.Ammo9x19,50);
                    });
                    break;
                case RoleTypeId.NtfSpecialist:
                    Timing.CallDelayed(0.1f, () =>
                    {
                        player.Health = 100;
                        player.SetAmmo(ItemType.Ammo556x45,120);
                        player.SetAmmo(ItemType.Ammo762x39,20);
                        player.SetAmmo(ItemType.Ammo9x19,120);
                    });
                    break;
                
                case RoleTypeId.ChaosConscript:
                    Timing.CallDelayed(0.1f, () =>
                    {
                        player.Health = 100;
                        player.SetAmmo(ItemType.Ammo556x45,0);
                        player.SetAmmo(ItemType.Ammo762x39,120);
                        player.SetAmmo(ItemType.Ammo9x19,0);
                    });
                    break;
                case RoleTypeId.ChaosMarauder:
                    Timing.CallDelayed(0.1f, () =>
                    {
                        player.Health = 100;
                        player.SetAmmo(ItemType.Ammo556x45,0);
                        player.SetAmmo(ItemType.Ammo762x39,120);
                        player.SetAmmo(ItemType.Ammo9x19,0);
                    });
                    break;
                case RoleTypeId.ChaosRepressor:
                    Timing.CallDelayed(0.1f, () =>
                    {
                        player.Health = 100;
                        player.SetAmmo(ItemType.Ammo556x45,0);
                        player.SetAmmo(ItemType.Ammo762x39,120);
                        player.SetAmmo(ItemType.Ammo9x19,0);
                    });
                    break;
                case RoleTypeId.ChaosRifleman:
                    Timing.CallDelayed(0.1f, () =>
                    {
                        player.Health = 100;
                        player.SetAmmo(ItemType.Ammo556x45,0);
                        player.SetAmmo(ItemType.Ammo762x39,120);
                        player.SetAmmo(ItemType.Ammo9x19,0);
                    });
                    break;
                case RoleTypeId.Scp106:
                    Timing.CallDelayed(0.1f, () =>
                    {
                        player.Health = 1200;
                    });
                    break;
                case RoleTypeId.Scp049:
                    Timing.CallDelayed(0.1f, () =>
                    {
                        player.Health = 2500;
                    });
                    break;
                case RoleTypeId.Scp0492:
                    Timing.CallDelayed(0.1f, () =>
                    {
                        player.Health = 500;
                    });
                    break;
                case RoleTypeId.Scp096:
                    Timing.CallDelayed(0.1f, () =>
                    {
                        player.Health = 2000;
                    });
                    break;
                case RoleTypeId.Scp939:
                    Timing.CallDelayed(0.1f, () =>
                    {
                        player.Health = 1800;
                    });
                    break;
            }

        }


    }
}