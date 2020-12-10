using System;
using System.Collections.Generic;
using System.Text;

namespace BookSzop.Dailogs
{
    public interface IDialog<T> where T : class
    {
        void Show(T data, Action onConfirm);
        void Close();
    }
}
