using CefSharp;
using CefSharp.Structs;
using CefSharp.Wpf;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace ChromiumWpf.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {

        }


        private CefSharp.Wpf.ChromiumWebBrowser _WebBrowser;

        public CefSharp.Wpf.ChromiumWebBrowser WebBrowser
        {
            get
            {
                if (_WebBrowser == null)
                {
                    _WebBrowser = new CefSharp.Wpf.ChromiumWebBrowser();
                    _WebBrowser.Loaded += _WebBrowser_Loaded;
                    _WebBrowser.StatusMessage += _WebBrowser_StatusMessage;
                    _WebBrowser.JsDialogHandler = new Develop.Extension.Wpf.WebView.CustomJSDialog();
                }
                return _WebBrowser;
            }
        }

        private void _WebBrowser_Unloaded(object sender, RoutedEventArgs e)
        {
            System.Console.WriteLine("_WebBrowser_Unloaded");
        }

        private void _WebBrowser_StatusMessage(object sender, StatusMessageEventArgs e)
        {
            System.Console.WriteLine("----------------"+e.Value+"========"+e.Browser.IsDisposed);
        }

        public RelayCommand BlankCommand => new RelayCommand(() =>
        {
            Task.Run(() => {
                Task.Delay(1000 * 10).Wait() ;
                System.Console.WriteLine("DISPOSE");
                Application.Current.Dispatcher.Invoke(() => {
                    _WebBrowser.Dispose();
                });
                
            });
        });

        private void _WebBrowser_Loaded(object sender, RoutedEventArgs e)
        {

            _WebBrowser.Load(@"C:\Users\Jch\Desktop\t.html");
        }
    }

    public class JSDialogHanlder : IJsDialogHandler
    {
        public bool OnBeforeUnloadDialog(IWebBrowser chromiumWebBrowser, IBrowser browser, string messageText, bool isReload, IJsDialogCallback callback)
        {
            return false;
        }

        public void OnDialogClosed(IWebBrowser chromiumWebBrowser, IBrowser browser)
        {
            
        }

        public bool OnJSDialog(IWebBrowser chromiumWebBrowser, IBrowser browser, string originUrl, CefJsDialogType dialogType, string messageText, string defaultPromptText, IJsDialogCallback callback, ref bool suppressMessage)
        {
            return false;
        }

        public void OnResetDialogState(IWebBrowser chromiumWebBrowser, IBrowser browser)
        {
            
        }
    }
}