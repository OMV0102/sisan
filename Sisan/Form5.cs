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
        public int alter_count = 0;   // количество альтернатив для выбранной проблемы

        //======================================================================

        #region СТРУКТУРЫ
        struct exp //  структура для хранения одного эксперта
        {
            public int id_exp;
            public string surname;
            public string name;
            public string otch;
            public string fio; // сокращенное ФИО
            public string password;
            public string position; // Должность
        }
        List<exp> exp_list;
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
            
            btn_matrix0.Visible = false;
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
