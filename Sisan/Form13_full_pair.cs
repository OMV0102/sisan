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

        // МЕТОД 2 (МЕТОД ПРЕДПОЧТЕНИЯ)

        public bool correct = false; // флаг правильных значений ячеек
        public bool change = false; // флаг внесенных изменений
        public bool is_edit; // флаг, что после редактирования не проверяем повторно ячейку
        public bool is_sort = false; // флаг, что во время редактирования не проверяем ячейки
        public int exp_count = 0; // количество экспертов
        public int E = -1; //порядковый номер нашего эксперта в exp_res
        public int sol_count; // количество альтернатив про выбранной проблеме
        public int max = 100; // максимальная оценка для ОДНОЙ альтернативы
        public int old_value = -1;  // Старое значение, котрое харнится в ячейке
        public bool is_yellow = false;

        public struct result
        {
            public int id_exp;
            public int[] marks;
        }
        List<result> exp_res; // список с оценками экспертов

        List<string> list_sol;  // список для просто альтернатив

        public bool[] numbers; // массив порядковых

        DataTable table; // таблица , к которой привязаня данные датагрида

        // при ЗАГРУЗКЕ ФОРМЫ 
        private void Form11_rang_Load(object sender, EventArgs e)
        {
            Form9_expert form = this.Owner as Form9_expert;

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

                label2.Text = "Расположите оценки от 1 до " + sol_count + " по всем альтернативам,\n";
                label2.Text += "(1 - самая предпочтительная альтернатива, " + sol_count + " - альтернатива не предпочтительная).";
                // выделяем память для массива порядковых чисел
                numbers = new bool[sol_count + 1]; // индексы используем с 1 по sol_count, по умолчанию false
                numbers[0] = true; // 0 индекс не используем, т.к. оценки с 1 по sol_count

                // проверяем, есть ли у нас уже какие-то результаты опроса, если да, то читаем их
                // иначе заполняем список -1 (типа не оценено)
                FileInfo fileInf = new FileInfo(directory + "matrix" + form.num_problem + "m2.txt");
                if (fileInf.Exists)
                {
                    exists = true;  // уже есть какие то результаты
                    int m = 0;
                    using (StreamReader sr = new StreamReader(directory + "matrix" + form.num_problem + "m2.txt", System.Text.Encoding.UTF8))
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
                                if(a.id_exp == form1_main.num_expert && a.marks[j] > 0 && a.marks[j] <= sol_count)
                                    numbers[a.marks[j]] = true;
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


                table = new DataTable("Альтернативы и оценки");
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
        private void save_matrix()
        {
            Form9_expert form = this.Owner as Form9_expert; // 9 форма - хозяин этой формы

            using (StreamWriter sw = new StreamWriter(directory + "matrix" + form.num_problem + "m2.txt", false, System.Text.Encoding.UTF8))
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
                // заполняем порядковый массив чисел типа не заняты все
                for (int i = 1; i < numbers.Count(); i++)
                {
                    numbers[i] = false;
                }

                // по оценкам смотрим занятые номера и запоминаем
                int chislo = -1;
                bool is_int;
                for (int i = 0; i < sol_count; i++)
                {
                    is_int = int.TryParse(dataGridView1.Rows[i].Cells[1].Value.ToString(), out chislo);
                    if(is_int == true && chislo > 0 && chislo <= sol_count)
                    {
                        numbers[chislo] = true;
                    }
                    
                }

                correct = true;  //допустим все правильно
                for (int i = 1; i < numbers.Count(); i++) // ищем НЕправильные ячейки
                {
                    if (numbers[i] == false)
                    {
                        correct = false;
                    }
                }

                if (correct == true) // если все ячейки нормально заполнены
                {
                    //======================================================
                    int num = -1;
                    string alt = "";
                    int value;
                    for (int i = 0; i < sol_count; i++)
                    {
                        num = -1;
                        alt = dataGridView1.Rows[i].Cells[0].Value.ToString(); // альтернатива
                        for (int j = 0; num == -1 && j < list_sol.Count(); j++)
                        {
                            if (list_sol[j] == alt)
                            {
                                num = j;
                            }
                        }

                        if (int.TryParse(dataGridView1.Rows[i].Cells[1].Value.ToString(), out value) == true && num >= 0 && num < exp_res[E].marks.Count())
                            exp_res[E].marks[num] = value; // запомнили введеную оценку
                    }

                    //==================================================================
                    correct = true;  //допустим все правильно
                    for (int i = 0; i < exp_res[E].marks.Count(); i++) // ищем НЕправильные ячейки
                    {
                        if (exp_res[E].marks[i] == -1)
                        {
                            correct = false;
                        }
                    }
                    //=================================================================
                    if (correct == true)
                    {
                        label2.BackColor = this.BackColor; // цвет label = цвет формы нейтральный
                        label3.BackColor = this.BackColor; // цвет label = цвет формы нейтральный

                        // СОХРАНЯЕМ
                        // сохраняем в файл matrix...
                        save_matrix();
                        //============================================
                        // в форме эксперта обновляем данные на ней
                        form.list_prob[form.N].exp[form.E].m2 = 1;  // опрос пройден
                        form.save_group(); // сохраняем измененное в файл group...
                        form.update(form.N, form.E);  // обновляем на 9 форме 
                        //============================================
                        this.Hide();  // СКРЫВАЕМ ФОРМУ пока MessageBox показывается 
                        DialogResult otvet = MessageBox.Show(
                        "Изменения сохранены!",
                        "Сохранение",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly);
                        if(otvet == DialogResult.OK)
                        {
                            form.Show();
                            form.TopMost = true; form.TopMost = false;
                            form.TopMost = true; form.TopMost = false;
                            this.Close();
                        }
                    }
                    else
                    {
                        dataGridView1.Rows[0].Cells[0].Selected = true;
                        this.Hide(); // СКРЫВАЕМ ФОРМУ пока MessageBox показывается 
                        DialogResult otvet = MessageBox.Show(
                        "Не все ячейки заполнены корректными значениями!\n" +
                        "или\n" +
                        "Есть ячейки с повторяющимися оценками!",
                        "Внимание",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly);
                        this.Show();
                        label2.BackColor = Color.FromArgb(254, 254, 34); // желтый фон
                        this.TopMost = true; this.TopMost = false;
                    }
                }
                else
                {
                    dataGridView1.Rows[0].Cells[0].Selected = true;
                    this.Hide(); // СКРЫВАЕМ ФОРМУ пока MessageBox показывается 
                    DialogResult otvet = MessageBox.Show(
                    "Не все ячейки заполнены корректными значениями!\n" +
                    "или\n" +
                    "Есть ячейки с повторяющимися оценками!",
                    "Внимание",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
                    this.Show();
                    label2.BackColor = Color.FromArgb(254, 254, 34); // желтый фон
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
        // для dataGridView1_CellValueChanged 
        private bool check_cell_value(int r, int c)
        {
            string text = dataGridView1.Rows[r].Cells[c].Value.ToString();
            if (text != "")// если введено что-то
            {
                int chislo = -1;
                bool is_int = int.TryParse(text, out chislo);
                bool is_uniq;
                int old_cell = -1;
                bool old_neskolko;
                int num;
                string alt;
                if (is_int == true) // если введено целое число
                {
                    if(chislo > 0 && chislo <= sol_count)// если введено целое число в правильном интервале
                    {
                        if (numbers[chislo] == false)   //Если введеное число свободно
                        {
                            // поищем, не занято ли кем-то старое число
                            is_uniq = true;
                            old_neskolko = false;
                            for (int i = 0; i < sol_count; i++)
                            {
                                if (i != r)
                                {
                                    if (dataGridView1.Rows[i].Cells[c].Value.ToString() == old_value.ToString() && old_value > 0 && old_value <= sol_count)
                                    {
                                        is_uniq = false;
                                        if(old_cell != -1) // если нашли уже не одну ячейку с старым значением , карсим предыдущу.
                                        {
                                            // сигналим желтым с красным этим , кто занял число
                                            dataGridView1.Rows[old_cell].Cells[c].Style.ForeColor = Color.FromName("Red"); // красный текст
                                            dataGridView1.Rows[old_cell].Cells[c].Style.BackColor = Color.FromArgb(254, 254, 34); // желтый фон
                                            old_neskolko = true;
                                        }
                                        old_cell = i; // и запоминаем новую
                                    }
                                }
                            }
                            if(old_neskolko == true)
                            {
                                dataGridView1.Rows[old_cell].Cells[c].Style.ForeColor = Color.FromName("Red"); // красный текст
                                dataGridView1.Rows[old_cell].Cells[c].Style.BackColor = Color.FromArgb(254, 254, 34); // желтый фон
                            }
                            else
                            {
                                if(old_cell >= 0 && old_cell < sol_count)
                                {
                                    dataGridView1.Rows[old_cell].Cells[c].Style.ForeColor = Color.FromName("Black"); // черный текст
                                    dataGridView1.Rows[old_cell].Cells[c].Style.BackColor = Color.FromName("ButtonHighlight"); // нейтральный фон
                                    //=================================================================================================
                                    // т.к. у нас может отсортировано , то мы по тексту альтернативы должны найти ее номер в списке
                                    num = -1;
                                    alt = dataGridView1.Rows[old_cell].Cells[0].Value.ToString();
                                    for (int i = 0; i < list_sol.Count(); i++)
                                    {
                                        if (list_sol[i] == alt)
                                        {
                                            num = i;
                                        }
                                    }
                                    if (num >= 0 && num < exp_res[E].marks.Count())
                                        exp_res[E].marks[num] = old_value; // запомнили введеную оценку
                                    //=================================================================================================
                                }
                            }
                            // освобождаем старое число
                            if (is_uniq == true && old_value > 0 && old_value <= sol_count)
                            {
                                numbers[old_value] = false;
                            }
                            change = true; // изменение засчитано
                            numbers[chislo] = true; // оценка занята
                            //=================================================================================================
                            // т.к. у нас может отсортировано , то мы по тексту альтернативы должны найти ее номер в списке
                            num = -1;
                            alt = dataGridView1.Rows[r].Cells[0].Value.ToString();
                            for (int i = 0; i < list_sol.Count(); i++)
                            {
                                if (list_sol[i] == alt)
                                {
                                    num = i;
                                }
                            }
                            if(num >= 0 && num < exp_res[E].marks.Count())
                                exp_res[E].marks[num] = chislo; // запомнили введеную оценку
                            //=================================================================================================
                            return true;
                        }
                        else // если введеное число ЗАНЯТО
                        {
                            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                            // поищем, не занято ли кем-то старое число
                            is_uniq = true;
                            old_neskolko = false;
                            for (int i = 0; i < sol_count; i++)
                            {
                                if (i != r)
                                {
                                    if (dataGridView1.Rows[i].Cells[c].Value.ToString() == old_value.ToString() && old_value > 0 && old_value <= sol_count)
                                    {
                                        is_uniq = false;
                                        if (old_cell != -1) // если нашли уже не одну ячейку с старым значением , карсим предыдущу.
                                        {
                                            // сигналим желтым с красным этим , кто занял число
                                            dataGridView1.Rows[old_cell].Cells[c].Style.ForeColor = Color.FromName("Red"); // красный текст
                                            dataGridView1.Rows[old_cell].Cells[c].Style.BackColor = Color.FromArgb(254, 254, 34); // желтый фон
                                            old_neskolko = true;
                                        }
                                        old_cell = i; // и запоминаем новую
                                    }
                                }
                            }
                            if (old_neskolko == true)
                            {
                                dataGridView1.Rows[old_cell].Cells[c].Style.ForeColor = Color.FromName("Red"); // красный текст
                                dataGridView1.Rows[old_cell].Cells[c].Style.BackColor = Color.FromArgb(254, 254, 34); // желтый фон
                            }
                            else
                            {
                                if (old_cell >= 0 && old_cell < sol_count)
                                {
                                    dataGridView1.Rows[old_cell].Cells[c].Style.ForeColor = Color.FromName("Black"); // черный текст
                                    dataGridView1.Rows[old_cell].Cells[c].Style.BackColor = Color.FromName("ButtonHighlight"); // нейтральный фон
                                    //=================================================================================================
                                    // т.к. у нас может отсортировано , то мы по тексту альтернативы должны найти ее номер в списке
                                    num = -1;
                                    alt = dataGridView1.Rows[old_cell].Cells[0].Value.ToString();
                                    for (int i = 0; i < list_sol.Count(); i++)
                                    {
                                        if (list_sol[i] == alt)
                                        {
                                            num = i;
                                        }
                                    }
                                    if (num >= 0 && num < exp_res[E].marks.Count())
                                        exp_res[E].marks[num] = old_value; // запомнили введеную оценку
                                    //=================================================================================================
                                }
                            }
                            // освобождаем старое число
                            if (is_uniq == true && old_value > 0 && old_value <= sol_count)
                            {
                                numbers[old_value] = false;
                            }
                            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                            // тогда мы найдем какой ячейкой это число занято
                            is_uniq = true;
                            for(int i = 0; i < sol_count; i++)
                            {
                                if (i != r)
                                {
                                    if (dataGridView1.Rows[i].Cells[c].Value.ToString() == chislo.ToString())
                                    {
                                        // сигналим желтым с красным этим , кто занял число
                                        dataGridView1.Rows[i].Cells[c].Style.ForeColor = Color.FromName("Red"); // красный текст
                                        dataGridView1.Rows[i].Cells[c].Style.BackColor = Color.FromArgb(254, 254, 34); // желтый фон
                                        is_uniq = false;
                                    }
                                }
                            }
                            // потом проверяем че мы нашли
                            if(is_uniq == true) // если НЕ нашли, что введенное число кем-то занято
                            {
                                // значит число занято самим собой, то есть мы оставили старое значение в ячейке
                                dataGridView1.Rows[r].Cells[c].Style.ForeColor = Color.FromName("Black"); // черный текст
                                dataGridView1.Rows[r].Cells[c].Style.BackColor = Color.FromName("ButtonHighlight"); // нейтральный фон
                                return true;
                            }
                            else // если НАШЛИ, что введенное число кем-то занято
                            {
                                // сигналим желтым с красным
                                dataGridView1.Rows[r].Cells[c].Style.ForeColor = Color.FromName("Red"); // красный текст
                                dataGridView1.Rows[r].Cells[c].Style.BackColor = Color.FromArgb(254, 254, 34); // желтый фон
                                return false;
                            }
                        }
                    }
                    else // если введено целое число НЕ в интервале
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

        // когда РЕДАКТИРУЕМ ЯЧЕЙКУ (именно меняется значение) (приоритет_1)
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 1 && e.RowIndex >= 0 && e.RowIndex < sol_count) // если ячейки с оценками
            {
                if(is_sort == false)
                {
                    check_cell_value(e.RowIndex, e.ColumnIndex);
                    is_edit = true;
                }
            }
        }

        // когда УШЛИ С ЯЧЕЙКИ (срабатывает, если НЕ РЕДАКТИРУЕМ) (приоритет_2)
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (is_edit == false)
            {
                if(is_yellow == true)
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
            is_edit = false;
        }

        // когда НАЖИМАЕМ НА ЯЧЕЙКУ (приоритет_3) 
        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.RowIndex >= 0 && e.RowIndex < sol_count) // если ячейки с оценками
            {
                // если мы тыкнули на ячейку и у нее желтый фон, то запомнили 
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor == Color.FromArgb(254, 254, 34)) // желтый фон
                {
                    is_yellow = true;
                }
                else // если не желтый тоже запомнили
                {
                    is_yellow = false;
                }
                // делаем нормальной, если тыкаем
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.FromName("ButtonHighlight"); // белый фон
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.FromName("Black"); // черный текст

                // запоминаем старое значение, чтоб его освободить потом
                string text = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                if (text != "")
                    int.TryParse(text, out old_value);
                else // Если занчение ячейки пустоя то занчит -1 присваиваем
                    old_value = -1;
            }
        }

        // кнопка СОРТИРОВАТЬ
        private void btn_sort_Click(object sender, EventArgs e)
        {
            correct = true;
            for (int i = 0; i < sol_count; i++)
            {
                if(dataGridView1.Rows[i].Cells[1].Value.ToString() == "" || dataGridView1.Rows[i].Cells[1].Style.BackColor == Color.FromArgb(254, 254, 34))
                {
                    correct = false;
                }
            }

            if (correct == true)
            {
                label2.BackColor = this.BackColor; // цвет label = цвет формы нейтральный
                label3.BackColor = this.BackColor; // цвет label = цвет формы нейтральный

                is_sort = true; // флаг сортировки включили 
                table.Columns[0].ReadOnly = false; // в таблице, к которой привязан датагрид снимаем на альтернативах только чтение
                dataGridView1.Columns[0].ReadOnly = false; // затем в альтернативах дата грида снимаем только чтение
                // Сортировка взята с сайта  https://metanit.com/sharp/tutorial/2.7.php
                string tmp_str = "";
                int tmp_mark;
                int Ni = -1;
                int Nj = -1;
                bool is_int_i = false;
                bool is_int_j = false;
                for (int i = 0; i < sol_count - 1; i++)
                {
                    for (int j = i + 1; j < sol_count; j++)
                    {
                        is_int_i = int.TryParse(dataGridView1.Rows[i].Cells[1].Value.ToString(), out Ni);
                        is_int_j = int.TryParse(dataGridView1.Rows[j].Cells[1].Value.ToString(), out Nj);
                        if (is_int_i == true && is_int_j == true)
                        {
                            if(Ni > Nj)
                            {
                                // в temp запомнили i элемент
                                tmp_str = dataGridView1.Rows[i].Cells[0].Value.ToString(); // альтернатива
                                tmp_mark = Ni; // оценка
                                // в i элемент запоминаем j элемент
                                dataGridView1.Rows[i].Cells[0].Value = dataGridView1.Rows[j].Cells[0].Value;// альтернатива
                                dataGridView1.Rows[i].Cells[1].Value = dataGridView1.Rows[j].Cells[1].Value;// оценка
                                // в j элемент запоминаем temp
                                dataGridView1.Rows[j].Cells[0].Value = tmp_str; // альтернатива
                                dataGridView1.Rows[j].Cells[1].Value = tmp_mark;// оценка
                            }
                        }
                    }
                }
                dataGridView1.Rows[0].Cells[0].Selected = true; // активной 00 ячейку делаем
                is_sort = false;// флаг сортировки вЫключили  
                table.Columns[0].ReadOnly = true; // в таблице, к которой привязан датагрид ставим опять на альтернативы только чтение
                dataGridView1.Columns[0].ReadOnly = true; // затем в альтернативах дата грида ставим опять только чтение
            }
            else
            {
                dataGridView1.Rows[0].Cells[0].Selected = true;
                this.Hide(); // СКРЫВАЕМ ФОРМУ пока MessageBox показывается 
                DialogResult otvet = MessageBox.Show(
                "Не все ячейки заполнены корректными значениями!\n" +
                "или\n" +
                "Есть ячейки с повторяющимися оценками!",
                "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
                this.Show();

                label2.BackColor = Color.FromArgb(254, 254, 34); // желтый фон
                label3.BackColor = Color.FromArgb(254, 254, 34); // желтый фон
                this.TopMost = true; this.TopMost = false;
            }
        }
    }
}
