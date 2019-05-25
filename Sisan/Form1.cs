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
    // исправлять путь к файлам в самомо конце
    // глобальный класс не переносить сюда
    // первыма классомвсегда должен быть - класс формы

    public partial class form1_main : Form
    {
        public form1_main()
        {
            InitializeComponent();
        }

        // перетаскивание окна по экрану
        private void form1_main_MouseDown(object sender, MouseEventArgs e)
        {
            // перетаскивание окна по экрану
            base.Capture = false;
            Message m = Message.Create(base.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }

        // кнопка СВЕРНУТЬ ОКНО
        private void button_minimize_Click(object sender, EventArgs e)
        {
            // обеспечивает сворачивание формы при нажатии на тире
            this.WindowState = FormWindowState.Minimized;
        }

        public static bool an_or_exp; // флаг, зашел как аналитик или как эксперт
        public static int num_expert = 0; // ID эксперта который зашел

        // кнопка АНАЛИТИК
        private void button_analyst_Click(object sender, EventArgs e)
        {
            an_or_exp = true; // заходим как аналитик
            Form_login form = new Form_login();
            form.Show();
            this.Hide();
        }

        // кнопка ЭКСПЕРТ
        private void button_expert_Click(object sender, EventArgs e)
        {
            // переход на форму эксперта при нажатии на кнопку "Эксперт"

            an_or_exp = false;
            Form_login form = new Form_login();            
            form.Show();
            this.Hide();

        }

        // кнопка ЗАКРЫТЬ ОКНО
        private void button_cross_Click(object sender, EventArgs e)
        {
            // обеспечевает закрытие формы при нажатии на крестик
            this.Close();
        }

        // чтоб НАЖИМАТЬ КНОПКИ ЧИСЛАМИ
        private void form1_main_KeyDown(object sender, KeyEventArgs e)
        {
            // ЧИСЛО 1 - АНАЛИТИК
            if(e.KeyData == Keys.NumPad1 || e.KeyData == Keys.D1)
            {
                button_analyst_Click(null, null);
            }
            // ЧИСЛО 1 - ЭКСПЕРТ
            if (e.KeyData == Keys.NumPad2 || e.KeyData == Keys.D2)
            {
                button_expert_Click(null, null);
            }
        }

        // при ЗАГРУЗКЕ ПРИЛОЖЕНИЯ
        private void form1_main_Load(object sender, EventArgs e)
        {
            string path = global_class.curr_dir + "\\expert_marks\\";
            if (Directory.Exists(path) == false)
            {
                //lbl_new_dir.Visible = true;
                Directory.CreateDirectory(path);
                File.CreateText(path + "problems.txt");
            }
            global_class.main_directory = path;
        }
    }

    public static class global_class  // ---ГЛОБАЛЬНЫЙ КЛАСС---
    {
        // ПУТЬ К ОСНОВНОМУ РАБОЧЕМУ КАТАЛОГУ
        // ЕСЛИ МЕНЯТЬ ПУТЬ, ТО ТОЛЬКО ТУТ
        public static string main_directory;  // сюда присваиваем каталог с данными
        // путь к текущему каталогу где лежит приложение С именем приложения
        public static string path_app = System.Reflection.Assembly.GetExecutingAssembly().Location;
        // путь к текущему каталогу где лежит приложение БЕЗ имени приложения
        public static string curr_dir = Environment.CurrentDirectory;
    }
}
