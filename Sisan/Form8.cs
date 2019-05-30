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
using System.IO;

namespace system_analysis
{
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }

        //======================================================
        // ПУТЬ К ОСНОВНОМУ РАБОЧЕМУ КАТАЛОГУ
        // ЕСЛИ МЕНЯТЬ ПУТЬ, ТО ТОЛЬКО В 1 ФОРМЕ 
        private string directory = global_class.main_directory;
        //======================================================

        struct exp
        {
            public int n; // номер
            public string name; // имя
            public string comp; // компетентность
            public string position; // должность
            public bool choice; // выбор
        }
        List<exp> exp_list;

        public static bool three_or_four;

        private int countProblem = 0; // счетчик загруженных проблем
        private bool promblem_OK = false; // флаг, проблемы загруженны илил нет
        private int Nselect_problem = -1; // номер выбранной проблемы в comboBox_problems
        private int method = -1; // номер метода
        private bool group_OK = false; // флаг файла группы
        private bool method_OK = false; // флаг метода

        private DataTable table = new DataTable("experts_choice");

        // при загрузке формы
        private void Form8_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Метод парных сравнений");
            comboBox1.Items.Add("Метод взвешенных экспертных оценок");

            string text = "";
            using (StreamReader sr = new StreamReader(directory + "problems.txt", System.Text.Encoding.UTF8))
            {
                text = sr.ReadToEnd();
            }

            if (text.Length != 0)
            {
                using (StreamReader sr = new StreamReader(directory + "problems.txt", System.Text.Encoding.UTF8))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        countProblem++;
                        comboBox_problems.Items.Add(line);
                    }
                }

                promblem_OK = true; // проблемы загруженны
                if(comboBox_problems.Items.Count != 0)
                    comboBox_problems.SelectedIndex = 0;
            }
        }

        // функция
        bool searchChoice(ref exp a, int i)
        {
            bool f = true;
            int j;
            for (j = 0; j < exp_list.Count && f; j++)
            {
                if (exp_list[j].n == i) // если номер эксперта совпадает с одним из номеров в списке
                {
                    a.choice = true; // значит его уже выбрали
                    a.comp = exp_list[j].comp; // значит в список уже записана его компетенция

                    f = false;
                    exp_list[j] = a; // обновляем данные в списке
                }
            }
            if (!f)
                return true;
            else
                return false;

        }

        void table_view()
        {
            table.Clear();
            table.Columns.Clear();
            table.Rows.Clear();

            table.Columns.Add(new DataColumn("ФИО"));
            table.Columns.Add(new DataColumn("Должность"));

            if (method == 1) // с компетеностью работаем только во втором методе
                table.Columns.Add(new DataColumn("Компетентность"));

            for (int j = 0; j < exp_list.Count; j++)
            {
                DataRow dr = table.NewRow();
                dr[0] = exp_list[j].name;
                dr[1] = exp_list[j].position;

                if (method == 1)
                    dr[2] = exp_list[j].comp;

                table.Rows.Add(dr);
            }

            dataGridView1.DataSource = table;
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].Width = 200;
            dataGridView1.Columns[2].Width = 250;
            for (int i = 0; i < exp_list.Count; i++)
            {
                if (exp_list[i].choice == true)
                {
                    dataGridView1.Rows[i].Cells[0].Value = true;
                }
            }
        }


        // список с ПРОБЛЕМАМИ
        private void comboBox_problems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (promblem_OK)
            {
                exp_list = new List<exp>();
                exp a;

                Nselect_problem = comboBox_problems.SelectedIndex;
                string path = directory + "group" + Convert.ToString(Nselect_problem) + ".txt";
                string text = "";
                String[] numb = { };
                if (File.Exists(path))
                {
                    using (StreamReader sr = new StreamReader(path, System.Text.Encoding.UTF8))
                    {
                        text = sr.ReadToEnd();

                        if (text == "")
                        {

                        }
                        else
                        {
                            group_OK = true;
                            sr.BaseStream.Position = 0; // перевели каретку в начало файла
                            string line;
                            int i = 0;
                            while ((line = sr.ReadLine()) != null) // считываем из файла, какие у нас уже эксперты выбраны для проблемы
                            {
                                if (i == 0)
                                {
                                    method = Convert.ToInt32(line);
                                    method_OK = true;
                                    comboBox1.SelectedIndex = method;
                                }
                                else
                                {
                                    numb = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    a.n = Convert.ToInt32(numb[0]); // номер эксперта
                                    if (method == 1) // компетентность, если выбран второй метод оценивания
                                        a.comp = numb[1];
                                    else
                                        a.comp = "";

                                    a.name = "";
                                    a.choice = true;
                                    a.position = "";
                                    exp_list.Add(a);
                                }
                                i++;
                            }

                        }
                    }
                }

                String[] world;
                path = directory + "experts.txt";

                using (StreamReader sr = new StreamReader(path, System.Text.Encoding.UTF8))
                {
                    string line;
                    int i = 0;
                    while ((line = sr.ReadLine()) != null)
                    {
                        world = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        a.n = i;
                        a.name = world[1] + " " + world[2] + " " + world[3];
                        a.position = "";
                        for (int j = 5; j < world.Length; j++)
                        {
                            a.position += world[j] + " ";
                        }
                        a.comp = "";
                        a.choice = false;

                        if (group_OK)
                        {
                            bool f = searchChoice(ref a, i); // проверяет, выбирали ли уже этого эксперта к этой проблеме
                            if (!f) // если нет, то закинь инфу
                                exp_list.Add(a);
                        }
                        else
                            exp_list.Add(a);

                        i++;
                    }
                }

                if (method_OK)
                    table_view();
            }
        }
    
        // кнопка СОХРАНИТЬ ИЗМЕНЕНИЯ
        private void button2_Click(object sender, EventArgs e)
        {
            bool f = true;
            if(group_OK)
            {
                exp a;
                for (int i = 0; i < exp_list.Count; i++)
                {
                    a = exp_list[i];
                    a.choice = Convert.ToBoolean(dataGridView1[0, i].Value);
                    if(a.choice && method == 1)
                    {
                        if(dataGridView1[3, i].Value.ToString() == "")
                        {
                            MessageBox.Show("Не введена компетентность эксперта!" +
                                "\nИзменения не могут быть сохранены!", "Внимание",
                       MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            f = false;
                            break;
                        }
                        else
                            a.comp = Convert.ToString(dataGridView1[3, i].Value);
                        
                    }
                    exp_list[i] = a;
                }
            }
           
            if(f)
            {
                string path = directory + "group" + Convert.ToString(Nselect_problem) + ".txt";
                FileInfo fileInf = new FileInfo(path);
                if (fileInf.Exists)
                    fileInf.Delete();

                string s = "";
                using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.UTF8))
                {
                    s = method.ToString();
                    sw.WriteLine(s);
                    s = "";
                    for (int i = 0; i < exp_list.Count; i++)
                    {
                        if (exp_list[i].choice)
                        {
                            s += exp_list[i].n.ToString() + " ";
                            if (method == 1)
                                s += exp_list[i].comp;

                            sw.WriteLine(s);
                            s = "";
                        }
                    }
                }

                MessageBox.Show("Изменения сохранены!", "Сохранено",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        // кнопка СВЕРНУТЬ ОКНО
        private void button_minimize_Click(object sender, EventArgs e)
        {
            // обеспечивает сворачивание формы при нажатии на тире
            this.WindowState = FormWindowState.Minimized;
        }

        // кнопка ЗАКРЫТЬ ПРИЛОЖЕНИЕ
        private void button_cross_Click(object sender, EventArgs e)
        {
            // обеспечевает закрытие всего приложения нажатии на крестик
            // вызываем главную форму приложения, главная форма всегда = 0
            Form form = Application.OpenForms[0];
            form.Close();  // Закрываем главную форму, а значит закрываем вообще всё
        }
        
        // перетаскивание окна по экрану
        private void Form8_MouseDown(object sender, MouseEventArgs e)
        {
            
            base.Capture = false;
            Message m = Message.Create(base.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }

        // кнопка НАЗАД
        private void button_back_Click(object sender, EventArgs e)
        {
            if (Form8.three_or_four == false)
            {
                form4_analyst_choice form = new form4_analyst_choice();
                form.Show();
                this.Close();
            }
            else if(Form8.three_or_four == true)
            {
                 form3_analyst_add form = new form3_analyst_add();
                form.Show();
                this.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            method_OK = true;
            method = comboBox1.SelectedIndex;
            table_view();
        }
    }
}
