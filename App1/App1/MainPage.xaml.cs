using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1
{
    public partial class MainPage : ContentPage
    {


        public string Name { get; set; }

        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_C_Clicked(object sender, EventArgs e)
        {
            myLabel.Text = "OK";

        }

        private void Button_plus_minus(object sender, EventArgs e)
        {
            Name = "DOKEY";
        }
    }
}
