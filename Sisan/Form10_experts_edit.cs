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
    public partial class Form10_experts_edit : Form
    {
        public Form10_experts_edit()
        {
            InitializeComponent();
        }

        //======================================================
        // ПУТЬ К ОСНОВНОМУ РАБОЧЕМУ КАТАЛОГУ
        // ЕСЛИ МЕНЯТЬ ПУТЬ, ТО ТОЛЬКО В 1 ФОРМЕ 
        private string directory = global_class.main_directory;
        //======================================================

        struct exp //  структура для хранения одного эксперта
        {
            public int n_id;
            public string fio;
            public string name;
            public string surname;
            public string otch;
            public string password_exp;
            public string position;
        }
        List<exp> exp_list;

        public string[] pass_char = {
            "1", "2", "3", "4", "5", "6",
            "7", "8", "9", "B", "C", "D",
            "F", "G", "H", "J", "K", "L",
            "M", "N", "P", "Q", "R", "S",
            "T", "V", "W", "X", "Z", "b",
            "c", "d", "f", "g", "h", "j",
            "k", "m", "n", "p", "q", "r",
            "s", "t", "v", "w", "x", "z",
            "A", "E", "U", "Y", "a", "e",
            "i", "u", "y", "a", "A", 
        };

        private bool add_new;
        private int exp_count; // кол-во экспертов

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

        // кнопка НАЗАД  ================================ТУТ ПОДРЕДАЧИТЬ
        private void button_back_Click(object sender, EventArgs e)
        {
            form4_analyst_choice form = new form4_analyst_choice();
            form.Show();
            this.Close();
        }

        // движение формы по экрану
        private void Form10_experts_edit_MouseDown(object sender, MouseEventArgs e)
        {
            // обеспечивает перемещение формы без рамки
            base.Capture = false;
            Message m = Message.Create(base.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }

        // кнопка < (ПРЕДЫДУЩИЙ ЭКСПЕРТ)
        private void btn_prev_Click(object sender, EventArgs e)
        {
            if(comboBox_experts.Items.Count > 2 && comboBox_experts.SelectedIndex != -1)
            {
                int n = comboBox_experts.SelectedIndex;
                if (n == 1)
                {
                    btn_next.Visible = false;
                    comboBox_experts.SelectedIndex = n - 1;
                }
                else
                {
                    comboBox_experts.SelectedIndex = n - 1;
                }
            }
            else if (comboBox_experts.Items.Count == 2 && comboBox_experts.SelectedIndex != -1)
            {
                int n = comboBox_experts.SelectedIndex;
                if (n == 1)
                {
                    btn_next.Visible = true;
                    btn_prev.Visible = false;
                    comboBox_experts.SelectedIndex = n - 1;
                }
            }

            if (comboBox_experts.Items.Count > 2 && btn_next.Visible == false)
            {
                btn_next.Visible = true;
            }

        }

        // кнопка > (СЛЕДУЮЩИЙ ЭКСПЕРТ)
        private void btn_next_Click(object sender, EventArgs e)
        {
            
            if (comboBox_experts.Items.Count > 2 && comboBox_experts.SelectedIndex != -1)
            {
                int n = comboBox_experts.SelectedIndex;
                if (n == comboBox_experts.Items.Count - 2)
                {
                    btn_next.Visible = false;
                    comboBox_experts.SelectedIndex = n + 1;
                }
                else
                {
                    comboBox_experts.SelectedIndex = n + 1;
                }
            }
            else if (comboBox_experts.Items.Count == 2 && comboBox_experts.SelectedIndex != -1)
            {
                int n = comboBox_experts.SelectedIndex;
                if (n == 0)
                {
                    btn_next.Visible = false;
                    btn_prev.Visible = true;
                    comboBox_experts.SelectedIndex = n + 1;
                }
            }

            if(comboBox_experts.Items.Count > 2 && btn_prev.Visible == false)
            {
                btn_prev.Visible = true;
            }

        }

        // ВЫБОР в КОМБОБОКСЕ эксперта
        private void comboBox_experts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_experts.SelectedIndex >= 0 && comboBox_experts.SelectedIndex < exp_count)
            {
                //===========================================
                if (comboBox_experts.Items.Count > 1 && comboBox_experts.SelectedIndex == 0)
                {
                    btn_prev.Visible = false;
                }
                else if (comboBox_experts.Items.Count > 1)
                {
                    btn_prev.Visible = true;
                }
                //===========================================
                if (comboBox_experts.Items.Count > 1 && comboBox_experts.SelectedIndex == comboBox_experts.Items.Count - 1)
                {
                    btn_next.Visible = false;
                }
                else if (comboBox_experts.Items.Count > 1)
                {
                    btn_next.Visible = true;
                }
                //===========================================
                int n = comboBox_experts.SelectedIndex;
                txt_id.Text = exp_list[n].n_id.ToString();
                txt_name.Text = exp_list[n].name;
                txt_surname.Text = exp_list[n].surname;
                if (exp_list[n].otch == "-")
                    txt_otch.Text = "";
                else
                    txt_otch.Text = exp_list[n].otch;
                txt_position.Text = exp_list[n].position;
                txt_password.PasswordChar = '*';
                txt_password.Text = exp_list[n].password_exp;

            }
        }

        // СОРТИРОВКА ПО фИО
        private void sort_fio()
        {
            // сортируем по ФИО
            exp b;
            for (int i = 0; i < exp_count - 1; i++)
            {
                for (int j = i + 1; j < exp_count; j++)
                {
                    if (string.Compare(exp_list[i].fio, exp_list[j].fio) > 0)
                    {
                        b = exp_list[i];
                        exp_list[i] = exp_list[j];
                        exp_list[j] = b;
                    }
                }
            }
        }

        // при ЗАГРУЗКЕ ФОРМЫ
        private void Form10_experts_edit_Load(object sender, EventArgs e)
        {
            // очищаем комбо бокс
            comboBox_experts.Items.Clear();
            label_save_status.Visible = false; // флаг, что изменений НЕ внесено в карточку эксперта
            label_view.Visible = false;
            label_edit.Visible = true;
            btn_delete.Visible = false;
            btn_save.Visible = false;
            btn_new.Visible = true;
            btn_pass.Visible = false;
            label_error.Visible = false;
            add_new = false;


            string text = "";
            if (File.Exists(directory + "experts.txt") == true)
            {
                using (StreamReader sr = new StreamReader(directory + "experts.txt", System.Text.Encoding.UTF8))
                {
                    text = sr.ReadToEnd();
                }
            }

            if (text.Length > 0)
            {
                text = "";
                string[] words;
                exp_count = 0;
                using (StreamReader sr = new StreamReader(directory + "experts.txt", System.Text.Encoding.UTF8))
                {
                    exp a;
                    exp_list = new List<exp>();
                    while ((text = sr.ReadLine()) != null)
                    {
                        //a = new exp();
                        words = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        // записали номер эксперта
                        a.n_id = Convert.ToInt32(words[0]);
                        // записали ФИО эксперта
                        // если в отчестве стоит "-"
                        // то не пишем его
                        if (words[3] == "-")
                            a.fio = words[1] + " " + words[2];
                        else
                            a.fio = words[1] + " " + words[2] + " " + words[3];
                        // отдельно записываем фамилию
                        a.surname = words[1];
                        // отдельно записываем имя
                        a.name = words[2];
                        // отдельно записываем отчество
                        if (words[3] == "-")
                            a.otch = "";
                        else
                            a.otch = words[3];
                        // запомнили пароль
                        a.password_exp = words[4];
                        // запомнили должность
                        a.position = "";
                        for (int i = 5; i < words.Count(); i++)
                            a.position += words[i] + " ";

                        exp_count++;
                        exp_list.Add(a);
                    }
                }
                // считали из файла всех 
                sort_fio();

                // ============ отсортировали ============

                for (int i = 0; i < exp_count; i++)
                {
                    comboBox_experts.Items.Add(exp_list[i].fio);
                }

                if (comboBox_experts.Items.Count > 0)
                {
                    comboBox_experts.SelectedIndex = 0;
                    /*txt_id.Text = exp_list[0].n_id.ToString();
                    txt_name.Text = exp_list[0].name;
                    txt_surname.Text = exp_list[0].surname;
                    txt_otch.Text = exp_list[0].otch;
                    txt_position.Text = exp_list[0].position;
                    txt_password.PasswordChar = '*';
                    txt_password.Text = exp_list[0].password_exp;*/

                }

                if (comboBox_experts.Items.Count == 1)
                {
                    btn_next.Visible = false;
                    btn_prev.Visible = false;
                }
            }
        }

        //===================================================================================
        // нажимаем на  label_view (статус просмотра)
        // ВКЛЮЧЕН РЕДАКТИРОВАНИЕ
        // ПЕРЕХОДИМ В РЕЖИМ ПРОСМОТРА
        private void label_view_Click(object sender, EventArgs e)
        {
            if (label_save_status.Visible == true)
            {
                if(add_new == false)
                {
                    DialogResult result = MessageBox.Show(
                    "Все несохраненные данные будут потеряны.\n" +
                    "Перейти в режим просмотра?",
                    "Внимание",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);

                    if (result == DialogResult.No)
                    {
                        this.TopMost = true; this.TopMost = false;
                    }


                    if (result == DialogResult.Yes)
                    {
                        // ТУТ ДОБАВИТЬ ЕЩЕ ПРОВЕРКИ ДЛЯ ПОТЕРИ ДАННЫХ

                        this.TopMost = true; this.TopMost = false;
                        label_view.Visible = false;
                        label_edit.Visible = true;
                        // кнопки
                        btn_delete.Visible = false;
                        btn_save.Visible = false;
                        btn_save.Text = "Сохранить";
                        btn_new.Visible = true;
                        btn_pass.Visible = false;
                        //показываем комбобокс и кнопки перелистывания
                        comboBox_experts.Enabled = true;
                        btn_prev.Visible = true;
                        btn_next.Visible = true;
                        // делаем НЕ редактируемые текстбоксы
                        txt_name.ReadOnly = true;
                        txt_surname.ReadOnly = true;
                        txt_otch.ReadOnly = true;
                        txt_position.ReadOnly = true;

                        txt_password.ReadOnly = true;
                        txt_password.PasswordChar = '*';

                    }
                }
                else
                {
                    this.TopMost = true; this.TopMost = false;
                    label_view.Visible = false;
                    label_edit.Visible = true;
                    // кнопки
                    btn_delete.Visible = false;
                    btn_save.Visible = false;
                    btn_save.Text = "Сохранить";
                    btn_new.Visible = true;
                    btn_pass.Visible = false;
                    //показываем комбобокс и кнопки перелистывания
                    comboBox_experts.Enabled = true;
                    btn_prev.Visible = true;
                    btn_next.Visible = true;
                    // делаем НЕ редактируемые текстбоксы
                    txt_name.ReadOnly = true;
                    txt_surname.ReadOnly = true;
                    txt_otch.ReadOnly = true;
                    txt_position.ReadOnly = true;

                    txt_password.ReadOnly = true;
                    txt_password.PasswordChar = '*';
                    add_new = true;
                }                
            }
            else
            {
                label_view.Visible = false;
                label_edit.Visible = true;
                // кнопки
                btn_delete.Visible = false;
                btn_save.Visible = false;
                btn_save.Text = "Сохранить";
                btn_new.Visible = true;
                btn_pass.Visible = false;
                //показываем комбобокс и кнопки перелистывания
                comboBox_experts.Enabled = true;
                btn_prev.Visible = true;
                btn_next.Visible = true;
                // делаем НЕ редактируемые текстбоксы
                txt_name.ReadOnly = true;
                txt_surname.ReadOnly = true;
                txt_otch.ReadOnly = true;
                txt_position.ReadOnly = true;

            }
        }

        // когда навожу указатель на label_view (статус просмотра)
        private void label_view_MouseEnter(object sender, EventArgs e)
        {
            label_view.Text = "Нажмите, чтобы перейти в режим ПРОСМОТРА";
            label_view.Height = 40;
        }

        // Когда убираю указатель с label_view (статус просмотра)
        private void label_view_MouseLeave(object sender, EventArgs e)
        {
            label_view.Text = "РЕЖИМ РЕДАКТИРОВАНИЯ";
            label_view.Height = 20;
        }

        //===================================================================================

        // нажимаем на label_edit (статус редактирования)
        // ВКЛЮЧЕН ПРОСМОТР
        // ПЕРЕХОДИМ В РЕЖИМ РЕДАКТИРОВАНИЯ
        private void label_edit_Click(object sender, EventArgs e)
        {
            label_edit.Visible = false;
            label_view.Visible = true;
            // кнопки
            btn_delete.Visible = true;
            btn_save.Visible = true;
            btn_save.Text = "Изменить";
            btn_new.Visible = false;
            btn_pass.Text = "Сбросить пароль";
            btn_pass.Visible = true;
            //блочим комбобокс и кнопки перелистывания
            comboBox_experts.Enabled = false;
            btn_prev.Visible = false;
            btn_next.Visible = false;
            // делаем редактируемые текстбоксы
            txt_name.ReadOnly = false;
            txt_surname.ReadOnly = false;
            txt_otch.ReadOnly = false;
            txt_position.ReadOnly = false;
            txt_password.ReadOnly = true;
            txt_password.PasswordChar = '*';

        }

        // когда навожу указатель на label_edit (статус редактирования)
        private void label_edit_MouseEnter(object sender, EventArgs e)
        {
            label_edit.Text = "Нажмите, чтобы перейти в режим РЕДАКТИРОВАНИЯ";
            label_edit.Height = 40;
        }

        // Когда убираю указатель с label_edit (статус редактирования)
        private void label_edit_MouseLeave(object sender, EventArgs e)
        {
            label_edit.Text = "РЕЖИМ ПРОСМОТРА";
            label_edit.Height = 20;
        }
        //===================================================================================

        // кнопка ПАРОЛЬ
        private void btn_pass_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            string text = "";
            for (int i = 0; i < 6; i++)
            {
                text += pass_char[rnd.Next(0, pass_char.Count() - 1)];
            }

            int n = comboBox_experts.SelectedIndex;
            if(label_view.Visible == true) // если в режиме редактирования эксперта
            {
                // то сбрасываем пароль

                DialogResult result = MessageBox.Show(
                "Для эксперта " + comboBox_experts.SelectedItem.ToString() +" будет сброшен пароль.\n" +
                "Сбросить?",
                "Сброс пароля",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2,
                MessageBoxOptions.DefaultDesktopOnly);

                if (result == DialogResult.Yes)
                {
                    this.TopMost = true; this.TopMost = false;
                    exp a;
                    a.n_id = exp_list[n].n_id;
                    a.fio = exp_list[n].fio;
                    a.name = exp_list[n].name;
                    a.surname = exp_list[n].surname;
                    a.otch = exp_list[n].otch;
                    a.position = exp_list[n].position;
                    a.password_exp = text;

                    exp_list.Insert(n, a);
                    exp_list.RemoveAt(n + 1);

                    MessageBox.Show(
                    "Для эксперта " + comboBox_experts.SelectedItem.ToString() + " сброшен пароль.\n" +
                    "Новый пароль: \"" + a.password_exp + "\"" +
                    "Пусть эксперт запомнит пароль, затем закройте это окно!",
                    "Сброс пароля",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);



                    label_save_status.Visible = true;
                }
                else if (result == DialogResult.No)
                {
                    this.TopMost = true; this.TopMost = false;
                }



            }
            // если при добавлении нью эксперта
            if (btn_pass.Text == "Сгенерировать пароль")
            {
                // то генерируем новый пароль
                txt_password.Text = text;
            }

        }

        // кнопка ДОБАВИТЬ НОВОГО ЭКСПЕРТА
        private void btn_new_Click(object sender, EventArgs e)
        {
            int id_free = 0;
            bool find = false;
            if (btn_new.Text == "Добавить нового эксперта")
            {
                label_edit.Visible = false;
                label_view.Visible = false;
                // кнопки
                btn_delete.Visible = false;
                btn_save.Visible = true;
                btn_save.Text = "Добавить";
                btn_pass.Text = "Сгенерировать пароль";
                btn_pass.Visible = true;
                btn_new.Text = "Отмена";
                //блочим комбобокс и кнопки перелистывания
                comboBox_experts.Visible = false;
                btn_prev.Visible = false;
                btn_next.Visible = false;
                // делаем редактируемые текстбоксы
                txt_name.ReadOnly = false;
                txt_name.Text = "";
                txt_surname.ReadOnly = false;
                txt_surname.Text = "";
                txt_otch.ReadOnly = false;
                txt_otch.Text = "";
                txt_position.ReadOnly = false;
                txt_position.Text = "";
                txt_password.ReadOnly = false;
                txt_password.PasswordChar = '\0';
                txt_password.Text = "";

                id_free = 0;
                // поиск свободного id
                while (find == false)
                {
                    for (int j = 0; j < exp_count; j++)
                    {
                        if (id_free == exp_list[j].n_id)
                        {
                            id_free++;
                            j = 0;
                        }
                        else if (j == exp_count - 1)
                        {
                            find = true;
                        }
                    }

                    if (find == true)
                    {
                        txt_id.Text = id_free.ToString();
                    }  
                }
            }
            else if (btn_new.Text == "Отмена")
            {
                btn_new.Text = "Добавить нового эксперта";
                btn_save.Text = "Сохранить";
                label_view_Click(null, null);
                comboBox_experts.Visible = true;
                comboBox_experts.SelectedIndex = 0;
                comboBox_experts_SelectedIndexChanged(null, null);

            }
        }

        // кнопка УДАЛИТЬ ЭКСПЕРТА
        private void btn_delete_Click(object sender, EventArgs e)
        {
            string text = comboBox_experts.SelectedItem.ToString();
            int n = comboBox_experts.SelectedIndex;

            if(exp_list.Count != 0)
            {

                DialogResult result = MessageBox.Show(
                "Удаление эксперта будет невозможно отменить.\n" +
                "Удалить?",
                "Удаление",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2,
                MessageBoxOptions.DefaultDesktopOnly);
                if (result == DialogResult.Yes)
                {
                    this.TopMost = true; this.TopMost = false;
                    result = MessageBox.Show(
                    "\"" + text + " будет удален." + "\"\n" +
                    "Точно удалить?",
                    "Удаление",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2,
                    MessageBoxOptions.DefaultDesktopOnly);
                    if (result == DialogResult.Yes)
                    {
                        // ТУТ ВОТ УДАЛЯЕМ ЕГО ВЕЗДЕ
                        int id = exp_list[n].n_id;
                        exp_list.RemoveAt(n);
                        comboBox_experts.Items.RemoveAt(n);
                        label_view_Click(null, null);
                        comboBox_experts.SelectedIndex = 0;
                        //label_save_status.Visible = true;
                        // удаляем старый файл
                        FileInfo fileInf = new FileInfo(directory + "experts.txt");
                        if (fileInf.Exists)
                            fileInf.Delete();

                        // записываем обратно все в файл из списка
                        using (StreamWriter sr = new StreamWriter(directory + "experts.txt", false, System.Text.Encoding.UTF8))
                        {
                            int i = 0;
                            string line = "";
                            while (i < exp_list.Count)
                            {
                                line = exp_list[i].n_id + " ";
                                line += exp_list[i].surname + " ";
                                line += exp_list[i].name + " ";
                                line += exp_list[i].otch + " ";
                                line += exp_list[i].password_exp + " ";
                                line += exp_list[i].position;

                                sr.WriteLine(line);
                                i++;
                            }
                        }
                        /*
                        FileInfo fileInf1 = new FileInfo(directory + "problems.txt");
                        if (fileInf1.Exists)
                        { 
                            using (StreamReader sr = new StreamReader(directory + "problems.txt", System.Text.Encoding.UTF8))
                            {
                                text = sr.ReadToEnd();
                            }

                            if (text.Length != 0)
                            {

                                text = "";
                                string[] words;
                                List<string> n_prob;
                                n_prob = new List<string>();
                                using (StreamReader sr = new StreamReader(directory + "problems.txt", System.Text.Encoding.UTF8))
                                {
                                    while ((text = sr.ReadLine()) != null)
                                    {
                                        words = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                        n_prob.Add(words[0]);
                                    }
                                }

                                List<string> n_gr;
                                n_gr = new List<string>();

                                for (int i = 0; i < n; i++)
                                {
                                    int N = 1;
                                    bool flag = false;
                                    FileInfo fileInf2 = new FileInfo(directory + "problems.txt");
                                    if (fileInf2.Exists)
                                    {
                                        using (StreamReader sr = new StreamReader(directory + "group" + n_prob[i] + ".txt", System.Text.Encoding.UTF8))
                                        {

                                            sr.ReadLine();
                                            int m = 1;
                                            while ((text = sr.ReadLine()) != null)
                                            {
                                                words = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                                if (words[0] == id.ToString())
                                                {
                                                    flag = true;
                                                    N = m;
                                                }
                                                m++;
                                            }
                                        }


                                        if (flag == true)
                                        {
                                            using (StreamReader sr = new StreamReader(directory + "group" + n_prob[i] + ".txt", System.Text.Encoding.UTF8))
                                            {
                                                while ((text = sr.ReadLine()) != null)
                                                {
                                                    n_gr.Add(text);
                                                }
                                            }

                                            n_gr.RemoveAt(N);
                                            using (StreamWriter sr = new StreamWriter(directory + "group" + n_prob[i] + ".txt", false, System.Text.Encoding.UTF8))
                                            {
                                                for (int j = 0; j < n_gr.Count; j++)
                                                    sr.WriteLine(n_gr[j]);

                                            }
                                            n_gr.Clear();

                                            FileInfo fileInf3 = new FileInfo(directory + "problems.txt");
                                            if (fileInf3.Exists)
                                            {
                                                using (StreamReader sr = new StreamReader(directory + "matrixv" + n_prob[i] + ".txt", System.Text.Encoding.UTF8))
                                                {
                                                    while ((text = sr.ReadLine()) != null)
                                                    {
                                                        n_gr.Add(text);
                                                    }
                                                }

                                                for (int j = 0; j < n_gr.Count; j++)
                                                {
                                                    text = n_gr[j];
                                                    words = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                                    if (words[0] == id.ToString())
                                                    {
                                                        n_gr.RemoveAt(j);
                                                        using (StreamWriter sr = new StreamWriter(directory + "matrixv" + n_prob[i] + ".txt", false, System.Text.Encoding.UTF8))
                                                        {
                                                            for (j = 0; j < n_gr.Count; j++)
                                                                sr.WriteLine(n_gr[j]);
                                                        }
                                                        j = n_gr.Count;

                                                    }
                                                }
                                            }
                                        }


                                    }
                                }

                            }
                        }*/


                        if (exp_list.Count != 0)
                        {
                            comboBox_experts.SelectedIndex = 0;
                            comboBox_experts_SelectedIndexChanged(null, null);
                        }
                        this.TopMost = true; this.TopMost = false;
                        
                    }
                    else if (result == DialogResult.No)
                    {
                        this.TopMost = true; this.TopMost = false;
                    }
                }
                
            }
        }

        // кнопка СОХРАНИТЬ / ДОБАВИТЬ / ИЗМЕНИТЬ
        private void btn_save_Click(object sender, EventArgs e)
        {
            if (btn_save.Text == "Сохранить")
            {
                if (label_save_status.Visible == true)
                {
                    // тупо сохраняем список экспертов в файл

                    using (StreamWriter sr = new StreamWriter(directory + "experts.txt", false, System.Text.Encoding.UTF8))
                    {
                        string line = "";
                        for (int i = 0; i < exp_list.Count; i++)
                        {
                            line = exp_list[i].n_id + " ";
                            line += exp_list[i].surname + " ";
                            line += exp_list[i].name + " ";
                            line += exp_list[i].otch + " ";
                            line += exp_list[i].password_exp + " ";
                            line += exp_list[i].position;

                            sr.WriteLine(line);
                        }
                    }
                    label_save_status.Visible = false;
                    btn_new.Visible = true;

                }
            }
            else
            {
                Regex rgx1 = new Regex(@"^\w+\s*", RegexOptions.IgnoreCase); // слово1+ пробел0+
                MatchCollection matches1 = rgx1.Matches(txt_surname.Text); // число совпадений имени с rgx1
                MatchCollection matches2 = rgx1.Matches(txt_name.Text); // число совпадений фамилии с rgx1
                MatchCollection matches3 = rgx1.Matches(txt_otch.Text);  // число совпадений отчества с rgx1
                MatchCollection matches4 = rgx1.Matches(txt_position.Text);  // число совпадений должности с rgx1
                if (matches1.Count < 1) //Имя
                {
                    label_error.Text = "Фамилия введенна некорректно!";
                    label_error.Visible = true;
                    txt_surname.ForeColor = Color.FromName("Red");
                }
                else if (matches2.Count < 1) // фамилия
                {
                    label_error.Text = "Имя введенно некорректно!";
                    label_error.Visible = true;
                    txt_name.ForeColor = Color.FromName("Red");
                }
                else if (txt_otch.Text.Length != 0 && matches3.Count < 1) // отчество
                {
                    label_error.Text = "Отчество введенно некорректно!";
                    label_error.Visible = true;
                    txt_otch.ForeColor = Color.FromName("Red");
                }
                else if (matches4.Count < 1) // должность
                {
                    label_error.Text = "Должность введенна некорректно!";
                    label_error.Visible = true;
                    txt_position.ForeColor = Color.FromName("Red");
                }
                else if (txt_password.Text.Length < 4 || txt_password.Text.Length > 15)
                {
                    label_error.Text = "Пароль должен быть не менее 4 символов\n и не более 15!";
                    label_error.Visible = true;
                    txt_password.ForeColor = Color.FromName("Red");
                }
                else
                {
                    exp a;
                    a.n_id = Convert.ToInt32(txt_id.Text);
                    if (txt_otch.Text == "")
                        a.fio = txt_surname.Text + " " + txt_name.Text;
                    else
                        a.fio = txt_surname.Text + " " + txt_name.Text + " " + txt_otch.Text;

                    a.name = txt_name.Text;
                    a.surname = txt_surname.Text;
                    if (txt_otch.Text != "")
                        a.otch = txt_otch.Text;
                    else
                        a.otch = "-";
                    a.password_exp = txt_password.Text;
                    a.position = txt_position.Text;

                    if (btn_save.Text == "Добавить")
                    {
                        DialogResult result = MessageBox.Show(
                        "Все данные верны?\n",
                        "Добавление",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button2,
                        MessageBoxOptions.DefaultDesktopOnly);
                        if (result == DialogResult.Yes)
                        {
                            this.TopMost = true; this.TopMost = false;
                            comboBox_experts.Items.Clear();
                            exp_list.Add(a);
                            exp_count++;
                            sort_fio();
                            for (int i = 0; i < exp_count; i++)
                            {
                                comboBox_experts.Items.Add(exp_list[i].fio);
                            }
                            add_new = true;

                            label_view_Click(null, null);

                            comboBox_experts.SelectedIndex = 0;
                            comboBox_experts.Visible = true;
                            label_save_status.Visible = true;
                            btn_new.Text = "Добавить нового эксперта";
                            btn_new.Visible = false;
                            btn_save.Text = "Сохранить";
                            btn_save.Visible = true;
                        }
                        else if (result == DialogResult.No)
                        {
                            this.TopMost = true; this.TopMost = false;
                        }

                    }
                    else if (btn_save.Text == "Изменить")
                    {
                        int n = comboBox_experts.SelectedIndex;
                        DialogResult result = MessageBox.Show(
                       "Все данные верны?\n",
                       "Добавление",
                       MessageBoxButtons.YesNo,
                       MessageBoxIcon.Warning,
                       MessageBoxDefaultButton.Button2,
                       MessageBoxOptions.DefaultDesktopOnly);
                        if (result == DialogResult.Yes)
                        {
                            this.TopMost = true; this.TopMost = false;

                            exp_list.RemoveAt(n);
                            comboBox_experts.Items.Clear();
                            exp_list.Add(a);
                            sort_fio();
                            for (int i = 0; i < exp_count; i++)
                            {
                                comboBox_experts.Items.Add(exp_list[i].fio);
                            }
                            add_new = true;

                            label_view_Click(null, null);

                            comboBox_experts.SelectedIndex = 0;
                            comboBox_experts.Visible = true;
                            label_save_status.Visible = true;
                            btn_new.Text = "Добавить нового эксперта";
                            btn_new.Visible = false;
                            btn_save.Text = "Сохранить";
                            btn_save.Visible = true;
                            btn_delete.Visible = false;
                        }
                        else if (result == DialogResult.No)
                        {
                            this.TopMost = true; this.TopMost = false;
                        }
                    }
                }
            }
        }


        #region НАЖАТИЕ_НА_ТЕКСТБОКСЫ_ДЛЯ_их_НЕЙТРАЛЬНОСТИ

        private void txt_surname_TextChanged(object sender, EventArgs e)  //  когда МЕНЯЕМ ФАМИЛИЮ
        {
            txt_surname.ForeColor = Color.FromName("WindowText");
            label_error.Visible = false;
        }

        private void txt_surname_MouseDown(object sender, MouseEventArgs e)  //  когда НАЖИМАЕМ на ФАМИЛИЮ
        {
            txt_surname.ForeColor = Color.FromName("WindowText");
            label_error.Visible = false;
        }

        private void txt_name_TextChanged(object sender, EventArgs e)  //  когда МЕНЯЕМ ИМЯ
        {
            txt_name.ForeColor = Color.FromName("WindowText");
            label_error.Visible = false;
        }

        private void txt_name_MouseDown(object sender, MouseEventArgs e)  //  когда НАЖИМАЕМ на ИМЯ
        {
            txt_name.ForeColor = Color.FromName("WindowText");
            label_error.Visible = false;
        }

        private void txt_otch_TextChanged(object sender, EventArgs e)  //  когда МЕНЯЕМ ОТЧЕСТВО
        {
            txt_otch.ForeColor = Color.FromName("WindowText");
            label_error.Visible = false;
        }

        private void txt_otch_MouseDown(object sender, MouseEventArgs e)  //  когда НАЖИМАЕМ на ОТЧЕСТВО
        {
            txt_otch.ForeColor = Color.FromName("WindowText");
            label_error.Visible = false;
        }

        private void txt_position_TextChanged(object sender, EventArgs e)  //  когда МЕНЯЕМ ДОЛЖНОСТЬ
        {
            txt_position.ForeColor = Color.FromName("WindowText");
            label_error.Visible = false;
        }

        private void txt_position_MouseDown(object sender, MouseEventArgs e)  //  когда НАЖИМАЕМ на ДОЛЖНОСТЬ
        {
            txt_position.ForeColor = Color.FromName("WindowText");
            label_error.Visible = false;
        }

        private void txt_password_TextChanged(object sender, EventArgs e)   //  когда МЕНЯЕМ ПАРОЛЬ
        {
            txt_password.ForeColor = Color.FromName("WindowText");
            label_error.Visible = false;
        }

        private void txt_password_MouseDown(object sender, MouseEventArgs e)  //  когда НАЖИМАЕМ на ПАРОЛЬ
        {
            txt_password.ForeColor = Color.FromName("WindowText");
            label_error.Visible = false;
        }

        #endregion
    }
}
