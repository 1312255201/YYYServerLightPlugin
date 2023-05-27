using System;
using System.Collections.Generic;
using System.Linq;
using MEC;
using PlayerRoles;
using PluginAPI.Core;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using Respawning;

namespace YYYServerLightPlugin.Events.Fuction;

public class DeathInFormation
{
    [PluginEvent(ServerEventType.RoundStart)]
    void RoundStart()
    {
        Log.Info("?awa");
        Timing.RunCoroutine(Respawntime());
        Log.Info("!awa");
    }

    [PluginEvent(ServerEventType.RoundRestart)]
    void RoundRestart()
    {

    }
    private IEnumerator<float> Respawntime()
    {
        yield return Timing.WaitForSeconds(1f);
        while (Round.IsRoundStarted)
        {
            yield return Timing.WaitForSeconds(1f);
            try
            {
                if (Player.GetPlayers().Any(x => x.Role == RoleTypeId.Spectator))
                {
                    Log.Info("awa");
                    string teamfuhuo = "";
                    if (Respawn.NextKnownTeam == SpawnableTeamType.NineTailedFox)
                    {
                        teamfuhuo = "<color=#1E90FF>白给狐٩(๑❛ᴗ❛๑)۶</color>";
                    }
                    if (Respawn.NextKnownTeam == SpawnableTeamType.ChaosInsurgency)
                    {
                        teamfuhuo = "<color=#3CB371>馄饨裂开者ヾ(๑╹◡╹)ﾉ</color>";
                    }
                    if (Respawn.NextKnownTeam == SpawnableTeamType.None)
                    {
                        teamfuhuo = "我不知道别看我QAQ";
                    }
                    string tmp = string.Concat("\n\n\n\n\n\n\n<align=right><size=23>", "<pos=30%>你已阵亡" + "\n<pos=30%>但是不用担心你马上会复活:</pos>", "\n<pos=30%>剩余时间:", Convert.ToInt32(TimeSpan.FromSeconds(RespawnManager.Singleton._timeForNextSequence - RespawnManager.Singleton._stopwatch.Elapsed.TotalSeconds).TotalSeconds).ToString(), "</pos>\n<pos=30%><color=#4169E1>👮九尾狐机票数:</color>", Respawn.NtfTickets, "</pos>\n<pos=30%><color=#228B22>🐻混沌车票数:</color>", Respawn.ChaosTickets, "</pos>\n<pos=30%>👻当前观察者人数：", Player.GetPlayers().Count(x=> !x.IsAlive).ToString(), "</pos>\n<pos=30%><color=#FFFF00>欢迎来到嘤嘤嘤服务器Q群1021889243</color></pos>\n<pos=30%>欢迎加群反馈BUG</pos>\n<pos=30%>复活角色:", teamfuhuo, "</pos></size></align>\n\n\n\n\n\n");
                    foreach (var player in Player.GetPlayers().Where(x => x.Role == RoleTypeId.Spectator))
                    {
                        try
                        {
                            player.ReceiveHint(tmp,2);
                        }
                        catch (Exception ex)
                        {
                            Log.Info(ex.Message);
                            Log.Info(ex.GetBaseException().ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Info(ex.Message);
                Log.Info(ex.GetBaseException().ToString());
            }
        }
    }
}