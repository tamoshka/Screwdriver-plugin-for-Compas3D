using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ScrewdriverPlugin
{
    /// <summary>
    /// Класс MainForm
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Поле хранящее в себе объект класса Builder
        /// </summary>
        private Builder _builder=new Builder();

        /// <summary>
        /// Поле хранящее в себе объект класса Parameters
        /// </summary>
        private Parameters _parameters = new Parameters();

        /// <summary>
        /// Конструктор класса MainForm
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Инициализация ряда параметров при загрузке формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            _parameters.AllParameters = new Dictionary<ParameterType, Parameter>();
            ComboBoxShapeOfHandle.SelectedIndex =1;
            ComboBoxShapeOfRod.SelectedIndex = 1;
            // Set up the ToolTip text for the Button and Checkbox.
            toolTip1.SetToolTip(this.TextBoxRodLength, 
                "Длина наконечника должна находиться в диапазоне от 45 до 500 мм");
            toolTip1.SetToolTip(this.TextBoxRodWidth, 
                "Диаметр наконечника должен находиться в диапазоне пятой части от длины отвёртки" +
                " +/- 2 мм");
            toolTip1.SetToolTip(this.TextBoxHandleWidth, 
                "Диаметр ручки должен находиться в диапазоне четверти от длины ручки +/- 5 мм");
            toolTip1.SetToolTip(this.TextBoxHandleLength, 
                "Длина ручки должна находиться в диапазоне от 45 до 150 мм");
        }

        /// <summary>
        /// Обработчик нажатия на кнопку "Создать"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonCreate_Click(object sender, EventArgs e)
        {
            if (TextBoxRodLength.BackColor == Color.Red || 
                TextBoxHandleLength.BackColor == Color.Red || 
                TextBoxRodWidth.BackColor == Color.Red || 
                TextBoxHandleWidth.BackColor == Color.Red || 
                TextBoxRodLength.BackColor == SystemColors.Window || 
                TextBoxRodWidth.BackColor == SystemColors.Window || 
                TextBoxHandleWidth.BackColor == SystemColors.Window || 
                TextBoxHandleLength.BackColor == SystemColors.Window)
            {
                
            }
            else
            {
                _builder.Build(_parameters);
            }
        }

        /// <summary>
        /// Обработчик выхода из текстбокса "Длина ручки"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxHandleLength_Leave(object sender, EventArgs e)
        {
            ParameterType parameterType = ParameterType.HandleLength;
            FirstValidate(TextBoxHandleLength, parameterType);
            if (TextBoxHandleLength.BackColor != SystemColors.Window)
            {
                SecondValidate(TextBoxHandleLength, parameterType);
                FirstValidate(TextBoxHandleWidth, ParameterType.HandleWidth);
                if (TextBoxHandleWidth.BackColor != SystemColors.Window)
                {
                    SecondValidate(TextBoxHandleWidth, ParameterType.HandleWidth);
                }
                FirstValidate(TextBoxRodLength, ParameterType.RodLength);
                if (TextBoxRodLength.BackColor != SystemColors.Window)
                {
                    SecondValidate(TextBoxRodLength, ParameterType.RodLength);
                }
            }
        }

        /// <summary>
        /// Обработчик выхода из текстбокса "Диаметр ручки"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxHandleWidth_Leave(object sender, EventArgs e)
        {
            ParameterType parameterType = ParameterType.HandleWidth;
            FirstValidate(TextBoxHandleWidth, parameterType);
            if (TextBoxHandleWidth.BackColor != SystemColors.Window)
            {
                SecondValidate(TextBoxHandleWidth, parameterType);
                FirstValidate(TextBoxRodWidth, ParameterType.RodWidth);
                if (TextBoxRodWidth.BackColor != SystemColors.Window)
                {
                    SecondValidate(TextBoxRodWidth, ParameterType.RodWidth);
                }
                FirstValidate(TextBoxHandleLength, ParameterType.HandleLength);
                if (TextBoxHandleLength.BackColor != SystemColors.Window)
                {
                    SecondValidate(TextBoxHandleLength, 
                        ParameterType.HandleLength);
                }
            }
        }

        /// <summary>
        /// Обработчик выхода из текстбокса "Длина наконечника"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxRodLength_Leave(object sender, EventArgs e)
        {
            ParameterType parameterType = ParameterType.RodLength;
            FirstValidate(TextBoxRodLength, parameterType);
            if (TextBoxRodLength.BackColor != SystemColors.Window)
            {
                SecondValidate(TextBoxRodLength, parameterType);
                FirstValidate(TextBoxHandleLength, ParameterType.HandleLength);
                if (TextBoxHandleLength.BackColor != SystemColors.Window)
                {
                    SecondValidate(TextBoxHandleLength, ParameterType.HandleLength);
                }
            }
        }

        /// <summary>
        /// /// Обработчик выхода из текстбокса "Диаметр наконечника"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxRodWidth_Leave(object sender, EventArgs e)
        {
            ParameterType parameterType = ParameterType.RodWidth;
            FirstValidate(TextBoxRodWidth, parameterType);
            if (TextBoxRodWidth.BackColor != SystemColors.Window)
            {
                SecondValidate(TextBoxRodWidth, parameterType);
                FirstValidate(TextBoxHandleWidth, ParameterType.HandleWidth);
                if (TextBoxHandleWidth.BackColor != SystemColors.Window)
                {
                    SecondValidate(TextBoxHandleWidth, ParameterType.HandleWidth);
                }
            }
        }

        /// <summary>
        /// Первичная валидация (проверка на введение в текстбоксы целых чисел
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="parameterType"></param>
        private void FirstValidate(System.Windows.Forms.TextBox textBox, 
            ParameterType parameterType)
        {
            try
            {
                int.Parse(textBox.Text);
                SetColors(parameterType, 3, 0, "");
            }
            catch (Exception e)
            {
                textBox.Text = "";
                SetColors(parameterType, 1, 0, e.Message);
            }
        }

        /// <summary>
        /// Вспомогательный метод для установки цвета для текстбокса
        /// </summary>
        /// <param name="parameterType">Тип параметра</param>
        /// <param name="whatColor">Устанавливаемый цвет</param>
        /// <param name="whatReason">Причина установки цвета</param>
        /// <param name="text">Текст устанавливаемый в подсказку</param>
        private void SetColors(ParameterType parameterType, int whatColor, int whatReason, 
            string text)
        {
            if (whatColor == 1)
            {
                if (parameterType == ParameterType.HandleLength)
                {
                    TextBoxHandleLength.BackColor = SystemColors.Window;
                    toolTip1.SetToolTip(this.TextBoxHandleLength, 
                        "Длина ручки должна находиться в диапазоне от 45 до 150 мм");
                }
                else if (parameterType == ParameterType.HandleWidth)
                {
                    TextBoxHandleWidth.BackColor = SystemColors.Window;
                    toolTip1.SetToolTip(this.TextBoxHandleWidth, 
                        "Диаметр ручки должен находиться в диапазоне четверти от длины ручки " +
                        "+/- 5 мм");
                }
                else if (parameterType == ParameterType.RodLength)
                {
                    TextBoxRodLength.BackColor = SystemColors.Window;
                    toolTip1.SetToolTip(this.TextBoxRodLength, 
                        "Длина наконечника должна находиться в диапазоне от 45 до 500 мм");
                }
                else if (parameterType == ParameterType.RodWidth)
                {
                    TextBoxRodWidth.BackColor = SystemColors.Window;
                    toolTip1.SetToolTip(this.TextBoxRodWidth, 
                        "Диаметр наконечника должен находиться в диапазоне пятой части от " +
                        "длины отвёртки +/- 2 мм");
                }
            }
            else if (whatColor == 2) 
            {
                if (parameterType == ParameterType.HandleLength)
                {
                    TextBoxHandleLength.BackColor = Color.Red;
                    if (whatReason == 1)
                    {
                        toolTip1.SetToolTip(this.TextBoxHandleLength, 
                            "Длина ручки должна находиться в диапазоне от 45 до 150 мм");
                    }
                    else
                    {
                        toolTip1.SetToolTip(this.TextBoxHandleLength, text);
                    }
                }
                else if (parameterType == ParameterType.HandleWidth)
                {
                    TextBoxHandleWidth.BackColor = Color.Red;
                    if (whatReason == 1)
                    {
                        toolTip1.SetToolTip(this.TextBoxHandleWidth, 
                            "Диаметр ручки должен находиться в диапазоне четверти " +
                            "от длины ручки +/- 5 мм");
                    }
                    else
                    {
                        toolTip1.SetToolTip(this.TextBoxHandleWidth, text);
                    }
                }
                else if (parameterType == ParameterType.RodLength)
                {
                    TextBoxRodLength.BackColor = Color.Red;
                    if (whatReason == 1)
                    {
                        toolTip1.SetToolTip(this.TextBoxRodLength, 
                            "Длина наконечника должна находиться в диапазоне от 45 до 500 мм");
                    }
                    else
                    {
                        toolTip1.SetToolTip(this.TextBoxRodLength, text);
                    }
                }
                else if (parameterType == ParameterType.RodWidth)
                {
                    TextBoxRodWidth.BackColor = Color.Red;
                    if (whatReason == 1)
                    {
                        toolTip1.SetToolTip(this.TextBoxRodWidth, 
                            "Диаметр наконечника должен находиться в диапазоне пятой части " +
                            "от длины отвёртки +/- 2 мм");
                    }
                    else
                    {
                        toolTip1.SetToolTip(this.TextBoxRodWidth, text);
                    }
                    
                }
            }
            else
            {
                if (parameterType == ParameterType.HandleLength)
                {
                    TextBoxHandleLength.BackColor = Color.Green;
                    toolTip1.SetToolTip(TextBoxHandleLength, null);
                }
                else if (parameterType == ParameterType.HandleWidth)
                {
                    TextBoxHandleWidth.BackColor = Color.Green;
                    toolTip1.SetToolTip(TextBoxHandleLength, null);
                }
                else if (parameterType == ParameterType.RodLength)
                {
                    TextBoxRodLength.BackColor = Color.Green;
                    toolTip1.SetToolTip(TextBoxRodLength, null);
                }
                else if (parameterType == ParameterType.RodWidth)
                {
                    TextBoxRodWidth.BackColor = Color.Green;
                    toolTip1.SetToolTip(TextBoxRodWidth, null);
                }
            }
        }

        /// <summary>
        /// Вторичная валидация, попытка создания параметра, попытка добавления 
        /// корректного параметра в словарь
        /// </summary>
        /// <param name="textBox">Используемый текстбокс</param>
        /// <param name="parameterType">Тип параметра</param>
        private void SecondValidate(System.Windows.Forms.TextBox textBox, 
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
                parameter.Value=int.Parse(textBox.Text);
            }
            catch (Exception e)
            {
                SetColors(parameterType, 2, 1, e.Message);
                cached = true;
            }
            if (!cached)
            {
                try
                {
                    _parameters.SetParameter(parameterType, parameter);
                    SetColors(parameterType, 3, 0, "");
                }
                catch (Exception e)
                {
                    SetColors(parameterType, 2, 0, e.Message);
                }
            }
        }

        /// <summary>
        /// Обработчик изменения выбранного индекса у комбобокса форма ручки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxShapeOfHandle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBoxShapeOfHandle.SelectedIndex==0)
            {
                _parameters.ShapeOfHandle = HandleType.Cylinder;
            }
            else
            {
                _parameters.ShapeOfHandle = HandleType.Prisme;
            }
        }

        /// <summary>
        /// Обработчик изменения выбранного индекса у комбобокса форма наконечника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxShapeOfRod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBoxShapeOfRod.SelectedIndex == 0)
            {
                _parameters.ShapeOfRod = RodType.Cruciform;
            }
            else
            {
                _parameters.ShapeOfRod = RodType.Flat;
            }
        }
    }
}
