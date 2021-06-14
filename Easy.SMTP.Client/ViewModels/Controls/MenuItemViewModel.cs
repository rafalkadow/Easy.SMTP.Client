using Easy.SMTP.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy.SMTP.Client.ViewModels.Controls
{
    /// <summary>
    /// Implement the IHamburgerMenuItemBase to allow set the Visibility of the item itself.
    /// </summary>
    public class MenuItemViewModel : PropertyChangedBase
    {
        private object _icon;
        private object _label;
        private object _toolTip;
        private bool _isVisible = true;

        public MenuItemViewModel(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;
        }

        public MainViewModel MainViewModel { get; }

        public object Icon
        {
            get => _icon;
            set => SetProperty(ref _icon, value);
        }

        public object Label
        {
            get => _label;
            set => SetProperty(ref _label, value);
        }

        public object ToolTip
        {
            get => _toolTip;
            set => SetProperty(ref _toolTip, value);
        }

        public bool IsVisible
        {
            get => _isVisible;
            set => SetProperty(ref _isVisible, value);
        }
    }
}
