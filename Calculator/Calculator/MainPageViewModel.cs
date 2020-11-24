using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Windows.Input;
using Calculator.Annotations;
using Xamarin.Forms;

namespace Calculator
{
    public class MainPageViewModel : ViewModelBase
    {
        private ICalculator _calculator;

        public MainPageViewModel(ICalculator calculator)
        {
            this._calculator = calculator;
        }

        // ADD NUMBER COMMAND "0-9"-----------------------------------------------------------------
        private ICommand _addNumberCommand;
        public ICommand AddNumberCommand
        {
            get
            {
                _addNumberCommand = new Command<string>(CalculatorAddNumberCommand);
                return _addNumberCommand;
            }

        }

        // SET OPERATION COMMAND "+" "-" "×" "÷" "=" -----------------------------------------------
        private ICommand _setOperationCommand;
        public ICommand SetOperationCommand
        {
            get
            {
                _setOperationCommand = new Command<string>(CalculatorSetOperationCommand);
                return _setOperationCommand;
            }
        }
        // MODIFY OPERAND COMMAND "%" "+/-" "." "C" ------------------------------------------------
        private ICommand _modifyOperandCommand;
        public ICommand ModifyOperandCommand
        {
            get
            {
                _modifyOperandCommand = new Command<string>(CalculatorModifyOperandCommand);
                return _modifyOperandCommand;
            }
        }

        // INOTIFY DISPLAY TEXT --------------------------------------------------------------------
        private string _displayText = string.Empty;

        public string DisplayText
        {
            get
            {
                return _displayText;
            }
            private set
            {
                if (_displayText != value)
                {
                    _displayText = value;
                    OnPropertyChanged(nameof(DisplayText));
                    UpdateDisplayTextColor();
                }
            }
        }
        // INOTIFY DISPLAY TEXT COLOR --------------------------------------------------------------
        private string color = "black";
        public string Color
        {
            get
            {
                return color;
            }
            private set
            {
                if (color != value)
                {
                    color = value;
                    OnPropertyChanged(nameof(Color));
                }
            }
        }
        // -----------------------------------------------------------------------------------------


        private void CalculatorAddNumberCommand(string commandString)
        {
            Operand currentOperandObject = _calculator.GetCurrentOperandObject();

            currentOperandObject.AddText(commandString);

            DisplayText = _calculator.GetCurrentOperandObject().Text;
        }

        private void UpdateDisplayTextColor()
        {
            if (_calculator.GetCurrentOperandObject().Value < 0)
            {
                Color = "red";
            }
            else
            {
                Color = "black";
            }
        }

        private void CalculatorModifyOperandCommand(string commandString)
        {
            _calculator.ModifyOperand(commandString);
            DisplayText = _calculator.GetCurrentOperandObject().Text;
        }


        private void CalculatorSetOperationCommand(string commandString)
        {
            switch (commandString)
            {
                case "+":
                    {
                        _calculator.SetOperationMode(OperationMode.Add);
                        break;
                    }
                case "-":
                    {
                        _calculator.SetOperationMode(OperationMode.Subtract);
                        break;
                    }
                case "×":
                    {
                        _calculator.SetOperationMode(OperationMode.Multiply);
                        break;
                    }
                case "÷":
                    {
                        _calculator.SetOperationMode(OperationMode.Divide);
                        break;
                    }
                case "=":
                    {
                        _calculator.CalculateResult();
                        break;
                    }
            }

            if (commandString == "=")
            {
                DisplayText = _calculator.Result.Text;
            }
            else
            {
                DisplayText = commandString;
            }
        }
    }
}
