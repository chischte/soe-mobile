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
        int operationStage = 0; // 0 = enter first  operand ...stay in this state until an operator is pressed
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
            firstOperand = "";
            secondOperand = "";
            operationStage = 0;
        }

        private void Button_plus_minus(object sender, EventArgs e)
        {

        }

        private void OnSelectOperation(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string pressed = button.Text;
            if (pressed == "+")
            {
                operationMode = "add";
            }
            if (pressed == "-")
            {
                operationMode = "subtract";
            }
            if (pressed == "×")
            {
                operationMode = "multiply";
            }
            if (pressed == "÷")
            {
                operationMode = "divide";
            }
            mathOperator = pressed;
            resultText.Text = pressed;
        }

        private void Button_Result_Clicked(object sender, EventArgs e)
        {
            int valueFirstOperand = int.Parse(firstOperand);
            int valueSecondOperand = int.Parse(secondOperand);
            int resultValue;

            if (operationMode == "add")
            {
                resultValue = valueFirstOperand + valueSecondOperand;
            }
            if (operationMode == "subtract")
            {
                resultValue = valueFirstOperand - valueSecondOperand;
            }
            if (operationMode == "multiply")
            {
                resultValue = valueFirstOperand * valueSecondOperand;
            }
            if (operationMode == "divide")
            {
                resultValue = valueFirstOperand / valueSecondOperand;
            }


            resultValue = int.Parse(firstOperand) + int.Parse(secondOperand);
            string resultString = resultValue.ToString();
            firstOperand = resultString;
            resultText.Text = resultString;
            operationStage = 3;
        }

        private void OnEnterOperand(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string operand = button.Text;


            if (operationStage == 0)
            {
                firstOperand += operand;
                resultText.Text = firstOperand;
                //resultText.Text = "8";
            }
            if (operationStage == 3)
            {
                secondOperand += operand;
                resultText.Text = secondOperand;
            }

        }
    }
}
