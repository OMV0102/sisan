using System;
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
    public partial class Form11_rang : Form
    {

        public Form11_rang()
        {
            InitializeComponent();
        }

        //======================================================
        // ПУТЬ К ОСНОВНОМУ РАБОЧЕМУ КАТАЛОГУ
        // ЕСЛИ МЕНЯТЬ ПУТЬ, ТО ТОЛЬКО В 1 ФОРМЕ 
        private string directory = global_class.main_directory;
        //======================================================

        // МЕТОД 3 (МЕТОД РАНГА)

        public bool correct = false; // флаг правильных значений ячеек
        public bool change = false; // флаг внесенных изменений
        public bool empty = false; // флаг пустых значений ячеек
        public bool is_edit; // флаг, что после редактирования не проверяем повторно ячейку
        public int exp_count = 0; // количество экспертов
        public int E = -1; //порядковый номер нашего эксперта в exp_res
        public int sol_count; // количество альтернатив про выбранной проблеме
        public int max = 100; // максимальная оценка для ОДНОЙ альтернативы

        public struct result
        {
            public int id_exp;
            public int[] marks;
        }
        List<result> exp_res; // список с оценками экспертов

        List<string> list_sol;  // список для просто альтернатив
        
        // при ЗАГРУЗКЕ ФОРМЫ 
        private void Form11_rang_Load(object sender, EventArgs e)
        {
            Form9_expert form = this.Owner as Form9_expert;
            label2.Text += max + "!";
            string[] words;
            label_problem.Text = form.problem; // проблему вывели в форму
            exp_count = form.list_prob[form.N].exp.Count();  // узнали сколько всего экспертов
            list_sol = new List<string>(); // выделили списку альтернатив память
            exp_res = new List<result>(); // выделили списку оценок экспертов память 
            result a; // переменная, для добавления в список оценок
            bool exists;
            // читаем проблемы
            string text = "";
            using (StreamReader sr = new StreamReader(directory + "solutions" + form.num_problem + ".txt", System.Text.Encoding.UTF8))
            {
                text = sr.ReadToEnd();
            }
            sol_count = 0;
            if (text.Length != 0)
            {

                using (StreamReader sr = new StreamReader(directory + "solutions" + form.num_problem + ".txt", System.Text.Encoding.UTF8))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        list_sol.Add(line);
                        sol_count++;
                    }
                }

                // проверяем, есть ли у нас уже какие-то результаты опроса, если да, то читаем их
                // иначе заполняем список -1 (типа не оценено)
                FileInfo fileInf = new FileInfo(directory + "matrix" + form.num_problem + "m3.txt");
                if (fileInf.Exists)
                {
                    exists = true;  // уже есть какие то результаты
                    int m = 0;
                    using (StreamReader sr = new StreamReader(directory + "matrix" + form.num_problem + "m3.txt", System.Text.Encoding.UTF8))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            words = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            a.id_exp = Convert.ToInt32(words[0]);
                            if (a.id_exp == form1_main.num_expert)
                                E = m;
                            a.marks = new int[sol_count]; // выделили память для результата экспертов для sol_count-1 альтернатив
                            for (int j = 0; j < a.marks.Count(); j++)
                            {
                                a.marks[j] = Convert.ToInt32(words[j + 1]);
                            }
                            m++;
                            exp_res.Add(a);
                        }
                    }
                }
                else
                {
                    exists = false; // нет никаких результатов
                    for (int i = 0; i < exp_count; i++)
                    {
                        a.id_exp = form.list_prob[form.N].exp[i].id_exp;
                        if (a.id_exp == form1_main.num_expert)
                            E = i;
                        a.marks = new int[sol_count]; // выделили память для результата экспертов для sol_count-1 альтернатив
                        for (int j = 0; j < a.marks.Count(); j++)
                        {
                            a.marks[j] = -1;
                        }
                        exp_res.Add(a);
                    }
                }


                DataTable table = new DataTable("Альтернативы и оценки");
                table.Clear();
                table.Columns.Clear();
                table.Rows.Clear();

                table.Columns.Add(new DataColumn("Альтернатива"));
                table.Columns[0].ReadOnly = true; // альтернативы можно только смотреть
                table.Columns.Add(new DataColumn("Оценка"));

                int n = exp_res[E].marks.Count();
                string[] tmp = new string[n];
                // заполнение датагрида
                for (int j = 0; j < n; j++)
                {
                    if (exists == false) // никаких результатов нет ни у одного эксперта
                    {
                        tmp[j] = "";
                    }
                    else // есь уже какие-то результаты
                    {
                        if (exp_res[E].marks[0] == -1) // если у нашего эксперта нет результатов
                            tmp[j] = "";
                        else // если у нашего эксперт есть результаты
                            tmp[j] = exp_res[E].marks[j].ToString();
                    }
                }

                for (int j = 0; j < list_sol.Count; j++)
                {
                    DataRow dr = table.NewRow();
                    dr[0] = list_sol[j];
                    dr[1] = tmp[j];
                    table.Rows.Add(dr);
                }

                dataGridView1.DataSource = table;
                dataGridView1.Columns[0].Width = 600; // ширина столбца альтерантив
                dataGridView1.Columns[1].Width = 97; // ширина столбца оценок
                // шобы столбцы нельзя было сортировать
                dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView1.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
                
            }
        }

        // функция СОХРАНЕНИЯ
        private void save()
        {
            Form9_expert form = this.Owner as Form9_expert; // 9 форма - хозяин этой формы

            using (StreamWriter sw = new StreamWriter(directory + "matrix" + form.num_problem + "m3.txt", false, System.Text.Encoding.UTF8))
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
            }
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
            if(change == true)
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
            if(e.RowIndex >= 0 && e.RowIndex < sol_count && e.ColumnIndex == 0)
            {
                e.ToolTipText = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            }
        }

        // функция ПРОВЕРКА ЗНАЧЕНИЙ У ячейки
        // для dataGridView1_CellMouseDown И dataGridView1_CellValueChanged
        private bool check_cell_value(int r, int c)
        {
            string text = dataGridView1.Rows[r].Cells[c].Value.ToString();
            if (text != "")// если введено что-то
            {
                int chislo = -1;
                bool is_int = int.TryParse(text, out chislo);
                if (is_int == true) // если введено целое число
                {
                    if (chislo >= 0 && chislo <= max)// если введено целое число в правильном интервале
                    {
                        change = true; // изменение засчитано
                        exp_res[E].marks[r] = chislo; // запомнили введеную оценку
                        dataGridView1.Rows[r].Cells[c].Style.BackColor = Color.FromName("ButtonHighlight"); // белый фон
                        dataGridView1.Rows[r].Cells[c].Style.ForeColor = Color.FromName("Black"); // черный текст
                        return true;
                    }
                    else// если введено целое число НЕ в интервале
                    {
                        dataGridView1.Rows[r].Cells[c].Style.ForeColor = Color.FromName("Red"); // красный текст
                        dataGridView1.Rows[r].Cells[c].Style.BackColor = Color.FromArgb(254, 254, 34); // желтый фон
                        return false;
                    }
                }
                else // если введено НЕ целое число
                {
                    dataGridView1.Rows[r].Cells[c].Style.ForeColor = Color.FromName("Red");// красный текст
                    dataGridView1.Rows[r].Cells[c].Style.BackColor = Color.FromArgb(254, 254, 34); // желтый фон
                    return false;
                }
            }
            else // если НЕ введено
            {
                dataGridView1.Rows[r].Cells[c].Style.BackColor = Color.FromArgb(254, 254, 34); // желтый фон
                return false;
            }
        }

        // когда НАЖИМАЕМ НА ЯЧЕЙКУ (приоритет_0)
        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.RowIndex >= 0 && e.RowIndex < sol_count) // если ячейки с оценками
            {
                // делаем нормальной, если тыкаем
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.FromName("ButtonHighlight"); // белый фон
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.FromName("Black"); // черный текст
            }
        }

        // когда РЕДАКТИРУЕМ ЯЧЕЙКУ (именно меняется значение) (приоритет_1)
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 1 && e.RowIndex >= 0 && e.RowIndex < sol_count) // если ячейки с оценками
            {
                check_cell_value(e.RowIndex, e.ColumnIndex);
                is_edit = true;
            }
        }

        // когда УШЛИ С ЯЧЕЙКИ (срабатывает, если НЕ РЕДАКТИРУЕМ) (приоритет_2)
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (is_edit == false)
            {
                if (e.ColumnIndex == 1 && e.RowIndex >= 0 && e.RowIndex < sol_count)  // если ячейки с оценками
                {
                    check_cell_value(e.RowIndex, e.ColumnIndex);
                }
            }
            is_edit = false;
        }
    }
}
