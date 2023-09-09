using System;
using System.Linq;
using CommandSystem;
using MEC;
using PluginAPI.Core;
using RemoteAdmin;
using YYYServerLightPlugin.Events.Fuction;

namespace YYYServerLightPlugin.Commands;

[CommandHandler(typeof(ClientCommandHandler))]
public class AdminChat: ICommand
{
    public static bool chatstart = false;
    public string Command { get; } = "adminchat";
    public string[] Aliases { get; } = new string[] {"ac" };
    public string Description { get; } = "向在线管理求助";
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (sender is PlayerCommandSender plr)
        {
            var player = Player.GetPlayers().FirstOrDefault(x => x.PlayerId == plr.PlayerId);
            if(Player.GetPlayers().Count(x=> x.RemoteAdminAccess) >= 1)
            {
                string msg = "<color=#FF0000>有玩家[" + player.Nickname + "]向管理员求助</color>\n" + string.Join(" ",arguments);
                foreach (var variPlayer in (Player.GetPlayers().Where(x=>x.RemoteAdminAccess)))
                {
                    variPlayer.SendBroadcast(msg,10);
                }

                response = "发送成功";
                return true;
            }
            else
            {
                response = "发送失败，当前服务器内没有管理员";
                return false;
            }
        }
        response = "未知错误";
        return false;
    }
}