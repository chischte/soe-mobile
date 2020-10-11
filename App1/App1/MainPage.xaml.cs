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
        private Calculator calculator;

        public MainPage(Calculator calculator)
        {
            InitializeComponent();
            this.calculator = calculator;

        }
        enum OperationStage
        {
            EnterFirstOperand = 0,   // Stay in this state until an operator is pressed
            EnterSecondOperand = 1,  // Stay in this state until result is pressed
            DisplayResult = 2       // Continue depending on what button (operation / number / result) is pushed
        }

        enum OperationMode
        {
            Add = 0,
            Subtract = 1,
            Multiply = 2,
            Divide = 3
        }

        private OperationStage operationStage = OperationStage.EnterFirstOperand;
        private OperationMode operationMode = OperationMode.Add;
        string firstOperand;
        string secondOperand;

        public string Name { get; set; }


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
                operationMode = OperationMode.Add;
                operationStage = OperationStage.EnterSecondOperand;
            }
            if (pressed == "-")
            {
                operationMode = OperationMode.Subtract;
                operationStage = OperationStage.EnterSecondOperand;
            }
            if (pressed == "×")
            {
                operationMode = OperationMode.Multiply;
                operationStage = OperationStage.EnterSecondOperand;
            }
            if (pressed == "÷")
            {
                operationMode = OperationMode.Divide;
                operationStage = OperationStage.EnterSecondOperand;
            }
            resultText.Text = pressed;
        }

        private void Button_Result_Clicked(object sender, EventArgs e)
        {
            double valueFirstOperand = Convert.ToDouble(firstOperand);
            double valueSecondOperand = Convert.ToDouble(secondOperand);
            double resultValue = 0;

            if (operationMode == OperationMode.Add)
            {
                resultValue = calculator.Add(valueFirstOperand, valueSecondOperand);
            }
            if (operationMode == OperationMode.Subtract)
            {
                resultValue = calculator.Subtract(valueFirstOperand, valueSecondOperand);
            }
            if (operationMode == OperationMode.Multiply)
            {
                resultValue = calculator.Multiply(valueFirstOperand, valueSecondOperand);
            }
            if (operationMode == OperationMode.Divide)
            {
                resultValue = calculator.Divide(valueFirstOperand, valueSecondOperand);
            }

            string resultString = resultValue.ToString();
            resultText.Text = resultString;
            operationStage = OperationStage.DisplayResult;
        }

        private void OnEnterOperand(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string operand = button.Text;


            if (operationStage == OperationStage.EnterFirstOperand)
            {
                firstOperand += operand;
                resultText.Text = firstOperand;
                //resultText.Text = "8";
            }
            if (operationStage == OperationStage.EnterSecondOperand)
            {
                secondOperand += operand;
                resultText.Text = secondOperand;
            }

        }
    }
}
