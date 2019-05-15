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
    public partial class Form13_full_pair : Form
    {

        public Form13_full_pair()
        {
            InitializeComponent();
        }

        //======================================================
        // ПУТЬ К ОСНОВНОМУ РАБОЧЕМУ КАТАЛОГУ
        // ЕСЛИ МЕНЯТЬ ПУТЬ, ТО ТОЛЬКО В 1 ФОРМЕ 
        private string directory = global_class.main_directory;
        //======================================================

        // МЕТОД 4 (МЕТОД ПОПАРНОГО ПОПАРНОГО СОПОСТАВЛЕНИЯ)

        private bool correct = false; // флаг правильных значений ячеек
        private bool change = false; // флаг внесенных изменений
        private bool empty = false; // флаг пустых значений ячеек
        private bool is_edit; // флаг, что после редактирования не проверяем повторно ячейку
        private int exp_count = 0; // количество экспертов
        private int sol_count; // количество альтернатив про выбранной проблеме
        private int q_count = 0; // количество вопросов
        private int max = 100; // ШКАЛА, по которой оценивается КАЖДАЯ АЛЬТЕРНАТИВА
        private bool is_yellow = false;
        private bool edit_on = false;


        public struct question
        {
            public string A;
            public string B;
            public int res_A;
            public int res_B;
        }
        List<question> q_list;// список с оценками экспертов
        
        // при ЗАГРУЗКЕ ФОРМЫ 
        private void Form11_rang_Load(object sender, EventArgs e)
        {
            Form9_expert form = this.Owner as Form9_expert;

            label9.Text = "нужно распределить " + max + " баллов между двумя альтернативами.";
            label_problem.Text = form.problem; // проблему вывели в форму
            exp_count = form.list_prob[form.N].exp.Count();  // узнали сколько всего экспертов
            
            bool exists;
            // читаем альтернатвы
            string text = "";
            FileInfo fileInf = new FileInfo(directory + "solutions" + form.num_problem + ".txt");
            if (fileInf.Exists)
            {
                using (StreamReader sr = new StreamReader(directory + "solutions" + form.num_problem + ".txt", System.Text.Encoding.UTF8))
                {
                    text = sr.ReadToEnd();
                }
            }

            sol_count = 0;
            bool alter = false;

            if (text.Length > 0)
            {

                using (StreamReader sr = new StreamReader(directory + "solutions" + form.num_problem + ".txt", System.Text.Encoding.UTF8))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        list_solution.Items.Add(line);
                        sol_count++;
                    }
                }
                alter = true;
            }
            else
            {
                alter = false;
            }

            if (alter == true)
            {
                // считаем количество вопросов
                q_count = (sol_count * sol_count - sol_count) / 2;

                q_list = new List<question>(); // выделили списку оценок экспертов память 
                question a; // переменная, для добавления в список оценок

                //добавляем альтернативы в структуру
                for (int i = 0; i < sol_count; i++)
                {
                    for (int j = i + 1; j < sol_count; j++)
                    {
                        a.A = list_solution.Items[i].ToString();
                        a.B = list_solution.Items[j].ToString();
                        a.res_A = -1; // у всех нет оценки
                        a.res_B = -1;// у всех нет оценки
                        q_list.Add(a);
                    }
                }

                // проверяем, есть ли у нас уже какие-то результаты опроса, если да, то читаем их
                fileInf = new FileInfo(directory + "matrix" + form.num_problem + "m4e" + form1_main.num_expert + ".txt");
                if (fileInf.Exists)
                {
                    exists = true;  // уже есть какие то результаты
                    string[] words;
                    string[,] matr = new string[sol_count, sol_count];
                    using (StreamReader sr = new StreamReader(directory + "matrix" + form.num_problem + "m4e" + form1_main.num_expert + ".txt", System.Text.Encoding.UTF8))
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
                    for (int i = 0; i < sol_count - 1; i++)// до предпоследней строки
                    {
                        for (int j = 0; j < sol_count; j++)
                        {
                            if (i < j)
                            {
                                a = q_list[count];
                                a.res_A = Convert.ToInt32(matr[i, j]);
                                a.res_B = Convert.ToInt32(matr[j, i]);
                                q_list[count] = a;
                                count++;
                            }
                        }
                    }

                }
                else
                {
                    exists = false;  //результатов (матрицы) НЕТ
                }

                DataTable table = new DataTable("Альтернативы и оценки");
                // навсякий очищаем все
                table.Clear();
                table.Columns.Clear();
                table.Rows.Clear();

                table.Columns.Add(new DataColumn("Альтернатива 1"));
                table.Columns[0].ReadOnly = true; // альтернативы слева можно только смотреть
                table.Columns.Add(new DataColumn("<== Оценка"));
                table.Columns.Add(new DataColumn("Оценка ==>"));
                table.Columns.Add(new DataColumn("Альтернатива 2"));
                table.Columns[3].ReadOnly = true; // альтернативы справа можно только смотреть

                // заполнение таблицы
                for (int j = 0; j < q_count; j++)
                {
                    DataRow dr = table.NewRow();
                    //записали альтернативу левую
                    dr[0] = q_list[j].A;

                    if(exists  == true) // если матрицы с результатами уже есть
                    {
                        dr[1] = q_list[j].res_A;//записали оценку левой альтернативы
                        dr[2] = q_list[j].res_B;//записали оценку правой альтернативы
                    }
                    else
                    {
                        dr[1] = "";//записали пустую оценку левой альтернативы
                        dr[2] = "";//записали пустую оценку правой альтернативы
                    }

                    //записали альтернативу правую
                    dr[3] = q_list[j].B;
                    table.Rows.Add(dr);
                }

                dataGridView1.DataSource = table; // данные таблицы в датагрид засунули

                dataGridView1.Columns[0].Width = 264; // ширина столбца левой альтерантивы
                dataGridView1.Columns[1].Width = 80; // ширина столбца оценки для левой альтернативы
                dataGridView1.Columns[2].Width = 80; // ширина столбца оценки для правой альтернативы
                dataGridView1.Columns[3].Width = 263; // ширина столбца правой альтерантивы

                // шобы столбцы нельзя было сортировать
                for (int j = 0; j < 4; j++)
                {
                    dataGridView1.Columns[j].SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                // выравнивание заголовков столбцов по центру
                dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        // функция СОХРАНЕНИЯ
        private void save()
        {
            Form9_expert form = this.Owner as Form9_expert; // 9 форма - хозяин этой формы

            /*using (StreamWriter sw = new StreamWriter(directory + "matrix" + form.num_problem + "m3.txt", false, System.Text.Encoding.UTF8))
            {
                string line = "";
                for(int i = 0; i < exp_res.Count; i++)
                {
                    //запомнили ID эксперта
                    line = exp_res[i].id_exp.ToString() + " ";
                    // запомнили оценочки
                    for(int j = 0; j < exp_res[i].marks.Count(); j++)
                    {
                        line += exp_res[i].marks[j].ToString() + " ";
                    }
                    sw.WriteLine(line);
                }
            }*/
        }

        // перетаскивание окна по экрану
        private void Form7_MouseDown(object sender, MouseEventArgs e)
        {
            
            base.Capture = false;
            Message m = Message.Create(base.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }
        
        // кнопка СОХРАНИТЬ
        private void btn_save_Click(object sender, EventArgs e)
        {
            Form9_expert form = this.Owner as Form9_expert;
            change = false;  // УБРАТЬ ПОТОМ !!!************************************************************************ УБРАААТЬ
            if (change == true)
            {
                empty = false;
                for (int i = 0; i < sol_count; i++) // ищем НЕправильные ячейки
                {
                    if (dataGridView1.Rows[i].Cells[1].Value.ToString() == "")
                    {
                        dataGridView1.Rows[i].Cells[1].Style.ForeColor = Color.FromName("Red"); // красный текст
                        dataGridView1.Rows[i].Cells[1].Style.BackColor = Color.FromArgb(254, 254, 34); // желтый фон
                        empty = true;
                    }
                }

                if (empty == false)
                {
                    label3.BackColor = this.BackColor; // цвет label = цвет формы нейтральный
                    correct = true;
                    for (int i = 0; i < sol_count; i++) // ищем НЕправильные ячейки
                    {
                        if (check_cell_value(i, 1) == false)
                        {
                            correct = false;
                        }
                    }

                    if (correct == true)
                    {
                        label2.BackColor = this.BackColor; // цвет label = цвет формы нейтральный
                        label8.BackColor = this.BackColor;

                        // СОХРАНЯЕМ
                        // сохраняем в файл matrix...
                        save();
                        //============================================
                        // в форме эксперта обновляем данные на ней
                        form.list_prob[form.N].exp[form.E].m3 = 1;  // опрос пройден
                        form.save_group(); // сохраняем измененное в файл group...
                        form.update(form.N, form.E);  // обновляем на 9 форме 
                        //============================================
                        this.Hide();  // СКРЫВАЕМ ФОРМУ пока MessageBox показывается 
                        MessageBox.Show(
                        "Изменения сохранены!",
                        "Сохранение",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly);

                        form.Show();
                        form.TopMost = true; form.TopMost = false;
                        this.Close();
                    }
                    else
                    {
                        dataGridView1.Rows[0].Cells[0].Selected = true;
                        this.Hide(); // СКРЫВАЕМ ФОРМУ пока MessageBox показывается 
                        DialogResult otvet = MessageBox.Show(
                        "Все ячейки оценок должны быть заполнены \nчислами от 0 до " + max + "!\n",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly);
                        this.Show();
                        label2.BackColor = Color.FromArgb(254, 254, 34); // желтый фон
                        label8.BackColor = Color.FromArgb(254, 254, 34); // желтый фон
                        this.TopMost = true; this.TopMost = false;
                        this.TopMost = true; this.TopMost = false;
                    }
                }
                else
                {
                    dataGridView1.Rows[0].Cells[0].Selected = true;
                    this.Hide(); // СКРЫВАЕМ ФОРМУ пока MessageBox показывается 
                    DialogResult otvet = MessageBox.Show(
                    "Все ячейки должны быть заполнены!\n" +
                    "Если для альтернативы по вашему мнению оценка не нужна, впишите в ячейку ноль!\n",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
                    this.Show();
                    label3.BackColor = Color.FromArgb(254, 254, 34); // желтый фон
                    this.TopMost = true; this.TopMost = false;
                    
                }
            }
        }

        // кнопка ЗАКРЫТЬ ПРИЛОЖЕНИЕ
        private void button_cross_Click(object sender, EventArgs e)
        {
            button_back_Click(null, null);
        }

        // кнопка СВЕРНУТЬ окно
        private void button_minimize_Click(object sender, EventArgs e)
        {
            // обеспечивает сворачивание формы при нажатии на тире
            this.WindowState = FormWindowState.Minimized;
        }

        // кнопка НАЗАД
        private void button_back_Click(object sender, EventArgs e)
        {
            Form9_expert form = this.Owner as Form9_expert;
            if (change == true)
            {
                correct = true;
                for (int i = 0; i < sol_count; i++) // ищем НЕправильные ячейки
                {
                    if (check_cell_value(i, 1) == false)
                    {
                        correct = false;
                    }
                }

                if (correct == true)
                {
                    this.Hide(); // СКРЫВАЕМ ФОРМУ пока MessageBox показывается 
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
            else
            {
                form.Show();  // Показываем форму эксперта
                form.TopMost = true; form.TopMost = false;
                this.Close();
            }

        }

        // НАВОДИМ НА АЛЬТЕРНАТИВУ КУРСОР отображается полностью весь текст в сноске
        private void dataGridView1_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < q_count && (e.ColumnIndex == 0 || e.ColumnIndex == 3))
            {
                string[] words;
                string text = "";
                int max_length = 4;

                text = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                words = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (words.Count() <= max_length)
                {
                    e.ToolTipText = text;
                }
                else
                {
                    text = "";
                    int i = 0;
                    while (i < words.Count())
                    {
                        for (int j = 0; j < max_length && i < words.Count(); j++)
                        {
                            text += words[i] + " ";
                            i++;
                        }
                        text += "\n";
                    }
                    e.ToolTipText = text;
                }
            }
        }

        // функция ПРОВЕРКА ЗНАЧЕНИЙ У ячейки
        // для dataGridView1_CellMouseDown И dataGridView1_CellValueChanged
        private bool check_cell_value(int r, int c)
        {
            int cc = 1;
            if (c == 1)
                cc = 2;
            if (c == 2)
                cc = 1;

            string text = dataGridView1.Rows[r].Cells[c].Value.ToString();
            if (text != "")// если введено что-то
            {
                int chislo = -1;
                bool is_int = int.TryParse(text, out chislo);
                if (is_int == true) // если введено целое число
                {
                    if (chislo >= 0 && chislo <= max)// если введено целое число в правильном интервале
                    {
                        edit_on = true;
                        dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
                        dataGridView1.Rows[r].Cells[cc].Value = max - chislo;
                        dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
                        question a = q_list[r];
                        if(c == 1)
                        {
                            a.res_A = chislo;
                            a.res_B = max - chislo;
                        }
                        else if (c == 2)
                        {
                            a.res_A = max - chislo;
                            a.res_B = chislo;
                        }
                        q_list[r] = a;
                        change = true; // изменение засчитано
                        dataGridView1.Rows[r].Cells[c].Style.BackColor = Color.FromName("ButtonHighlight"); // белый фон
                        dataGridView1.Rows[r].Cells[c].Style.ForeColor = Color.FromName("Black"); // черный текст
                        dataGridView1.Rows[r].Cells[cc].Style.BackColor = Color.FromName("ButtonHighlight"); // белый фон
                        dataGridView1.Rows[r].Cells[cc].Style.ForeColor = Color.FromName("Black"); // черный текст
                        return true;
                    }
                    else// если введено целое число НЕ в интервале
                    {
                        dataGridView1.Rows[r].Cells[c].Style.ForeColor = Color.FromName("Red"); // красный текст
                        dataGridView1.Rows[r].Cells[c].Style.BackColor = Color.FromArgb(254, 254, 34); // желтый фон
                        dataGridView1.Rows[r].Cells[cc].Style.ForeColor = Color.FromName("Red"); // красный текст
                        dataGridView1.Rows[r].Cells[cc].Style.BackColor = Color.FromArgb(254, 254, 34); // желтый фон
                        return false;
                    }
                }
                else // если введено НЕ целое число
                {
                    dataGridView1.Rows[r].Cells[c].Style.ForeColor = Color.FromName("Red");// красный текст
                    dataGridView1.Rows[r].Cells[c].Style.BackColor = Color.FromArgb(254, 254, 34); // желтый фон
                    dataGridView1.Rows[r].Cells[cc].Style.ForeColor = Color.FromName("Red");// красный текст
                    dataGridView1.Rows[r].Cells[cc].Style.BackColor = Color.FromArgb(254, 254, 34); // желтый фон
                    return false;
                }
            }
            else // если НЕ введено
            {
                dataGridView1.Rows[r].Cells[cc].Value = "";
                question a = q_list[r];
                a.res_A = -1;
                a.res_B = -1;
                q_list[r] = a;

                dataGridView1.Rows[r].Cells[c].Style.BackColor = Color.FromName("ButtonHighlight"); // белый фон
                dataGridView1.Rows[r].Cells[c].Style.ForeColor = Color.FromName("Black"); // черный текст
                dataGridView1.Rows[r].Cells[cc].Style.BackColor = Color.FromName("ButtonHighlight"); // белый фон
                dataGridView1.Rows[r].Cells[cc].Style.ForeColor = Color.FromName("Black"); // черный текст
                return true; // НЕПОНЯТНО ВОТ НАДО ПОДУМАТЬ
            }
        }

        // когда РЕДАКТИРУЕМ ЯЧЕЙКУ (именно меняется значение) (приоритет_1)
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (edit_on == false && (e.ColumnIndex == 1 || e.ColumnIndex == 2) && e.RowIndex >= 0 && e.RowIndex < q_count) // если ячейки с оценками
            {
                is_edit = true;
                check_cell_value(e.RowIndex, e.ColumnIndex);
                
            }
        }

        // когда УШЛИ С ЯЧЕЙКИ (срабатывает, если НЕ РЕДАКТИРУЕМ) (приоритет_2)
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex == 1 || e.ColumnIndex == 2) && e.RowIndex >= 0 && e.RowIndex < q_count) // если ячейки с оценками
            {
                if (is_edit == false)
                {
                    if (is_yellow == true)
                    {
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.FromName("Red"); // красный текст
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.FromArgb(254, 254, 34); // желтый фон
                    }
                    else
                    {
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.FromName("ButtonHighlight"); // белый фон
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.FromName("Black"); // черный текст
                    }
                }
            }
            edit_on = false;
            is_edit = false;
        }

        // когда НАЖИМАЕМ НА ЯЧЕЙКУ (приоритет_3)
        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if ((e.ColumnIndex == 1 || e.ColumnIndex == 2) && e.RowIndex >= 0 && e.RowIndex < q_count) // если ячейки с оценками
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor == Color.FromArgb(254, 254, 34)) // желтый фон
                {
                    is_yellow = true;
                }
                else // если не желтый тоже запомнили
                {
                    is_yellow = false;
                }
                edit_on = false;
                // делаем нормальной, если тыкаем
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.FromName("ButtonHighlight"); // белый фон
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.FromName("Black"); // черный текст
            }
        }
    }
}
