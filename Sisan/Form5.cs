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
using System.Text.RegularExpressions;

namespace system_analysis
{
    public partial class form5_analyst_report : Form
    {
        public form5_analyst_report()
        {
            InitializeComponent();
        }

        //======================================================
        // ПУТЬ К ОСНОВНОМУ РАБОЧЕМУ КАТАЛОГУ
        // ЕСЛИ МЕНЯТЬ ПУТЬ, ТО ТОЛЬКО В 1 ФОРМЕ 
        private string directory = global_class.main_directory;
        //======================================================

        // перемещение окна по экрану
        private void form5_analyst_report_MouseDown(object sender, MouseEventArgs e)
        {
            // обеспечивает перемещение формы без рамки
            base.Capture = false;
            Message m = Message.Create(base.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }

        // кнопка НАЗАД
        private void button_back_Click(object sender, EventArgs e)
        {
            // обеспечевает переход назад при нажатии на стрелку
            Form form = new form4_analyst_choice();
            form.Show();
            form.TopMost = true; form.TopMost = false;
            
            this.Close();
        }

        // кнопка СВЕРНУТЬ окно
        private void button_minimize_Click(object sender, EventArgs e)
        {
            // обеспечивает сворачивание формы при нажатии на тире
            this.WindowState = FormWindowState.Minimized;
        }

        // кнопка ЗАКРЫТЬ окно
        private void button_cross_Click(object sender, EventArgs e)
        {
            button_back_Click(null, null);
        }
        //=================================================================
        public static string NumbProblem = "";
        public static DataTable matrix = new DataTable("Матрица");

        
        public static int count = 0;
        public bool pass = true;
        public bool flag = true;
        private int method = -1;

        public struct experts
        {
            public int num;
            public string name;
            public double comp;
            public double S;
        }

        public struct alt
        {
            public double wt;
            public string A;
        }


        List<experts> list_experts;
        List<string> sol_sort;

        void result_method_0()
        {
            matrix.Clear();
            matrix.Columns.Clear();
            matrix.Rows.Clear();

            List<double> C = new List<double>();
            double R = 0.0;
            string selectedState = comboBox1_problems.SelectedItem.ToString();
            String[] words = selectedState.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            NumbProblem = words[0];
            string path = directory + "matrix" + NumbProblem + ".txt";
            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists)  // если файл есть
            {
                // открываем файл с матрицей на чтение
                using (StreamReader sr = new StreamReader(path, System.Text.Encoding.UTF8))
                {
                    int m = 0;

                    string line;
                    count = 0;
                    // читаем в цикле построчно
                    while ((line = sr.ReadLine()) != null)
                    {
                        C.Add(0);
                        words = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; i < words.Length; i++)
                        {
                            if (words[i] != "9")
                            {
                                C[m] += Convert.ToDouble(words[i]);
                                R += Convert.ToDouble(words[i]);

                            }
                            if (words[i] == "-1")
                            {
                                pass = false;
                            }
                        }
                        m++;
                        // делим строку на элементы строки
                        // если это первая итерация, создаем столбцы
                        if (count == 0 && flag == true)
                        {
                            flag = false;
                            for (int i = 0; i < words.Length; i++)
                            {
                                // добавляем столбцы
                                matrix.Columns.Add(new DataColumn(Convert.ToString(i)));
                            }
                        }
                        // теперь создает текущую строку
                        DataRow dr = matrix.NewRow();
                        for (int j = 0; j < words.Length; j++)
                        {
                            if (words[j] != "9") // если не диагональ
                                dr[Convert.ToString(j)] = words[j]; // то берем значение из файла и пишем сюда
                            else // иначе
                                dr[Convert.ToString(j)] = " "; // пишем пробел
                        }
                        matrix.Rows.Add(dr); // добавляем строку в матрицу
                        count++;
                    }

                }
                if (pass == false)
                {
                    listBox0_alt.Items.Clear();
                    btn_matrix0.Enabled = false;
                    MessageBox.Show(
                   "Прохождение опроса начато экспертом, но еще не закончено!",
                   "",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Error,
                   MessageBoxDefaultButton.Button1,
                   MessageBoxOptions.DefaultDesktopOnly);
                    //comboBox1_problems.SelectedIndex = -1;
                    comboBox1_problems.SelectedText = "";
                    this.TopMost = true; this.TopMost = false;
                    
                }
                else
                {

                    //===========================================================================================
                    /*m = 0;
                    path = directory + "solutions" + NumbProblem + ".txt";
                    using (StreamReader sr = new StreamReader(path, System.Text.Encoding.UTF8))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            sol_sort.Add(line);
                            m++;
                            //listBox1.Items.Add(line);
                        }
                    }
                    // заполняем список с просто альтернативами
                    for (int i = 0; i < sol_sort.Count; i++)
                    {
                        listBox2.Items.Add(sol_sort[i]);
                    }*/

                    for (int i = 0; i < C.Count; i++)
                        C[i] = C[i] / R;
                    double tmp = 0;
                    string stmp = "";
                    //сортировка по убыванию
                    int step = C.Count / 2;
                    while (step > 0)
                    {
                        for (int i = 0; i < C.Count - step; i++)
                        {
                            int j = i;
                            while (j >= 0)
                            {
                                if (C[j] < C[j + step])
                                {
                                    tmp = C[j];
                                    C[j] = C[j + step];
                                    C[j + step] = tmp;
                                    stmp = sol_sort[j];
                                    sol_sort[j] = sol_sort[j + step];
                                    sol_sort[j + step] = stmp;
                                    j = j - step;

                                }
                                else j--;
                            }
                        }
                        step = step / 2;

                    }

                    // Заполняем список по упорядоченным
                    for (int i = 0; i < sol_sort.Count; i++)
                    {
                        listBox0_alt.Items.Add(sol_sort[i]);
                    }

                    for (int i = 0; i < sol_sort.Count; i++)
                    {
                        //listBox3.Items.Add((Math.Round(C[i], 4)));
                        //listBox3.Items.Add((Math.Round(C[i], 4)));
                    }

                    btn_matrix0.Enabled = true;
                }
            }
            else
            {
                listBox0_alt.Items.Clear();
                btn_matrix0.Enabled = false;
                MessageBox.Show(
                "Опрос по выбранной проблеме еще не пройден экспертом!",
                "",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
                comboBox1_problems.SelectedText = "";
                this.TopMost = true; this.TopMost = false;
                
            }
        }

        double comp_exp(int k)
        {
            for(int i = 0; i < list_experts.Count ; i++)
            {
                if (k == list_experts[i].num)
                    return list_experts[i].S;
            }
            return 0;
        }

        void result_method_1()
        {
            List<alt> all = new List<alt>();
            alt t;
            t.A = "";
            t.wt = 0;
            for (int i = 0; i < sol_sort.Count; i++)
            {
                all.Add(t); // отваливаем память для списка альтернатив
            }

            experts temp;
            double R = 0;

            for (int i = 0; i < list_experts.Count; i++)
            {
                R += list_experts[i].comp;
            }

            for (int i = 0; i < list_experts.Count; i++)
            {
                temp = list_experts[i];
                temp.S = temp.comp / R;
                list_experts[i] = temp;
            }

            string path = directory + "matrixv" + NumbProblem + ".txt";
            List<double> rating = new List<double>();
            List<string>  wt = new List<string>();

            int k = 0;
            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    k++;
                    string[] world = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    for(int i = 1; i < sol_sort.Count + 1; i++)
                    {
                        t = all[i-1];
                        t.wt += Convert.ToDouble(world[i]) * comp_exp(Convert.ToInt32(world[0]));
                        all[i-1] = t;
                    }
                }
            }
            if (k == list_experts.Count)
            {
                for (int i = 0; i < sol_sort.Count; i++)
                {
                    t = all[i];
                    t.A = sol_sort[i];
                    all[i] = t;
                }


                var sortedAlt = from u in all // ого, встроенная сортировка
                                orderby u.wt descending
                                select u;
                all = sortedAlt.ToList<alt>();

                for (int i = 0; i < all.Count; i++)
                {
                    listBox0_alt.Items.Add(all[i].A);
                    //listBox3.Items.Add(all[i].wt.ToString());
                }
            }
            else
            {
                MessageBox.Show("Эксперты еще не закончили работу!", "Внимание",
                       MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
            
        // когда выбираем проблему в списке
        private void comboBox1_problems_SelectedIndexChanged(object sender, EventArgs e)
        {
            int m = 0;
             sol_sort = new List<string>();
           
            flag = true;
            listBox0_alt.Items.Clear();
            listBox3_alt.Items.Clear();
            //listBox3.Items.Clear();

            string selectedState = comboBox1_problems.SelectedItem.ToString();
            String[] words = selectedState.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            NumbProblem = words[0];
            string path = directory + "solutions" + NumbProblem + ".txt";
            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    sol_sort.Add(line);
                    m++;
                }
            }
            // заполняем список с просто альтернативами
            for (int i = 0; i < sol_sort.Count; i++)
            {
                listBox3_alt.Items.Add(sol_sort[i]);
            }

            path = directory + "group" + NumbProblem + ".txt";
            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists)  // если файл есть
            {
                list_experts = new List<experts>();
                experts temp;

                using (StreamReader sr = new StreamReader(path, System.Text.Encoding.UTF8))
                {
                    int i = 0;
                    string line;
                    // читаем в цикле построчно
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (i == 0)
                            method = Convert.ToInt32(line);
                        else
                        {
                            string[] world = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            temp.num = Convert.ToInt32(world[0]);
                            if (method == 1)
                                temp.comp = Convert.ToInt32(world[1]);
                            else
                                temp.comp = -1;
                            temp.name = "";
                            temp.S = -1;
                            list_experts.Add(temp);
                        }
                        i++;
                    }
                }
                path = directory + "experts.txt";
                using (StreamReader sr = new StreamReader(path, System.Text.Encoding.UTF8))
                {
                    string line;
                    // читаем в цикле построчно
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] world = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        int i;
                        bool f = true;
                        for ( i = 0; i < list_experts.Count && f; i++)
                        {
                            if (list_experts[i].num.ToString() == world[0])
                            {
                                f = false;
                                temp = list_experts[i];
                                temp.name = world[1] + " " + world[2] + " " + world[3];
                                list_experts[i] = temp;
                            }
                        }

                    }
                }

                if(method == 0)
                {
                    label4.Visible = true;
                    comboBox1.Visible = true;
                    for (int i = 0; i < list_experts.Count; i++)
                    {
                        comboBox1.Items.Add(list_experts[i].name);
                    }
                }
                
                if(method == 1)
                {
                    label4.Visible = false;
                    comboBox1.Visible = false;
                    result_method_1();
                }
                
            }
            else
            {
                // все умерли
            }

        }

        // кнопка ПОКАЗАТЬ МАТРИЦУ 
        private void btn_matrix0_Click(object sender, EventArgs e)
        {
            // показываем форму с введенной матрицей
            Form form_matrix = new form6_matrix();
            form_matrix.Owner = this;
            form_matrix.Show();
            this.Hide();
        }

        // при ЗАГРУЗКЕ ФОРМЫ
        private void form5_analyst_report_Load(object sender, EventArgs e)
        {
            /*btn_matrix0.Visible = false;
            label4.Visible = false;
            comboBox1.Visible = false;
            listBox0_alt.Items.Clear();
            btn_matrix0.Enabled = false;
            string text = "";
            using (StreamReader sr = new StreamReader(directory + "problems.txt", System.Text.Encoding.UTF8))
            {
                text = sr.ReadToEnd();
            }

            if (text.Length != 0)
            {
                using (StreamReader sr = new StreamReader(directory +  "problems.txt", System.Text.Encoding.UTF8))
                {

                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        comboBox1_problems.Items.Add(line);
                    }
                }
            }*/

        }
    }
}
