﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace system_analysis
{

    public partial class form2_opros0 : Form
    {
        //======================================================
        // ПУТЬ К ОСНОВНОМУ РАБОЧЕМУ КАТАЛОГУ
        // ЕСЛИ МЕНЯТЬ ПУТЬ, ТО ТОЛЬКО В 1 ФОРМЕ 
        private string directory = global_class.main_directory;
        //======================================================

        // МЕТОД 0 (МЕТОД ПАРНЫХ СРАВНЕНИЙ)

        private int N = 0; // количество вопросов
        private int current = 0;
        private bool alter;
        private int sol_count = 0;
        private bool change = false;

        public struct question
        {
            public string A;
            public string B;
            public string result;
        }

        List<question> q;

        public form2_opros0()
        {
            InitializeComponent();
        }

        // кнопка НАЗАД
        private void button_back_Click(object sender, EventArgs e)
        {
            Form9_expert form = this.Owner as Form9_expert;
            if (change == true)
            {
                this.Hide();
                DialogResult otvet = MessageBox.Show(
                "Все несохраненные изменения будут потеряны.\n" +
                "Закрыть оценивание?",
                "Внимание",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2,
                MessageBoxOptions.DefaultDesktopOnly);

                if (otvet == DialogResult.Yes)
                {
                    form.Show();  // Показываем форму эксперта
                    form.TopMost = true; form.TopMost = false;
                    this.Close();
                }

                if (otvet == DialogResult.No)
                {
                    this.Show();
                    this.TopMost = true; this.TopMost = false;
                }
            }
            else
            {
                form.Show();  // Показываем форму эксперта
                form.TopMost = true; form.TopMost = false;
                this.Close();
            }
        }

        // кнопка СВЕРНУТЬ
        private void button_minimize_Click(object sender, EventArgs e)
        {
            // обеспечивает сворачивание формы при нажатии на тире
            this.WindowState = FormWindowState.Minimized;
        }

        // кнопка ЗАКРЫТЬ ОКНО
        private void button_cross_Click(object sender, EventArgs e)
        {
            button_back_Click(null, null);
        }

        // перетаскивание окна по экрану
        private void form2_opros0_MouseDown(object sender, MouseEventArgs e)
        {
            // обеспечивает перемещение формы без рамки
            base.Capture = false;
            Message m = Message.Create(base.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }

        // при ЗАГРУЗКЕ ФОРМЫ
        private void form2_opros0_Load(object sender, EventArgs e)
        {
            Form9_expert form = this.Owner as Form9_expert;
            //String[] words = СТРОКА.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);  // это на всякий под рукой
            list_solution.Items.Clear();
            change = false;

            // Заполняем проблему
            label_problems.Text = form.problem;

            string text = "";
            // считываем альтернативы
            FileInfo fileInf = new FileInfo(directory + "solutions" + form.num_problem + ".txt");
            if (fileInf.Exists)  // если файл существует вообще
            {
                using (StreamReader sr = new StreamReader(directory + "solutions" + form.num_problem + ".txt", System.Text.Encoding.UTF8))
                {
                    text = sr.ReadToEnd();
                }
            }

            if (text.Length > 0)
            {
                sol_count = 0;

                using (StreamReader sr = new StreamReader(directory + "solutions" + form.num_problem + ".txt", System.Text.Encoding.UTF8))
                {
                    while ((text = sr.ReadLine()) != null)
                    {
                        list_solution.Items.Add(text);
                        sol_count++;
                    }
                }

                alter = true;
            }
            else
            {
                alter = false;
            }

            if(alter == true)
            {
                // считаем количество вопросов
                N = (sol_count * sol_count - sol_count) / 2;
                label3.Text = " из " + N;
                question a;  // впомогательная переменная
                q = new List<question>(); // список вопросов

                //добавляем альтернативы в структуру
                for (int i = 0; i < sol_count; i++)
                {
                    for (int j = i + 1; j < sol_count; j++)
                    {
                        a.A = list_solution.Items[i].ToString();
                        a.B = list_solution.Items[j].ToString();
                        a.result = "-1";
                        q.Add(a);
                    }
                }

                // добавляем номера вопросов в комбобокс
                for (int i = 0; i < N; i++)
                {
                    comboBox_number.Items.Add(i + 1);
                }

                string path = directory + "matrix" + form.num_problem + "m0e" + form1_main.num_expert + ".txt";

                text = "";
                if (System.IO.File.Exists(path))
                {
                    using (StreamReader sr = new StreamReader(path, System.Text.Encoding.UTF8))
                    {
                        text = sr.ReadToEnd();
                    }
                }

                //если матрица существует и там чето даже есть
                if (text.Length > 0)
                {
                    string[] words;
                    string[,] matr = new string[sol_count, sol_count];

                    //
                    using (StreamReader sr = new StreamReader(path, System.Text.Encoding.UTF8))
                    {
                        string line = "";
                        int i = 0;
                        while ((line = sr.ReadLine()) != null)
                        {
                            words = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                            for (int j = 0; j < sol_count; j++)
                            {
                                matr[i, j] = words[j];
                            }
                            i++;
                        }
                    }

                    int count = 0;
                    for (int i = 0; i < sol_count-1; i++)// до предпоследней строки
                    {
                        for (int j = 0; j < sol_count; j++)
                        {
                            if(i < j)
                            {
                                question b = q[count];
                                b.result = matr[i, j];
                                q[count] = b;
                                count++;
                            }
                        }
                    }
                }

                // выбираем первый вопрос (индекс 0)
                comboBox_number.SelectedIndex = 0;
            }
        }
    
        // при выборе ПУНКТ1
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (current >= 0 && current < sol_count)
            {
                question a = q[current];
                a.result = "100";
                q[current] = a;
            }
        }

        // при выборе ПУНКТ2
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (current >= 0 && current < sol_count)
            {
                question a = q[current];
                a.result = "0";
                q[current] = a;
            }
        }

        // при выборе ПУНКТ3
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (current >= 0 && current < sol_count)
            {
                question a = q[current];
                a.result = "50";
                q[current] = a;
            }
        }

        // кнопка > (СЛЕДУЮЩИЙ ВОПРОС)
        private void btn_next_Click(object sender, EventArgs e)
        {
            
        }

        // кнопка < (ПРЕДЫДУЩИЙ ВОПРОС)
        private void btn_prev_Click(object sender, EventArgs e)
        {
            if (comboBox_number.SelectedIndex >= 0 && comboBox_number.SelectedIndex < sol_count)
            {
                int index = comboBox_number.SelectedIndex;
                if (index == 0)
                {
                    comboBox_number.SelectedIndex = sol_count - 1;
                }
                else
                {
                    comboBox_number.SelectedIndex = index + 1;
                }
            }
        }

        // ФУНКЦИЯ СОХРАНЕНИЯ в файл matrix...
        private void save()
        {
            Form9_expert form = this.Owner as Form9_expert;

            string path = directory + "matrix" + form.num_problem + "m0e" + form1_main.num_expert + ".txt";
            
            int n = list_solution.Items.Count;

            string[,] arr = new string[n, n];
            int count = 0;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++, count++)
                {
                    if (i == j)
                    {
                        arr[i, j] = "9";
                        count--;
                    }
                    else
                    {
                        if (i > j)
                        {
                            count--;
                            if (arr[j, i] == "1")
                                arr[i, j] = "0";

                            if (arr[j, i] == "0")
                                arr[i, j] = "1";

                            if (arr[j, i] == "0,5" || arr[j, i] == "0.5")
                                arr[i, j] = "0,5";

                            if (arr[j, i] == "-1")
                                arr[i, j] = "-1";
                            count--;

                        }
                        else
                            arr[i, j] = q[count].result;
                    }
                }
            }
            
            using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.UTF8))
            {
                string matrix = ""; 
                for (int i = 0; i < n; i++)
                {
                    matrix = "";
                    for (int j = 0; j < n; j++)
                    {
                        matrix += arr[i, j] + " ";
                    }
                    sw.WriteLine(matrix);
                }
            }
        }

        // кнопка СОХРАНИТЬ
        private void btn_save_Click(object sender, EventArgs e)
        {
            if (start)
            {
                Form9_expert form = this.Owner as Form9_expert;

                bool end = true;
                for (int i = 0; i < q.Count; i++)
                    if (q[i].result == "-1")
                        end = false;

                if (!end)
                {
                    this.Hide();
                    DialogResult otvet = MessageBox.Show(
                    "Вы не прошли опрос до конца.\n" +
                    "Можете вернуться и продолжить в любое время.\n" +
                    "Сохранить?",
                    "Внимание",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
                    if (otvet == DialogResult.Yes)
                    {
                        start = false;
                        // сохраняем в файл matrix...
                        save();
                        //============================================
                        // в форме эксперта обновляем данные на ней
                        form.list_prob[form.N].exp[form.E].m0 = -1; // не закончил проходить
                        form.save_group(); // сохраняем измененное в файл group...
                        form.update(form.N, form.E);  // обновляем на 9 форме 
                        //============================================
                        radioButton1.Checked = false;
                        radioButton2.Checked = false;
                        radioButton3.Checked = false;

                        label3.Text = " из N";
                        textBox2.Text = "";
                        textBox3.Text = "";

                        //==================================
                        form.Show();
                        form.TopMost = true; form.TopMost = false;
                        this.Close();
                        //===================================
                    }
                    if (otvet == DialogResult.No)
                    {
                        this.Show();
                        this.TopMost = true; this.TopMost = false;
                    }
                        
                }
                else
                {
                    start = false;
                    // сохраняем в файл matrix...
                    save();
                    //============================================
                    // в форме эксперта обновляем данные на ней
                    form.list_prob[form.N].exp[form.E].m0 = 1; // не закончил проходить
                    form.save_group(); // сохраняем измененное в файл group...
                    form.update(form.N, form.E);  // обновляем на 9 форме 
                    //============================================

                    MessageBox.Show(
                        "Изменения сохранены!",
                        "Сохранено",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    //==================================
                    form.Show();
                    form.TopMost = true; form.TopMost = false;
                    this.Close();
                    //===================================
                }
            }
        }

        // при выборе НОМЕРА ВОПРОСА 
        private void comboBox_number_SelectedIndexChanged(object sender, EventArgs e)
        {
            current = comboBox_number.SelectedIndex;
            if (current >= 0 && current < sol_count)
            {
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                radioButton3.Checked = false;

                textBox2.Text = q[current].A;
                textBox3.Text = q[current].B;

                switch (q[current].result)
                {
                    case "100":
                        radioButton1.Checked = true;
                        break;
                    case "0":
                        radioButton2.Checked = true;
                        break;
                    case "50":
                        radioButton3.Checked = true;
                        break;
                    default:
                        break;
                }
            }
            
        }

    }
}
