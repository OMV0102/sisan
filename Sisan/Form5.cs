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
        st_problem prob; // вспомогательная переменная для добавления проблемы
        public int alter_count = 0;   // количество альтернатив для выбранной проблемы
        public int exp_count = 0;   // количество экспертов для выбранной проблемы
        public int index = -1; // индекс проблемы при выборе комобобокса проблемы
        public int index_exp = -1;  // индекс эксперта при выборе комобобокса экспертов в методе 0
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
            public string[] alters;
        }
        public List<solutions> alter_list;
        //======================================================================
        public struct st_problem //  структура для хранения одной проблемы
        {
            public int num_prob;
            public bool open_close;
            public string txt_prob;
            public int status_prob;
            public metod0 m0;
            public metod1 m1;
            public metod2 m2;
            public metod3 m3;
            public metod4 m4;
        }
        public List<st_problem> prob_list;
        //======================================================================
        public struct metod0_inf //  структура для хранения информации о методе 0
        {
            public int id_exp;
            public float[,] matr;
            public float[] ves;
            public int status;
        }

        public struct metod0 //  структура для хранения метода 0
        {
            public int status;   // по факту не используется в данном методе
            public metod0_inf[] inf;
        }
        //========================================================================
        public struct metod1_inf //  структура для хранения информации о методе 1
        {
            public int id_exp;
            public float comp;
            public int status;
            public float[] marks;
        }

        public struct metod1 //  структура для хранения метода 1
        {
            public int status;
            public metod1_inf[] inf;
            public float[] ves;
        }
        //========================================================================
        public struct metod2_inf //  структура для хранения информации о методе 2
        {
            public int id_exp;
            public float[] marks;
            public int status;
        }

        public struct metod2 //  структура для хранения метода 2
        {
            public int status;
            public metod2_inf[] inf;
            public float L;
            public float[] ves;
        }
        //========================================================================
        public struct metod3_inf //  структура для хранения информации о методе 3
        {
            public int id_exp;
            public float[] marks;
            public int status;
        }

        public struct metod3 //  структура для хранения метода 3
        {
            public int status;
            public metod3_inf[] inf;
            public float[] ves;
        }
        //========================================================================
        public struct metod4_inf //  структура для хранения информации о методе 4
        {
            public int id_exp;
            public float[,] matr;
            public int status;
        }

        public struct metod4 //  структура для хранения метода 4
        {
            public int status;
            public metod4_inf[] inf;
            public float[] ves;
        }
        //========================================================================
       
        #endregion

        // при ЗАГРУЗКЕ ФОРМЫ
        private void form5_analyst_report_Load(object sender, EventArgs e)
        {
            lbl_notmarks.Visible = false;
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
                using (StreamReader sr = new StreamReader(directory + "problems.txt", System.Text.Encoding.UTF8))
                {
                    string line = "";
                    prob_list = new List<st_problem>();
                    while ((line = sr.ReadLine()) != null)
                    {
                        // считываем проблему
                        words = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        prob = new st_problem();
                        prob.num_prob = Convert.ToInt32(words[0]);
                        prob.open_close = Convert.ToBoolean(words[1]);
                        prob.txt_prob = "";
                        for (int i = 2; i < words.Count(); i++)
                            prob.txt_prob += words[i] + " ";
                        // =========== проблему считали ===========
                        text = "";
                        line = "";
                        alter_count = 0;
                        FileInfo fileInf3 = new FileInfo(directory + "solutions" + prob.num_prob + ".txt");
                        if (fileInf3.Exists)  // если файл существует вообще
                        {
                            using (StreamReader sr1 = new StreamReader(directory + "solutions" + prob.num_prob + ".txt", System.Text.Encoding.UTF8))
                            {
                                while ((line = sr1.ReadLine()) != null)
                                    alter_count++;
                            }
                        }

                        if (alter_count > 0)
                        {
                            using (StreamReader sr1 = new StreamReader(directory + "solutions" + prob.num_prob + ".txt", System.Text.Encoding.UTF8))
                            {
                                line = "";
                                sol.alters = new string[alter_count];
                                sol.id_prob = prob.num_prob;
                                int i = 0;
                                int n = 1;
                                while ((line = sr1.ReadLine()) != null)
                                {
                                    sol.alters[i] = n + ". " + line;
                                    i++;
                                    n++;
                                }
                            }
                            alter_list.Add(sol);
                        }
                        // ========= альтернативы считали ==============

                        // теперь считываем group
                        text = "";
                        line = "";
                        exp_count = 0;
                        FileInfo fileInf4 = new FileInfo(directory + "group" + prob.num_prob + ".txt");
                        if (fileInf4.Exists)  // если файл существует вообще
                        {
                            using (StreamReader sr1 = new StreamReader(directory + "group" + prob.num_prob + ".txt", System.Text.Encoding.UTF8))
                            {
                                while ((line = sr1.ReadLine()) != null)
                                    exp_count++;
                            }
                        }

                        if (exp_count > 0)
                        {
                            line = "";
                            // ===== выделили память для каждого метода размером с кол-во экспертов exp_count
                            prob.m0.inf = new metod0_inf[exp_count];

                            prob.m1.inf = new metod1_inf[exp_count];
                            prob.m1.ves = new float[alter_count];
                            prob.m2.inf = new metod2_inf[exp_count];
                            prob.m2.ves = new float[alter_count];
                            prob.m3.inf = new metod3_inf[exp_count];
                            prob.m3.ves = new float[alter_count];
                            prob.m4.inf = new metod4_inf[exp_count];
                            prob.m4.ves = new float[alter_count];
                            // ====================================================================
                            using (StreamReader sr1 = new StreamReader(directory + "group" + prob.num_prob + ".txt", System.Text.Encoding.UTF8))
                            {
                                for (int k = 0; k < exp_count; k++)
                                {
                                    if ((line = sr1.ReadLine()) != null)
                                    {
                                        words = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                        // ==== Запоминаем для метода 0 ===================
                                        prob.m0.inf[k].id_exp = Convert.ToInt32(words[0]); // запомнили id эксперта
                                        prob.m0.inf[k].status = Convert.ToInt32(words[2]); // статус прохождения опроса
                                        prob.m0.inf[k].matr = new float[alter_count, alter_count]; // память под оценки эксперта k
                                        prob.m0.inf[k].ves = new float[alter_count]; // память под веса оценок эксперта k
                                        load_m0(k); // загрузка метода 0 для k эксперта
                                        // ==== Метод 0 запомнили =======================
                                        // ==== Запоминаем для метода 1 ===================
                                        prob.m1.inf[k].id_exp = Convert.ToInt32(words[0]); // запомнили id эксперта
                                        prob.m1.inf[k].comp = Convert.ToSingle(words[1]); // запомнили компетентность эксперта
                                        prob.m1.inf[k].status = Convert.ToInt32(words[3]); // статус прохождения опроса
                                        prob.m1.inf[k].marks = new float[alter_count]; // память под оценки эксперта k
                                        load_m1(k); // загрузка метода 1 для k эксперта
                                        // ==== Метод 1 запомнили =======================
                                        // ==== Запоминаем для метода 2 ===================
                                        prob.m2.inf[k].id_exp = Convert.ToInt32(words[0]); // запомнили id эксперта
                                        prob.m2.inf[k].status = Convert.ToInt32(words[4]); // статус прохождения опроса
                                        prob.m2.inf[k].marks = new float[alter_count]; // память под оценки эксперта k
                                        load_m2(k); // загрузка метода 2 для k эксперта
                                        // ==== Метод 2 запомнили =======================
                                        // ==== Запоминаем для метода 3 ===================
                                        prob.m3.inf[k].id_exp = Convert.ToInt32(words[0]); // запомнили id эксперта
                                        prob.m3.inf[k].status = Convert.ToInt32(words[5]); // статус прохождения опроса
                                        prob.m3.inf[k].marks = new float[alter_count]; // память под оценки эксперта k
                                        load_m3(k); // загрузка метода 3 для k эксперта
                                        // ==== Метод 3 запомнили =======================
                                        // ==== Запоминаем для метода 4 ===================
                                        prob.m4.inf[k].id_exp = Convert.ToInt32(words[0]); // запомнили id эксперта
                                        prob.m4.inf[k].status = Convert.ToInt32(words[6]); // статус прохождения опроса
                                        prob.m4.inf[k].matr = new float[alter_count, alter_count]; // память под оценки эксперта k
                                        load_m4(k); // загрузка метода 4 для k эксперта
                                        // ==== Метод 4 запомнили =======================
                                    }
                                }
                            }
                            //  === приводим к целым числам веса для 0 метода ==============
                            for (int k = 0; k < exp_count; k++)
                            {
                                for (int j = 0; j < alter_count; j++)
                                    prob.m0.inf[k].ves[j] *= 1000; 
                            }
                            //  === досчитываем веса для 2 3 4 метода и приводим к целым числам ==============
                            for (int j = 0; j < alter_count; j++)
                            {
                                prob.m2.ves[j] /= prob.m2.L;
                                prob.m2.ves[j] *= 1000;

                                prob.m3.ves[j] *= 1000;

                                prob.m4.ves[j] *= 10;
                            }
                            //====================================================
                            prob.status_prob = exp_count;
                        }
                        else
                        {
                            prob.status_prob = 0;
                        }
                        prob_list.Add(prob);
                        prob_count++;
                    }

                }
            }
            else
            {
                prob_count = 0;
                lbl_notprob.Visible = true;
                label1.Visible = false;
                label2.Visible = false;
                panel1.Visible = false;
                list_solution.Visible = false;
                this.Height = 300;
            }

            if(prob_count > 0)
            {
                for(int i = 0; i < prob_count; i++)
                {
                    comboBox_problems.Items.Add(prob_list[i].txt_prob);
                }
            }

            if (prob_count > 0)
            {
                comboBox_problems.SelectedIndex = 0;
            }
        }

        // для ЗАГРУЗКИ МЕТОДА 0
        public void load_m0(int k)
        {
            if (prob.m0.inf[k].status == 1 || prob.m0.inf[k].status == -1)
            {
                // если статус пройден или не до конца пройден то считываем матрицу
                if (File.Exists(directory + "matrix" + prob.num_prob + "m0e" + prob.m0.inf[k].id_exp + ".txt") == true)
                {
                    float sum = 0;
                    using (StreamReader sr2 = new StreamReader(directory + "matrix" + prob.num_prob + "m0e" + prob.m0.inf[k].id_exp + ".txt", System.Text.Encoding.UTF8))
                    {
                        string[] words1;
                        string line = "";
                        for (int i = 0; i < alter_count; i++)
                        {
                            if ((line = sr2.ReadLine()) != null)
                            {
                                words1 = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                prob.m0.inf[k].ves[i] = 0;
                                for (int j = 0; j < alter_count; j++)
                                {
                                    prob.m0.inf[k].matr[i, j] = Convert.ToSingle(words1[j]);
                                    if (prob.m0.inf[k].status == 1 && i != j)
                                    {
                                        sum += prob.m0.inf[k].matr[i, j];
                                        prob.m0.inf[k].ves[i] += prob.m0.inf[k].matr[i, j];
                                    }

                                }
                            }
                        }
                    }
                    // == считали из файла матрицу и если статус 1 то запомнили веса ===

                    if (prob.m0.inf[k].status == 1)
                    {
                        for (int i = 0; i < alter_count; i++)
                            prob.m0.inf[k].ves[i] /= sum;
                    }
                    else if (prob.m0.inf[k].status == -1)
                    {
                        for (int i = 0; i < alter_count; i++)
                            prob.m0.inf[k].ves[i] = -1;
                    }
                }
            }
            else if (prob.m0.inf[k].status == 0)
            {
                for (int i = 0; i < alter_count; i++)
                {
                    // веса равны -1
                    prob.m0.inf[k].ves[i] = -1;
                    for (int j = 0; j < alter_count; j++)
                    {
                        if (i == j)
                            prob.m0.inf[k].matr[i, j] = 9;
                        else
                            prob.m0.inf[k].matr[i, j] = -1;
                    }
                }
                    
            }
        }

        // для ЗАГРУЗКИ МЕТОДА 1
        public void load_m1(int k)
        {
            if (prob.m1.inf[k].status == 1)
            {
                // если статус пройдeн у эксперта то читаем его оценочки
                if (File.Exists(directory + "matrix" + prob.num_prob + "m1.txt") == true)
                {
                    using (StreamReader sr2 = new StreamReader(directory + "matrix" + prob.num_prob + "m1.txt", System.Text.Encoding.UTF8))
                    {
                        string[] words1;
                        string line = "";

                        while ((line = sr2.ReadLine()) != null)
                        {
                            words1 = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            if (Convert.ToInt32(words1[0]) == prob.m1.inf[k].id_exp)
                            {
                                for (int j = 0; j < alter_count; j++)
                                {
                                    prob.m1.inf[k].marks[j] = Convert.ToSingle(words1[j+1]);
                                    //prob.m1.ves[j] += prob.m1.inf[k].marks[j] / scale_m1 * prob.m1.inf[k].comp;
                                    prob.m1.ves[j] += prob.m1.inf[k].marks[j] * prob.m1.inf[k].comp;

                                }
                            }
                        }

                    }
                    // == считали из файла матрицу и если статус 1 то запомнили веса ===
                }
            }
            else if (prob.m1.inf[k].status == 0)
            {
                for (int j = 0; j < alter_count; j++)
                {
                    prob.m1.inf[k].marks[j] = -1;
                }
            }
        }

        // для ЗАГРУЗКИ МЕТОДА 2
        public void load_m2(int k)
        {
            if (prob.m2.inf[k].status == 1)
            {
                // если статус пройдeн у эксперта то читаем его оценочки
                if (File.Exists(directory + "matrix" + prob.num_prob + "m2.txt") == true)
                {
                    using (StreamReader sr2 = new StreamReader(directory + "matrix" + prob.num_prob + "m2.txt", System.Text.Encoding.UTF8))
                    {
                        string[] words1;
                        string line = "";

                        while ((line = sr2.ReadLine()) != null)
                        {
                            words1 = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            if (Convert.ToInt32(words1[0]) == prob.m2.inf[k].id_exp)
                            {
                                for (int j = 0; j < alter_count; j++)
                                {
                                    prob.m2.inf[k].marks[j] = Convert.ToSingle(words1[j + 1]);
                                    prob.m2.ves[j] += (float)alter_count + 1 - prob.m2.inf[k].marks[j];
                                    prob.m2.L += prob.m2.ves[j];
                                }
                            }
                        }
                    }
                    // == считали из файла матрицу и если статус 1 то запомнили веса ===
                }
            }
            else if (prob.m2.inf[k].status == 0)
            {
                for (int j = 0; j < alter_count; j++)
                {
                    prob.m2.inf[k].marks[j] = -1;
                }
            }
        }

        // для ЗАГРУЗКИ МЕТОДА 3
        public void load_m3(int k)
        {
            if (prob.m3.inf[k].status == 1)
            {
                // если статус пройдeн у эксперта то читаем его оценочки
                if (File.Exists(directory + "matrix" + prob.num_prob + "m3.txt") == true)
                {
                    float S = 0;
                    using (StreamReader sr2 = new StreamReader(directory + "matrix" + prob.num_prob + "m3.txt", System.Text.Encoding.UTF8))
                    {
                        string[] words1;
                        string line = "";

                        while ((line = sr2.ReadLine()) != null)
                        {
                            words1 = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            if (Convert.ToInt32(words1[0]) == prob.m3.inf[k].id_exp)
                            {
                                for (int j = 0; j < alter_count; j++)
                                {
                                    prob.m3.inf[k].marks[j] = Convert.ToSingle(words1[j + 1]);
                                    S += prob.m3.inf[k].marks[j];
                                }

                                if (S > 0)
                                {
                                    for (int j = 0; j < alter_count; j++)
                                    {
                                        prob.m3.ves[j] += prob.m3.inf[k].marks[j] / S / exp_count;
                                    }
                                }
                            }
                        }

                    }
                    // == считали из файла матрицу и если статус 1 то запомнили веса ===
                }
            }
            else if (prob.m3.inf[k].status == 0)
            {
                for (int j = 0; j < alter_count; j++)
                {
                    prob.m3.inf[k].marks[j] = -1;
                }
            }
        }

        // для ЗАГРУЗКИ МЕТОДА 4
        public void load_m4(int k)
        {
            if (prob.m4.inf[k].status == 1 || prob.m4.inf[k].status == -1)
            {
                // если статус пройден или не до конца пройден то считываем матрицу
                if (File.Exists(directory + "matrix" + prob.num_prob + "m4e" + prob.m4.inf[k].id_exp + ".txt") == true)
                {
                    float N = alter_count * (alter_count - 1);
                    float sum_str = 0;
                    using (StreamReader sr2 = new StreamReader(directory + "matrix" + prob.num_prob + "m4e" + prob.m4.inf[k].id_exp + ".txt", System.Text.Encoding.UTF8))
                    {
                        string[] words1;
                        string line = "";
                        for (int i = 0; i < alter_count; i++)
                        {
                            if ((line = sr2.ReadLine()) != null)
                            {
                                words1 = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                sum_str = 0;
                                for (int j = 0; j < alter_count; j++)
                                {
                                    prob.m4.inf[k].matr[i, j] = Convert.ToSingle(words1[j]);
                                    if (prob.m4.inf[k].status == 1 && i != j)
                                    {
                                        sum_str += prob.m4.inf[k].matr[i, j];
                                    }
                                }

                                if (prob.m4.inf[k].status == 1)
                                {
                                    prob.m4.ves[i] += sum_str / N;
                                }
                            }
                        }
                    }
                    // == считали из файла матрицу и если статус 1 то запомнили веса ===
                }
            }
            else if (prob.m4.inf[k].status == 0)
            {
                for (int i = 0; i < alter_count; i++)
                {
                    for (int j = 0; j < alter_count; j++)
                    {
                        if (i == j)
                            prob.m4.inf[k].matr[i, j] = 9;
                        else
                            prob.m4.inf[k].matr[i, j] = -1;
                    }
                }

            }
        }

        // ВЫБОР ПРОБЛЕМЫ в comboBox_problems
        private void comboBox_problems_SelectedIndexChanged(object sender, EventArgs e)
        {
            index = comboBox_problems.SelectedIndex;
            if (index >= 0 && index < prob_count)
            {
                exp_count = prob_list[index].status_prob;
                alter_count = alter_list[index].alters.Count();
                //===== ВЫВОДИМ АЛЬТЕРНАТИВЫ ======
                list_solution.Items.Clear();
                for (int i = 0; i < alter_count; i++)
                {
                    list_solution.Items.Add(alter_list[index].alters[i]);
                }
                //=================================

                // если у проблемы как минимум назначен один эксперт
                if (prob_list[index].status_prob > 0)
                {
                    //================================
                    lbl_notexp.Visible = false;
                    label2.Visible = true;
                    panel1.Visible = true;
                    //================================
                    bool notmarks = true;
                    for (int i = 0; i < exp_count; i++)
                    {
                        if (prob_list[index].m0.inf[i].status == 1 || prob_list[index].m1.inf[i].status == 1)
                            notmarks = false;
                        if (prob_list[index].m2.inf[i].status == 1 || prob_list[index].m3.inf[i].status == 1 || prob_list[index].m4.inf[i].status == 1)
                            notmarks = false;
                    }

                    if(notmarks == false) // если оценки по проблеме хоть какие то есть
                    {
                        //================================
                        lbl_notmarks.Visible = false;
                        label2.Visible = true;
                        panel1.Visible = true;
                        //================================
                        // Дальше ВЫВОДИМ РЕЗУЛЬТАТЫ

                        // ===== МЕТОД 0 =============
                        // по id эксперта в списке проблем ищем эксперта в списке экспертов 
                        comboBox_exp.Items.Clear();
                        for (int k = 0; k < exp_count; k++)
                        {
                            for (int i = 0; i < exp_list.Count; i++)
                            {
                                if (prob_list[index].m0.inf[k].id_exp == exp_list[i].id_exp)
                                {
                                    comboBox_exp.Items.Add(exp_list[i].fio);
                                }
                            }
                        }
                        // ВЫБИРАЕМ нулевого эксперта в комбоксе экспертов метода 0
                        if(comboBox_exp.Items.Count > 0)
                            comboBox_exp.SelectedIndex = 0;
                        // ===== МЕТОД 1 =============
                        update_m1();
                        // ===== МЕТОД 2 =============
                        update_m2();
                        // ===== МЕТОД 3 =============
                        update_m3();
                        // ===== МЕТОД 4 =============
                        //update_m4();
                        //=======================
                    }
                    // если эксперты назначены но еще никто ничего не оценил
                    else if(notmarks == true)
                    {
                        //================================
                        lbl_notmarks.Visible = true;
                        label2.Visible = false;
                        panel1.Visible = false;
                        //================================
                    }
                }
                // если у проблемы не назначены эксперты
                else if (prob_list[index].status_prob == 0)
                {
                    //================================
                    lbl_notexp.Visible = true;
                    label2.Visible = false;
                    panel1.Visible = false;
                    //================================
                }

            }
        }

        // ВЫБОР ЭКСПЕРТА В МЕТОДЕ 0 (ПАРНЫХ СРАВНЕНИЙ)
        private void comboBox_exp_SelectedIndexChanged(object sender, EventArgs e)
        {
            index_exp = comboBox_exp.SelectedIndex;
            if (index_exp >= 0 && index_exp < exp_count)
            {
                string exp_fio = comboBox_exp.SelectedItem.ToString();
                int id_exp = -1;
                // ищем id эксперта в списке экспертов по его фио
                for (int i = 0; i < exp_list.Count; i++)
                {
                    if (exp_fio == exp_list[i].fio)
                    {
                        id_exp = exp_list[i].id_exp;
                    }
                }
                // если нашли id
                if (id_exp != -1)
                {
                    index_exp = -1;
                    // ищем в списке проблем в методе 0 эксперта по найдненному id
                    for (int i = 0; i < exp_count; i++)
                    {
                        if (prob_list[index].m0.inf[i].id_exp == id_exp)
                        {
                            index_exp = i;
                        }
                    }
                    // если нашли индекс эксперта по его id в списке проблем метода 0
                    if (index_exp >= 0 && index_exp < exp_count)
                    {
                        if (prob_list[index].m0.inf[index_exp].status == 1)
                        {
                            //===================================
                            label8.Visible = true;
                            label9.Visible = true;
                            listBox0_alt.Visible = true;
                            listBox0_ves.Visible = true;
                            btn_matrix0.Visible = true;
                            lbl_m0_notmarks.Visible = false;
                            //================================
                            listBox0_alt.Items.Clear();
                            listBox0_ves.Items.Clear();
                            // записали альтернативы и веса в вспомогательный массив
                            string alt_temp = "";
                            int ves_temp = 0;
                            string[] alter = new string[alter_count];
                            int[] ves = new int[alter_count];

                            for (int i = 0; i < alter_count; i++)
                            {
                                alter[i] = alter_list[index].alters[i];
                                ves[i] = Convert.ToInt32(prob_list[index].m0.inf[index_exp].ves[i]);
                            }
                            // сортируем альтернативы
                            // сортировка взята с https://metanit.com/sharp/tutorial/2.7.php
                            for (int i = 0; i < alter_count - 1; i++)
                            {
                                for (int j = i + 1; j < alter_count; j++)
                                {
                                    if (ves[i] < ves[j])
                                    {
                                        alt_temp = alter[i];
                                        ves_temp = ves[i];
                                        alter[i] = alter[j];
                                        ves[i] = ves[j];
                                        alter[j] = alt_temp;
                                        ves[j] = ves_temp;
                                    }
                                }
                            }
                            // выводим в listbox0_alt и listbox0_ves
                            for (int i = 0; i < alter_count; i++)
                            {
                                listBox0_alt.Items.Add(alter[i]);
                                listBox0_ves.Items.Add(ves[i]);
                            }
                        }
                        else
                        {
                            //===================================
                            // эксперт еще не оценил альтернативы методом 0
                            label8.Visible = false;
                            label9.Visible = false;
                            listBox0_alt.Visible = false;
                            listBox0_ves.Visible = false;
                            btn_matrix0.Visible = false;
                            lbl_m0_notmarks.Visible = true;
                            //================================
                        }
                    }
                }
            }
        }

        // ВЫВОД РЕЗУЛЬТАТОВ МЕТОДА 1 
        public void update_m1()
        {
            int exp_otvet = 0;
            for (int i = 0; i < exp_count; i++)
            {
                if (prob_list[index].m1.inf[i].status == 1)
                {
                    exp_otvet++;
                }
            }

            if (exp_otvet > 0)
            {
                //===================================
                // ХОТЯБЫ ОДИН из экспертов оценил альтернативы методом 1
                lbl_statm1.Visible = true;
                label16.Visible = true;
                label10.Visible = true;
                listBox1_alt.Visible = true;
                listBox1_ves.Visible = true;
                btn_matrix1.Visible = true;
                lbl_m1_notmarks.Visible = false;
                //================================
                listBox1_alt.Items.Clear();
                listBox1_ves.Items.Clear();
                lbl_statm1.Text = "Ранжирование альтернатив на основе\n оценок " + exp_otvet + " из " + exp_count + " экспертов";
                // записали альтернативы и веса в вспомогательный массив
                string alt_temp = "";
                int ves_temp = 0;
                string[] alter = new string[alter_count];
                int[] ves = new int[alter_count];

                for (int i = 0; i < alter_count; i++)
                {
                    alter[i] = alter_list[index].alters[i];
                    ves[i] = Convert.ToInt32(prob_list[index].m1.ves[i]);
                }
                // сортируем альтернативы
                // сортировка взята с https://metanit.com/sharp/tutorial/2.7.php
                for (int i = 0; i < alter_count - 1; i++)
                {
                    for (int j = i + 1; j < alter_count; j++)
                    {
                        if (ves[i] < ves[j])
                        {
                            alt_temp = alter[i];
                            ves_temp = ves[i];
                            alter[i] = alter[j];
                            ves[i] = ves[j];
                            alter[j] = alt_temp;
                            ves[j] = ves_temp;
                        }
                    }
                }
                // выводим в listbox0_alt и listbox0_ves
                for (int i = 0; i < alter_count; i++)
                {
                    listBox1_alt.Items.Add(alter[i]);
                    listBox1_ves.Items.Add(ves[i]);
                }
            }
            else
            {
                //===================================
                // НИКТО из экспертов еще не оценил альтернативы методом 1
                lbl_statm1.Visible = false;
                label16.Visible = false;
                label10.Visible = false;
                listBox1_alt.Visible = false;
                listBox1_ves.Visible = false;
                btn_matrix1.Visible = false;
                lbl_m1_notmarks.Visible = true;
                //================================
            }

        }

        // ВЫВОД РЕЗУЛЬТАТОВ МЕТОДА 2 
        public void update_m2()
        {
            int exp_otvet = 0;
            for (int i = 0; i < exp_count; i++)
            {
                if (prob_list[index].m2.inf[i].status == 1)
                {
                    exp_otvet++;
                }
            }

            if (exp_otvet > 0)
            {
                //===================================
                // ХОТЯБЫ ОДИН из экспертов оценил альтернативы методом 2
                lbl_statm2.Visible = true;
                label18.Visible = true;
                label19.Visible = true;
                listBox2_alt.Visible = true;
                listBox2_ves.Visible = true;
                btn_matrix2.Visible = true;
                lbl_m2_notmarks.Visible = false;
                //================================
                listBox2_alt.Items.Clear();
                listBox2_ves.Items.Clear();
                lbl_statm2.Text = "Ранжирование альтернатив на основе\n оценок " + exp_otvet + " из " + exp_count + " экспертов";
                // записали альтернативы и веса в вспомогательный массив
                string alt_temp = "";
                int ves_temp = 0;
                string[] alter = new string[alter_count];
                int[] ves = new int[alter_count];

                for (int i = 0; i < alter_count; i++)
                {
                    alter[i] = alter_list[index].alters[i];
                    ves[i] = Convert.ToInt32(prob_list[index].m2.ves[i]);
                }
                // сортируем альтернативы
                // сортировка взята с https://metanit.com/sharp/tutorial/2.7.php
                for (int i = 0; i < alter_count - 1; i++)
                {
                    for (int j = i + 1; j < alter_count; j++)
                    {
                        if (ves[i] < ves[j])
                        {
                            alt_temp = alter[i];
                            ves_temp = ves[i];
                            alter[i] = alter[j];
                            ves[i] = ves[j];
                            alter[j] = alt_temp;
                            ves[j] = ves_temp;
                        }
                    }
                }
                // выводим в listbox0_alt и listbox0_ves
                for (int i = 0; i < alter_count; i++)
                {
                    listBox2_alt.Items.Add(alter[i]);
                    listBox2_ves.Items.Add(ves[i]);
                }
            }
            else
            {
                //===================================
                // НИКТО из экспертов еще не оценил альтернативы методом 2
                lbl_statm2.Visible = false;
                label18.Visible = false;
                label19.Visible = false;
                listBox2_alt.Visible = false;
                listBox2_ves.Visible = false;
                btn_matrix2.Visible = false;
                lbl_m2_notmarks.Visible = true;
                //================================
            }

        }

        // ВЫВОД РЕЗУЛЬТАТОВ МЕТОДА 3
        public void update_m3()
        {
            int exp_otvet = 0;
            for (int i = 0; i < exp_count; i++)
            {
                if (prob_list[index].m3.inf[i].status == 1)
                {
                    exp_otvet++;
                }
            }

            if (exp_otvet > 0)
            {
                //===================================
                // ХОТЯБЫ ОДИН из экспертов оценил альтернативы методом 3
                lbl_statm3.Visible = true;
                label22.Visible = true;
                label21.Visible = true;
                listBox3_alt.Visible = true;
                listBox3_ves.Visible = true;
                btn_matrix3.Visible = true;
                lbl_m3_notmarks.Visible = false;
                //================================
                listBox3_alt.Items.Clear();
                listBox3_ves.Items.Clear();
                lbl_statm1.Text = "Ранжирование альтернатив на основе\n оценок " + exp_otvet + " из " + exp_count + " экспертов";
                // записали альтернативы и веса в вспомогательный массив
                string alt_temp = "";
                int ves_temp = 0;
                string[] alter = new string[alter_count];
                int[] ves = new int[alter_count];

                for (int i = 0; i < alter_count; i++)
                {
                    alter[i] = alter_list[index].alters[i];
                    ves[i] = Convert.ToInt32(prob_list[index].m3.ves[i]);
                }
                // сортируем альтернативы
                // сортировка взята с https://metanit.com/sharp/tutorial/2.7.php
                for (int i = 0; i < alter_count - 1; i++)
                {
                    for (int j = i + 1; j < alter_count; j++)
                    {
                        if (ves[i] < ves[j])
                        {
                            alt_temp = alter[i];
                            ves_temp = ves[i];
                            alter[i] = alter[j];
                            ves[i] = ves[j];
                            alter[j] = alt_temp;
                            ves[j] = ves_temp;
                        }
                    }
                }
                // выводим в listbox0_alt и listbox0_ves
                for (int i = 0; i < alter_count; i++)
                {
                    listBox3_alt.Items.Add(alter[i]);
                    listBox3_ves.Items.Add(ves[i]);
                }
            }
            else
            {
                //===================================
                // НИКТО из экспертов еще не оценил альтернативы методом 1
                lbl_statm3.Visible = false;
                label22.Visible = false;
                label21.Visible = false;
                listBox3_alt.Visible = false;
                listBox3_ves.Visible = false;
                btn_matrix3.Visible = false;
                lbl_m3_notmarks.Visible = true;
                //================================
            }

        }

        // кнопка ПОКАЗАТЬ МАТРИЦУ 0
        private void btn_matrix0_Click(object sender, EventArgs e)
        {
            // показываем форму с введенной матрицей
            form6_matrix form0 = new form6_matrix();
            form0.Owner = this;
            form0.Show();
            //this.Hide();
        }
    }
}
