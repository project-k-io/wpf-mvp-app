using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Company.Product.Models.Helpers;
using Company.Product.ViewModels;
using Company.Product.WinApp.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Company.Product.WinApp
{
    public class AppViewModel : MainViewModel
    {
        #region Fields

        private readonly AppSettings _settings;

        #endregion

        #region Static

        protected ILogger Logger;

        #endregion

        public AppViewModel() 
        {
            Title = "Company Product WinApp";
        }


        public AppViewModel(IOptions<AppSettings> settings)
        {
            _settings = settings.Value;
            Assembly = Assembly.GetExecutingAssembly();
            Logger = LogManager.GetLogger<AppViewModel>();
            Logger.LogDebug("Import Logging()");

            Title = $"Company Product WinApp ver {Ver}";
        }

        public void SaveSettings()
        {
            Logger.LogDebug("LoadSettings");
        }

        public void LoadSettings()
        {
            Logger.LogDebug("SaveSettings()");
        }

        public async Task SaveAppSettings(string directory)
        {
            var path = Path.Combine(directory, "appsettings.json");
            var root = new
            {
                AppSettings = _settings
            };
            await FileHelper.SaveToFileAsync(path, root);
        }

        #region Properties

        public Assembly Assembly { get; set; }

        public Action<Action> OnDispatcher { get; set; }

        public string Title { get; set; }
        public string Ver { get; set; } = "1.0";



        #endregion
    }
}