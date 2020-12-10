using System;
using System.Collections.Generic;
using System.Text;

namespace BookSzop.ViewModels.Base
{
    public class DailogViewModelBase : ViewModelBase
    {
        public Action OnSave;
        public Action OnClose;
    }
}
