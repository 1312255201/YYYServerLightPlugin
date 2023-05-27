using System;
using System.Linq;
using CommandSystem;
using MEC;
using PluginAPI.Core;
using RemoteAdmin;
using YYYServerLightPlugin.Events.Fuction;

namespace YYYServerLightPlugin.Commands;

[CommandHandler(typeof(ClientCommandHandler))]
public class ChatCommand: ICommand
{
    public static bool chatstart = false;
    public string Command { get; } = "teamchat";
    public string[] Aliases { get; } = new string[] {"c" };
    public string Description { get; } = "队伍聊天";
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (sender is PlayerCommandSender plr)
        {
            if(chatstart)
            {
                var player = Player.GetPlayers().FirstOrDefault(x => x.PlayerId == plr.PlayerId);
                if (arguments.Count != 0)
                {
                    if (player != default)
                    {
                        if (player.IsIntercomMuted == false)
                        {
                            Timing.RunCoroutine(ShowTeamInFormation.chatTiming(player, string.Join(" ", arguments)));
                            response = "发送成功";
                            return true;
                        }
                        else
                        {
                            response = "你被禁言了联系管理解除禁止";
                            return false;
                        }
                    }
                }
                else
                {
                    response = "请输入内容";
                    return false;
                }
            }
            else
            {
                response = "聊天未启动";
                return false;
            }
        }
        response = "未知错误";
        return false;

    }
}