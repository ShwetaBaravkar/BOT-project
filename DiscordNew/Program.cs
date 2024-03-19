using Discord.Commands;
using Discord.WebSocket;
using Discord;
using DiscordNew.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DiscordNew.Models;

namespace DiscordNew;

public static class Program
{
    //private static void Main()
    //{

    //    var configuration = new ConfigurationBuilder()
    //    .SetBasePath(Directory.GetCurrentDirectory())
    //    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    //    .Build();


    //    new Startup().Initialize(configuration).GetAwaiter().GetResult();
    //}
    private static DiscordSocketClient _client;
    private static CommandService _commands;
    private static CommandHandlingService _commandHandling; // Assuming this class handles commands

    public static async Task Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var services = new ServiceCollection();

        services.AddSingleton<DiscordSocketClient>();
        services.AddSingleton<CommandService>();      

        //services.Configure<ApplicationSettings>(configuration =>
        //{
        //   // configuration.Bind(configuration.GetSection("ApplicationSettings"));
        //    configuration.GetSection("ApplicationSettings").Bind(applicationSettings);// Bind settings from appsettings.json
        //});

        services.Configure<ApplicationSettings>(options =>
        {
            configuration.GetSection("ApplicationSettings").Bind(options);
        });

        //var applicationSettings = new ApplicationSettings();
        //configuration.GetSection("ApplicationSettings").Bind(applicationSettings);

        services.AddSingleton<CommandHandlingService>();

        var provider = services.BuildServiceProvider();

        _client = provider.GetRequiredService<DiscordSocketClient>();
        _commands = provider.GetRequiredService<CommandService>();
        _commandHandling = provider.GetRequiredService<CommandHandlingService>(); // Assuming this class handles commands

        _client.Ready += OnReady;
        _client.Log += LogAsync;
        _commands.Log += LogAsync;

        await _client.LoginAsync(TokenType.Bot, configuration["ApplicationSettings:Token"]); // Replace with your actual bot token
        await _client.StartAsync();

        await _commandHandling.InitializeAsync();


        await Task.Delay(Timeout.Infinite);


    }


    private static Task LogAsync(LogMessage log)
    {
        Console.WriteLine(log.ToString());

        return Task.CompletedTask;
    }
    private static Task OnReady()
    {
        //Logs the bot name and all the servers that it's connected to
        Console.WriteLine($"Connected to these servers as '{_client.CurrentUser.Username}': ");
        foreach (var guild in _client.Guilds)
            Console.WriteLine($"- {guild.Name}");

        // Set the activity from the environment variable or fallback to 'I'm alive!'
        _client.SetGameAsync(Environment.GetEnvironmentVariable("DISCORD_BOT_ACTIVITY") ?? "I'm alive!",
            type: ActivityType.CustomStatus);
        Console.WriteLine($"Activity set to '{_client.Activity.Name}'");

        return Task.CompletedTask;
    }
}


