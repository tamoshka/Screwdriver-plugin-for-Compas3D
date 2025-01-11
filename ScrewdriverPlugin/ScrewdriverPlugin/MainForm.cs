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
                    (ParameterType parameterType,
                    TextBox firstChainedTextBox,
                    ParameterType firstChainedParameterType,
                    TextBox secondChainedTextBox,
                    ParameterType secondChainedParameterType)>
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
            this.Validate(textBox, chained.parameterType);
            if (textBox.BackColor != SystemColors.Window)
            {
                this.Validate(chained.firstChainedTextBox, chained.firstChainedParameterType);
                this.Validate(chained.secondChainedTextBox, chained.secondChainedParameterType);
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
                new Dictionary<TextBox,
                (ParameterType parameterType,
                TextBox chainedTextBox,
                ParameterType chainedParameterType)>
            {
                {
                    this.TextBoxRodLength,
                    (ParameterType.RodLength,
                    this.TextBoxHandleLength,
                    ParameterType.HandleLength)
                },
                {
                    this.TextBoxRodWidth,
                    (ParameterType.RodWidth,
                    this.TextBoxHandleWidth,
                    ParameterType.HandleWidth)
                },
            };

            TextBox textBox = (TextBox)sender;
            var chained = rodTextBoxesChainedParameters[textBox];
            this.Validate(textBox, chained.parameterType);
            if (textBox.BackColor != SystemColors.Window)
            {
                this.Validate(chained.chainedTextBox, chained.chainedParameterType);
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
             //TODO: refactor +
            Color whatColor,
            string text)
        {
            textBox.BackColor = whatColor;
            if (whatColor == SystemColors.Window)
            {
                var message = textBox.Text != string.Empty
                    ? "Доступны только целочисленные значения"
                    //TODO: duplication +
                    : this.RangeTextCaster(parameter);
                this.toolTip1.SetToolTip(textBox, message);
                textBox.Text = string.Empty;
            }
            else
            {
                this.toolTip1.SetToolTip(textBox, text);
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
                    Color.Green,
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
                    SystemColors.Window,
                    message);
            }
            catch (ValueException)
            {
                string toolTipText =
                    this.RangeTextCaster(this._parameters.AllParameters[parameterType]);
                this.SetColors(
                    textBox,
                    this._parameters.AllParameters[parameterType],
                    Color.Red,
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
                    Color.Red,
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
            string toolTipRodLengthText = this.TextLengthCaster(rodLength, "наконечника");
            this.toolTip1.SetToolTip(this.TextBoxRodLength, toolTipRodLengthText);
            string toolTipRodWidthDefaultText =
                this.TextWidthCaster("наконечника", "одной второй", "2", "диаметра");
            this.toolTip1.SetToolTip(this.TextBoxRodWidth, toolTipRodWidthDefaultText);
            string toolTipHandleWidthDefaultText =
                this.TextWidthCaster("ручки", "четверти", "5", "длины");
            this.toolTip1.SetToolTip(this.TextBoxHandleWidth, toolTipHandleWidthDefaultText);
            Parameter handleLength = this._parameters.AllParameters[ParameterType.HandleLength];
            //TODO: duplication +
            string toolTipHandleLengthText = this.TextLengthCaster(handleLength, "ручки");
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
        /// Вспомогательный метод для генерации текста для граничных условий длины при загрузке формы.
        /// </summary>
        /// <param name="parameter">Параметр.</param>
        /// <param name="part">Слово обозначающее элемент детали.</param>
        /// <returns>Текст для подсказки.</returns>
        private string TextLengthCaster (Parameter parameter, string part)
        {
            return $"Длина {part} должна находиться в диапазоне от" +
                $" {parameter.MinValue.ToString()} до {parameter.MaxValue.ToString()} мм";
        }

        /// <summary>
        /// Вспомогательный метод для генерации текста для граничных условий диаметра при загрузке формы.
        /// </summary>
        /// <param name="part">Слово обозначающее элемент детали.</param>
        /// <param name="scale">Слово обозначающее соотношение элементов деталей.</param>
        /// <param name="deviation">Допустимая погрешность для элемента.</param>
        /// <param name="type">Тип параметра от которого зависит основной.</param>
        /// <returns>Текст для подсказки.</returns>
        private string TextWidthCaster(string part, string scale, string deviation, string type)
        {
            return $"Диаметр {part} должен находиться в диапазоне {scale}" +
                $" от {type} ручки +/- {deviation} мм";
        }
    }
}