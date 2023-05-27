using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MEC;
using PlayerRoles;
using PluginAPI.Core;
using PluginAPI.Core.Attributes;
using PluginAPI.Events;

namespace YYYServerLightPlugin.Events.Fuction;
public class awa
{
    private Player player = null;
    private int playerid = 0;
    private bool wait2 = false;
    private List<string> strings = new List<string>();
    public Player playerawa { get => player; set => player = value; }
    public int playeridawa { get => playerid; set => playerid = value; }
    public List<string> message { get => strings; set => strings = value; }
    public bool wait { get => wait2; set => wait2 = value; }
}
public class ShowTeamInFormation
{
    private List<CoroutineHandle> Coroutines = new List<CoroutineHandle>();
    private string ntfteaminfo;
    private string chiteaminfo;
    public static List<awa> awas = new List<awa>();
    //DD
    public static List<string> chatList = new List<string>();
    //科学家
    public static List<string> chatList2 = new List<string>();
    //九尾狐
    public static List<string> chatList3 = new List<string>();
    //混沌
    public static List<string> chatList4 = new List<string>();
    //阴间
    public static List<string> chatList5 = new List<string>();
    //SCP
    public static List<string> chatList6 = new List<string>();
    //训练人员
    public static List<string> chatList7 = new List<string>();
    private static int chatid = 0;
    public static Dictionary<RoleTypeId, string> keyValuePairs = new Dictionary<RoleTypeId, string>()
    {
        {RoleTypeId.NtfPrivate,"九尾狐 列兵" },
        {RoleTypeId.NtfCaptain,"九尾狐 指挥官" },
        {RoleTypeId.NtfSergeant,"九尾狐 中士" },
        {RoleTypeId.NtfSpecialist,"九尾狐 收容专家" },
        {RoleTypeId.FacilityGuard,"设施保安" },
        {RoleTypeId.ChaosConscript,"混沌 征召兵" },
        {RoleTypeId.ChaosMarauder,"混沌 掠夺者" },
        {RoleTypeId.ChaosRepressor,"混沌 压制者" },
        {RoleTypeId.ChaosRifleman,"混沌 步枪兵" },
        {RoleTypeId.Scp096,"SCP-096" },
        {RoleTypeId.Scp049,"SCP-049" },
        {RoleTypeId.Scp173,"SCP-173" },
        {RoleTypeId.Scp939,"SCP-939" },
        {RoleTypeId.Scp106,"SCP-106" },
        {RoleTypeId.Scp0492,"SCP-049-2" },
        {RoleTypeId.Scp079,"SCP-079" },
        {RoleTypeId.ClassD,"D级人员" },
        {RoleTypeId.Scientist,"科学家" },
        {RoleTypeId.Tutorial,"训练人员" },
        {RoleTypeId.Overwatch,"观察者" },
        {RoleTypeId.CustomRole,"本地角色？" },
        {RoleTypeId.Spectator,"观察者" },
        {RoleTypeId.None,"空" }
    };
    [PluginEvent(PluginAPI.Enums.ServerEventType.WaitingForPlayers)]
    void OnWaitingForPlayer()
    {
        Coroutines.Add(Timing.RunCoroutine(HintRun()));
    }
    public void GetSCPHP()
    {
        int ntfnum = Player.GetPlayers().Count(x => x.Team == Team.FoundationForces || x.Role == RoleTypeId.FacilityGuard);
        int chinum = Player.GetPlayers().Count(x => x.Team == Team.ChaosInsurgency);
        try
        {
            foreach (var player in Player.GetPlayers())
            {

                if (player.Team == Team.FoundationForces || player.Role == RoleTypeId.FacilityGuard)
                {
                    ntfteaminfo = ntfteaminfo + "\n[" + keyValuePairs[player.Role] + "]" + player.Nickname + " HP:" + player.Health;
                }
                if (player.Team == Team.ChaosInsurgency)
                {
                    chiteaminfo = chiteaminfo + "\n[" + keyValuePairs[player.Role] + "]" + player.Nickname + " HP:" + player.Health;
                }
            }
        }
        catch
        {
        }
        try
        {
            if (ntfnum > 5)
            {
                ntfteaminfo = "九尾狐当前人数:" + ntfnum;
            }
            if (chinum > 5)
            {
                chiteaminfo = "混沌当前人数:" + chinum;
            }
        }
        catch
        {
        }
        try
        {
            foreach (var player1 in Player.GetPlayers())
            {
                if (player1.Team == Team.ChaosInsurgency)
                {
                    foreach (var awa2 in awas)
                    {
                        if (player1.PlayerId == awa2.playeridawa)
                        {
                            string temp = "";

                            foreach (var message in awa2.message)
                            {
                                if (message.Contains("CHI血量信息"))
                                {
                                    temp = message;
                                }
                            }
                            awa2.message.Remove(temp);
                            StringBuilder str = new StringBuilder();
                            str.Append("\n<size=0><color=#00BFFF>[CHI血量信息]</color></size><align=right><size=23>");
                            str.Append(chiteaminfo);
                            str.Append("</size>");
                            awa2.message.Add(str.ToString());
                            str.Clear();
                        }
                    }
                    continue;
                }
                foreach (awa awa2 in awas)
                {
                    if (player1.PlayerId == awa2.playeridawa)
                    {
                        string temp = "";

                        foreach (string message in awa2.message)
                        {
                            if (message.Contains("CHI血量信息"))
                            {
                                temp = message;
                            }
                        }
                        if (temp != "")
                        {
                            awa2.message.Remove(temp);
                        }
                    }

                }
            }

        }
        catch
        {

        }
        try
        {
            foreach (var player1 in Player.GetPlayers())
            {
                if (player1.Team == Team.FoundationForces || player1.Role == RoleTypeId.FacilityGuard)
                {
                    foreach (var awa2 in awas)
                    {
                        if (player1.PlayerId == awa2.playeridawa)
                        {
                            string temp = "";

                            foreach (var message in awa2.message)
                            {
                                if (message.Contains("NTF血量信息"))
                                {
                                    temp = message;
                                }
                            }
                            awa2.message.Remove(temp);
                            if(player1.Items.Any(x=> x.ItemTypeId == ItemType.Radio))
                            {
                                StringBuilder str = new StringBuilder();
                                str.Append("\n<size=0><color=#00BFFF>[NTF血量信息]</color></size><align=right><size=23>");
                                str.Append(ntfteaminfo);
                                str.Append("</size>");
                                awa2.message.Add(str.ToString());
                                str.Clear();
                            }
                        }
                    }
                    continue;
                }
                foreach (awa awa2 in awas)
                {
                    if (player1.PlayerId == awa2.playeridawa)
                    {
                        string temp = "";

                        foreach (string message in awa2.message)
                        {
                            if (message.Contains("NTF血量信息"))
                            {
                                temp = message;
                            }
                        }
                        if (temp != "")
                        {
                            awa2.message.Remove(temp);
                        }
                    }

                }
            }

        }
        catch
        {
        }
        ntfteaminfo = "";
        chiteaminfo = "";
    }
    public static IEnumerator<float> chatTiming(Player player,string txt)
    {
        yield return Timing.WaitForSeconds(1f);
        chatid++;
        txt.Replace("<", "＜");
        txt.Replace(">", "＞");
        txt.Replace("\n", "");
        txt.Replace("\r", "");
        if (txt.Length <= 30)
        {
            List<string> list = new List<string>();
            if (player.Role == RoleTypeId.ClassD)
            {
                if (chatList.Count <= 5)
                {
                    chatList.Add("<pos=35%>[" + keyValuePairs[player.Role] + "]" + player.Nickname + ":" + txt);
                }
                else
                {
                    chatList.RemoveAt(0);
                    chatList.Add("<pos=35%>[" + keyValuePairs[player.Role] + "]" + player.Nickname + ":" + txt);
                }
                for (int i = 0; i < chatList.Count; i++)
                {
                    list.Add(chatList[i]);
                }
            }
            if (player.Role == RoleTypeId.Scientist)
            {
                if (chatList2.Count <= 5)
                {
                    chatList2.Add("<pos=35%>[" + keyValuePairs[player.Role] + "]" + player.Nickname + ":" + txt);
                }
                else
                {
                    chatList2.RemoveAt(0);
                    chatList2.Add("<pos=35%>[" + keyValuePairs[player.Role] + "]" + player.Nickname + ":" + txt);
                }
                for (int i = 0; i < chatList2.Count; i++)
                {
                    list.Add(chatList2[i]);
                }
            }
            if (player.Team == Team.FoundationForces || player.Role == RoleTypeId.FacilityGuard)
            {
                if (chatList3.Count <= 5)
                {
                    chatList3.Add("<pos=35%>[" + keyValuePairs[player.Role] + "]" + player.Nickname + ":" + txt);
                }
                else
                {
                    chatList3.RemoveAt(0);
                    chatList3.Add("<pos=35%>[" + keyValuePairs[player.Role] + "]" + player.Nickname + ":" + txt);
                }
                for (int i = 0; i < chatList3.Count; i++)
                {
                    list.Add(chatList3[i]);
                }
            }
            if (player.Team == Team.ChaosInsurgency)
            {
                if (chatList4.Count <= 5)
                {
                    chatList4.Add("<pos=35%>[" + keyValuePairs[player.Role] + "]" + player.Nickname + ":" + txt);
                }
                else
                    {
                        chatList4.RemoveAt(0);
                        chatList4.Add("<pos=35%>[" + keyValuePairs[player.Role] + "]" + player.Nickname + ":" + txt);
                    }
                for (int i = 0; i < chatList4.Count; i++)
                {
                    list.Add(chatList4[i]);
                }
            }
            if (player.Role == RoleTypeId.Tutorial)
            {
                if (chatList7.Count <= 5)
                {
                    chatList7.Add("<pos=35%>[" + keyValuePairs[player.Role] + "]" + player.Nickname + ":" + txt);
                }
                else
                {
                    chatList7.RemoveAt(0);
                    chatList7.Add("<pos=35%>[" + keyValuePairs[player.Role] + "]" + player.Nickname + ":" + txt);
                }
                for (int i = 0; i < chatList7.Count; i++)
                {
                    list.Add(chatList7[i]);
                }
            }
            if (player.Role == RoleTypeId.Spectator || player.Role == RoleTypeId.None)
            {
                if (chatList5.Count <= 5)
                {
                    chatList5.Add("<pos=35%>[" + keyValuePairs[player.Role] + "]" + player.Nickname + ":" + txt);
                }
                else
                {
                    chatList5.RemoveAt(0);
                    chatList5.Add("<pos=35%>[" + keyValuePairs[player.Role] + "]" + player.Nickname + ":" + txt);
                }
                for (int i = 0; i < chatList5.Count; i++)
                {
                    list.Add(chatList5[i]);
                }
            }
            if (player.Team == Team.SCPs)
            {
                if (chatList6.Count <= 5)
                {
                    chatList6.Add("<pos=35%>[" + keyValuePairs[player.Role] + "]" + player.Nickname + ":" + txt);
                }
                else
                    {
                        chatList6.RemoveAt(0);
                        chatList6.Add("<pos=35%>[" + keyValuePairs[player.Role] + "]" + player.Nickname + ":" + txt);
                    }
                    for (int i = 0; i < chatList6.Count; i++)
                    {
                        list.Add(chatList6[i]);
                    }
                    try
                    {
                        string txtscptmp = ""; 
                        for (int i = chatList6.Count - 3; i < chatList6.Count; i++)
                        {
                            if (i >= 0)
                            {
                                var awaawa = "";
                                awaawa = chatList6[i];
                                txtscptmp += "<size=16>"+awaawa.Replace("<pos=35%>","") + "</size>\n";
                            }
                        }
                        foreach (var variaPlayer in Player.GetPlayers())
                        {
                            if (variaPlayer.Team == Team.SCPs)
                            {
                                variaPlayer.ClearBroadcasts();
                                variaPlayer.SendBroadcast(txtscptmp,10);
                            }
                        }
                    }
                    catch 
                    {
                        Log.Info("awa");
                    }
            }
            AddChatHint(player,"<size=23><align=right>" + "<pos=35%>队伍聊天 指令.c [内容]\n" + string.Join("\n", list) + "</size>" , chatid);
            list.Clear();
        }
    }
    public static void AddChatHint(Player player,string thing,int id)
    {
        foreach (awa awa2 in awas)
        {
            var player1 = Player.GetPlayers().FirstOrDefault(b => b.PlayerId == awa2.playeridawa);
            if(player1 != null)
            {
                if((player1.Team == player.Team) || ((player.Team == Team.FoundationForces || player.Role == RoleTypeId.FacilityGuard) && (player1.Role == RoleTypeId.FacilityGuard || player1.Team == Team.FoundationForces)) )
                {
                    string temp = "";
                    foreach (string message in awa2.message)
                    {
                        if (message.Contains("聊天的Timing"))
                        {
                            temp = message;
                        }
                    }
                    if (temp != "")
                    {
                        awa2.message.Remove(temp);
                    }
                    awa2.message.Add("<size=0>聊天的Timing"+id+"</size>\n" + thing);
                    Timing.CallDelayed(10f, () => {
                        foreach (awa awa2 in awas)
                        {
                            string temp = "";
                            foreach (string message in awa2.message)
                            {
                                if (message.Contains("聊天的Timing"+id))
                                {
                                    temp = message;
                                }
                            }
                            if (temp != "")
                            {
                                awa2.message.Remove(temp);
                            }
                        }
                    });
                }
            }

        }


    }
    private IEnumerator<float> HintRun()
    {
        yield return Timing.WaitForSeconds(5f);
        int awa = 0;
        while (true)
        {
            yield return Timing.WaitForSeconds(1f);
            awa++;
            if (awa >= 3)
            {
                awa = 0;
                try
                {
                    GetSCPHP();
                }
                catch
                {
                    Log.Info("Debug2");
                }
            }
            for (int i = 0; i < awas.Count(); i++)
            {
                try
                {
                    bool chatawa = false;
                    if (awas[i].message.Count >= 1 && awas[i].wait == false)
                    {
                        StringBuilder str = new StringBuilder();
                        str.Append("<size=0>我是一般</size>");
                        foreach (var mm in awas[i].message)
                        {
                            if (mm.Contains("聊天的Timing"))
                            {
                                str.Insert(0,mm);
                                chatawa = true;
                            }
                        }
                        if (chatawa)
                        {
                            for (int j = 0; j < 5; j++)
                            {
                                str.Insert(0,"\n");
                            }
                        }
                        foreach (var mm in awas[i].message)
                        {
                            if (mm.Contains("SCP血量信息") || mm.Contains("NTF血量信息") || mm.Contains("CHI血量信息"))
                            {
                                str.Insert(0, "\n\n\n\n\n\n\n\n");
                                str.Insert(0, "<pos=60%>"+mm+"</pos>");
                            }
                            if (mm.Contains("玩家角色介绍"))
                            {
                                str.Append(mm);
                            }
                            if (mm.Contains("临时消息"))
                            {
                                str.Append(mm);
                            }
                        }
                        if(chatawa)
                        {
                            for (int j = 0; j < 15; j++)
                            {
                                str.Append("\n");
                            }
                        }
                        else
                        {
                            for (int j = 0; j < 23; j++)
                            {
                                str.Append("\n");
                            }
                        }

                        try
                        {
                            var ttttt = Player.GetPlayers().FirstOrDefault(x => x.PlayerId == awas[i].playeridawa);
                            if (ttttt != null)
                            {
                                ttttt.ReceiveHint(str.ToString(), 2);
                            }
                        }
                        catch 
                        {
                            Log.Info("debug");
                                
                        }
                        str.Clear();
                    }
                }
                catch
                {

                }
            }
        }
    }
    [PluginEvent(PluginAPI.Enums.ServerEventType.PlayerJoined)]
    void PlayerJoined(Player player)
    {
        awa tempawa = new awa();
        tempawa.playerawa = player;
        tempawa.playeridawa = player.PlayerId;
        awas.Add(tempawa);
    }
    [PluginEvent(PluginAPI.Enums.ServerEventType.RoundEnd)]
    void RoundEnd(RoundSummary.LeadingTeam leadingTeam)
    {
        chatList.Clear();
        chatList2.Clear();
        chatList3.Clear();
        chatList4.Clear();
        chatList5.Clear();
        chatList6.Clear();
        chatList7.Clear();
        foreach (awa awa2 in awas)
        {
            awa2.playerawa = null;
            awa2.playeridawa = 0;
            awa2.message.Clear();
        }
        awas.Clear();
        foreach (CoroutineHandle coroutineHandle in Coroutines)
        {
            Timing.KillCoroutines(coroutineHandle);
        }
        Coroutines.Clear();
    }
    [PluginEvent(PluginAPI.Enums.ServerEventType.PlayerLeft)]
    void PlayerLeft(Player player)
    {
        for (int i = awas.Count - 1; i >= 0; i--)
        {
            if (awas[i].playeridawa == player.PlayerId)
            {
                awas.Remove(awas[i]);
                Log.Info("玩家退出删除他的Hint" + player.PlayerId);
            }
        }
    }
}