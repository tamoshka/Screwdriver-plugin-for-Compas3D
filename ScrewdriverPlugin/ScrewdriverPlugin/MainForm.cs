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
    public partial class MainForm : Form
    {
        private Builder _builder=new Builder();
        private Parameters _parameters = new Parameters();

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _parameters.AllParameters = new Dictionary<ParameterType, Parameter>();
            ComboBoxShapeOfHandle.SelectedIndex =1;
            ComboBoxShapeOfRod.SelectedIndex = 1;
            // Set up the ToolTip text for the Button and Checkbox.
            toolTip1.SetToolTip(this.TextBoxRodLength, "Длина наконечника должна находиться в диапазоне от 45 до 500 мм");
            toolTip1.SetToolTip(this.TextBoxRodWidth, "Диаметр наконечника должен находиться в диапазоне пятой части от длины отвёртки +/- 2 мм");
            toolTip1.SetToolTip(this.TextBoxHandleWidth, "Диаметр ручки должен находиться в диапазоне четверти от длины ручки +/- 5 мм");
            toolTip1.SetToolTip(this.TextBoxHandleLength, "Длина ручки должна находиться в диапазоне от 45 до 150 мм");
        }

        private void ButtonCreate_Click(object sender, EventArgs e)
        {
            if (TextBoxRodLength.BackColor == Color.Red || TextBoxHandleLength.BackColor == Color.Red || TextBoxRodWidth.BackColor == Color.Red 
                || TextBoxHandleWidth.BackColor == Color.Red || TextBoxRodLength.BackColor == SystemColors.Window || TextBoxRodWidth.BackColor == SystemColors.Window 
                || TextBoxHandleWidth.BackColor == SystemColors.Window || TextBoxHandleLength.BackColor == SystemColors.Window)
            {
                
            }
            else
            {
                _builder.Build(_parameters);
            }
        }

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
                    SecondValidate(TextBoxHandleLength, ParameterType.HandleLength);
                }
            }
        }

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

        private void FirstValidate(System.Windows.Forms.TextBox textBox, ParameterType parameterType)
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

        private void SetColors(ParameterType parameterType, int whatColor, int whatReason, string text)
        {
            if (whatColor == 1)
            {
                if (parameterType == ParameterType.HandleLength)
                {
                    TextBoxHandleLength.BackColor = SystemColors.Window;
                    toolTip1.SetToolTip(this.TextBoxHandleLength, "Длина ручки должна находиться в диапазоне от 45 до 150 мм");
                }
                else if (parameterType == ParameterType.HandleWidth)
                {
                    TextBoxHandleWidth.BackColor = SystemColors.Window;
                    toolTip1.SetToolTip(this.TextBoxHandleWidth, "Диаметр ручки должен находиться в диапазоне четверти от длины ручки +/- 5 мм");
                }
                else if (parameterType == ParameterType.RodLength)
                {
                    TextBoxRodLength.BackColor = SystemColors.Window;
                    toolTip1.SetToolTip(this.TextBoxRodLength, "Длина наконечника должна находиться в диапазоне от 45 до 500 мм");
                }
                else if (parameterType == ParameterType.RodWidth)
                {
                    TextBoxRodWidth.BackColor = SystemColors.Window;
                    toolTip1.SetToolTip(this.TextBoxRodWidth, "Диаметр наконечника должен находиться в диапазоне пятой части от длины отвёртки +/- 2 мм");
                }
            }
            else if (whatColor == 2) 
            {
                if (parameterType == ParameterType.HandleLength)
                {
                    if (whatReason == 1)
                    {
                        TextBoxHandleLength.BackColor = Color.Red;
                        toolTip1.SetToolTip(this.TextBoxHandleLength, "Длина ручки должна находиться в диапазоне от 45 до 150 мм");
                    }
                    else
                    {
                        TextBoxHandleLength.BackColor = Color.Red;
                        toolTip1.SetToolTip(this.TextBoxHandleLength, text);
                        /*if (TextBoxHandleWidth.BackColor != SystemColors.Window)
                        {
                            TextBoxHandleWidth.BackColor = Color.Red;
                            toolTip1.SetToolTip(this.TextBoxHandleWidth, "Диаметр ручки должен находиться в диапазоне четверти от длины ручки +/- 5 мм");
                        }
                        if (TextBoxRodLength.BackColor != SystemColors.Window)
                        {
                            TextBoxRodLength.BackColor = Color.Red;
                            toolTip1.SetToolTip(this.TextBoxRodLength, "Длина наконечника должна находиться в диапазоне от 45 до 500 мм");
                        }
                        if (TextBoxRodWidth.BackColor != SystemColors.Window)
                        {
                            TextBoxRodWidth.BackColor = Color.Red;
                            toolTip1.SetToolTip(this.TextBoxRodWidth, "Диаметр наконечника должен находиться в диапазоне пятой части от длины отвёртки +/- 2 мм");
                        }*/
                    }
                }
                else if (parameterType == ParameterType.HandleWidth)
                {
                    if (whatReason == 1)
                    {
                        TextBoxHandleWidth.BackColor = Color.Red;
                        toolTip1.SetToolTip(this.TextBoxHandleWidth, "Диаметр ручки должен находиться в диапазоне четверти от длины ручки +/- 5 мм");
                    }
                    else
                    {
                        TextBoxHandleWidth.BackColor = Color.Red;
                        toolTip1.SetToolTip(this.TextBoxHandleWidth, text);
                        /*if (TextBoxRodWidth.BackColor != SystemColors.Window)
                        {
                            TextBoxRodWidth.BackColor = Color.Red;
                            toolTip1.SetToolTip(this.TextBoxRodWidth, "Диаметр наконечника должен находиться в диапазоне пятой части от длины отвёртки +/- 2 мм");
                        }*/
                    }
                }
                else if (parameterType == ParameterType.RodLength)
                {
                    if (whatReason == 1)
                    {
                        TextBoxRodLength.BackColor = Color.Red;
                        toolTip1.SetToolTip(this.TextBoxRodLength, "Длина наконечника должна находиться в диапазоне от 45 до 500 мм");
                    }
                    else
                    {
                        TextBoxRodLength.BackColor = Color.Red;
                        toolTip1.SetToolTip(this.TextBoxRodLength, text);
                        /*if (TextBoxRodWidth.BackColor != SystemColors.Window)
                        {
                            TextBoxRodWidth.BackColor = Color.Red;
                            toolTip1.SetToolTip(this.TextBoxRodWidth, "Диаметр наконечника должен находиться в диапазоне пятой части от длины отвёртки +/- 2 мм");
                        }*/
                    }
                }
                else if (parameterType == ParameterType.RodWidth)
                {
                    if (whatReason == 1)
                    {
                        TextBoxRodWidth.BackColor = Color.Red;
                        toolTip1.SetToolTip(this.TextBoxRodWidth, "Диаметр наконечника должен находиться в диапазоне пятой части от длины отвёртки +/- 2 мм");
                    }
                    else
                    {
                        TextBoxRodWidth.BackColor = Color.Red;
                        toolTip1.SetToolTip(this.TextBoxRodWidth, text);
                    }
                    
                }
            }
            else
            {
                if (parameterType == ParameterType.HandleLength)
                {
                    if (whatReason == 1)
                    {
                        TextBoxHandleLength.BackColor = Color.Green;
                        toolTip1.SetToolTip(TextBoxHandleLength, null);
                    }
                    else
                    {
                        TextBoxHandleLength.BackColor = Color.Green;
                        toolTip1.SetToolTip(TextBoxHandleLength, null);
                        /*if (TextBoxHandleWidth.BackColor!=SystemColors.Window)
                        {
                            TextBoxHandleWidth.BackColor = Color.Green;
                            toolTip1.SetToolTip(TextBoxHandleWidth, null);
                        }
                        if (TextBoxRodLength.BackColor != SystemColors.Window)
                        {
                            TextBoxRodLength.BackColor = Color.Green;
                            toolTip1.SetToolTip(TextBoxRodLength, null);
                        }
                        if (TextBoxRodWidth.BackColor != SystemColors.Window)
                        {
                            TextBoxRodWidth.BackColor = Color.Green;
                            toolTip1.SetToolTip(TextBoxRodWidth, null);
                        }*/
                    }
                }
                else if (parameterType == ParameterType.HandleWidth)
                {
                    if (whatReason == 1)
                    {
                        TextBoxHandleWidth.BackColor = Color.Green;
                        toolTip1.SetToolTip(TextBoxHandleWidth, null);
                    }
                    else
                    {
                        TextBoxHandleWidth.BackColor = Color.Green;
                        toolTip1.SetToolTip(TextBoxHandleLength, null);
                        /*if (TextBoxRodWidth.BackColor != SystemColors.Window)
                        {
                            TextBoxRodWidth.BackColor = Color.Green;
                            toolTip1.SetToolTip(TextBoxRodWidth, null);
                        }*/
                    }
                }
                else if (parameterType == ParameterType.RodLength)
                {
                    if (whatReason == 1)
                    {
                        TextBoxRodLength.BackColor = Color.Green;
                        toolTip1.SetToolTip(TextBoxRodLength, null);
                    }
                    else 
                    {
                        TextBoxRodLength.BackColor = Color.Green;
                        toolTip1.SetToolTip(TextBoxHandleLength, null);
                        /*if (TextBoxRodWidth.BackColor != SystemColors.Window)
                        {
                            TextBoxRodWidth.BackColor = Color.Green;
                            toolTip1.SetToolTip(TextBoxRodWidth, null);
                        }*/
                    }
                }
                else if (parameterType == ParameterType.RodWidth)
                {
                    if (whatReason == 1)
                    {
                        TextBoxRodWidth.BackColor = Color.Green;
                        toolTip1.SetToolTip(TextBoxRodWidth, null);
                    }
                    else
                    {
                        TextBoxRodWidth.BackColor = Color.Green;
                        toolTip1.SetToolTip(TextBoxRodWidth, null);
                    }
                }
            }
        }

        private void SecondValidate(System.Windows.Forms.TextBox textBox, ParameterType parameterType)
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
                parameter.MaxValue = 43;
                parameter.MinValue = 10;
            }
            else if (parameterType == ParameterType.RodLength)
            {
                parameter.MaxValue = 500;
                parameter.MinValue = 45;
            }
            else if (parameterType == ParameterType.RodWidth)
            {
                parameter.MaxValue = 22;
                parameter.MinValue = 5;
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

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
