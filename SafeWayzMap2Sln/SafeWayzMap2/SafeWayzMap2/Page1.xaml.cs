using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SafeWayzMap2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        public string Address { get; set; }
        public Page1(string address)
        {
            InitializeComponent();
            Address = address;
            BindingContext = this;
            
        }

        
    }
}