using System;
using System.Linq;
using CommandSystem;
using PluginAPI.Core;
using RemoteAdmin;
using YYYServerLightPlugin.API;

namespace YYYServerLightPlugin.Commands.AdminCommand;

[CommandHandler(typeof(RemoteAdminCommandHandler))]
public class debug: ICommand
{
    public static bool chatstart = false;
    public string Command { get; } = "debug";
    public string[] Aliases { get; } = new string[] {"debug" };
    public string Description { get; } = "debug";
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (sender is PlayerCommandSender plr)
        {
            var player = Player.GetPlayers().FirstOrDefault(x => x.PlayerId == plr.PlayerId);
            switch (arguments.At(0))
            {
                case "1" :
                    player.ReloadWeapen();
                    break;
            }
        }
        response = "未知错误";
        return false;

    }
}