using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection.Emit;
using System.Windows.Forms;
using StressTesting;

namespace ScrewdriverPlugin
{
    /// <summary>
    /// Класс MainForm.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Поле хранящее в себе объект класса Builder.
        /// </summary>
        private Builder _builder = new Builder();

        /// <summary>
        /// Поле хранящее в себе объект класса Parameters.
        /// </summary>
        private Parameters _parameters = new Parameters();


        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            this.InitializeComponent();

            // StressTester stress = new StressTester();
            // stress.StressTesting();
        }

        //TODO: RSDN
        /// <summary>
        /// Инициализация ряда параметров при загрузке формы.
        /// </summary>
        /// <param name="sender">Объект.</param>
        /// <param name="e">Аргумент.</param>
        private void Form1_Load(object sender, EventArgs e)
        {
            this._parameters.AllParameters = new Dictionary<ParameterType, Parameter>();
            this.ComboBoxShapeOfHandle.SelectedIndex = 1;
            this.ComboBoxShapeOfRod.SelectedIndex = 1;
            Parameter rodLength = new Parameter();
            rodLength.TypeOfParameter = ParameterType.RodLength;
            this._parameters.DefineMinMax(rodLength);
            string toolTipRodLengthText = "Длина наконечника должна находиться в диапазоне от " +
                rodLength.MinValue.ToString() +
                " до " + rodLength.MaxValue.ToString() +
                " мм";
            this.toolTip1.SetToolTip(this.TextBoxRodLength, toolTipRodLengthText);
            string toolTipRodWidthDefaultText =
                "Диаметр наконечника должен находиться в диапазоне пятой части от длины отвёртки +/- 2 мм";
            this.toolTip1.SetToolTip(this.TextBoxRodWidth, toolTipRodWidthDefaultText);
            string toolTipHandleWidthDefaultText =
                "Диаметр ручки должен находиться в диапазоне четверти от длины ручки +/- 5 мм";
            this.toolTip1.SetToolTip(this.TextBoxHandleWidth, toolTipHandleWidthDefaultText);
            Parameter handleLength = new Parameter();
            handleLength.TypeOfParameter = ParameterType.HandleLength;
            this._parameters.DefineMinMax(handleLength);
            string toolTipHandleLengthText = "Длина ручки должна находиться в диапазоне от " +
                handleLength.MinValue.ToString() +
                " до " + handleLength.MaxValue.ToString() +
                " мм";
            this.toolTip1.SetToolTip(this.TextBoxHandleLength, toolTipHandleLengthText);
        }

        /// <summary>
        /// Обработчик нажатия на кнопку "Создать".
        /// </summary>
        /// <param name="sender">Объект.</param>
        /// <param name="e">Аргумент.</param>
        private void ButtonCreate_Click(object sender, EventArgs e)
        {
            if (this.TextBoxRodLength.BackColor == Color.Green &&
                this.TextBoxHandleLength.BackColor == Color.Green &&
                this.TextBoxRodWidth.BackColor == Color.Green &&
                this.TextBoxHandleWidth.BackColor == Color.Green)
            {
                this._builder.Build(this._parameters);
            }
        }

        /*/// <summary>
        /// Обработчик выхода из текстБокса "Длина ручки".
        /// </summary>
        /// <param name="sender">Объект.</param>
        /// <param name="e">Аргумент.</param>
        private void TextBoxHandleLength_Leave(object sender, EventArgs e)
        {
            ParameterType parameterType = ParameterType.HandleLength;
            this.FirstValidate(this.TextBoxHandleLength, parameterType);
            if (this.TextBoxHandleLength.BackColor != SystemColors.Window)
            {
                this.Validator(this.TextBoxHandleLength, parameterType);
                this.FirstValidate(this.TextBoxHandleWidth, ParameterType.HandleWidth);
                if (this.TextBoxHandleWidth.BackColor != SystemColors.Window)
                {
                    this.Validator(this.TextBoxHandleWidth, ParameterType.HandleWidth);
                }

                this.FirstValidate(this.TextBoxRodLength, ParameterType.RodLength);
                if (this.TextBoxRodLength.BackColor != SystemColors.Window)
                {
                    this.Validator(this.TextBoxRodLength, ParameterType.RodLength);
                }
            }
        }*/

        /*/// <summary>
        /// Обработчик выхода из текстБокса "Диаметр ручки".
        /// </summary>
        /// <param name="sender">Объект.</param>
        /// <param name="e">Аргумент.</param>
        private void TextBoxHandleWidth_Leave(object sender, EventArgs e)
        {
            ParameterType parameterType = ParameterType.HandleWidth;
            this.FirstValidate(this.TextBoxHandleWidth, parameterType);
            if (this.TextBoxHandleWidth.BackColor != SystemColors.Window)
            {
                this.Validator(this.TextBoxHandleWidth, parameterType);
                this.FirstValidate(this.TextBoxRodWidth, ParameterType.RodWidth);
                if (this.TextBoxRodWidth.BackColor != SystemColors.Window)
                {
                    this.Validator(this.TextBoxRodWidth, ParameterType.RodWidth);
                }

                this.FirstValidate(this.TextBoxHandleLength, ParameterType.HandleLength);
                if (this.TextBoxHandleLength.BackColor != SystemColors.Window)
                {
                    this.Validator(
                        this.TextBoxHandleLength,
                        ParameterType.HandleLength);
                }
            }
        }*/

        /// <summary>
        /// Обработчик выхода из текстБоксов ручки.
        /// </summary>
        /// <param name="sender">Объект.</param>
        /// <param name="e">Аргумент.</param>
        private void TextBoxHandle_Leave(object sender, EventArgs e)
        {
            var widthTextBoxes = new Dictionary<TextBox, ParameterType>()
            {
                {TextBoxHandleWidth, ParameterType.HandleWidth },
                {TextBoxHandleLength, ParameterType.HandleLength }
            };

            ParameterType parameterType;
            System.Windows.Forms.TextBox textBox;
            ParameterType chainedParameterType;
            System.Windows.Forms.TextBox chainedTextBox;
            ParameterType secondChainedParameterType;
            System.Windows.Forms.TextBox secondChainedTextBox;

            switch (((System.Windows.Forms.Control)sender).Name)
            {
                case "TextBoxHandleWidth":
                {
                    parameterType = ParameterType.HandleWidth;
                    chainedParameterType = ParameterType.RodWidth;
                    secondChainedParameterType = ParameterType.HandleLength;
                    textBox = this.TextBoxHandleWidth;
                    chainedTextBox = this.TextBoxRodWidth;
                    secondChainedTextBox = this.TextBoxHandleLength;
                    break;
                }

                default:
                {
                    parameterType = ParameterType.HandleLength;
                    chainedParameterType = ParameterType.HandleWidth;
                    secondChainedParameterType = ParameterType.RodLength;
                    textBox = this.TextBoxHandleLength;
                    chainedTextBox = this.TextBoxHandleWidth;
                    secondChainedTextBox = this.TextBoxRodLength;
                    break;
                }
            }

            this.Validator(textBox, parameterType);
            if (textBox.BackColor != SystemColors.Window)
            {
                this.Validator(chainedTextBox, chainedParameterType);
                this.Validator(secondChainedTextBox, secondChainedParameterType);
            }
        }

        /// <summary>
        /// Обработчик выхода из текстБоксов наконечника.
        /// </summary>
        /// <param name="sender">Объект.</param>
        /// <param name="e">Аргумент.</param>
        private void TextBoxRod_Leave(object sender, EventArgs e)
        {
            ParameterType parameterType;
            System.Windows.Forms.TextBox textBox;
            ParameterType chainedParameterType;
            System.Windows.Forms.TextBox chainedTextBox;
            switch (((System.Windows.Forms.Control)sender).Name)
            {
                case "TextBoxRodLength":
                {
                    parameterType = ParameterType.RodLength;
                    chainedParameterType = ParameterType.HandleLength;
                    textBox = this.TextBoxRodLength;
                    chainedTextBox = this.TextBoxHandleLength;
                    break;
                }

                default:
                {
                    parameterType = ParameterType.RodWidth;
                    chainedParameterType = ParameterType.HandleWidth;
                    textBox = this.TextBoxRodWidth;
                    chainedTextBox = this.TextBoxHandleWidth;
                    break;
                }
            }

            this.Validator(textBox, parameterType);
            if (textBox.BackColor != SystemColors.Window)
            {
                this.Validator(chainedTextBox, chainedParameterType);
            }
        }

        /*/// <summary>
        /// Обработчик выхода из текстБокса "Длина наконечника".
        /// </summary>
        /// <param name="sender">Объект.</param>
        /// <param name="e">Аргумент.</param>
        private void TextBoxRodLength_Leave(object sender, EventArgs e)
        {
             //TODO: duplication
            ParameterType parameterType = ParameterType.RodLength;
            this.FirstValidate(this.TextBoxRodLength, parameterType);
            if (this.TextBoxRodLength.BackColor != SystemColors.Window)
            {
                this.Validator(this.TextBoxRodLength, parameterType);
                this.FirstValidate(this.TextBoxHandleLength, ParameterType.HandleLength);
                if (this.TextBoxHandleLength.BackColor != SystemColors.Window)
                {
                    this.Validator(this.TextBoxHandleLength, ParameterType.HandleLength);
                }
            }
        }*/

        /*/// <summary>
        /// /// Обработчик выхода из текстБокса "Диаметр наконечника".
        /// </summary>
        /// <param name="sender">Объект.</param>
        /// <param name="e">Аргумент.</param>
        private void TextBoxRodWidth_Leave(object sender, EventArgs e)
        {
             //TODO: duplication
            ParameterType parameterType = ParameterType.RodWidth;
            this.FirstValidate(this.TextBoxRodWidth, parameterType);
            if (this.TextBoxRodWidth.BackColor != SystemColors.Window)
            {
                this.Validator(this.TextBoxRodWidth, parameterType);
                this.FirstValidate(this.TextBoxHandleWidth, ParameterType.HandleWidth);
                if (this.TextBoxHandleWidth.BackColor != SystemColors.Window)
                {
                    this.Validator(this.TextBoxHandleWidth, ParameterType.HandleWidth);
                }
            }
        }*/

        /*/// <summary>
        /// Первичная валидация (проверка на введение в текстБоксы целых чисел.
        /// </summary>
        /// <param name="textBox">ТекстБокс.</param>
        /// <param name="parameterType">Тип параметра.</param>
        private void FirstValidate(
            System.Windows.Forms.TextBox textBox,
            ParameterType parameterType)
        {
            try
            {
                int.Parse(textBox.Text);
                this.SetColors(textBox, 3, string.Empty);
            }
            catch
            {
                var message = textBox.Text != string.Empty
                    ? "Ошибка"
                    : string.Empty;

                this.SetColors(textBox, 1, message);
            }
        }*/

        /// <summary>
        /// Вспомогательный метод для установки цвета для текстБокса.
        /// </summary>
        /// <param name="textBox">Передаваемый текстБокс.</param>
        /// <param name="whatColor">Устанавливаемый цвет.</param>
        /// <param name="text">Текст устанавливаемый в подсказку.</param>
        private void SetColors(
            System.Windows.Forms.TextBox textBox,
            Parameter parameter,
            int whatColor,
            string text)
        {
            switch (whatColor)
            {
                case 1:
                {
                    textBox.BackColor = SystemColors.Window;
                    var message = textBox.Text != string.Empty
                        ? "Доступны только целочисленные значения"
                        : "Введите значения от " +
                            parameter.MinValue.ToString() +
                            " до " + parameter.MaxValue.ToString() +
                            " мм";
                    this.toolTip1.SetToolTip(textBox, message);

                    textBox.Text = string.Empty;
                    break;
                }

                case 2:
                {
                    textBox.BackColor = Color.Red;
                    switch (text)
                    {
                        case "Нарушение в определении граничных условий":
                        {
                            this.LabelWarning.Text = "Критическая ошибка системы.";
                            this.ButtonCreate.Enabled = false;
                            break;
                        }

                        default:
                        {
                            this.toolTip1.SetToolTip(textBox, text);
                            break;
                        }
                    }

                    break;
                }

                case 3:
                {
                    textBox.BackColor = Color.Green;
                    this.toolTip1.SetToolTip(textBox, string.Empty);
                    break;
                }
            }
        }

        /// <summary>
        /// Вторичная валидация, попытка создания параметра, попытка добавления
        /// корректного параметра в словарь.
        /// </summary>
        /// <param name="textBox">Используемый текстБокс.</param>
        /// <param name="parameterType">Тип параметра.</param>
        private void Validator(
            System.Windows.Forms.TextBox textBox,
            ParameterType parameterType)
        {
            Parameter parameter = new Parameter();
            parameter.TypeOfParameter = parameterType;
            //TODO: FormatException
            try
            {
                parameter.Value = int.Parse(textBox.Text);
                this._parameters.SetParameter(parameter);
                this.SetColors(textBox, parameter, 3, string.Empty);
            }
            //TODO: base exception - зло
            catch (Exception e)
            {
                switch (e.Message)
                {
                    case "Значение за граничными пределами":
                    {
                        this._parameters.DefineMinMax(parameter);
                        string toolTipText = "Введите значения от " +
                        parameter.MinValue.ToString() +
                        " до " + parameter.MaxValue.ToString() +
                        " мм";
                        this.SetColors(textBox, parameter, 2, toolTipText);
                        break;
                    }

                    case "Входная строка имела неверный формат.":
                    {
                        this._parameters.DefineMinMax(parameter);
                        var message = textBox.Text != string.Empty
                            ? "Ошибка"
                            : string.Empty;

                        this.SetColors(textBox, parameter, 1, message);
                        break;
                    }

                    default:
                    {
                        this.SetColors(textBox, parameter, 2, e.Message);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Обработчик изменения выбранного индекса у комбоБокса форма ручки.
        /// </summary>
        /// <param name="sender">Объект.</param>
        /// <param name="e">Аргумент.</param>
        private void ComboBoxShapeOfHandle_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.ComboBoxShapeOfHandle.SelectedIndex)
            {
                case 0:
                {
                    this._parameters.ShapeOfHandle = HandleType.Cylinder;
                    break;
                }

                case 1:
                {
                    this._parameters.ShapeOfHandle = HandleType.Prisme;
                    break;
                }
            }
        }

        /// <summary>
        /// Обработчик изменения выбранного индекса у комбоБокса форма наконечника.
        /// </summary>
        /// <param name="sender">Объект.</param>
        /// <param name="e">Аргумент.</param>
        private void ComboBoxShapeOfRod_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.ComboBoxShapeOfRod.SelectedIndex)
            {
                case 0:
                {
                    this._parameters.ShapeOfRod = RodType.Cruciform;
                    break;
                }

                case 1:
                {
                    this._parameters.ShapeOfRod = RodType.Flat;
                    break;
                }

                case 2:
                {
                    this._parameters.ShapeOfRod = RodType.Rectangle;
                    break;
                }
            }
        }

        /// <summary>
        /// Обработчик изменения состояния CheckBox "Наличие отверстия".
        /// </summary>
        /// <param name="sender">Объект.</param>
        /// <param name="e">Аргумент.</param>
        private void CheckBoxIsHoleExist_CheckedChanged(object sender, EventArgs e)
        {
            this._parameters.IsHoleExist = this.CheckBoxIsHoleExist.Checked;
        }
    }
}
