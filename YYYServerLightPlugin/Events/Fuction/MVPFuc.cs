using System;
using System.Collections.Generic;
using System.Linq;
using MEC;
using PlayerRoles.PlayableScps.Scp049;
using PlayerStatsSystem;
using PluginAPI.Core;
using PluginAPI.Core.Attributes;

namespace YYYServerLightPlugin.Events.Fuction;

public class MVPFuc
{
    public DateTime starttime;
    public string firstescapeplayer;
    public int first_player_escape_time;
    public Dictionary<int,int> KillNum = new Dictionary<int, int>();
    public Dictionary<int,int> TotalDamege = new Dictionary<int, int>();
    [PluginEvent(PluginAPI.Enums.ServerEventType.RoundStart)]
    void RoundStart()
    {
        starttime = DateTime.Now;
    }

    [PluginEvent(PluginAPI.Enums.ServerEventType.PlayerEscape)]
    void PlayerEscape(Player player, PlayerRoles.RoleTypeId newRole)
    {
        if(first_player_escape_time == 0)
        {
            firstescapeplayer = player.Nickname;
            first_player_escape_time = (int)(DateTime.Now - starttime).TotalSeconds;
        }
    }
    private IEnumerator<float> ShowInfo()
    {
        yield return Timing.WaitForSeconds(0.01f);

        string txt = "<size=23>MVP 时刻 MVP\n《——————————《=w=》——————————》";
        var sortedKillNum = KillNum.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        var sortedTotalDamege = TotalDamege.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        foreach (var x in sortedKillNum)
        {
            var tmpplr = Player.Get(x.Key);
            if (tmpplr != null)
            {
                txt += "\nACE! <color=#7FFFAA>" + tmpplr.Nickname + "</color> 杀了 <color=#FF0000>" + x.Value.ToString() + "</color> 个人";
                break;
            }
        }
        foreach (var x in sortedTotalDamege)
        {
            var tmpplr = Player.Get(x.Key);
            if (tmpplr != null)
            {
                txt += "\n<color=#7FFFAA>" + tmpplr.Nickname + "</color> 造成了最多的伤害 造成了 <color=#FF0000>" + x.Value.ToString() + "</color> 滴血的伤害";
                break;
            }
        }
        if (first_player_escape_time == 0)
        {
            txt += "\n本回合无人逃出";
        }
        else
        {
            txt += "\n" + firstescapeplayer + " 是第一个<color=#7FFF00>逃离设施的人</color> 用时 " + first_player_escape_time.ToString() + " 秒";
        }
        txt += "</size>";
        for (int i = 0; i < 30; i++)
        {
            txt += "\n";
        }
        for(int i = 0; i < 10;i++)
        {
            yield return Timing.WaitForSeconds(0.5f);
            foreach (Player player in Player.GetPlayers())
            {
                player.ReceiveHint(txt, 10);
            }
        }

    }

    [PluginEvent(PluginAPI.Enums.ServerEventType.RoundEnd)]
    void RoundEnd(RoundSummary.LeadingTeam leadingTeam)
    {
        Timing.RunCoroutine(ShowInfo());
    }
    [PluginEvent(PluginAPI.Enums.ServerEventType.PlayerDying)]
    void PlayerDying(Player player, Player attacker, PlayerStatsSystem.DamageHandlerBase damageHandler)
    {
        if(attacker != null)
        {
            if (KillNum.ContainsKey(attacker.PlayerId))
            {
                KillNum[attacker.PlayerId]++;
            }
            else
            {
                KillNum.Add(attacker.PlayerId, 1);
            }
        }
    }
    [PluginEvent(PluginAPI.Enums.ServerEventType.PlayerDamage)]
    void PlayerDamage(Player target, Player attacker, PlayerStatsSystem.DamageHandlerBase damageHandler)
    {
        int tmpdamage = 0;
        if(attacker != null)
        {
            if (damageHandler is StandardDamageHandler standardDamageHandler)
            {
                if (standardDamageHandler.Damage == -1)
                {
                    tmpdamage = (int)target.Health;
                }
                else if (standardDamageHandler.Damage >= target.Health)
                {
                    tmpdamage = (int)target.Health;
                }
                else
                {
                    tmpdamage = (int)standardDamageHandler.Damage;
                }

                if (TotalDamege.ContainsKey(attacker.PlayerId))
                {
                    TotalDamege[attacker.PlayerId] += tmpdamage;
                }
                else
                {
                    TotalDamege.Add(attacker.PlayerId, tmpdamage);
                }
            }
        }
    }
    [PluginEvent(PluginAPI.Enums.ServerEventType.PlayerChangeRole)]
    void PlayerChangeRole(Player player, PlayerRoles.PlayerRoleBase oldRole, PlayerRoles.RoleTypeId newRole, PlayerRoles.RoleChangeReason changeReason)
    {
        if(newRole == PlayerRoles.RoleTypeId.Scp049)
        {
            Timing.CallDelayed(1f, () =>
            {
                if(player.ReferenceHub.roleManager.CurrentRole is Scp049Role scp049Role)
                {
                    Scp049SenseAbility scp049SenseAbility;
                    if (!scp049Role.SubroutineModule.TryGetSubroutine<Scp049SenseAbility>(out scp049SenseAbility))
                    {
                        scp049SenseAbility.Cooldown.Trigger(10000000000);
                    }
                }
            });
        }
    }
    [PluginEvent(PluginAPI.Enums.ServerEventType.RoundRestart)]
    void RoundRestart()
    {
        firstescapeplayer = null;
        first_player_escape_time = 0;
        TotalDamege.Clear();
        KillNum.Clear();
    }
}