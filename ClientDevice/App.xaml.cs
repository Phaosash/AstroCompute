using ClientDevice.Classes;
using ErrorLogging;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace ClientDevice;

public partial class App : Application {
    public static IServiceProvider? ServiceProvider { get; private set; }

    protected override void OnStartup(StartupEventArgs e){
        base.OnStartup(e);

        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);

        ServiceProvider = serviceCollection.BuildServiceProvider();

        var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }

    private static void ConfigureServices (IServiceCollection services){
        services.AddSingleton<MainWindow>();
        services.AddSingleton<UserInterfaceManager>();
        services.AddSingleton<LoggingManager>();

        services.AddHttpClient<IAstroContract, AstroService> (client => {
            client.BaseAddress = new Uri("https://localhost:7124/");
        });
    }
}