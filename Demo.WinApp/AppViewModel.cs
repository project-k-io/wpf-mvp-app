using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Demo.Models.Helpers;
using Demo.ViewModels;
using Demo.WinApp.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Demo.WinApp
{
    public class AppViewModel : MainViewModel
    {
        #region Fields

        private readonly AppSettings _settings;

        #endregion

        #region Static

        protected ILogger Logger;

        #endregion

        public AppViewModel(IOptions<AppSettings> settings)
        {
            _settings = settings.Value;
            Assembly = Assembly.GetExecutingAssembly();
            Logger = LogManager.GetLogger<AppViewModel>();
            Logger.LogDebug("Import Logging()");
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

        #endregion
    }
}