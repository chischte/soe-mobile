using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1
{
    public partial class MainPage : ContentPage
    {
        private readonly ICalculator _calculator;

        public MainPage(ICalculator calculator)
        {
            InitializeComponent();
            _calculator = calculator;
            displayText.Text = "0";

        }
        // FIELDS ---------------------------------------------------------------------------------

        private OperationStage _operationStage = OperationStage.EnterFirstOperand;
        private OperationMode _operationMode = OperationMode.Add;

        private string _resultString;


        // OPERATION MANAGER ----------------------------------------------------------------------

        enum OperationStage
        {
            EnterFirstOperand = 0, // Stay in this state until an operator is pressed
            EnterSecondOperand = 1, // Stay in this state until result is pressed
            DisplayResult = 2 // Continue depending on what button (operation / number / result) is pushed
        }

        enum OperationMode
        {
            Add = 0,
            Subtract = 1,
            Multiply = 2,
            Divide = 3
        }


        // CONVERT, GET AND SET CURRENT OPERAND ---------------------------------------------------
        private void SetCurrentOperand(String currentString)
        {
            double currentOperandDouble = Convert.ToDouble(currentString);

            if (_operationStage == OperationStage.EnterFirstOperand)
            {
                _calculator.FirstOperand = currentOperandDouble;
            }

            if (_operationStage == OperationStage.EnterSecondOperand)
            {
                _calculator.SecondOperand = currentOperandDouble;
            }
            if (_operationStage == OperationStage.DisplayResult)
            {
                _calculator.Result = currentOperandDouble;
            }

        }
        private String GetCurrentOperand()
        {
            double currentOperand = 0;

            if (_operationStage == OperationStage.EnterFirstOperand)
            {
                currentOperand = _calculator.FirstOperand;
            }
            else if (_operationStage == OperationStage.EnterSecondOperand)
            {
                currentOperand = _calculator.SecondOperand;
            }
            else if (_operationStage == OperationStage.DisplayResult)
            {
                currentOperand = _calculator.Result;
            }

            return currentOperand.ToString();
        }


        // ENTER OPERAND --------------------------------------------------------------------------
        private void OnEnterNumber(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string operand = button.Text;

            // Start calculation Chaining
            if (_operationStage == OperationStage.DisplayResult)
            {
                _operationStage = OperationStage.EnterFirstOperand;
                _calculator.FirstOperand = 0;
                _calculator.SecondOperand = 0;
            }

            // Add input to current string
            string currentOperand = GetCurrentOperand();
            if (currentOperand == "0")
            {
                currentOperand = "";
            }

            currentOperand += operand;
            SetCurrentOperand(currentOperand);
            displayText.Text = GetCurrentOperand();
        }

        // BUTTON FUNCTIONS -----------------------------------------------------------------------
        private void OnDotClicked(object sender, EventArgs e)
        {
            if (_operationStage == OperationStage.EnterFirstOperand)
            {
                _calculator.FirstOperandHasPoint = true;
            }
            if (_operationStage == OperationStage.EnterSecondOperand)
            {
                _calculator.SeconOperandHasPoint = true;
            }

            // Update Display with dot:
            string currentOperand = GetCurrentOperand();
            if (!currentOperand.Contains("."))
            {
                displayText.Text = GetCurrentOperand() + ".";
            }
            else displayText.Text = GetCurrentOperand();
        }

        private void Button_C_Clicked(object sender, EventArgs e)
        {
            displayText.Text = "0";
            _calculator.FirstOperand = 0;
            _calculator.SecondOperand = 0;
            _calculator.Result = 0;
            _resultString = "";
            _operationStage = OperationStage.EnterFirstOperand;
        }

        private void OnPlusMinusClicked(object sender, EventArgs e)
        {
            string currentOperand = GetCurrentOperand();
            if (currentOperand.Contains("-"))
            {
                currentOperand = currentOperand.Remove(0, 1);
            }
            else if (currentOperand != "0")
            {
                currentOperand = currentOperand.Insert(0, "-");
            }
            SetCurrentOperand(currentOperand);
            displayText.Text = GetCurrentOperand();
        }

        private void OnSelectOperation(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string pressed = button.Text;

            // Continue with a Result:
            if (_operationStage == OperationStage.DisplayResult)
            {
                _calculator.FirstOperand = _calculator.Result;
            }

            // Continue a chained Calculation:
            if (_operationStage == OperationStage.EnterSecondOperand)
            {
                CalculateAndDisplayResult();
            }

            // Jump to enter second operand:
            _operationStage = OperationStage.EnterSecondOperand;
            _calculator.SecondOperand = 0;


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

            displayText.Text = pressed;
        }



        private string DivideStringBy100(string inputString)
        {
            string outputString = "0";
            try
            {
                double inputDouble = Convert.ToDouble(inputString);
                outputString = (inputDouble / 100).ToString();
            }
            catch (System.FormatException)
            {
                Console.WriteLine("SYSTEM FORMAT EXCEPTION");
                displayText.Text = "INVALID ENTRY";
            }
            return outputString;
        }

        private void OnPercentageClicked(object sender, EventArgs e)
        {
            if (_operationStage == OperationStage.EnterFirstOperand)
            {
                _calculator.FirstOperand = _calculator.FirstOperand / 100;
            }

            if (_operationStage == OperationStage.EnterSecondOperand)
            {
                _calculator.SecondOperand = _calculator.SecondOperand / 100;
            }

            if (_operationStage == OperationStage.DisplayResult)
            {
                _calculator.Result = _calculator.Result / 100;
            }
            displayText.Text = GetCurrentOperand();
        }


        // GET RESULT -----------------------------------------------------------------------------

        private void Button_Result_Clicked(object sender, EventArgs e)
        {
            CalculateAndDisplayResult();
        }


        private void CalculateAndDisplayResult()
        {
            _operationStage = OperationStage.DisplayResult;

            double resultValue = 0;


            if (_operationMode == OperationMode.Add)
            {
                _calculator.Add();
            }

            if (_operationMode == OperationMode.Subtract)
            {
                _calculator.Subtract();
            }

            if (_operationMode == OperationMode.Multiply)
            {
                _calculator.Multiply();
            }

            if (_operationMode == OperationMode.Divide)
            {
                _calculator.Divide();
            }

            displayText.Text = _calculator.Result.ToString();

            // Prepare for chaining calculations:
            _calculator.FirstOperand = _calculator.Result; ;
        }

        //private bool ConvertStringsToDoubleWorks()
        //{
        //    bool convertionIsSuccess = true;
        //    try
        //    {
        //        _firstOperandDouble = Convert.ToDouble(_firstOperandString);
        //        _secondOperandDouble = Convert.ToDouble(_secondOperandString);
        //    }
        //    catch (System.FormatException)
        //    {
        //        Console.WriteLine("SYSTEM FORMAT EXCEPTION");
        //        displayText.Text = "INVALID ENTRY";
        //        convertionIsSuccess = false;
        //    }

        //    return convertionIsSuccess;
        //}
    }
}
