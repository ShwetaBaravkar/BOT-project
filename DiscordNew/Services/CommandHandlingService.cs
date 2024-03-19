using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using System.Windows.Input;
using DiscordNew.Models;
using Microsoft.Extensions.Options;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace DiscordNew.Services
{
    public class CommandHandlingService
    {
        private readonly DiscordSocketClient _discord;
        private readonly IServiceProvider _serviceProvider;
        private readonly CommandService _command;

        private readonly ApplicationSettings _appSettings;
        public CommandHandlingService(IServiceProvider services, IOptions<ApplicationSettings> appSettings)
        //public CommandHandlingService(IServiceProvider serviceProvider )
        {
            _appSettings = appSettings.Value;
            _command = services.GetRequiredService<CommandService>();
            _discord = services.GetRequiredService<DiscordSocketClient>();
            _serviceProvider = services;

            // Hook CommandExecuted to handle post-command-execution logic.
            _command.CommandExecuted += CommandExecutedAsync;

            // Hook MessageReceived so we can process each message to see if it qualifies as a command.
            _discord.MessageReceived += MessageReceivedAsync;
        }

        private async Task MessageReceivedAsync(SocketMessage rawMessage)
        {
            // Ignore system messages, or messages from other bots
            if (rawMessage is not SocketUserMessage { Source: MessageSource.User } message)
                return;

            var argPos = 0;

            // Perform prefix check.
            // This will pick the prefix from the environment variable or fallback to '!' if none is provided
            // By default, the bot will accept being mentioned to activate a command
            
            //To check the prefix message.HasStringPrefix(_appSettings.COMMAND_PREFIX ?? "!", ref argPos);
            //To check if message starts with a mention of a specific user.
            
            if (!message.HasStringPrefix(_appSettings.COMMAND_PREFIX ?? "!", ref argPos)
                )
                return;

            // Create the websocket context
            var context = new SocketCommandContext(_discord, message);

            // Perform the execution of the command. In this method, the command service will perform precondition
            // and parsing check, then execute the command if one is matched.
            // Note that normally a result will be returned by this format,
            // but here we will handle the result in CommandExecutedAsync.
            await _command.ExecuteAsync(context, argPos, _serviceProvider);

        }

        private async Task CommandExecutedAsync(Optional<CommandInfo> optional, ICommandContext context, IResult result)
        {

            // command is unspecified when there was a search failure (command not found);and return
            if (!optional.IsSpecified)
                return;
            // the command was successful , you can log the result that a command succeeded.
            if (result.IsSuccess)
                return;

            // the command failed, user notified about error .
            await context.Channel.SendMessageAsync($"error : '{result}'");
        }

        public async Task InitializeAsync()
        {
            // Register modules that are public and inherit ModuleBase<T>.
            //await _commandService.AddModulesAsync(Assembly.GetEntryAssembly(), _serviceProvider);
            await _command.AddModulesAsync(Assembly.GetEntryAssembly(), _serviceProvider);
        }
    }
}
