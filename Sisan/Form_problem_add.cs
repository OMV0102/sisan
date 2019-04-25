using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace system_analysis
{
    public partial class Form_problem_add : Form
    {
        public Form_problem_add()
        {
            InitializeComponent();
        }

        private void btn_edit_ok_Click(object sender, EventArgs e)
        {
            form3_analyst_add owner = this.Owner as form3_analyst_add;
            if (owner != null)
            {
                Regex rgx1 = new Regex(@"^\s+\w*\s*", RegexOptions.IgnoreCase);   // пробел1+ слово0 пробел0+
                Regex rgx2 = new Regex(@"\w+\s*", RegexOptions.IgnoreCase); // слово1+ пробел0+
                MatchCollection matches1 = rgx1.Matches(textBox1.Text); // число совпадений с rgx1
                MatchCollection matches2 = rgx2.Matches(textBox1.Text);  // число совпадений с rgx1

                if (textBox1.Text == "")  // ТОЛЬКО ИЗ ЧИСЕЛ ВОЗМОЖНА (доработать)
                {
                    Close();  // тупо закрыть
                    /*MessageBox.Show(
                    "Введите проблемы или нажмите клавишу \"Отмена\".",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
                    this.TopMost = true; this.TopMost = false;*/
                }
                else
                if (matches1.Count > 0)
                {
                    MessageBox.Show(
                    "Уберите лишние пробелы в начале ввода проблемы!",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
                    this.TopMost = true; this.TopMost = false;
                }
                else
                    if (matches2.Count < 1)
                    {
                        MessageBox.Show(
                        "Проблема введена некорректно!",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly);
                        this.TopMost = true; this.TopMost = false;
                    }
                    else
                    {
                        string selectedState;
                        String[] words;

                        if (owner.edit_or_add == false)
                        {
                            selectedState = textBox1.Text;
                            words = selectedState.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            selectedState = "";

                            char c = words[0].First();
                            if (char.IsDigit(c))
                            {
                                words[0] = Convert.ToString(owner.comboBox_problems.Items.Count);
                                for (int i = 0; i < words.Length; i++)
                                {
                                    selectedState += words[i] + " ";

                                }
                                owner.comboBox_problems.Items.Add(selectedState);
                            }
                            else
                            {

                                selectedState = Convert.ToString(owner.comboBox_problems.Items.Count) + " ";
                                for (int i = 0; i < words.Length; i++)
                                {
                                    selectedState += words[i] + " ";

                                }

                                owner.comboBox_problems.Items.Add(selectedState);
                            }

                        }

                        if (owner.edit_or_add == true)
                        {
                            int index = owner.comboBox_problems.SelectedIndex;
                        
                            selectedState = textBox1.Text;
                            words = selectedState.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            selectedState = "";
                            char c = Convert.ToChar(words[0]);
                            if (char.IsDigit(c))
                            {
                                words[0] = Convert.ToString(owner.comboBox_problems.SelectedIndex);
                                for (int i = 0; i < words.Length; i++)
                                {
                                    selectedState += words[i] + " ";

                                }
                                owner.comboBox_problems.Items.Insert(index, selectedState);
                                owner.comboBox_problems.Items.RemoveAt(index + 1);
                            }
                            else
                            {
                                
                                selectedState = Convert.ToString(index) + " ";
                                for (int i = 0; i < words.Length; i++)
                                {
                                    selectedState += words[i] + " ";

                                }

                                owner.comboBox_problems.Items.Insert(index, selectedState);
                                owner.comboBox_problems.Items.RemoveAt(index + 1);
                            }
                        }
                        owner.TopMost = true; owner.TopMost = false;
                        this.Close();
                    }

            }
        }

        private void btn_edit_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form_problem_add_Load(object sender, EventArgs e)
        {
            form3_analyst_add owner = this.Owner as form3_analyst_add;
            if (owner != null)
            {
                if(owner.edit_or_add == true)
                {
                    int index = owner.comboBox_problems.SelectedIndex;
                    textBox1.Text = Convert.ToString(owner.comboBox_problems.Items[index]);
                }
                this.TopMost = true; this.TopMost = false;


            }
        }
    }
}
