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
        private List<CoroutineHandle> Coroutines = new();
        private string ntfteaminfo;
        private string chiteaminfo;
        public List<awa> awas = new();
        public Dictionary<RoleTypeId, string> keyValuePairs = new()
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
        private void GetSCPHP()
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
            catch (Exception e)
            {
                Log.Debug(e.ToString());
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
                Log.Debug("报错位置1");
                foreach (Player player in Player.GetPlayers())
                {
                    player.SendConsoleMessage("报错位置1", "yellow");
                }
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
                Log.Debug("报错位置3");
                foreach (Player player in Player.GetPlayers())
                {
                    player.SendConsoleMessage("报错位置3", "yellow");
                }
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
                Log.Debug("报错位置4");
                foreach (Player player in Player.GetPlayers())
                {
                    player.SendConsoleMessage("报错位置4", "yellow");
                }
            }
            ntfteaminfo = "";
            chiteaminfo = "";
        }
        private IEnumerator<float> HintRun()
        {
            yield return Timing.WaitForSeconds(5f);
            int awa = 0;
            while (true)
            {
                yield return Timing.WaitForSeconds(1f);
                awa++;
                if (awa >= 5)
                {
                    awa = 0;
                    GetSCPHP();
                }
                for (int i = 0; i < awas.Count(); i++)
                {
                    try
                    {
                        if (awas[i].message.Count >= 1 && awas[i].wait == false)
                        {
                            StringBuilder str = new StringBuilder();
                            str.Append("<size=0>我是一般</size>");
                            foreach (var mm in awas[i].message)
                            {
                                if (mm.Contains("SCP血量信息") || mm.Contains("NTF血量信息") || mm.Contains("CHI血量信息"))
                                {
                                    str.Insert(0, "\n\n\n\n\n\n\n\n");
                                    str.Insert(0, "<pos=60%>"+mm+"</pos>");
                                }
                                if (mm.Contains("聊天的Timing"))
                                {
                                    str.Insert(0, mm);
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
                            for(int j = 0; j < 23; j++)
                            {
                                str.Append("\n");
                            }
                            if(Player.Get(awas[i].playeridawa) != null)
                            {
                                Player.Get(awas[i].playeridawa).ReceiveHint(str.ToString(), 2);
                            }
                            str.Clear();
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