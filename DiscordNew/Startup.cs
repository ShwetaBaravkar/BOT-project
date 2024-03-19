using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordNew.Models;
using DiscordNew.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DiscordNew
{
    public class Startup
    {
        //private DiscordSocketClient _client;
        

        //public async Task Initialize(IConfiguration configuration) {

        //    await using var services = ConfigureServices();
        //    _client = services.GetRequiredService<DiscordSocketClient>();

        //    _client.Ready += OnReady;
        //    _client.Log += LogAsync;
        //    services.GetRequiredService<CommandService>().Log += LogAsync;

        //    // Tokens should be considered secret data and never hard-coded.
        //    // We can read from the environment variable to avoid hardcoding.
        //    await _client.LoginAsync(TokenType.Bot, configuration["Token"]);
        //    await _client.StartAsync();

        //    //services.Configure<ApplicationSettings>(configuration.GetSection("ApplicationSettings"));


        //    // Here we initialize the logic required to register our commands.
        //    await services.GetRequiredService<CommandHandlingService>().InitializeAsync();

        //    await Task.Delay(Timeout.Infinite);
        //}

        //private Task LogAsync(LogMessage message)
        //{
        //    Console.WriteLine(message.ToString());
        //    return Task.CompletedTask;
        //}

        //private Task OnReady()
        //{
        //    // Logs the bot name and all the servers that it's connected to
        //    Console.WriteLine($"Connected to server : '{_client.CurrentUser.Username}'");

        //    foreach(var guild in _client.Guilds)
        //    {
        //        Console.WriteLine($"- {guild.Name}");

        //        // Set the activity from the environment variable or fallback to 'I'm alive!'
        //        _client.SetGameAsync(Environment.GetEnvironmentVariable("DISCORD_BOT_ACTIVITY") ?? "I'm alive!",
        //            type: ActivityType.CustomStatus);

        //        Console.WriteLine($"Activity : '{_client.Activity.Name}'");
        //    }
        //    return Task.CompletedTask;
        //}

        //private static ServiceProvider ConfigureServices(IConfiguration configuration)
        //{
        //    services.Configure<ApplicationSettings>(configuration.GetSection("ApplicationSettings"));

        //    return new ServiceCollection()
        //        .AddSingleton<DiscordSocketClient>()
        //        .AddSingleton<CommandService>()
        //        .AddSingleton<CommandHandlingService>()
        //        .BuildServiceProvider()
        //}

        //public void ConfigureServices(IServiceCollection services)
        //{
        //    // 1. Read configuration from appsettings.json
        //    var configuration = new ConfigurationBuilder()
        //        .SetBasePath(Directory.GetCurrentDirectory())
        //        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        //        .Build();

        //    // 2. Create a new options instance
        //    services.Configure<ApplicationSettings>(configuration.GetSection("ApplicationSettings"));

        //    // 3. Register other services
        //    services.AddSingleton<DiscordSocketClient>();
        //    services.AddSingleton<CommandService>();
        //    services.AddSingleton<CommandHandlingService>();
        //}
    }
}
