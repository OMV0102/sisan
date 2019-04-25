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
    public partial class Form9_expert : Form
    {
        public Form9_expert()
        {
            InitializeComponent();
        }

        //======================================================
        // ПУТЬ К ОСНОВНОМУ РАБОЧЕМУ КАТАЛОГУ
        // ЕСЛИ МЕНЯТЬ ПУТЬ, ТО ТОЛЬКО В 1 ФОРМЕ 
        private string directory = global_class.main_directory;
        //======================================================

        public struct st_exp //  структура для хранения группы экспертов к прооблеме
        {
            public int id_exp;
            public string comp;
            public int m0;
            public int m1;
            public int m2;
            public int m3;
            public int m4;
        }

        public struct st_problem //  структура для хранения одной проблемы
        {
            public int num_prob;
            public bool open_close;
            public string txt_prob;
            public st_exp[] exp;

        }
        public List<st_problem> list_prob;
        public string problem; // текст проблемы
        public int num_problem; // уникальный номер проблемы (ID)
        public int N = -1; //порядковый номер проблемы в list_prob
        public int E = -1; //порядковый номер эксперта в list_prob

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
        private void Form9_expert_MouseDown(object sender, MouseEventArgs e)
        {
            base.Capture = false;
            Message m = Message.Create(base.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }

        // кнопка НАЗАД
        private void button_back_Click(object sender, EventArgs e)
        {
            Form form = Application.OpenForms[0];
            form.Show();
            this.Close();
        }

        // кнопка СМЕНИТЬ ПАРОЛЬ эксперта
        private void btn_change_pass_Click(object sender, EventArgs e)
        {
            Form_change_pass form = new Form_change_pass();
            form.Show();
            this.Close();
        }

        //  при загрузке формы
        private void Form9_expert_Load(object sender, EventArgs e)
        {
            
            if (Form_change_pass.is_ok == true)
            {
                Form_change_pass.is_ok = false;
                MessageBox.Show(
                "Пароль успешно изменен!",
                "",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
                this.TopMost = true; this.TopMost = false;
                
            }
            //============================================

            list_prob = new List<st_problem>();
            string text = "";
            string[] words;
            st_problem a;
            a.exp = new st_exp[1]; // выделили ПАМЯТЬ под массив экспертов (пока что 1)
            FileInfo fileInf = new FileInfo(directory + "problems.txt");
            if (fileInf.Exists)  // если файл существует вообще
            {
                using (StreamReader sr = new StreamReader(directory + "problems.txt", System.Text.Encoding.UTF8))
                {
                    text = sr.ReadToEnd();
                }

            }
                
            if (text.Length != 0)
            {
                using (StreamReader sr = new StreamReader(directory + "problems.txt", System.Text.Encoding.UTF8))
                {
                    int i = 0;
                    while ((text = sr.ReadLine()) != null)
                    {
                        words = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        // запоминаем номер проблем
                        a.num_prob = Convert.ToInt32(words[0]);
                        // запоминаем открыта ли эта проблема для оценивания
                        a.open_close = Convert.ToBoolean(words[1]);
                        // запоминаем саму проблему
                        a.txt_prob = "";
                        for (int j = 2; j < words.Count(); j++)
                            a.txt_prob += words[j] + " ";

                        // теперь читаем group
                        int N = 0;
                        FileInfo fileInf1 = new FileInfo(directory + "group" + a.num_prob + ".txt");
                        if (fileInf1.Exists)  // если файл существует вообще
                        {
                            using (StreamReader sr1 = new StreamReader(directory + "group" + a.num_prob + ".txt", System.Text.Encoding.UTF8))
                            {
                                
                                while ((text = sr1.ReadLine()) != null)
                                {
                                    N++;
                                }
                            }

                        }

                        if (N != 0)
                        {
                            using (StreamReader sr1 = new StreamReader(directory + "group" + a.num_prob + ".txt", System.Text.Encoding.UTF8))
                            {
                                //запоминаем инфу о экспертах 
                                Array.Resize(ref a.exp, N); // перевыделили ПАМЯТЬ под экспертов

                                int j = 0;
                                while ((text = sr1.ReadLine()) != null)
                                {
                                    words = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                                    //запомнили id эксперта
                                    a.exp[j].id_exp = Convert.ToInt32(words[0]);
                                    //запомнили компетентность эксперта
                                    a.exp[j].comp = words[1];
                                    //запомнили прошел ли эксперт 0 метод
                                    a.exp[j].m0 = Convert.ToInt32(words[2]);
                                    //запомнили прошел ли эксперт 1 метод
                                    a.exp[j].m1 = Convert.ToInt32(words[3]);
                                    //запомнили прошел ли эксперт 2 метод
                                    a.exp[j].m2 = Convert.ToInt32(words[4]);
                                    //запомнили прошел ли эксперт 3 метод
                                    a.exp[j].m3 = Convert.ToInt32(words[5]);
                                    //запомнили прошел ли эксперт 4 метод
                                    a.exp[j].m4 = Convert.ToInt32(words[6]);

                                    j++;
                                }

                            }
                        }

                        list_prob.Add(a);
                        i++;
                    }
                }
            }
            //===================================
            // тут допустим считали проблемы
            // и group
            // теперь просматриваем все, что считали
            // добавляемв  комбобокс только те проблемы, которые можно смотреть эксперту

            if (list_prob.Count > 0) //если наш могучий список не пуст
            {
                for (int i = 0; i < list_prob.Count; i++)
                {
                    for (int j = 0; j < list_prob[i].exp.Length; j++)
                    {
                        if (form1_main.num_expert == list_prob[i].exp[j].id_exp)
                        {
                            comboBox_problems.Items.Add(list_prob[i].txt_prob);
                        }
                    }
                }
            }
            
            if(comboBox_problems.Items.Count > 0)
            {
                label_no.Visible = false;
                btn_m0.Visible = true;
                btn_m1.Visible = true;
                btn_m2.Visible = true;
                btn_m3.Visible = true;
                btn_m4.Visible = true;
                label_m0.Visible = true;
                label_m1.Visible = true;
                label_m2.Visible = true;
                label_m3.Visible = true;
                label_m4.Visible = true;

                comboBox_problems.SelectedIndex = 0; // ВЫБИРАЕМ СРАЗУ 0 ПРОБЛЕМУ
            }
            else
            {
                label_no.Visible = true;
                label_close.Visible = false;
                btn_m0.Visible = false;
                btn_m1.Visible = false;
                btn_m2.Visible = false;
                btn_m3.Visible = false;
                btn_m4.Visible = false;
                label_m0.Visible = false;
                label_m1.Visible = false;
                label_m2.Visible = false;
                label_m3.Visible = false;
                label_m4.Visible = false;
            }
        }

        // функция ОБНОВЛЕНИЕ ФОРМЫ
        // вызывается из пяти форм опросов после изменения/сохранения
        // вызывается при выборе проблемы в комбобоксе
        public void update(int N, int E)
        {
            // переключение лэйблов и кнопок
            if (N != -1)
            {
                // разрешение на прохождение опроса
                switch (list_prob[N].open_close)
                {
                    case false:
                        label_close.Visible = true;
                        btn_m0.Visible = false;
                        btn_m1.Visible = false;
                        btn_m2.Visible = false;
                        btn_m3.Visible = false;
                        btn_m4.Visible = false;
                        break;

                    case true:
                        label_close.Visible = false;
                        btn_m0.Visible = true;
                        btn_m1.Visible = true;
                        btn_m2.Visible = true;
                        btn_m3.Visible = true;
                        btn_m4.Visible = true;
                        break;
                }

                // метод 0 (МЕТОД ПАРНЫХ СРАВНЕНИЙ)
                switch (list_prob[N].exp[E].m0)
                {
                    case 0:
                        btn_m0.Text = "Начать оценивание";
                        label_m0.Text = "Не пройден";
                        label_m0.ForeColor = Color.FromName("Red");
                        break;

                    case 1:
                        btn_m0.Text = "Изменить ответы";
                        label_m0.Text = "Пройден";
                        label_m0.ForeColor = Color.FromName("Green");
                        break;

                    case (-1):
                        btn_m0.Text = "Закончить оценивание";
                        label_m0.Text = "Не закончен";
                        label_m0.ForeColor = Color.FromName("Orange");
                        break;
                }

                // метод 1 (МЕТОД ВЗВЕШЕННЫХ ЭКСПЕРТНЫХ ОЦЕНОК)
                switch (list_prob[N].exp[E].m1)
                {
                    case 0:
                        btn_m1.Text = "Начать оценивание";
                        label_m1.Text = "Не пройден";
                        label_m1.ForeColor = Color.FromName("Red");
                        break;

                    case 1:
                        btn_m1.Text = "Изменить ответы";
                        label_m1.Text = "Пройден";
                        label_m1.ForeColor = Color.FromName("Green");
                        break;

                    case (-1):
                        btn_m1.Text = "Закончить оценивание";
                        label_m1.Text = "Не закончен";
                        label_m1.ForeColor = Color.FromName("Orange");
                        break;
                }

                // метод 2 (МЕТОД ПРЕДПОЧТЕНИЯ)
                switch (list_prob[N].exp[E].m2)
                {
                    case 0:
                        btn_m2.Text = "Начать оценивание";
                        label_m2.Text = "Не пройден";
                        label_m2.ForeColor = Color.FromName("Red");
                        break;

                    case 1:
                        btn_m2.Text = "Изменить ответы";
                        label_m2.Text = "Пройден";
                        label_m2.ForeColor = Color.FromName("Green");
                        break;

                    case (-1):
                        btn_m2.Text = "Закончить оценивание";
                        label_m2.Text = "Не закончен";
                        label_m2.ForeColor = Color.FromName("Orange");
                        break;
                }

                // метод 3 (МЕТОД РАНГА)
                switch (list_prob[N].exp[E].m3)
                {
                    case 0:
                        btn_m3.Text = "Начать оценивание";
                        label_m3.Text = "Не пройден";
                        label_m3.ForeColor = Color.FromName("Red");
                        break;

                    case 1:
                        btn_m3.Text = "Изменить ответы";
                        label_m3.Text = "Пройден";
                        label_m3.ForeColor = Color.FromName("Green");
                        break;

                    case (-1):
                        btn_m3.Text = "Закончить оценивание";
                        label_m3.Text = "Не закончен";
                        label_m3.ForeColor = Color.FromName("Orange");
                        break;
                }

                // метод 4 (МЕТОД ПОЛНОГО ПОПАРНОГО СОСПОСТАВЛЕНИЯ)
                switch (list_prob[N].exp[E].m4)
                {
                    case 0:
                        btn_m4.Text = "Начать оценивание";
                        label_m4.Text = "Не пройден";
                        label_m4.ForeColor = Color.FromName("Red");
                        break;

                    case 1:
                        btn_m4.Text = "Изменить ответы";
                        label_m4.Text = "Пройден";
                        label_m4.ForeColor = Color.FromName("Green");
                        break;

                    case (-1):
                        btn_m4.Text = "Закончить оценивание";
                        label_m4.Text = "Не закончен";
                        label_m4.ForeColor = Color.FromName("Orange");
                        break;
                }
            }
        }

        // ФУНКЦИЯ СОХРАНЕНИЯ в файл group...
        // вызывается из пяти форм опросов после изменения/сохранения
        public void save_group()
        {
            string line;
            FileInfo fileInf = new FileInfo(directory + "group" + num_problem + ".txt");
            if (fileInf.Exists)  // если файл существует вообще
            {
                using (StreamWriter sw = new StreamWriter(directory + "group" + num_problem + ".txt", false, System.Text.Encoding.UTF8))
                {

                    for (int i = 0; i < list_prob[N].exp.Count(); i++)
                    { 
                        line = "";
                        line += list_prob[N].exp[i].id_exp + " ";
                        line += list_prob[N].exp[i].comp + " ";
                        line += list_prob[N].exp[i].m0 + " ";
                        line += list_prob[N].exp[i].m1 + " ";
                        line += list_prob[N].exp[i].m2 + " ";
                        line += list_prob[N].exp[i].m3 + " ";
                        line += list_prob[N].exp[i].m4 + " ";
                        sw.WriteLine(line);
                    }
                }

            }

            
        }

        // при ВЫБОРЕ ПРОБЛЕМЫ
        private void comboBox_problems_SelectedIndexChanged(object sender, EventArgs e)
        {
            problem = comboBox_problems.SelectedItem.ToString();
            //  узнаем порядковый номер проблемы в списке по тексту проблемы из комбобокса
            // узнаем порядковый номер эксперта в списке по его id из первой формы
            //  узнаем уникальный id номер проблемы в списке по тексту проблемы из комбобокса
            for (int i = 0; i < list_prob.Count; i++)
            {
                if (list_prob[i].txt_prob == problem)
                {
                    N = i; // запомнили 
                    num_problem = list_prob[i].num_prob;
                }
                    
                for (int j = 0; j < list_prob[i].exp.Count(); j++)
                {
                    if (list_prob[i].exp[j].id_exp == form1_main.num_expert)
                        E = j;
                }
            }

            update(N, E);
        }

        // кнопка МЕТОД 0 (МЕТОД ПАРНЫХ СРАВНЕНИЙ)
        private void btn_m0_Click(object sender, EventArgs e)
        {
            form2_opros0 form = new form2_opros0();
            form.Owner = this;
            form.Show();
            this.Hide();
        }

        // кнопка МЕТОД 1 (ВЗВЕШЕННЫХ ЭКСПЕРТНЫХ ОЦЕНОК)
        private void btn_start_m1_Click(object sender, EventArgs e)
        {            
            Form7 form = new Form7(problem, num_problem);
            form.Owner = this;
            form.Show();
            this.Hide();
        }

        // кнопка МЕТОД 2 (МЕТОД ВЗВЕШЕННЫХ ЭКСПЕРТНЫХ ОЦЕНОК)
        private void btn_m2_Click(object sender, EventArgs e)
        {

        }

        // кнопка МЕТОД 3 (МЕТОД РАНГА)
        private void btn_m3_Click(object sender, EventArgs e)
        {

        }

        // кнопка МЕТОД 4 (МЕТОД ПОЛНОГО ПОПАРНОГО СОСПОСТАВЛЕНИЯ)
        private void btn_m4_Click(object sender, EventArgs e)
        {

        }
    }
}
