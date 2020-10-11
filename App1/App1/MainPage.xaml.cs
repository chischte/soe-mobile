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
        enum OperationStage
        {
            EnterFirstOperand = 0,   // Stay in this state until an operator is pressed
            EnterSecondOperand = 1,  // Stay in this state until result is pressed
            DisplayResult = 2       // Continue depending on what button (operation / number / result) is pushed
        }

        string mathOperator;
        OperationStage operationStage = 0; // 0 = enter first  operand ...stay in this state until an operator is pressed
                                // 1 = enter operator ...
                                // 2 = enter second operand
                                // 3 = display result
        string firstOperand;
        string secondOperand;
        string operationMode; //add / subtract / multiply / divide
        public string Name { get; set; }

        private ICalculator calculator;

        //public MainPage(ICalculator calculator)
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
            int loesch = calculator.GetResult();
            Console.WriteLine(loesch);


        }

        private void OnSelectOperation(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string pressed = button.Text;
            if (pressed == "+")
            {
                operationMode = "add"; 
                Console.WriteLine("ADD PRESSED");
                operationStage = OperationStage.EnterSecondOperand;
            }
            if (pressed == "-")
            {
                operationMode = "subtract";
                operationStage = OperationStage.EnterSecondOperand;
            }
            if (pressed == "×")
            {
                operationMode = "multiply";
                operationStage = OperationStage.EnterSecondOperand;
            }
            if (pressed == "÷")
            {
                operationMode = "divide";
                operationStage = OperationStage.EnterSecondOperand;
            }
            mathOperator = pressed;
            resultText.Text = pressed;
        }

        private void Button_Result_Clicked(object sender, EventArgs e)
        {
            int valueFirstOperand = int.Parse(firstOperand);
            int valueSecondOperand = int.Parse(secondOperand);
            int resultValue=0;

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

            string resultString = resultValue.ToString();
            resultText.Text = resultString;
            operationStage = OperationStage.DisplayResult;
        }

        private void OnEnterOperand(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string operand = button.Text;


            if (operationStage==OperationStage.EnterFirstOperand)
            {
                firstOperand += operand;
                resultText.Text = firstOperand;
                //resultText.Text = "8";
            }
            if (operationStage==OperationStage.EnterSecondOperand)
            {
                secondOperand += operand;
                resultText.Text = secondOperand;
            }

        }
    }
}
