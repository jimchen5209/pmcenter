﻿using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using static pmcenter.Methods;

namespace pmcenter.Commands
{
    internal class BanIdCommand : ICommand
    {
        public bool OwnerOnly => true;

        public string Prefix => "banid";

        public async Task<bool> ExecuteAsync(TelegramBotClient botClient, Update update)
        {
            try
            {
                BanUser(long.Parse(update.Message.Text.ToLower().Split(" ")[1]));
                await Conf.SaveConf(false, true);
                await botClient.SendTextMessageAsync(
                    update.Message.From.Id,
                    Vars.CurrentLang.Message_UserBanned,
                    ParseMode.Markdown,
                    false,
                    Vars.CurrentConf.DisableNotifications,
                    update.Message.MessageId);
            }
            catch (Exception ex)
            {
                await botClient.SendTextMessageAsync(
                    update.Message.From.Id,
                    Vars.CurrentLang.Message_GeneralFailure.Replace("$1", ex.Message),
                    ParseMode.Markdown,
                    false,
                    Vars.CurrentConf.DisableNotifications,
                    update.Message.MessageId);
            }
            return true;
        }
    }
}
