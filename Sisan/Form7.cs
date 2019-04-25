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
    public partial class Form7 : Form
    {

        public Form7(string p, int n)
        {
            InitializeComponent();
        }

        //======================================================
        // ПУТЬ К ОСНОВНОМУ РАБОЧЕМУ КАТАЛОГУ
        // ЕСЛИ МЕНЯТЬ ПУТЬ, ТО ТОЛЬКО В 1 ФОРМЕ 
        private string directory = global_class.main_directory;
        //======================================================

        //private int N = 0;
        //private bool start = false;
        //private int current = 1;
        private bool alter = false;
        public int max = 100;
        public bool change = false;
        public int exp_count = 0; // количество экспертов
        public int E = -1; //порядковый номер нашего эксперта в exp_res
        public int sol_count; // количество альтернатив про выбранной проблеме


        public struct result
        {
            public int id_exp;
            public int[] marks;
        }
        List<result> exp_res; // список с оценками экспертов

        List<string> list_sol;  // список для просто альтернатив


        // при ЗАГРУЗКЕ ФОРМЫ 
        private void Form7_Load(object sender, EventArgs e)
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
                alter = true;

                if (alter)
                {
                    a.marks = new int[sol_count]; // выделили память для результата экспертов для первой альтернативы
                    // проверяем, есть ли у нас уже какие-то результаты опроса, если да, то читаем их
                    // иначе заполняем список -1 (типа не оценено)
                    FileInfo fileInf = new FileInfo(directory + "matrix" + form.num_problem + "m1.txt");
                    if (fileInf.Exists)
                    {
                        exists = true;
                        int m = 0;
                        using (StreamReader sr = new StreamReader(directory + "matrix" + form.num_problem + "m1.txt", System.Text.Encoding.UTF8))
                        {
                            string line;
                            while ((line = sr.ReadLine()) != null)
                            {
                                words = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                a.id_exp = Convert.ToInt32(words[0]);
                                if (a.id_exp == form1_main.num_expert)
                                    E = m;
                                if(m > 0)
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
                        exists = false;
                        for (int i = 0; i < exp_count; i++)
                        {
                            a.id_exp = form.list_prob[form.N].exp[i].id_exp;
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
                    if(exists == false)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            tmp[j] = "";
                        }
                    }
                    else
                    {
                        for (int j = 0; j < n; j++)
                        {
                            tmp[j] = exp_res[E].marks[j].ToString();
                        }
                    }


                    for (int j = 0; j < list_sol.Count; j++)
                    {
                        DataRow dr = table.NewRow();
                        dr[0] = list_sol[j];
                        dr[1] = tmp[j] ;
                        table.Rows.Add(dr);
                    }

                    dataGridView1.DataSource = table;
                    dataGridView1.Columns[0].Width = 600; // ширина столбца альтерантив
                    dataGridView1.Columns[1].Width = 97; // ширина столбца оценок
                    // шобы столбцы нельзя было сортировать
                    dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                    dataGridView1.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
                    // TO DO *************************************************************************************************
                    // надо сделать чтоб только числа вводить
                    //dataGridView1.Columns[1]

                }
            }
        }

        // функция СОХРАНЕНИЯ
        private void save()
        {
             // 9 форма - хозяин этой формы
            Form9_expert form = this.Owner as Form9_expert;
            List<string> temp = new List<string>();

            /*using (StreamReader sr = new StreamReader(directory + "matrix" + form.num_problem + "m1.txt", System.Text.Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] words = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (words[0] == form1_main.num_expert.ToString())
                    {
                        line = form1_main.num_expert.ToString() + ' ';
                        for (int i = 0; i < q.Count; i++)
                            line += dataGridView1[1, i].Value.ToString() + ' ';
                    }
                    temp.Add(line);
                }
            }*/

            using (StreamWriter sw = new StreamWriter(directory + "matrix" + form.num_problem + "m1.txt", false, System.Text.Encoding.UTF8))
            {
                for(int i = 0; i < temp.Count; i++)
                {
                    sw.WriteLine(temp[i]);
                }
            }
                MessageBox.Show(
                    "Изменения сохранены!",
                    "",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
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
            /*Form9_expert form = this.Owner as Form9_expert;
            int res = 0;
            bool is_empty = false;

            for (int i = 0; i < q.Count; i++) // ищем незаполненные ячейки
            {
                if (dataGridView1[1, i].Value.ToString() == "")
                    is_empty = true;
            }

            if (is_empty == false)
            {
                for (int i = 0; i < q.Count; i++)
                {
                    if (dataGridView1[1, i].Value.ToString() != "")
                        res += Convert.ToInt32(dataGridView1[1, i].Value);
                }
                if (res == max)
                {
                    save();
                    form.list_prob[form.N].exp[form.E].m1 = 1;
                    form.update(form.N, form.E);
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
                    if (res < max)
                    {
                        //DialogResult result = MessageBox.Show(
                        //"Вы не прошли опрос до конца.\n" +
                        //"Можете вернуться и продолжить в любое время.\n" +
                        //"Ваши результаты будут сохранены.",
                        //"Внимание",
                        //MessageBoxButtons.OK,
                        //MessageBoxIcon.Warning,
                        //MessageBoxDefaultButton.Button1,
                        //MessageBoxOptions.DefaultDesktopOnly);
                        DialogResult result = MessageBox.Show(
                        "Сумма оценок не должна быть меньше " + max + "!",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly);
                        //form.list_prob[form.N].exp[form.E].m1 = -1;
                        //save();
                        //form.update(form.N, form.E);
                    }
                    if (res > max)
                    {
                        MessageBox.Show(
                        "Сумма оценок не должна превышать " + max + "!\n",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly);

                    }
                    this.TopMost = true; this.TopMost = false;
                }
            }
            else
            {
                MessageBox.Show(
                "Все ячейки оценок должны быть заполнены!" +
                "Если для альтернативы Сумма оценок не должна превышать !\n",
                "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
                this.TopMost = true; this.TopMost = false;
            }*/
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

        }

        // НАВОДИМ НА АЛЬТЕРНАТИВУ КУРСОР отображается полностью весь текст в сноске
        private void dataGridView1_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            string text = "";
            if(e.RowIndex >= 0 && e.RowIndex < sol_count && e.ColumnIndex == 0)
            {
                text = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                e.ToolTipText = text;
            }
        }
    }
}
