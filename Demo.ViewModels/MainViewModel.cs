using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Demo.Models;
using Demo.Models.Helpers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Extensions.Logging;

namespace Demo.ViewModels
{
    public class MainViewModel : ViewModelBase
    {

        private static readonly ILogger Logger = LogManager.GetLogger<MainViewModel>();

        public ICommand LoadDataCommand { get; }
        public ICommand DownloadCommand { get; }

        public MainViewModel()
        {
            LoadDataCommand = new RelayCommand(LoadData);
            DownloadCommand = new RelayCommand(DownloadFiles);
        }

        private void DownloadFiles()
        {
        }

        public void LoadData()
        {
        }

        public void Init()
        {
        }
    }
}
