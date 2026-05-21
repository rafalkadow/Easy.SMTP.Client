using Easy.SMTP.Client.Core;
using Easy.SMTP.Client.Models;
using NLog;

namespace Easy.SMTP.Client.ViewModels.Controls
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
