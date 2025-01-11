using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography;
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

        /// <summary>
        /// Обработчик выхода из текстБоксов ручки.
        /// </summary>
        /// <param name="sender">Объект.</param>
        /// <param name="e">Аргумент.</param>
        private void TextBoxHandle_Leave(object sender, EventArgs e)
        {
            var handleTextBoxesChainedParameters =
                new Dictionary<
                    TextBox,
                    (ParameterType, TextBox, ParameterType, TextBox, ParameterType)>
            {
                {
                    this.TextBoxHandleWidth,
                    (ParameterType.HandleWidth,
                    this.TextBoxRodWidth,
                    ParameterType.RodWidth,
                    this.TextBoxHandleLength,
                    ParameterType.HandleLength)
                },
                {
                    this.TextBoxHandleLength,
                    (ParameterType.HandleLength,
                    this.TextBoxHandleWidth,
                    ParameterType.HandleWidth,
                    this.TextBoxRodLength,
                    ParameterType.RodLength)
                },
            };
            TextBox textBox = (TextBox)sender;
            var chained = handleTextBoxesChainedParameters[textBox];
            this.Validate(textBox, chained.Item1);
            if (textBox.BackColor != SystemColors.Window)
            {
                this.Validate(chained.Item2, chained.Item3);
                this.Validate(chained.Item4, chained.Item5);
            }
        }

        /// <summary>
        /// Обработчик выхода из текстБоксов наконечника.
        /// </summary>
        /// <param name="sender">Объект.</param>
        /// <param name="e">Аргумент.</param>
        private void TextBoxRod_Leave(object sender, EventArgs e)
        {
            var rodTextBoxesChainedParameters =
                new Dictionary<TextBox, (ParameterType, TextBox, ParameterType)>
            {
                {
                    this.TextBoxRodLength,
                    (ParameterType.RodLength, this.TextBoxHandleLength, ParameterType.HandleLength)
                },
                {
                    this.TextBoxRodWidth,
                    (ParameterType.RodWidth, this.TextBoxHandleWidth, ParameterType.HandleWidth)
                },
            };

            TextBox textBox = (TextBox)sender;
            var chained = rodTextBoxesChainedParameters[textBox];
            this.Validate(textBox, chained.Item1);
            if (textBox.BackColor != SystemColors.Window)
            {
                this.Validate(chained.Item2, chained.Item3);
            }
        }

        /// <summary>
        /// Вспомогательный метод для установки цвета для текстБокса.
        /// </summary>
        /// <param name="textBox">Передаваемый текстБокс.</param>
        /// <param name="whatColor">Устанавливаемый цвет.</param>
        /// <param name="text">Текст устанавливаемый в подсказку.</param>
        private void SetColors(
            System.Windows.Forms.TextBox textBox,
            Parameter parameter,
            KnownColor whatColor,
            string text)
        {
            switch (whatColor)
            {
                case KnownColor.Window:
                {
                    textBox.BackColor = SystemColors.Window;
                    var message = textBox.Text != string.Empty
                        ? "Доступны только целочисленные значения"
                         //TODO: duplication +
                        : this.RangeTextCaster(parameter);
                    this.toolTip1.SetToolTip(textBox, message);

                    textBox.Text = string.Empty;
                    break;
                }

                case KnownColor.Red:
                {
                    textBox.BackColor = Color.Red;
                    this.toolTip1.SetToolTip(textBox, text);
                    break;
                }

                case KnownColor.Green:
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
        private void Validate(
            System.Windows.Forms.TextBox textBox,
            ParameterType parameterType)
        {
            try
            {
                this._parameters.SetParameter(parameterType, int.Parse(textBox.Text));
                this.SetColors(
                    textBox,
                    this._parameters.AllParameters[parameterType],
                    KnownColor.Green,
                    string.Empty);
            }
            catch (FormatException)
            {
                var message = textBox.Text != string.Empty
                    ? "Ошибка"
                    : string.Empty;

                this.SetColors(
                    textBox,
                    this._parameters.AllParameters[parameterType],
                    KnownColor.Window,
                    message);
            }
            catch (ValueException)
            {
                string toolTipText =
                    this.RangeTextCaster(this._parameters.AllParameters[parameterType]);
                this.SetColors(
                    textBox,
                    this._parameters.AllParameters[parameterType],
                    KnownColor.Red,
                    toolTipText);
            }
            catch (MinMaxException)
            {
                this.LabelWarning.Text = "Критическая ошибка системы.";
                this.ButtonCreate.Enabled = false;
            }
            catch (ParametersException e)
            {
                this.SetColors(
                    textBox,
                    this._parameters.AllParameters[parameterType],
                    KnownColor.Red,
                    e.Message);
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

        /// <summary>
        /// Инициализация ряда параметров при загрузке формы.
        /// </summary>
        /// <param name="sender">Объект.</param>
        /// <param name="e">Аргумент.</param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.ComboBoxShapeOfHandle.SelectedIndex = 1;
            this.ComboBoxShapeOfRod.SelectedIndex = 1;
            Parameter rodLength = this._parameters.AllParameters[ParameterType.RodLength];
            //TODO: duplication +
            string toolTipRodLengthText = this.TextCaster(rodLength, "наконечника");
            this.toolTip1.SetToolTip(this.TextBoxRodLength, toolTipRodLengthText);
            string toolTipRodWidthDefaultText =
                "Диаметр наконечника должен находиться в диапазоне пятой части " +
                "от длины отвёртки +/- 2 мм";
            this.toolTip1.SetToolTip(this.TextBoxRodWidth, toolTipRodWidthDefaultText);
            string toolTipHandleWidthDefaultText =
                "Диаметр ручки должен находиться в диапазоне четверти от длины ручки +/- 5 мм";
            this.toolTip1.SetToolTip(this.TextBoxHandleWidth, toolTipHandleWidthDefaultText);
            Parameter handleLength = this._parameters.AllParameters[ParameterType.HandleLength];
            //TODO: duplication +
            string toolTipHandleLengthText = this.TextCaster(handleLength, "ручки");
            this.toolTip1.SetToolTip(this.TextBoxHandleLength, toolTipHandleLengthText);
        }

        /// <summary>
        /// Вспомогательный метод для генерации текста граничных условий.
        /// </summary>
        /// <param name="parameter">Передаваемый параметр.</param>
        /// <returns>Текст для подсказки.</returns>
        private string RangeTextCaster (Parameter parameter)
        {
            return "Введите значения от " +
                    parameter.MinValue.ToString() +
                    " до " +
                    parameter.MaxValue.ToString() +
                    " мм";
        }

        /// <summary>
        /// Вспомогательный метод для генерации текста граничных условий при загрузке формы.
        /// </summary>
        /// <param name="parameter">Параметр.</param>
        /// <param name="word">Слово.</param>
        /// <returns>Текст для подсказки.</returns>
        private string TextCaster (Parameter parameter, string word)
        {
            return $"Длина {word} должна находиться в диапазоне от" +
                $" {parameter.MinValue.ToString()} до {parameter.MaxValue.ToString()} мм";
        }
    }
}
