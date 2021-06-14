using Easy.SMTP.Client.ViewModels;
using Easy.SMTP.Models;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using NLog;
using System;
using System.Windows;

namespace Easy.SMTP.Client.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        public event Action<bool> AskApplicationClose;
        public MainWindow()
        {
            logger.Info($"MainWindow()");
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            this.Unloaded += (sender, args) => { DialogParticipation.SetRegister(this, null); };
        }


        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var mainViewModel = new MainViewModel(this);
            DataContext = mainViewModel;
            DialogParticipation.SetRegister(this, this);
            mainViewModel.CloseApplication -= AskCloseApplication;
            mainViewModel.CloseApplication += AskCloseApplication;

            mainViewModel.ShowMessageApplication += MainViewModel_ShowMessageApplication;
        }

        private void MainViewModel_ShowMessageApplication(ActionMessage actionMessage)
        {
            this.ShowMessageAsync(actionMessage.Title, actionMessage.Text, MessageDialogStyle.Affirmative, new MetroDialogSettings()
            {
                ColorScheme = MetroDialogColorScheme.Inverted
            });
        }

        #region AskCloseApplication
        public async void AskCloseApplication()
        {
            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Yes",
                NegativeButtonText = "No",
                MaximumBodyHeight = 100,
                ColorScheme = MetroDialogColorScheme.Accented
            };

            var result = await this.ShowMessageAsync("Easy.SMTP.Client", "Do you want to close application ?", MessageDialogStyle.AffirmativeAndNegative, mySettings);

            if (result == MessageDialogResult.Affirmative)
            {
                AskApplicationClose?.Invoke(true);
            }
            else
            {
                AskApplicationClose?.Invoke(false);
            }
        }
        #endregion

    }
}
