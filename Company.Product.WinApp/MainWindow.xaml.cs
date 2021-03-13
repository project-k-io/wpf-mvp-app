using System.Windows;
using Company.Product.Models.Helpers;
using Company.Product.Views.Helpers;
using Company.Product.WinApp.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Company.Product.WinApp
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Static

        private readonly ILogger _logger = LogManager.GetLogger<MainWindow>();

        #endregion

        #region Fields

        private readonly AppSettings _settings;

        #endregion


        public MainWindow(IOptions<AppSettings> settings)
        {
            InitializeComponent();
            _settings = settings.Value;
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            _logger.LogDebug("Loaded()");
            if (!(DataContext is AppViewModel model)) return;


            model.OnDispatcher = ViewLib.GetAddDelegate(this);
            model.LoadData();
        }

        public void LoadSettings()
        {
            var settings = _settings.Window;
            settings.SizeToFit();
            settings.MoveIntoView();

            Top = settings.Top;
            Left = settings.Left;
            Width = settings.Width;
            Height = settings.Height;
            WindowState = settings.WindowState;
        }

        public void SaveSettings()
        {
            var settings = _settings.Window;

            if (WindowState != WindowState.Minimized)
            {
                settings.Top = Top;
                settings.Left = Left;
                settings.Height = Height;
                settings.Width = Width;
                settings.WindowState = WindowState;
            }
        }
    }
}