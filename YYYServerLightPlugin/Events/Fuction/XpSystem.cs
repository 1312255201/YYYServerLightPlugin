using System.IO;
using System.Threading.Tasks;
using MEC;
using PlayerRoles;
using PluginAPI.Core;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using YYYServerLightPlugin.API;

namespace YYYServerLightPlugin.Events.Fuction;

public class XpSystem
{
    [PluginEvent(ServerEventType.Scp079LevelUpTier)]
    void Scp079LevelUpTier(Player player, int tier)
    {
        IniFile.AddExp(player.UserId,10);
    }

    [PluginEvent(ServerEventType.PlayerDeath)]
    void PlayerDeath(Player player, Player attacker, PlayerStatsSystem.DamageHandlerBase damageHandler)
    {
        if (attacker != null)
        {
            bool itsadd = false;
            if(player.Team == Team.SCPs && player.Role != RoleTypeId.Scp0492)
            {
                if ((attacker != player))
                {
                    itsadd = true;
                    Timing.CallDelayed(0.3f, () => {
                        if(player.Role == RoleTypeId.Spectator)
                        {
                            IniFile.AddExp(attacker.ReferenceHub.characterClassManager.UserId, 50);
                        }
                    });
            
                }
            }
            if(attacker.Team == Team.SCPs)
            {
                foreach (var cat in Player.GetPlayers())
                {
                    if (cat.Role == RoleTypeId.Scp079)
                    {
                        IniFile.AddExp(cat.ReferenceHub.characterClassManager.UserId, 5);
                    }
                }
                if ((attacker != player) && itsadd == false)
                {
                    itsadd = true;
                    Timing.CallDelayed(0.3f, () => {
                        if(player.Role == RoleTypeId.Spectator)
                        {
                            IniFile.AddExp(attacker.ReferenceHub.characterClassManager.UserId, 5);
                        }
                    });
            
                }
            }
            if(attacker.IsHuman)
            {
                if(attacker != player && itsadd == false)
                {
                    Timing.CallDelayed(0.3f, () => {
                        if(player.Role == RoleTypeId.Spectator)
                        {
                            IniFile.AddExp(attacker.ReferenceHub.characterClassManager.UserId, 5);
                        }
                    });
                }
            }
        }

    }
    [PluginEvent(ServerEventType.PlayerJoined)]
    void PlayerJoined(Player player)
    {
        Task.Run(() => {
            int oldexp = IniFile.ReadExp(player.UserId);
            int nowexp = IniFile.MyExp(player);
            string path = "C:\\Users\\Administrator\\AppData\\Roaming\\SCP Secret Laboratory\\经验\\" + player.UserId;
            if (!(oldexp == -1 || oldexp == 0))
            {
                if (nowexp == 0)
                {
                    IniFile.AddExp2(player.UserId, IniFile.ReadExp(player.UserId));
                    if (IniFile.MyExp(player) != 0)
                    {
                        if (File.Exists(path))
                        {
                            File.Delete(path);
                            Log.Info("删除文件" + path + "转移完毕");
                        }
                    }
                    Log.Info("已经转移" +player.Nickname + "的经验" + IniFile.MyExp(player).ToString());
                }
                else if (oldexp > nowexp)
                {
                    Log.Info("玩家" + player.Nickname + player.UserId + "可能有经验冲突");
                }
                else
                {
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                        Log.Info("删除文件" + path + "老exp小于新exp");
                    }
                }
            }
            else
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                    Log.Info("是0或-1");
                }
            }
            MyApi.SetNick(player, nowexp);
        });
    }
}