using System;

namespace Company.Product.WinApp.Settings
{
    public class AppSettings
    {
        public WindowSettings Window { get; set; } = new WindowSettings();
        public LayoutSettings Layout { get; set; } = new LayoutSettings();
        public string RecentFile { get; set; }
        public Guid LastListTaskId { get; set; }
        public Guid LastTreeTaskId { get; set; }
    }
}