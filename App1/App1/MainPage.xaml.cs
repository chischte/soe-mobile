﻿using System;
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

        private OperationStage _operationStage = OperationStage.EnterFirstOperand;
        private OperationMode _operationMode = OperationMode.Add;

        private string _firstOperandString = "0";
        private string _secondOperandString = "0";
        private string _resultString;

        private double _firstOperandDouble;
        private double _secondOperandDouble;
        private double _resultDouble;

        private String GetCurrentOperand()
        {
            if (_operationStage == OperationStage.EnterFirstOperand)
            {
                return _firstOperandString;
            }
            else // OperationStage is "EnterSecondOperand
            {
                return _secondOperandString;
            }
        }

        private void SetCurrentOperand(String currentString)
        {
            if (_operationStage == OperationStage.EnterFirstOperand)
            {
                _firstOperandString = currentString;
            }

            if (_operationStage == OperationStage.EnterSecondOperand)
            {
                _secondOperandString = currentString;
            }

        }

        private void Button_C_Clicked(object sender, EventArgs e)
        {
            displayText.Text = "0";
            _firstOperandString = "0";
            _secondOperandString = "";
            _firstOperandDouble = 0;
            _secondOperandDouble = 0;
            _resultString = "";
            _operationStage = OperationStage.EnterFirstOperand;
        }

        private void OnPlusMinusClicked(object sender, EventArgs e)
        {
            if (!(_operationStage == OperationStage.DisplayResult))
            {
                string currentOperand = GetCurrentOperand();
                if (currentOperand.Contains("-"))
                {
                    currentOperand = currentOperand.Remove(0, 1);
                }
                else
                {
                    currentOperand = currentOperand.Insert(0, "-");
                }

                SetCurrentOperand(currentOperand);
                displayText.Text = GetCurrentOperand();
            }
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

            // Continue a chained Calculation:
            if (_operationStage == OperationStage.EnterSecondOperand)
            {
                CalculateResult();
            }

            // Jump to enter second operand:
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

            displayText.Text = pressed;
        }

        private void OnEnterNumber(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string operand = button.Text;

            // Start calculation Chaining
            if (_operationStage == OperationStage.DisplayResult)
            {
                _operationStage = OperationStage.EnterFirstOperand;
                _firstOperandString = "";
                _secondOperandString = "";
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

        private void OnDotClicked(object sender, EventArgs e)
        {
            string currentOperand = GetCurrentOperand();
            if (!currentOperand.Contains("."))
            {
                currentOperand += ".";
                SetCurrentOperand(currentOperand);
                displayText.Text = GetCurrentOperand();
            }
        }

        private bool ConvertStringsToDoubleWorks()
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
                displayText.Text = "INVALID ENTRY";
                convertionIsSuccess = false;
            }

            return convertionIsSuccess;
        }

        private void GetResultFromCalculator()
        {
            // Fix Bugs Crashing when Operand Strings are empty:
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
            displayText.Text = _resultString;

        }

        private void CalculateResult()
        {
            if (ConvertStringsToDoubleWorks())
            {
                GetResultFromCalculator();
            }
        }

        private void Button_Result_Clicked(object sender, EventArgs e)
        {
            CalculateResult();
        }

        private void OnPercentageClicked(object sender, EventArgs e)
        {
            if (_operationStage == OperationStage.EnterFirstOperand)
            {
                try
                {
                    _firstOperandDouble = Convert.ToDouble(_firstOperandString);
                    _firstOperandString = (_firstOperandDouble / 100).ToString();
                    displayText.Text = _firstOperandString;
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("SYSTEM FORMAT EXCEPTION");
                    displayText.Text = "INVALID ENTRY";
                }

            }

            if (_operationStage == OperationStage.EnterSecondOperand)
            {
                try
                {
                    _secondOperandDouble = Convert.ToDouble(_secondOperandString);
                    _secondOperandString = (_secondOperandDouble / 100).ToString();
                    displayText.Text = _secondOperandString;
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("SYSTEM FORMAT EXCEPTION");
                    displayText.Text = "INVALID ENTRY";
                }



            }

            if (_operationStage == OperationStage.DisplayResult)
            {
                try
                {
                    _resultDouble = Convert.ToDouble(_resultString);
                    _resultString = (_resultDouble / 100).ToString();
                    displayText.Text = _resultString;
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("SYSTEM FORMAT EXCEPTION");
                    displayText.Text = "INVALID ENTRY";
                }
            }
        }
    }
}
