using Easy.SMTP.Client.ViewModels;
using Easy.SMTP.Core;
using Easy.SMTP.Models;
using NLog;

namespace Easy.SMTP.ViewModels.Controls
{
    public class MessageObjectModel : PropertyChangedBase
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        SettingsModel _settingsModelObject = null;

        private MainViewModel _mainViewModel;
        public MessageObjectModel(MainViewModel mainViewModel)
        {
            logger.Info($"MessageViewModel(mainViewModel={mainViewModel.ToString()})");
            _mainViewModel = mainViewModel;
            _settingsModelObject = mainViewModel.SettingsViewModelElement.SettingsModelObject;
        }


        public SettingsModel SettingsModelObject
        {
            get
            {
                return _settingsModelObject;
            }
            set
            {
                if (_settingsModelObject != value)
                {
                    _settingsModelObject = value;
                    NotifyOfPropertyChange(() => SettingsModelObject);
                }
            }
        }
    }
}
