using BookSzop.ViewModels.Dialogs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookSzop.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for BookDialog.xaml
    /// </summary>
    public partial class BookDialogView : Window
    {
        public BookDialogView(BookDialogViewModel bookDialogViewModel) : base ()
        {
            InitializeComponent();
            DataContext = bookDialogViewModel;
        }
    }
}
