﻿using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Company.Product.Models.Helpers;
using Company.Product.WinApp.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Company.Product.WinApp
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static ILogger _logger;
        private readonly IHost _host;
        private string _basePath;
        private AppViewModel _viewModel;
        private MainWindow _window;

        public App()
        {
            _host = Host.CreateDefaultBuilder() // Use default settings
                //new HostBuilder()          // Initialize an empty HostBuilder
                .ConfigureAppConfiguration((context, builder) =>
                {
                    _basePath = Environment.CurrentDirectory;
                    builder.SetBasePath(_basePath);
                    // Add other configuration files...
                    // builder.AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true);
                }).ConfigureServices((context, services) => { ConfigureServices(context.Configuration, services); })
                .ConfigureLogging(logging =>
                {
                    logging.AddConsole();
                    logging.AddDebug();
                    // logging.AddProvider(new OutputLoggerProvider(Output.LogEvent));
                    // Add other loggers...
                })
                .Build();

            var _ = new LogManager(_host.Services);
            _logger = LogManager.GetLogger<App>();
            _logger.LogDebug("Test");

            DispatcherUnhandledException += OnDispatcherUnhandledException;
        }

        private void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));
            // services.AddScoped<ISampleService, SampleService>();
            services.AddSingleton<MainWindow>();
            services.AddSingleton<AppViewModel>();
            services.Configure<LoggerFilterOptions>(o => o.MinLevel = LogLevel.Debug);
        }

        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            _logger.LogError(e.ToString());
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            _logger.LogDebug("OnStartup()");
            // Start Host
            await _host.StartAsync();

            // Get app settings

            // MainWindow
            _window = _host.Services.GetRequiredService<MainWindow>();
            _window.LoadSettings();

            // Created ViewModel
            _viewModel = _host.Services.GetRequiredService<AppViewModel>();
            _viewModel.LoadSettings();

            // ToDo: Open Database
            _viewModel.Init();

            // Set MainWindow DataContext
            _window.DataContext = _viewModel;

            // 
            _window.Closing += async (sender, args) => await WindowOnClosing(sender, args);

            // Show 
            _window.Show();
            base.OnStartup(e);
        }

        private async Task WindowOnClosing(object sender, CancelEventArgs e)
        {
            await SaveSettingsAsync();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (_host)
            {
                await _host.StopAsync(TimeSpan.FromSeconds(5));
            }

            base.OnExit(e);
        }

        private async Task SaveSettingsAsync()
        {
            _window.SaveSettings();
            _viewModel.SaveSettings();

            await _viewModel.SaveAppSettings(_basePath);
        }
    }
}