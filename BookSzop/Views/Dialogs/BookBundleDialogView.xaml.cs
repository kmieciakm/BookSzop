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
    /// Interaction logic for BookBundleDailogView.xaml
    /// </summary>
    public partial class BookBundleDialogView : Window
    {
        public BookBundleDialogView(BookBundleDialogViewModel bookBundleViewModel)
        {
            InitializeComponent();
            DataContext = bookBundleViewModel;
        }
    }
}
