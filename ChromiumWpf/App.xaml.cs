using CefSharp;
using CefSharp.Wpf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ChromiumWpf
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        string cef_root = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CEF");
        public App()
        {
            var path = Environment.GetEnvironmentVariable("PATH");
            Environment.SetEnvironmentVariable("PATH", path + ";" + cef_root);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var settings = new CefSettings()
            {
                BrowserSubprocessPath = $@"{cef_root}\CefSharp.BrowserSubprocess.exe",
                UserDataPath = $@"{cef_root}\userdata",
                ResourcesDirPath = cef_root,
                LocalesDirPath = $@"{cef_root}",

                Locale = "zh-CN",
                CachePath = $@"{cef_root}\cache",
            };
            CefSharpSettings.LegacyJavascriptBindingEnabled = true;
            Cef.Initialize(settings, performDependencyCheck: false, browserProcessHandler: null);
        }
    }
}
