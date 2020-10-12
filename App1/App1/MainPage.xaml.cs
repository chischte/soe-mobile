using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1
{
    // implement percentage
    // fix bug display 08 after first entry of 8
    // Add colors

    public partial class MainPage : ContentPage
    {
        private readonly ICalculator _calculator;
        public MainPage(ICalculator calculator)
        {
            InitializeComponent();
            _calculator = calculator;
            resultText.Text = "0";

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

        private OperationStage _operationStage = OperationStage.EnterFirstOperand;
        private OperationMode _operationMode = OperationMode.Add;
        
        private string _firstOperandString = "0";
        private string _secondOperandString = "0";
        
        private double _firstOperandDouble = 0;
        private double _secondOperandDouble = 0;


        private string _resultString;

        public string Name { get; set; }


        private void Button_C_Clicked(object sender, EventArgs e)
        {
            resultText.Text = "0";
            _firstOperandString = "";
            _secondOperandString = "";
            _firstOperandDouble = 0;
            _secondOperandDouble = 0;
            _resultString = "";
            _operationStage = OperationStage.EnterFirstOperand;
        }


        private void Button_plus_minus(object sender, EventArgs e)
        {
        }

        private void OnSelectOperation(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string pressed = button.Text;

            // Continue with a Result:
            if (_operationStage == OperationStage.DisplayResult)
            {
                _firstOperandString = _resultString;
            }

            // Continue a chained Calculation 
            if (_operationStage == OperationStage.EnterSecondOperand)
            {
                CalculateResult();

            }
            _operationStage = OperationStage.EnterSecondOperand;
            _secondOperandString = "";


            if (pressed == "+")
            {
                _operationMode = OperationMode.Add;
            }
            if (pressed == "-")
            {
                _operationMode = OperationMode.Subtract;
            }
            if (pressed == "×")
            {
                _operationMode = OperationMode.Multiply;
            }
            if (pressed == "÷")
            {
                _operationMode = OperationMode.Divide;
            }
            resultText.Text = pressed;
        }



        private void OnEnterOperand(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string operand = button.Text;


            if (_operationStage == OperationStage.EnterFirstOperand)
            {
                _firstOperandString += operand;
                resultText.Text = _firstOperandString;
            }
            if (_operationStage == OperationStage.EnterSecondOperand)
            {
                _secondOperandString += operand;
                resultText.Text = _secondOperandString;
            }

            if (_operationStage == OperationStage.DisplayResult)
            {
                _operationStage = OperationStage.EnterFirstOperand;
                _firstOperandString = operand;
                _secondOperandString = "";

            }

        }

        private bool convertStringsToDouble()
        {
            bool convertionIsSuccess = true;
            try
            {
                _firstOperandDouble = Convert.ToDouble(_firstOperandString);
                _secondOperandDouble = Convert.ToDouble(_secondOperandString);
            }
            catch (System.FormatException)
            {
                Console.WriteLine("SYSTEM FORMAT EXCEPTION");
                resultText.Text = "INVALID ENTRY";
                convertionIsSuccess = false;
            }
            return convertionIsSuccess;
        }

        private void getResultFromCalculator()
        {
            // Fix Bugs Crashing when Operand Strings are empty
            if (_firstOperandString == "")
            {
                _firstOperandString = "0";
            }
            if (_secondOperandString == "")
            {
                _secondOperandString = "0";
            }
            _operationStage = OperationStage.DisplayResult;

            double resultValue = 0;


            if (_operationMode == OperationMode.Add)
            {
                resultValue = _calculator.Add(_firstOperandDouble, _secondOperandDouble);
            }
            if (_operationMode == OperationMode.Subtract)
            {
                resultValue = _calculator.Subtract(_firstOperandDouble, _secondOperandDouble);
            }
            if (_operationMode == OperationMode.Multiply)
            {
                resultValue = _calculator.Multiply(_firstOperandDouble, _secondOperandDouble);
            }
            if (_operationMode == OperationMode.Divide)
            {
                resultValue = _calculator.Divide(_firstOperandDouble, _secondOperandDouble);
            }
            _resultString = resultValue.ToString();
            _firstOperandString = _resultString;
            resultText.Text = _resultString;

        }

        private void CalculateResult()
        {
            if (convertStringsToDouble())
            {
                getResultFromCalculator();
            }
        }


        private void Button_Result_Clicked(object sender, EventArgs e)
        {
            CalculateResult();
        }
    }
}
