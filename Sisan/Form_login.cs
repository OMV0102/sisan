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
    public partial class Form_login : Form
    {
        public Form_login()
        {
            InitializeComponent();
        }

        //======================================================
        // ПУТЬ К ОСНОВНОМУ РАБОЧЕМУ КАТАЛОГУ
        // ЕСЛИ МЕНЯТЬ ПУТЬ, ТО ТОЛЬКО В 1 ФОРМЕ 
        private string directory = global_class.main_directory;
        //======================================================

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

        public static string password_file = ""; // пароль из файла

        struct exp //  структура для хранения одного эксперта
        {
            public int n_id;
            public string fio;
            public string password_exp;
            public string position;
        }
        List<exp> exp_list;


        // кнопка ВОЙТИ
        private void btn_entry_Click(object sender, EventArgs e)
        {
            // если зашли как аналитик
            if (form1_main.an_or_exp == true)
            {
                if (password_file.Length != 0)
                {

                    if (password_file == txt_password.Text)
                    {
                        // если аналитик угадал свой пароль, он переходит на следующий квест

                        Form form = new form4_analyst_choice();
                        form.Show();
                        this.Close();
                    }
                    else
                    {
                        // если пароль введен неверно, орем на пользователя
                        // красным цветом
                        label_error.Visible = true;
                        txt_password.ForeColor = Color.FromName("Red");
                        this.ActiveControl = txt_password;
                    }

                }
                else
                {
                    //если пароль из файла не считался, то ниче не делаем
                    // (грустим)
                }
            }
            // если зашли как эксперт
            else if (form1_main.an_or_exp == false)
            {
                if (password_file.Length != 0)
                {
                    if (password_file == txt_password.Text)
                    {
                        //запоминаем номер эксперта по котрым зашли
                        form1_main.num_expert = exp_list[comboBox_user.SelectedIndex].n_id;

                        // если пользователь угадал свой пароль, он переходит на следующий квест
                        
                        Form9_expert form = new Form9_expert();
                        form.fio = comboBox_user.SelectedItem.ToString();
                        exp_list.Clear();  // очищаем на всякий случай список с экспертами 
                        comboBox_user.Items.Clear(); // и комбобокс тоже чистим
                        form.Show();
                        this.Close();
                    }
                    else
                    {
                        // если пароль введен неверно, орем на пользователя
                        // красным цветом
                        label_error.Visible = true;
                        txt_password.ForeColor = Color.FromName("Red");
                        this.ActiveControl = txt_password;
                    }

                }
                else
                {
                    //если пароль из файла не считался, то тоже ниче не делаем
                    // (грустим)
                }

            }
        }

        // кнопка НАЗАД
        private void button_back_Click(object sender, EventArgs e)
        {
            // обеспечевает закрытие формы при нажатии на стрелку
            // и переход на главную первую форму
            Form form = Application.OpenForms[0];
            form.Show();
            this.Close();
        }

        // при ЗАГРУЗКЕ ФОРМЫ
        private void Form_login_Load(object sender, EventArgs e)
        {
            
            label_error.Visible = false; // убираем надпись красную
            checkBox1.Checked = false;

            // если зашли как аналитик, загружаем сразу его пароль
            if (form1_main.an_or_exp == true)
            {
                label_analyst.Visible = true;
                comboBox_user.Visible = false;
                FileInfo fileInf = new FileInfo(directory + "analyst_password.txt");
                if (fileInf.Exists)  // если файл существует вообще
                {
                    using (StreamReader sr = new StreamReader(directory + "analyst_password.txt", System.Text.Encoding.UTF8))
                    {
                        password_file = sr.ReadLine();
                    }
                }
                ActiveControl = txt_password;
            }
            // если зашли как эксперт, загружаем список экспертов из файла
            else if (form1_main.an_or_exp == false)
            {
                label_analyst.Visible = false;
                comboBox_user.Visible = true;

                string text = "";
                FileInfo fileInf = new FileInfo(directory + "experts.txt");
                if (fileInf.Exists) // если фпйл существует вообще
                {
                    using (StreamReader sr = new StreamReader(directory + "experts.txt", System.Text.Encoding.UTF8))
                    {
                        text = sr.ReadToEnd();
                    }
                }

                if (text.Length != 0)
                {

                    text = "";
                    string[] words;

                    using (StreamReader sr = new StreamReader(directory + "experts.txt", System.Text.Encoding.UTF8))
                    {
                        exp a;
                        exp_list = new List<exp>();
                        while ((text = sr.ReadLine()) != null)
                        {
                            words = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            // записали номер эксперта
                            a.n_id = Convert.ToInt32(words[0]);
                            // записали ФИО эксперта
                            // если в отчестве стоит "-"
                            // то не пишем его
                            if(words[3] == "-")
                                a.fio = words[1] + " " + words[2].First() + ".";
                            else
                                a.fio = words[1] + " " + words[2].First() + "." + " " + words[3].First() + ".";
                            // запомнили пароль
                            a.password_exp = words[4];
                            // запомнили должность
                            a.position = "";
                            for (int i = 5; i < words.Count(); i++)
                                a.position += words[i] + " ";

                            exp_list.Add(a);
                        }
                    }

                    for(int i = 0; i < exp_list.Count; i++)
                    {
                        comboBox_user.Items.Add(exp_list[i].fio);
                    }

                    // выбираем сразу же 0 эксперта
                    if(comboBox_user.Items.Count != 0)
                    {
                        comboBox_user.SelectedIndex = 0;
                    }

                }
                else
                {
                    // Если файл с экспертами не загрузился, просто плачем
                    // и ничо не делаем
                }


            }
        }

        // когда нажимаем на текстбокс пароля
        private void txt_password_MouseDown(object sender, MouseEventArgs e)
        {
            label_error.Visible = false; // скрыть надпись красную
            txt_password.ForeColor = Color.FromName("WindowText");// сделать текст опять черным
        }

        // когда меняем пароль (стираем/пишем) в текстбоксе
        private void txt_password_TextChanged(object sender, EventArgs e)
        {
            label_error.Visible = false; // скрыть надпись красную
            txt_password.ForeColor = Color.FromName("WindowText");// сделать текст опять черным
        }

        // ГАЛОЧКА когда щелкаем на нее
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                txt_password.PasswordChar = '\0';
            }
            else if (checkBox1.Checked == false)
            {
                txt_password.PasswordChar = '*';
            }

            this.ActiveControl = txt_password;
            
        }

        // когда выбираем эксперта из списка
        private void comboBox_user_SelectedIndexChanged(object sender, EventArgs e)
        {
            int select_num = comboBox_user.SelectedIndex;
            password_file = exp_list[select_num].password_exp;

            label_error.Visible = false; // скрыть надпись красную
            txt_password.ForeColor = Color.FromName("WindowText");// сделать текст опять черным

            this.ActiveControl = txt_password;
        }
        
        // перетаскивание окна по экрану
        private void Form_login_MouseDown(object sender, MouseEventArgs e)
        {
            base.Capture = false;
            Message m = Message.Create(base.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }
    }
}
