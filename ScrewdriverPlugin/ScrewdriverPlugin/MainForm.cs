using System;
using System.Collections.Generic;
using System.Drawing;
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
            this.toolTip1.SetToolTip(
                this.TextBoxRodLength,
                "Длина наконечника должна находиться в диапазоне от 45 до 500 мм");
            this.toolTip1.SetToolTip(
                this.TextBoxRodWidth,
                "Диаметр наконечника должен находиться в диапазоне пятой части от длины отвёртки +/- 2 мм");
            this.toolTip1.SetToolTip(
                this.TextBoxHandleWidth,
                "Диаметр ручки должен находиться в диапазоне четверти от длины ручки +/- 5 мм");
            this.toolTip1.SetToolTip(
                this.TextBoxHandleLength,
                "Длина ручки должна находиться в диапазоне от 45 до 150 мм");
        }

        /// <summary>
        /// Обработчик нажатия на кнопку "Создать".
        /// </summary>
        /// <param name="sender">Объект.</param>
        /// <param name="e">Аргумент.</param>
        private void ButtonCreate_Click(object sender, EventArgs e)
        {
            if (this.TextBoxRodLength.BackColor == Color.Red ||
                this.TextBoxHandleLength.BackColor == Color.Red ||
                this.TextBoxRodWidth.BackColor == Color.Red ||
                this.TextBoxHandleWidth.BackColor == Color.Red ||
                this.TextBoxRodLength.BackColor == SystemColors.Window ||
                this.TextBoxRodWidth.BackColor == SystemColors.Window ||
                this.TextBoxHandleWidth.BackColor == SystemColors.Window ||
                this.TextBoxHandleLength.BackColor == SystemColors.Window)
            {
            }
            else
            {
                this._builder.Build(this._parameters);
            }
        }

        /// <summary>
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
                this.SecondValidate(this.TextBoxHandleLength, parameterType);
                this.FirstValidate(this.TextBoxHandleWidth, ParameterType.HandleWidth);
                if (this.TextBoxHandleWidth.BackColor != SystemColors.Window)
                {
                    this.SecondValidate(this.TextBoxHandleWidth, ParameterType.HandleWidth);
                }

                this.FirstValidate(this.TextBoxRodLength, ParameterType.RodLength);
                if (this.TextBoxRodLength.BackColor != SystemColors.Window)
                {
                    this.SecondValidate(this.TextBoxRodLength, ParameterType.RodLength);
                }
            }
        }

        /// <summary>
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
                this.SecondValidate(this.TextBoxHandleWidth, parameterType);
                this.FirstValidate(this.TextBoxRodWidth, ParameterType.RodWidth);
                if (this.TextBoxRodWidth.BackColor != SystemColors.Window)
                {
                    this.SecondValidate(this.TextBoxRodWidth, ParameterType.RodWidth);
                }

                this.FirstValidate(this.TextBoxHandleLength, ParameterType.HandleLength);
                if (this.TextBoxHandleLength.BackColor != SystemColors.Window)
                {
                    this.SecondValidate(
                        this.TextBoxHandleLength,
                        ParameterType.HandleLength);
                }
            }
        }

        /// <summary>
        /// Обработчик выхода из текстБокса "Длина наконечника".
        /// </summary>
        /// <param name="sender">Объект.</param>
        /// <param name="e">Аргумент.</param>
        private void TextBoxRodLength_Leave(object sender, EventArgs e)
        {
            ParameterType parameterType = ParameterType.RodLength;
            this.FirstValidate(this.TextBoxRodLength, parameterType);
            if (this.TextBoxRodLength.BackColor != SystemColors.Window)
            {
                this.SecondValidate(this.TextBoxRodLength, parameterType);
                this.FirstValidate(this.TextBoxHandleLength, ParameterType.HandleLength);
                if (this.TextBoxHandleLength.BackColor != SystemColors.Window)
                {
                    this.SecondValidate(this.TextBoxHandleLength, ParameterType.HandleLength);
                }
            }
        }

        /// <summary>
        /// /// Обработчик выхода из текстБокса "Диаметр наконечника".
        /// </summary>
        /// <param name="sender">Объект.</param>
        /// <param name="e">Аргумент.</param>
        private void TextBoxRodWidth_Leave(object sender, EventArgs e)
        {
            ParameterType parameterType = ParameterType.RodWidth;
            this.FirstValidate(this.TextBoxRodWidth, parameterType);
            if (this.TextBoxRodWidth.BackColor != SystemColors.Window)
            {
                this.SecondValidate(this.TextBoxRodWidth, parameterType);
                this.FirstValidate(this.TextBoxHandleWidth, ParameterType.HandleWidth);
                if (this.TextBoxHandleWidth.BackColor != SystemColors.Window)
                {
                    this.SecondValidate(this.TextBoxHandleWidth, ParameterType.HandleWidth);
                }
            }
        }

        /// <summary>
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
                this.SetColors(parameterType, 3, 0, string.Empty);
            }
            catch (Exception e)
            {
                this._parameters.AllParameters.Remove(parameterType);
                textBox.Text = string.Empty;
                this.SetColors(parameterType, 1, 0, e.Message);
            }
        }

        /// <summary>
        /// Вспомогательный метод для установки цвета для текстБокса.
        /// </summary>
        /// <param name="parameterType">Тип параметра.</param>
        /// <param name="whatColor">Устанавливаемый цвет.</param>
        /// <param name="whatReason">Причина установки цвета.</param>
        /// <param name="text">Текст устанавливаемый в подсказку.</param>
        private void SetColors(
            ParameterType parameterType,
            int whatColor,
            int whatReason,
            string text)
        {
            if (whatColor == 1)
            {
                if (parameterType == ParameterType.HandleLength)
                {
                    this.TextBoxHandleLength.BackColor = SystemColors.Window;
                    this.toolTip1.SetToolTip(
                        this.TextBoxHandleLength,
                        "Длина ручки должна находиться в диапазоне от 45 до 150 мм");
                }
                else if (parameterType == ParameterType.HandleWidth)
                {
                    this.TextBoxHandleWidth.BackColor = SystemColors.Window;
                    this.toolTip1.SetToolTip(
                        this.TextBoxHandleWidth,
                        "Диаметр ручки должен находиться в диапазоне четверти от длины ручки +/- 5 мм");
                }
                else if (parameterType == ParameterType.RodLength)
                {
                    this.TextBoxRodLength.BackColor = SystemColors.Window;
                    this.toolTip1.SetToolTip(
                        this.TextBoxRodLength,
                        "Длина наконечника должна находиться в диапазоне от 45 до 500 мм");
                }
                else if (parameterType == ParameterType.RodWidth)
                {
                    this.TextBoxRodWidth.BackColor = SystemColors.Window;
                    this.toolTip1.SetToolTip(
                        this.TextBoxRodWidth,
                        "Диаметр наконечника должен находиться в диапазоне пятой части от длины отвёртки +/- 2 мм");
                }
            }
            else if (whatColor == 2)
            {
                if (parameterType == ParameterType.HandleLength)
                {
                    this.TextBoxHandleLength.BackColor = Color.Red;
                    if (whatReason == 1)
                    {
                        this.toolTip1.SetToolTip(
                            this.TextBoxHandleLength,
                            "Длина ручки должна находиться в диапазоне от 45 до 150 мм");
                    }
                    else
                    {
                        this.toolTip1.SetToolTip(this.TextBoxHandleLength, text);
                    }
                }
                else if (parameterType == ParameterType.HandleWidth)
                {
                    this.TextBoxHandleWidth.BackColor = Color.Red;
                    if (whatReason == 1)
                    {
                        this.toolTip1.SetToolTip(
                            this.TextBoxHandleWidth,
                            "Диаметр ручки должен находиться в диапазоне четверти от длины ручки +/- 5 мм");
                    }
                    else
                    {
                        this.toolTip1.SetToolTip(this.TextBoxHandleWidth, text);
                    }
                }
                else if (parameterType == ParameterType.RodLength)
                {
                    this.TextBoxRodLength.BackColor = Color.Red;
                    if (whatReason == 1)
                    {
                        this.toolTip1.SetToolTip(
                            this.TextBoxRodLength,
                            "Длина наконечника должна находиться в диапазоне от 45 до 500 мм");
                    }
                    else
                    {
                        this.toolTip1.SetToolTip(this.TextBoxRodLength, text);
                    }
                }
                else if (parameterType == ParameterType.RodWidth)
                {
                    this.TextBoxRodWidth.BackColor = Color.Red;
                    if (whatReason == 1)
                    {
                        this.toolTip1.SetToolTip(
                            this.TextBoxRodWidth,
                            "Диаметр наконечника должен находиться в диапазоне пятой части от длины отвёртки +/- 2 мм");
                    }
                    else
                    {
                        this.toolTip1.SetToolTip(this.TextBoxRodWidth, text);
                    }
                }
            }
            else
            {
                if (parameterType == ParameterType.HandleLength)
                {
                    this.TextBoxHandleLength.BackColor = Color.Green;
                    this.toolTip1.SetToolTip(this.TextBoxHandleLength, null);
                }
                else if (parameterType == ParameterType.HandleWidth)
                {
                    this.TextBoxHandleWidth.BackColor = Color.Green;
                    this.toolTip1.SetToolTip(this.TextBoxHandleWidth, null);
                }
                else if (parameterType == ParameterType.RodLength)
                {
                    this.TextBoxRodLength.BackColor = Color.Green;
                    this.toolTip1.SetToolTip(this.TextBoxRodLength, null);
                }
                else if (parameterType == ParameterType.RodWidth)
                {
                    this.TextBoxRodWidth.BackColor = Color.Green;
                    this.toolTip1.SetToolTip(this.TextBoxRodWidth, null);
                }
            }
        }

        /// <summary>
        /// Вторичная валидация, попытка создания параметра, попытка добавления
        /// корректного параметра в словарь.
        /// </summary>
        /// <param name="textBox">Используемый текстБокс.</param>
        /// <param name="parameterType">Тип параметра.</param>
        private void SecondValidate(
            System.Windows.Forms.TextBox textBox,
            ParameterType parameterType)
        {
            bool cached = false;
            Parameter parameter = new Parameter();
            if (parameterType == ParameterType.HandleLength)
            {
                parameter.MaxValue = 150;
                parameter.MinValue = 45;
            }
            else if (parameterType == ParameterType.HandleWidth)
            {
                parameter.MaxValue = 42;
                parameter.MinValue = 7;
            }
            else if (parameterType == ParameterType.RodLength)
            {
                parameter.MaxValue = 500;
                parameter.MinValue = 45;
            }
            else if (parameterType == ParameterType.RodWidth)
            {
                parameter.MaxValue = 21;
                parameter.MinValue = 3;
            }

            try
            {
                parameter.Value = int.Parse(textBox.Text);
            }
            catch (Exception e)
            {
                this.SetColors(parameterType, 2, 1, e.Message);
                cached = true;
            }

            if (!cached)
            {
                try
                {
                    this._parameters.SetParameter(parameterType, parameter);
                    this.SetColors(parameterType, 3, 0, string.Empty);
                }
                catch (Exception e)
                {
                    this.SetColors(parameterType, 2, 0, e.Message);
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
            if (this.ComboBoxShapeOfHandle.SelectedIndex == 0)
            {
                this._parameters.ShapeOfHandle = HandleType.Cylinder;
            }
            else
            {
                this._parameters.ShapeOfHandle = HandleType.Prisme;
            }
        }

        /// <summary>
        /// Обработчик изменения выбранного индекса у комбоБокса форма наконечника.
        /// </summary>
        /// <param name="sender">Объект.</param>
        /// <param name="e">Аргумент.</param>
        private void ComboBoxShapeOfRod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ComboBoxShapeOfRod.SelectedIndex == 0)
            {
                this._parameters.ShapeOfRod = RodType.Cruciform;
            }
            else if (this.ComboBoxShapeOfRod.SelectedIndex == 1)
            {
                this._parameters.ShapeOfRod = RodType.Flat;
            }
            else if (this.ComboBoxShapeOfRod.SelectedIndex == 2)
            {
                this._parameters.ShapeOfRod = RodType.Rectangle;
            }
        }

        private void CheckBoxIsHoleExist_CheckedChanged(object sender, EventArgs e)
        {
            if (this.CheckBoxIsHoleExist.Checked)
            {
                this._parameters.IsHoleExist = true;
            }
            else
            {
                this._parameters.IsHoleExist = false;
            }
        }
    }
}
