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
    public partial class Form_change_pass : Form
    {
        public Form_change_pass()
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

        public static bool is_ok = false; // флаг пароль изменен успешно

        struct exp // структура для хранения одного эксперта
        {
            public int n_id;
            public string fio;
            public string surname;
            public string name;
            public string otch;
            public string password_exp;
            public string position;
        }
        List<exp> exp_list;

        private string password_file = ""; // пароль

        // кнопка СОХРАНИТЬ
        private void btn_save_Click(object sender, EventArgs e)
        {
            // если зашли как аналитик
            if (form1_main.an_or_exp == true)
            {
                if (password_file.Length != 0) // Если пароль из файла считался
                {
                    if (txt_pass_curr.Text == password_file) // Если юзер угадал текущий пароль
                    {
                        if (txt_pass_new.Text.Length == 0) // Если новый пароль не введен
                        {
                            // если новый пароль не введен, орем на пользователя
                            // красным цветом
                            label_error2.Visible = true;
                            txt_pass_new.ForeColor = Color.FromName("Red");
                        }
                        else // Если новый пароль введен
                        {
                            if (txt_pass_new.Text.Length < 6 || txt_pass_new.Text.Length > 15) // Если новый пароль меньше 4 символов
                            {
                                // орем на пользователя
                                // красным цветом
                                MessageBox.Show(
                                "Пароль должен быть не менее 6 символов и не более 15.\n",
                                "Предупреждение",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning,
                                MessageBoxDefaultButton.Button1,
                                MessageBoxOptions.DefaultDesktopOnly);
                                txt_pass_new.ForeColor = Color.FromName("Red");
                                this.TopMost = true; this.TopMost = false;
                            }
                            else
                            {
                                // Сохраняем новый пароль
                                using (StreamWriter sr = new StreamWriter(directory + "analyst_password.txt", false, System.Text.Encoding.UTF8))
                                {
                                    sr.WriteLine(txt_pass_new.Text);
                                }

                                is_ok = true;
                                // Переходим назад на форму аналитика
                                form4_analyst_choice form = new form4_analyst_choice();
                                form.Show();
                                this.Close();
                                
                                
                            }
                        }
                    }
                    else
                    {
                        // если пароль введен неверно, орем на пользователя
                        // красным цветом
                        label_error.Visible = true;
                        txt_pass_curr.ForeColor = Color.FromName("Red");
                    }
                }
                else
                {
                    //если пароль из файла не считался, то ниче не делаем
                    // (грустим)
                }

            }
            // если зашли как эксперт, загружаем пароль эксперта
            else if (form1_main.an_or_exp == false)
            {
                if (password_file.Length != 0) // Если пароль из файла считался
                {
                    if (txt_pass_curr.Text == password_file) // Если юзер угадал текущий пароль
                    {
                        if (txt_pass_new.Text.Length == 0) // Если новый пароль не введен
                        {
                            // если новый пароль не введен, орем на пользователя
                            // красным цветом
                            label_error2.Visible = true;
                            txt_pass_new.ForeColor = Color.FromName("Red");
                        }
                        else // Если новый пароль введен
                        {
                            if (txt_pass_new.Text.Length < 4 || txt_pass_new.Text.Length > 15) // Если новый пароль меньше 4 символов
                            {
                                // орем на пользователя
                                // красным цветом
                                MessageBox.Show(
                                "Пароль должен быть не менее 6 символов и не более 15.\n",
                                "Предупреждение",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning,
                                MessageBoxDefaultButton.Button1,
                                MessageBoxOptions.DefaultDesktopOnly);
                                txt_pass_new.ForeColor = Color.FromName("Red");
                                this.TopMost = true; this.TopMost = false;
                                
                            }
                            else
                            {

                                // удаляем старый файл
                                FileInfo fileInf = new FileInfo(directory + "experts.txt");
                                if (fileInf.Exists)
                                    fileInf.Delete();

                                // записываем обратно все в файл изх списка
                                using (StreamWriter sr = new StreamWriter(directory + "experts.txt", false, System.Text.Encoding.UTF8))
                                {
                                    string line = "";
                                    for(int i = 0;  i < exp_list.Count; i++)
                                    {
                                        line = exp_list[i].n_id + " "; // Добавили ID
                                        line += exp_list[i].surname + " "; //  Добавили фамилию
                                        line += exp_list[i].name + " "; //  Добавили имя
                                        line += exp_list[i].otch + " "; //  Добавили отчество

                                        // если пишем нужного эксперта
                                        if (exp_list[i].n_id == form1_main.num_expert)
                                            line += txt_pass_new.Text + " "; // запомнили новый пароль
                                        else
                                            line += exp_list[i].password_exp + " "; 

                                        // пишем должность
                                        line += exp_list[i].position;

                                        sr.WriteLine(line);
                                    }
                                }

                                is_ok = true;
                                // Переходим назад на форму аналитика
                                Form9_expert form = new Form9_expert();
                                form.Show();
                                this.Close();
                            }
                        }
                    }
                    else
                    {
                        // если пароль введен неверно, орем на пользователя
                        // красным цветом
                        label_error.Visible = true;
                        txt_pass_curr.ForeColor = Color.FromName("Red");
                    }
                }
                else
                {
                    //если пароль из файла не считался, то тоже ниче не делаем
                    // (грустим)
                }

            }   
        }


        // при  загрузке формы
        private void Form_change_pass_Load(object sender, EventArgs e)
        {
            label_error.Visible = false; // убираем надпись красную
            label_error2.Visible = false; // убираем надпись красную2
            checkBox1.Checked = false; // текущий пароль скрыт по умолчанию
            checkBox2.Checked = true; // новый пароль показывается по умолчанию
            is_ok = false;

            // если зашли как аналитик, загружаем сразу его пароль
            if (form1_main.an_or_exp == true)
            {
                FileInfo fileInf = new FileInfo(directory + "analyst_password.txt");
                if (fileInf.Exists)  // если файл существует вообще
                {
                    using (StreamReader sr = new StreamReader(directory + "analyst_password.txt", System.Text.Encoding.UTF8))
                    {
                        password_file = sr.ReadLine();
                    }
                }
            }
            // если зашли как эксперт, загружаем пароль эксперта
            else if (form1_main.an_or_exp == false)
            {
                string text = "";
                FileInfo fileInf = new FileInfo(directory + "experts.txt");
                if (fileInf.Exists)  // если файл существует вообще
                {
                    using (StreamReader sr = new StreamReader(directory + "experts.txt", System.Text.Encoding.UTF8))
                    {
                        text = sr.ReadToEnd();
                    }
                }

                string[] words;
                if (text.Length != 0)
                {
                    text = "";
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
                            if (words[3] == "-")
                                a.fio = words[1] + " " + words[2];
                            else
                                a.fio = words[1] + " " + words[2] + " " + words[3];
                            // запомнили имя
                            a.surname = words[1];
                            // запомнили имя
                            a.name = words[2];
                            // запомнили отчество
                            a.otch = words[3];
                            // запомнили пароль
                            a.password_exp = words[4];
                            // запомнили должность
                            a.position = "";
                            for (int i = 5; i < words.Count(); i++)
                                a.position += words[i] + " ";
                            // запомним пароль эксперта отдельно
                            // ищем по его ID
                            if (words[0] == form1_main.num_expert.ToString())
                            {
                                password_file = words[4];  // запомнили его пароль
                            }

                            exp_list.Add(a);
                        }
                    }
                }
            }
        }

        // когда нажимаем на текстбокс пароля
        private void txt_pass_curr_MouseDown(object sender, MouseEventArgs e)
        {
            label_error.Visible = false; // скрыть надпись красную
            txt_pass_curr.ForeColor = Color.FromName("WindowText");// сделать текст опять черным
        }

        // когда меняем пароль (стираем/пишем) в текстбоксе пароля
        private void txt_pass_curr_TextChanged(object sender, EventArgs e)
        {
            label_error.Visible = false; // скрыть надпись красную
            txt_pass_curr.ForeColor = Color.FromName("WindowText");// сделать текст опять черным
        }

        // когда нажимаем на текстбокс нового пароля
        private void txt_pass_new_MouseDown(object sender, MouseEventArgs e)
        {
            label_error2.Visible = false; // скрыть надпись красную
            txt_pass_new.ForeColor = Color.FromName("WindowText");// сделать текст опять черным
        }

        // когда меняем пароль (стираем/пишем) в текстбоксе нового пароля
        private void txt_pass_new_TextChanged(object sender, EventArgs e)
        {
            label_error2.Visible = false; // скрыть надпись красную
            txt_pass_new.ForeColor = Color.FromName("WindowText");// сделать текст опять черным
        }

        // ГАЛОЧКА1 (старого пароля) когда щелкаем на нее
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                txt_pass_curr.PasswordChar = '\0';
            }
            else if (checkBox1.Checked == false)
            {
                txt_pass_curr.PasswordChar = '*';
            }

            this.ActiveControl = txt_pass_curr;
        }

        // ГАЛОЧКА2 (нового пароля) когда щелкаем на нее
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                txt_pass_new.PasswordChar = '\0';
            }
            else if (checkBox2.Checked == false)
            {
                txt_pass_new.PasswordChar = '*';
            }

            this.ActiveControl = txt_pass_new;
        }
        
        // перетаскивание окна по экрану
        private void Form_change_pass_MouseDown(object sender, MouseEventArgs e)
        {
            
            base.Capture = false;
            Message m = Message.Create(base.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }

        // кнопка НАЗАД
        private void button_back_Click(object sender, EventArgs e)
        {
            if (form1_main.an_or_exp == true) 
            {
                // если зашли как аналитик , возвращаемся на 4 форму
                form4_analyst_choice form = new form4_analyst_choice();
                form.Show();
                this.Close();
            }
            else if (form1_main.an_or_exp == false)
            {
                // если зашли как эксперт , возвращаемся на 9 форму
                Form9_expert form = new Form9_expert();
                form.Show();
                this.Close();
            }

        }
    }
}
