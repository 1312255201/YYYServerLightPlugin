using System;
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
                        if (new System.Random(Environment.TickCount + player.PlayerId).Next(1, 100) >= 50)
                        {
                            player.AddItem(ItemType.KeycardJanitor);
                        }
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
                    });
                    break;
                case RoleTypeId.NtfCaptain:
                    Timing.CallDelayed(0.1f, () =>
                    {
                        player.Health = 100;
                    });
                    break;
                case RoleTypeId.NtfPrivate:
                    Timing.CallDelayed(0.1f, () =>
                    {
                        player.Health = 100;
                    });
                    break;
                case RoleTypeId.NtfSergeant:
                    Timing.CallDelayed(0.1f, () =>
                    {
                        player.Health = 100;
                    });
                    break;
                case RoleTypeId.NtfSpecialist:
                    Timing.CallDelayed(0.1f, () =>
                    {
                        player.Health = 100;
                    });
                    break;
                case RoleTypeId.ChaosConscript:
                    Timing.CallDelayed(0.1f, () =>
                    {
                        player.Health = 100;
                    });
                    break;
                case RoleTypeId.ChaosMarauder:
                    Timing.CallDelayed(0.1f, () =>
                    {
                        player.Health = 100;
                    });
                    break;
                case RoleTypeId.ChaosRepressor:
                    Timing.CallDelayed(0.1f, () =>
                    {
                        player.Health = 100;
                    });
                    break;
                case RoleTypeId.ChaosRifleman:
                    Timing.CallDelayed(0.1f, () =>
                    {
                        player.Health = 100;
                    });
                    break;
                case RoleTypeId.Scp106:
                    Timing.CallDelayed(0.1f, () =>
                    {
                        player.Health = 1400;
                    });
                    break;
                case RoleTypeId.Scp049:
                    Timing.CallDelayed(0.1f, () =>
                    {
                        player.Health = 3500;
                    });
                    break;
                case RoleTypeId.Scp0492:
                    Timing.CallDelayed(0.1f, () =>
                    {
                        player.Health = 800;
                    });
                    break;
                case RoleTypeId.Scp096:
                    Timing.CallDelayed(0.1f, () =>
                    {
                        player.Health = 2500;
                    });
                    break;
                case RoleTypeId.Scp939:
                    Timing.CallDelayed(0.1f, () =>
                    {
                        player.Health = 2500;
                    });
                    break;
            }

            if (newRole.GetTeam() != Team.SCPs)
            {
                player.SetAmmo(ItemType.Ammo9x19,100);
                player.SetAmmo(ItemType.Ammo12gauge,100);
                player.SetAmmo(ItemType.Ammo44cal,100);
                player.SetAmmo(ItemType.Ammo556x45,100);
                player.SetAmmo(ItemType.Ammo762x39,100);
            }
        }
    }
}