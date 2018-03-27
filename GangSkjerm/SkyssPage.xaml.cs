using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GangSkjerm
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SkyssPage : Page
    {
        public SkyssPage()
        {
            this.InitializeComponent();
            webView.Navigate(new Uri("https://avgangsvisning.skyss.no/board/#/?stops=12016411,12011454,12016410&hideLines=000400_0_Direction2%3A12011454,000530_0_Direction2%3A12011454,000600_0_Direction2%3A12011454&viewFreq=10000&terminal=true&colors=dark&name=Neste%20bussavganger"));

        }
    }
}
