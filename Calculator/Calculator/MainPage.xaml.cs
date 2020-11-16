using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Calculator
{
    public enum OperationMode
    {
        Add = 0,
        Subtract = 1,
        Multiply = 2,
        Divide = 3
    }


    public enum OperationStage
    {
        EnterFirstOperand = 0, // Stay in this state until an operator is pressed
        EnterSecondOperand = 1, // Stay in this state until result is pressed
        DisplayResult = 2 // Continue depending on what button (operation / number / result) is pushed
    }


    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageViewModel vm)
        {
            BindingContext = vm;
            InitializeComponent();
        }
    }
}
