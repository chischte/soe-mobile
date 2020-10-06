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
        string mathOperator;
        int operationStage=0; // 0 = enter first  operand ...stay in this state until an operator is pressed
                              // 1 = enter operator ...
                              // 2 = enter second operand
                              // 3 = display result
        string firstOperand;
        string secondOperand;
        string operationMode; //add / subtract / multiply / divide


        public string Name { get; set; }

        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_C_Clicked(object sender, EventArgs e)
        {
            resultText.Text = "0";
        }

        private void Button_plus_minus(object sender, EventArgs e)
        {
            Name = "DOKEY";
        }

        private void Button_Plus_Clicked(object sender, EventArgs e)
        {
            operationStage = 3;
            resultText.Text = "10";

        }

        private void OnSelectOperator(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string pressed = button.Text;
            if(pressed== "÷")
            {
                operationMode = "divide";
            }
            mathOperator = pressed;
            resultText.Text = pressed;
        }

        private void Button_Subtract_Clicked(object sender, EventArgs e)
        {

        }

        private void Button_Result_Clicked(object sender, EventArgs e)
        {

            int resultValue = int.Parse(firstOperand) + int.Parse(secondOperand);
            resultText.Text = resultValue.ToString();

        }

        private void Button_8_Clicked(object sender, EventArgs e)
        {
           if(operationStage==0)
            {
                firstOperand = "8";
                resultText.Text = "8";
                //resultText.Text = "8";
            }
           if (operationStage==3)
            {
                secondOperand = "16";
                resultText.Text = "16";
            }

        }
    }
}
