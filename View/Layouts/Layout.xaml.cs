using Microsoft.Extensions.DependencyInjection;
using RUTS.Data;
using RUTS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Application = System.Windows.Application;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using UserControl = System.Windows.Controls.UserControl;

namespace RUTS.View.Layouts
{
    public partial class Layout : UserControl
    {
        private Window window;
        private readonly RUTSDesignTimeDbContextFactory _dbcontext;

        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string), typeof(Layout), new PropertyMetadata(string.Empty));

        public string HeaderIcon
        {
            get { return (string)GetValue(HeaderIconProperty); }
            set { SetValue(HeaderIconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderIcon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderIconProperty =
            DependencyProperty.Register("HeaderIcon", typeof(string), typeof(Layout), new PropertyMetadata(string.Empty));

        public Layout()
        {
            InitializeComponent();
        }

        public Layout(RUTSDesignTimeDbContextFactory dbcontext)
        {
            InitializeComponent();
            _dbcontext = dbcontext;
        }

        [DllImport("User32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void pnlControlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            window = Window.GetWindow(this);
            WindowInteropHelper helper = new WindowInteropHelper(window);
            SendMessage(helper.Handle, 161, 2, 0);
        }

        private void pnlControlBar_MouseEnter(object sender, MouseEventArgs e)
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            window = Window.GetWindow(this);
            window.WindowState = WindowState.Minimized;
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            window = Window.GetWindow(this);
            if (window.WindowState == WindowState.Normal)
            {
                window.WindowState = WindowState.Maximized;
                window.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
                window.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
                root.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
                root.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
                ViewModelBase.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
                ViewModelBase.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
            }
            else
            {
                window.WindowState = WindowState.Normal;
                window.Height = 700;
                window.Width = 1300;
                root.Height = 700;
                root.Width = 1300;
                ViewModelBase.Width = 1300;
                ViewModelBase.Height = 700;
            }
        }
        private void GoToDashboard(object sender, RoutedEventArgs e)
        {
            if(ViewModelBase.CurrentPage == "Home")
            {
                return;
            }
            ViewModelBase.CurrentPage = "Home";
            window      = Window.GetWindow(this);
            Window home = App.AppHost.Services.GetRequiredService<Home>();
            home.Show();
            window.Close();
            SetSides(home);
        }

        private void GoToCurrencies(object sender, RoutedEventArgs e)
        {
            if (ViewModelBase.CurrentPage == "Currencies")
            {
                return;
            }
            ViewModelBase.CurrentPage = "Currencies";
            window = Window.GetWindow(this);
            Window CurrenciesView = App.AppHost.Services.GetRequiredService<View.Currencies.Index>();
            CurrenciesView.Show();
            window.Close();
            SetSides(CurrenciesView);
        }

        private void GoToAccounts(object sender, RoutedEventArgs e)
        {
            if (ViewModelBase.CurrentPage == "Accounts")
            {
                return;
            }
            ViewModelBase.CurrentPage = "Accounts";
            window           = Window.GetWindow(this);
            var AccountsView = App.AppHost.Services.GetRequiredService<View.Accounts.Index>();
            AccountsView.Show();
            window.Close();
            SetSides(AccountsView);
        }

        private void GoToCorrespondents(object sender, RoutedEventArgs e)
        {
            if (ViewModelBase.CurrentPage == "Correspondents")
            {
                return;
            }
            ViewModelBase.CurrentPage = "Correspondents";
            window                 = Window.GetWindow(this);
            var CorrespondentsView = App.AppHost.Services.GetRequiredService<View.Correspondents.Index>();
            CorrespondentsView.Show();
            window.Close();
            SetSides(CorrespondentsView);
        }

        private void GoToBeneficaries(object sender, RoutedEventArgs e)
        {
            if (ViewModelBase.CurrentPage == "Beneficaries")
            {
                return;
            }
            ViewModelBase.CurrentPage = "Beneficaries";
            window               = Window.GetWindow(this);
            Window BeneficariesView = App.AppHost.Services.GetRequiredService<View.Beneficaries.Index>();
            window.Close();
            SetSides(BeneficariesView);
        }

        private void GoToTransactions(object sender, RoutedEventArgs e)
        {
            if (ViewModelBase.CurrentPage == "Transactions")
            {
                return;
            }
            ViewModelBase.CurrentPage = "Transactions";
            window               = Window.GetWindow(this);
            var TransactionsView = App.AppHost.Services.GetRequiredService<View.Transactions.Index>();
            window.Close();
            window = TransactionsView;
            SetSides(TransactionsView);
        }

        private void GoToResourcesItem(object sender, RoutedEventArgs e)
        {
            if (ViewModelBase.CurrentPage == "ResourcesItem")
            {
                return;
            }
            ViewModelBase.CurrentPage = "ResourcesItem";
            window                = Window.GetWindow(this);
            var ResourceItemsView = App.AppHost.Services.GetRequiredService<View.ResourceItems.Index>();
            ResourceItemsView.Show();
            window.Close();
            SetSides(ResourceItemsView);
        }

        private void GoToBanks(object sender, RoutedEventArgs e)
        {
            if (ViewModelBase.CurrentPage == "Banks")
            {
                return;
            }
            ViewModelBase.CurrentPage = "Banks";
            window        = Window.GetWindow(this);
            var BanksView = App.AppHost.Services.GetRequiredService<View.Banks.Index>();
            window.Close();
            BanksView.Show();
            SetSides(BanksView);
        }

        private void SetSides(Window window)
        {
            if(ViewModelBase.Width != 1300)
            {
                window.Left = 0;
                window.Top = 0;
            }
            window.Width  = ViewModelBase.Width;
            window.Height = ViewModelBase.Height;
        }
    }
}
