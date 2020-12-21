using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Demo.Models.Helpers;
using GalaSoft.MvvmLight;
using Microsoft.Extensions.Logging;

namespace Demo.ViewModels.Extensions
{
    public static class ViewModelExtensions
    {
        private static readonly ILogger Logger = LogManager.GetLogger<MainViewModel>();

        public static bool Set<T>(this ViewModelBase m, T oldValue, Action<T> setValue, T newValue, bool broadcast = false, [CallerMemberName] string propertyName = null)
        {
            try
            {
                if (EqualityComparer<T>.Default.Equals(oldValue, newValue))
                    return false;

                setValue(newValue);

                m.RaisePropertyChanged(propertyName, oldValue, newValue, broadcast);
                return true;
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message);
                return false;
            }
        }
    }
}
