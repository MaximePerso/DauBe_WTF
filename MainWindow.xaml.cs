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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DauBe_WTF.ViewModel;
using DauBe_WTF.ViewModel.SubVM;
using Doli.DoPE;
using System.Threading;
using System.Windows.Threading;
using System.Text.RegularExpressions;
using DauBe_WTF.SecondaryWindows.Splashscreen;

namespace DauBe_WTF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            System.Windows.FrameworkCompatibilityPreferences.KeepTextBoxDisplaySynchronizedWithTextProperty = false;
            InitializeComponent();
            //NewINstanceOfChart = new RealTimeChartsVM();
            //Doli code was not written to be easily used using WPF, or MVVM. We need an extra step to bind data fomr the codebehind (here) to the viewmodel.
            DataContext = new MainVM();
        }

        private void Window_Loaded(object sender, EventArgs e)
        {
            Splashscreen sp = new Splashscreen();
            sp.Show();
            // Connect to EDC
            ((DauBe_WTF.ViewModel.MainVM)DataContext).Doli.ConnectToEdc(sp);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //close all liks (could not find the dedicated DoPE function for that)
            Environment.Exit(0);
        }
    }
}

