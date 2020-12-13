using BookSzop.Commands;
using BookSzop.Models;
using BookSzop.ViewModels.Base;
using ShopService.Models.BookModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace BookSzop.ViewModels.Dialogs
{
    public class BookDialogViewModel : DialogViewModelBase
    {
        private Book _bookToSave { get; set; }

        public BookDialogViewModel(Book book)
        {
            _bookToSave = book;
            _author = book.Author;
            _title = book.Title;
        }

        private string _author;
        public string Author
        {
            get => _author;
            set
            {
                _author = value;
                ClearErrors(nameof(Author));
                ValidateMinLength(nameof(Author), _author, 5);
                ValidateMaxLength(nameof(Author), _author, 40);
                OnPropertyChanged(nameof(Author));
                OnPropertyChanged(nameof(SaveButtonEnable));
            }
        }
        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                ClearErrors(nameof(Title));
                ValidateMinLength(nameof(Title), _title, 2);
                ValidateMaxLength(nameof(Title), _title, 50);
                OnPropertyChanged(nameof(Title));
                OnPropertyChanged(nameof(SaveButtonEnable));
            }
        }
        public bool SaveButtonEnable
        {
            get => IsFormCorrect();
        }

        public override ICommand Save
        {
            get => new RelayCommand(param =>
            {
                _bookToSave.Author = Author;
                _bookToSave.Title = Title;

                OnSave?.Invoke();
                OnClose?.Invoke();
            });
        }
        public override ICommand Cancel
        {
            get => new RelayCommand(param =>
            {
                OnClose?.Invoke();
            });
        }

        public bool IsFormCorrect()
        {
            return
                !(
                    HasErrors ||
                    string.IsNullOrEmpty(Author) ||
                    string.IsNullOrEmpty(Title)
                );
        }
    }
}
