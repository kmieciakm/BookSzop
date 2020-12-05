using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace BookSzop.Commands
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        private readonly Action<object> _action;
        private readonly Predicate<object> _condition;

        public RelayCommand(Action<object> action) : this(null, action) { }

        public RelayCommand(Predicate<object> condition, Action<object> action)
        {
            _condition = condition;
            _action = action;
        }

        public bool CanExecute(object parameter)
        {
            return _condition == null ? true : _condition.Invoke(parameter);
        }

        public void Execute(object parameter)
        {
            _action?.Invoke(parameter);
        }
    }
}
