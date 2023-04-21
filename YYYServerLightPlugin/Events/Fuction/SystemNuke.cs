using System.Collections;
using System.Collections.Generic;
using MEC;
using PluginAPI.Core;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;

namespace YYYServerLightPlugin.Events.Fuction;

public class SystemNuke
{
    private CoroutineHandle coroutine;
    private CoroutineHandle coroutine2;
    [PluginEvent(ServerEventType.RoundStart)]
    void RoundStart()
    {
        coroutine = Timing.RunCoroutine(SystemNukeTiming());
    }
    private IEnumerator<float> SystemNukeTiming()
    {
        yield return Timing.WaitForSeconds(1500f);
        Warhead.Start();
        Warhead.IsLocked = true;
        Server.SendBroadcast( "<color=#FF0000>[☢系统核弹☢]</color>\n<color=#00FF00>系统核弹已开启无法关闭</color>",10);
    }

    [PluginEvent(ServerEventType.WarheadDetonation)]
    void WarheadDetonation()
    {
        coroutine2 = Timing.RunCoroutine(EndOfGame());
    }
    private IEnumerator<float> EndOfGame()
    {
        Server.SendBroadcast( "<color=#FF0000>[☢系统核弹☢]</color>\n<color=#00FF00>地下核弹已经引爆，为了确保安全，即将在200s后向地面核聚变打击\n先关人员请尽快撤离</color>",10);
        yield return Timing.WaitForSeconds(200f);
        foreach (var variabPlayer in Player.GetPlayers())
        {
            if (variabPlayer.IsAlive)
            {
                variabPlayer.Kill();
            }
        }
    }

    [PluginEvent(ServerEventType.RoundRestart)]
    void RoundRestart()
    {
        Timing.KillCoroutines(coroutine);
        Timing.KillCoroutines(coroutine2);
    }
}