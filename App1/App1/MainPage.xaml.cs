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
        private void SetCurrentOperandValue(String currentString)
        {
            double currentOperandDouble = Convert.ToDouble(currentString);

            if (_operationStage == OperationStage.EnterFirstOperand)
            {
                _calculator.FirstOperand.Value = currentOperandDouble;
            }

            if (_operationStage == OperationStage.EnterSecondOperand)
            {
                _calculator.SecondOperand.Value = currentOperandDouble;
            }
            if (_operationStage == OperationStage.DisplayResult)
            {
                _calculator.Result = currentOperandDouble;
            }

        }
        private string GetCurrentOperandString()
        {
            double currentOperand = 0;

            if (_operationStage == OperationStage.EnterFirstOperand)
            {
                currentOperand = _calculator.FirstOperand.Value;
            }
            else if (_operationStage == OperationStage.EnterSecondOperand)
            {
                currentOperand = _calculator.SecondOperand.Value;
            }
            else if (_operationStage == OperationStage.DisplayResult)
            {
                currentOperand = _calculator.Result;
            }

            string currentOperandString = currentOperand.ToString();

            if (GetCurrentOperandHasAPoint() && !currentOperandString.Contains("."))
            {
                currentOperandString += ".";
            }
            return currentOperandString;
        }

        private bool GetCurrentOperandHasAPoint()
        {
            bool currentOperandHasAPoint = false;

            if (_operationStage == OperationStage.EnterFirstOperand)
            {
                currentOperandHasAPoint = _calculator.FirstOperand.HasAPoint;
            }
            else if (_operationStage == OperationStage.EnterSecondOperand)
            {
                currentOperandHasAPoint = _calculator.SecondOperand.HasAPoint;
            }

            return currentOperandHasAPoint;
        }

        private void MoveResultToFirstOperandIfStageResultDisplay()
        {
            if (_operationStage == OperationStage.DisplayResult)
            {
                _calculator.FirstOperand.Value = _calculator.Result;
                _calculator.FirstOperand.HasAPoint = false;
                _calculator.SecondOperand.Reset();
                _operationStage = OperationStage.EnterFirstOperand;
            }
        }

        private Operand GetCurrentOperandObject()
        {
            Operand currentOperandObject = new Operand();

            if (_operationStage == OperationStage.EnterFirstOperand)
            {
                currentOperandObject = _calculator.FirstOperand;
            }
            else if (_operationStage == OperationStage.EnterSecondOperand)
            {
                currentOperandObject = _calculator.SecondOperand;
            }
            return currentOperandObject;
        }


        // ENTER OPERAND --------------------------------------------------------------------------
        private void OnEnterNumber(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string operand = button.Text;

            MoveResultToFirstOperandIfStageResultDisplay();

            // Add input to current string
            string currentOperand = GetCurrentOperandString();
            if (currentOperand == "0")
            {
                currentOperand = "";
            }

            currentOperand += operand;
            SetCurrentOperandValue(currentOperand);
            displayText.Text = GetCurrentOperandString();
        }

        // BUTTON FUNCTIONS -----------------------------------------------------------------------
        private void OnDotClicked(object sender, EventArgs e)
        {
            MoveResultToFirstOperandIfStageResultDisplay();
            GetCurrentOperandObject().HasAPoint = true;
            displayText.Text = GetCurrentOperandString();
        }

        private void Button_C_Clicked(object sender, EventArgs e)
        {
            displayText.Text = "0";
            _calculator.FirstOperand.Reset();
            _calculator.SecondOperand.Reset();
            _calculator.Result = 0;
            _operationStage = OperationStage.EnterFirstOperand;
        }

        private void OnPlusMinusClicked(object sender, EventArgs e)
        {
            GetCurrentOperandObject().Invert();
            displayText.Text = GetCurrentOperandString();
        }

        private void OnSelectOperation(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string pressed = button.Text;

            // Continue with a Result:
            if (_operationStage == OperationStage.DisplayResult)
            {
                _calculator.FirstOperand.Value = _calculator.Result;
            }

            // Continue a chained Calculation:
            if (_operationStage == OperationStage.EnterSecondOperand)
            {
                CalculateAndDisplayResult();
            }

            // Jump to enter second operand:
            _operationStage = OperationStage.EnterSecondOperand;
            _calculator.SecondOperand.Value = 0;


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

        private void OnPercentageClicked(object sender, EventArgs e)
        {
            GetCurrentOperandObject().divideBy100();
            displayText.Text = GetCurrentOperandString();
        }


        // GET RESULT -----------------------------------------------------------------------------
        private void Button_Result_Clicked(object sender, EventArgs e)
        {
            CalculateAndDisplayResult();
        }

        private void CalculateAndDisplayResult()
        {
            _operationStage = OperationStage.DisplayResult;


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
            // Allow multiple clicks on equal sign:
            _calculator.FirstOperand.Value = _calculator.Result;

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
