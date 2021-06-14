using Easy.SMTP.Client.ViewModels;
using Easy.SMTP.Client.Views;
using Easy.SMTP.Enums;
using NLog;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;

namespace Easy.SMTP.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        private ApplicationClosingStatusEnum ApplicationClosingStatus = ApplicationClosingStatusEnum.Begin;

        public App()
        {
            logger.Info($"App()");
            DispatcherUnhandledException += HandleException;
        }

        private void HandleException(object sender, DispatcherUnhandledExceptionEventArgs args)
        {
            var msg = args.Exception.Message + @", Inner message:" + (args.Exception.InnerException != null
                ? args.Exception.InnerException.Message
                : @"null");

            Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (Action)(() =>
            {
                MessageBox.Show(msg, @"Easy.SMTP,UNCACHED EXCEPTION");
            }));

            logger.Fatal(args.Exception, @"UNCACHED EXCEPTION");

            args.Handled = true;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainWindow = new MainWindow();
            ((MainWindow)MainWindow).AskApplicationClose += App_AskCloseApp;
            MainWindow.Closing += MainWindow_Closing;
            MainWindow.Show();
        }

        public void App_AskCloseApp(bool operation)
        {
            if (operation)
            {
                ApplicationClosingStatus = ApplicationClosingStatusEnum.Exit;
                MainWindow.Close();
            }
            else
            {
                ApplicationClosingStatus = ApplicationClosingStatusEnum.Begin;
            }
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            MainViewModel mainViewModelObject = ((MainViewModel)MainWindow.DataContext);

            if (ApplicationClosingStatus.Equals(ApplicationClosingStatusEnum.Begin))
            {
                e.Cancel = true;
                mainViewModelObject.CloseApp();
            }
        }
    }
}
