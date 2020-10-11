﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    public partial class App : Application
    {
        
        public App()
        {
            Calculator calculator = new Calculator();
            InitializeComponent();
            MainPage = new MainPage(calculator);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
