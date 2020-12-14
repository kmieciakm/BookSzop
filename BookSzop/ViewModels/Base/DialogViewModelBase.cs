using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace BookSzop.ViewModels.Base
{
    public abstract class DialogViewModelBase : ViewModelBase
    {
        public Action OnSave;
        public Action OnClose;

        public abstract ICommand Save { get; }
        public abstract ICommand Cancel { get; }
    }
}
