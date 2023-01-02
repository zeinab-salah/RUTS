using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using RUTS.ViewModel;

namespace RUTS.View.Currencies
{
    /// <summary>
    /// Interaction logic for Create.xaml
    /// </summary>
    public partial class Create : Window
    {
        public Create()
        {
            this.Height = RUTS.ViewModel.ViewModelBase.Height;
            this.Width = RUTS.ViewModel.ViewModelBase.Width;
            InitializeComponent();
        }

    }
}
