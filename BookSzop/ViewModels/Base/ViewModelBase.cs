using BookSzop.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace BookSzop.ViewModels.Base
{
    public abstract class ViewModelBase : INotifyPropertyChanged, INotifyCollectionChanged, INotifyDataErrorInfo
    {
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs eventArgs)
        {
            PropertyChanged?.Invoke(this, eventArgs);
        }

        protected virtual void OnCollectionChange(NotifyCollectionChangedEventArgs eventArgs)
        {
            CollectionChanged?.Invoke(this, eventArgs);
        }
        #endregion

        #region Errors
        protected readonly Dictionary<string, List<string>> _errorsByPropertyName = new Dictionary<string, List<string>>();

        public bool HasErrors => _errorsByPropertyName.Any();

        public IEnumerable GetErrors(string propertyName)
        {
            return _errorsByPropertyName.ContainsKey(propertyName)
                ? _errorsByPropertyName[propertyName]
                : null;
        }

        protected void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        protected void AddError(string propertyName, string error)
        {
            if (!_errorsByPropertyName.ContainsKey(propertyName))
                _errorsByPropertyName[propertyName] = new List<string>();

            if (!_errorsByPropertyName[propertyName].Contains(error))
            {
                _errorsByPropertyName[propertyName].Add(error);
                OnErrorsChanged(propertyName);
            }
        }

        protected void ClearErrors(string propertyName)
        {
            if (_errorsByPropertyName.ContainsKey(propertyName))
            {
                _errorsByPropertyName.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }
        }
        #endregion

        #region Validators
        protected void ValidateMinLength(string propertyName, string value, int minLength)
        {
            if (string.IsNullOrWhiteSpace(value) || value?.Length < minLength)
                AddError(propertyName, $"{propertyName} must be at least {minLength} characters long.");
        }

        protected void ValidateMaxLength(string propertyName, string value, int maxLength)
        {
            if (value?.Length >= maxLength)
                AddError(propertyName, $"{propertyName} exceeded maximum {maxLength} characters length.");
        }

        protected void ValidateIsPositiveIntegerNumber(string propertyName, string value)
        {
            if (!int.TryParse(value, out int result) || result <= 0)
            {
                AddError(propertyName, $"{propertyName} must be a positive integer number.");
            }
        }

        protected void ValidateIsPositiveRealNumber(string propertyName, string value)
        {
            if (!double.TryParse(value, out double result) || result <= 0.01)
            {
                AddError(propertyName, $"{propertyName} must be a positive real number.");
            }
        }

        protected void ValidatePasswordPolicy(string propertyName, string password)
        {
            if (PasswordHelper.ValidatePassword(password, out string errorMessage) == false)
                AddError(propertyName, errorMessage);
        }

        protected void ValidatePasswordConfirmation(string propertyName, string confirmationPassword, string password)
        {
            if (confirmationPassword != password)
            {
                AddError(propertyName, "Password and its confirmation do not match");
            }
        }
        #endregion
    }
}
