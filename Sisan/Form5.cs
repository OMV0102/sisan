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
        //public string problem; // текст проблемы
        //public int num_problem; // уникальный номер проблемы (ID)
        //public int N = -1; //порядковый номер проблемы в list_prob
        //public int E = -1; //порядковый номер эксперта в list_prob
        public int prob_count = 0;   // количество проблем
        public int alter_count = 0;   // количество альтернатив для выбранной проблемы
        //======================================================================

        #region СТРУКТУРЫ
        public struct exp //  структура для хранения одного эксперта
        {
            public int id_exp;
            public string surname;
            public string name;
            public string otch;
            public string fio; // сокращенное ФИО
            public string password;
            public string position; // Должность
        }
        public List<exp> exp_list;
        //======================================================================
        public struct solutions //  структура для хранения альтернатив
        {
            public int id_prob;
            public string[] alter;
        }
        public List<solutions> alter_list;
        //======================================================================
        public struct metod0_inf //  структура для хранения информации о методе 0
        {
            public int id_exp;
            public int[,] matr;
            public int[] ves;
        }

        public struct metod0 //  структура для хранения метода 0
        {
            public int status;
            public metod0_inf[] inf;
        }
        //========================================================================
        public struct metod1_inf //  структура для хранения информации о методе 1
        {
            public int id_exp;
            public string comp;
            public int[] marks;
        }

        public struct metod1 //  структура для хранения метода 1
        {
            public int mstatus;
            public metod1_inf[] inf;
            public int[] ves;
        }
        //========================================================================
        public struct metod2_inf //  структура для хранения информации о методе 2
        {
            public int id_exp;
            public int[] marks;
        }

        public struct metod2 //  структура для хранения метода 2
        {
            public int status;
            public metod2_inf[] inf;
            public int[] ves;
        }
        //========================================================================
        public struct metod3_inf //  структура для хранения информации о методе 3
        {
            public int id_exp;
            public int[] marks;
        }

        public struct metod3 //  структура для хранения метода 3
        {
            public int status;
            public metod3_inf[] inf;
            public int[] v_matr;
            public int[] ves;
        }
        //========================================================================
        public struct metod4_inf //  структура для хранения информации о методе 4
        {
            public int id_exp;
            public int[,] matr;
        }

        public struct metod4 //  структура для хранения метода 4
        {
            public int status;
            public metod0_inf[] inf;
            public int[] ves;
        }
        //========================================================================
        public struct st_problem //  структура для хранения одной проблемы
        {
            public int num_prob;
            public bool open_close;
            public string txt_prob;
            metod0 m0;
            metod1 m1;
            metod2 m2;
            metod3 m3;
            metod4 m4;
        }
        public List<st_problem> list_prob;
        #endregion

        // при ЗАГРУЗКЕ ФОРМЫ
        private void form5_analyst_report_Load(object sender, EventArgs e)
        {
            lbl_status.Visible = false;
            btn_matrix0.Visible = false;
            btn_matrix1.Visible = false;
            btn_matrix2.Visible = false;
            btn_matrix3.Visible = false;
            btn_matrix4.Visible = false;
            String[] words; //СТРОКА.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);  // это на всякий под рукой
            string text = "";

            FileInfo fileInf1 = new FileInfo(directory + "experts.txt");
            if (fileInf1.Exists)  // если файл существует вообще
            {
                using (StreamReader sr = new StreamReader(directory + "experts.txt", System.Text.Encoding.UTF8))
                {
                    text = sr.ReadToEnd();
                }
            }

            if (text.Length > 0)
            {
                exp_list = new List<exp>();
                using (StreamReader sr = new StreamReader(directory + "experts.txt", System.Text.Encoding.UTF8))
                {
                    string line = "";
                    exp a;
                    while ((line = sr.ReadLine()) != null)
                    {
                        a = new exp();
                        words = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        // записали номер эксперта
                        a.id_exp = Convert.ToInt32(words[0]);
                        // записали ФИО эксперта сокращенное Фамилия И.О.
                        if (words[3] == "-")
                            a.fio = words[1] + " " + words[2].First() + ".";
                        else
                            a.fio = words[1] + " " + words[2].First() + ". " + words[3].First() + ".";
                        // запомнили фамилию
                        a.surname = words[1];
                        // запомнили имя
                        a.name = words[2];
                        // запомнили отчество
                        a.otch = words[3];
                        // запомнили пароль
                        a.password = words[4];
                        // запомнили должность
                        a.position = "";
                        for (int i = 5; i < words.Count(); i++)
                            a.position += words[i] + " ";

                        exp_list.Add(a);
                    }
                }
            }
            //======== экспертов запомнили ==============

            alter_list = new List<solutions>();  // память для списка где харанится альтернативы для проблем
            solutions sol = new solutions();  // вспомогательная переменная для строки выше
            //====== читаем проблемы ==========
            FileInfo fileInf2 = new FileInfo(directory + "problems.txt");
            if (fileInf2.Exists)  // если файл существует вообще
            {
                using (StreamReader sr = new StreamReader(directory + "problems.txt", System.Text.Encoding.UTF8))
                {
                    text = sr.ReadToEnd();
                }
            }

            if (text.Length > 0)
            {
                lbl_notprob.Visible = false;
                StreamReader sr = new StreamReader(directory + "problems.txt", System.Text.Encoding.UTF8);
                string line = "";
                list_prob = new List<st_problem>();
                while ((line = sr.ReadLine()) != null)
                {
                    // считываем проблему
                    words = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    st_problem prob = new st_problem();
                    prob.num_prob = Convert.ToInt32(words[0]);
                    prob.open_close = Convert.ToBoolean(words[1]);
                    prob.txt_prob = words[2];
                    // теперь читаем альтернативы
                    text = "";
                    FileInfo fileInf3 = new FileInfo(directory + "solutions" + prob.num_prob + ".txt");
                    line = "";
                    alter_count = 0;
                    if (fileInf3.Exists)  // если файл существует вообще
                    {
                        using (StreamReader sr1 = new StreamReader(directory + "solutions" + prob.num_prob + ".txt", System.Text.Encoding.UTF8))
                        {
                            while ((line = sr.ReadLine()) != null)
                                alter_count++;
                        }

                        using (StreamReader sr1 = new StreamReader(directory + "solutions" + prob.num_prob + ".txt", System.Text.Encoding.UTF8))
                        {
                            text = sr1.ReadToEnd();
                        }
                    }

                    if (text.Length > 0)
                    {
                        StreamReader sr1 = new StreamReader(directory + "solutions" + prob.num_prob + ".txt", System.Text.Encoding.UTF8);
                        line = "";
                        sol.alter = new string[alter_count];
                        sol.id_prob = prob.num_prob;
                        int i = 0;
                        while ((line = sr.ReadLine()) != null)
                        {
                            sol.alter[i] = line;
                            i++;
                        }
                        alter_list.Add(sol);
                    }

                }
            }
            else
            {
                label1.Visible = false;
                label2.Visible = false;
                panel1.Visible = false;
                list_solution.Visible = false;
                this.Height = 300;
            }
        }

        // кнопка ПОКАЗАТЬ МАТРИЦУ 0
        private void btn_matrix0_Click(object sender, EventArgs e)
        {
            // показываем форму с введенной матрицей
            Form form_matrix = new form6_matrix();
            form_matrix.Owner = this;
            form_matrix.Show();
            this.Hide();
        }
    }
}
