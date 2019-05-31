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
    public partial class form3_analyst_add : Form
    {
        //======================================================
        // ПУТЬ К ОСНОВНОМУ РАБОЧЕМУ КАТАЛОГУ
        // ЕСЛИ МЕНЯТЬ ПУТЬ, ТО ТОЛЬКО В 1 ФОРМЕ 
        private string directory = global_class.main_directory;
        //======================================================

        public form3_analyst_add()
        {
            InitializeComponent();
        }

        //=================================================================
        public int prob_count = 0;   // количество проблем
        st_problem prob; // вспомогательная переменная для добавления проблемы
        public int alter_count = 0;   // количество альтернатив для выбранной проблемы
        public int exp_count = 0;   // количество экспертов для выбранной проблемы
        public int index_prob = -1; // индекс проблемы при выборе комобобокса проблемы
        //=================================================================
        private bool add_new_prob;
        private bool add_new_sol;
        public bool edit_or_add;  // флаг для ред/добав проблемы
        public int predN_prob; // предыдущий номер проблемы, используется в comboBox_problems_SelectedIndexChanged
        public bool mem = false;  // флаг, если ничего не выведено в список альтернатив, то не сохраняем их
        //=================================================================

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
            public int scale;
            public metod0_inf[] m0;
            public metod1_inf[] m1;
            public metod2_inf[] m2;
            public metod3_inf[] m3;
            public metod4_inf[] m4;
        }
        public List<st_problem> prob_list;
        public struct metod0_inf //  структура для хранения информации о методе 0
        {
            public int id_exp;
            public float[,] matr;
            public int status;
        }

        public struct metod1_inf //  структура для хранения информации о методе 1
        {
            public int id_exp;
            public float comp;
            public int status;
            public float[] marks;
        }
        public struct metod2_inf //  структура для хранения информации о методе 2
        {
            public int id_exp;
            public float[] marks;
            public int status;
        }
        public struct metod3_inf //  структура для хранения информации о методе 2
        {
            public int id_exp;
            public float[] marks;
            public int status;
        }
        public struct metod4_inf //  структура для хранения информации о методе 4
        {
            public int id_exp;
            public float[,] matr;
            public int status;
        }
        #endregion

        
        // кнопка ЗАКРЫТЬ ОКНО
        private void button_cross_Click(object sender, EventArgs e)
        {

            if (label_save_status.Visible == true)
            {
                // Обеспечивает закрытие формы
                DialogResult result = MessageBox.Show(
                    "Все несохраненные данные будут потеряны.\nЗакрыть программу?",
                    "Внимание",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);

                // Если нажата кнопка ДА , то закрыть форму
                if (result == DialogResult.Yes)
                {
                    // обеспечивает закрытие всего приложения нажатии на крестик
                    // вызываем главную форму приложения, главная форма всегда = 0
                    Form form = Application.OpenForms[0];
                    form.Close();  // Закрываем главную форму, а значит закрываем вообще всё
                }

                // Если нажата кнопка НЕТ, то закрыть
                // диалоговое окно, и показать форму
                if (result == DialogResult.No)
                    this.TopMost = true; this.TopMost = false;
            }
            else
            {
                Form form = Application.OpenForms[0];
                form.Close();  // Закрываем главную форму, а значит закрываем вообще всё
            }

        }

        // кнопка СВЕРНУТЬ ОКНО
        private void button_minimize_Click(object sender, EventArgs e)
        {
            // Обеспечивает сворачивание формы в анель задач при нажатии на тире
            this.WindowState = FormWindowState.Minimized;
        }

        // перетаскивание формы по экарану
        private void form3_analyst_add_MouseDown(object sender, MouseEventArgs e)
        {
            // Обеспечивает перемещение формы на экране
            base.Capture = false;
            Message m = Message.Create(base.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }

        // кнопка НАЗАД
        private void button_back_Click(object sender, EventArgs e)
        {
            if (label_save_status.Visible == true)
            {
                // обеспечивает переход назад при нажатии на стрелку
                DialogResult result = MessageBox.Show(
                "Все введеные данные будут потеряны.\nПерейти назад?",
                "Внимание",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);

                // Если нажата кнопка ДА , то перейти на родительскую форму
                if (result == DialogResult.Yes)
                {
                    if (add_new_sol == true)
                    {
                        string text = comboBox_problems.SelectedItem.ToString();
                        string[] words = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        FileInfo fileinf = new FileInfo(directory + "solutions" + words[0] + ".txt");
                        if (fileinf.Exists)
                            fileinf.Delete();

                    }

                    form4_analyst_choice form = new form4_analyst_choice();
                    form.Show();
                    form.TopMost = true; form.TopMost = false;
                    this.Close();
                }

                // Если нажата кнопка НЕТ, то закрыть
                // диалоговое окно, и показать форму
                if (result == DialogResult.No)
                    this.TopMost = true; this.TopMost = false;
            }
            else
            {
                Form form = new form4_analyst_choice();
                form.Show();
                form.TopMost = true; form.TopMost = false;
                this.Close();
            }

        }
        //==========================================================================================================

        // кнопка СОХРАНИТЬ  НЕ ГОТОВО
        private void btn_save_all_Click(object sender, EventArgs e)
        {
            if (label_save_status.Visible == true)
            {
                prob_count = prob_list.Count();
                if (prob_count == 0)
                {
                    // сохраняем файл problems пустым
                    using (StreamWriter sr = new StreamWriter(directory + "problems.txt", false, System.Text.Encoding.UTF8))
                    {
                        sr.Write("");
                    }
                    DirectoryInfo dir = new DirectoryInfo(directory);
                    foreach (FileInfo file in dir.GetFiles("solutions*.txt"))
                    {
                        file.Delete();
                    }
                    foreach (FileInfo file in dir.GetFiles("group*.txt"))
                    {
                        file.Delete();
                    }
                    foreach (FileInfo file in dir.GetFiles("matrix*.txt"))
                    {
                        file.Delete();
                    }
                }
                else if (list_solution.Items.Count < 2)
                {
                    this.Hide();
                    MessageBox.Show(
                    "Альтернатив в списке должно быть как минимум 2!",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
                    this.Show();
                    this.TopMost = true; this.TopMost = false;
                }
                else if (exp_count < 1)
                {
                    this.Hide();
                    MessageBox.Show(
                    "Вы забыли назначить экспертов по проблеме:\n" +
                    "",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
                    this.Show();
                    this.TopMost = true; this.TopMost = false;
                }
                else
                { 
                    DialogResult result = MessageBox.Show(
                    "Сохранить проблему и альтернативы?",
                    "",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);

                    if (result == DialogResult.Yes)
                    {
                        string selectedState = comboBox_problems.SelectedItem.ToString();
                        string[] words = selectedState.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        string path = directory + "problems.txt";

                        using (StreamWriter sr = new StreamWriter(path, false, System.Text.Encoding.UTF8))
                        {
                            for (int i = 0; i < comboBox_problems.Items.Count; i++)
                            {
                                string line = comboBox_problems.Items[i].ToString();
                                string[] line1 = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                line1[0] = Convert.ToString(i);
                                line = "";
                                for (int j = 0; j < line1.Length; j++)
                                {
                                    line += line1[j];
                                    line += " ";
                                }
                                sr.WriteLine(line);

                            }
                        }

                        //selectedState = comboBox_problems.SelectedItem.ToString();
                        //words = selectedState.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        int num = comboBox_problems.SelectedIndex;
                        path = directory + "solutions" + Convert.ToString(num) + ".txt";

                        using (StreamWriter sr = new StreamWriter(path, false, System.Text.Encoding.UTF8))
                        {
                            for (int i = 0; i < list_solution.Items.Count; i++)
                            {
                                string line = list_solution.Items[i].ToString();
                                string[] line1 = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                //line1[0] = Convert.ToString(i);
                                line = "";
                                for (int j = 0; j < line1.Length; j++)
                                {
                                    line += line1[j];
                                    line += " ";
                                }
                                sr.WriteLine(line);

                            }
                        }


                        //тут все сохраняем
                        // после сохранения вывести месэдж что сохранили и внизу переход назад
                        Form form = new form4_analyst_choice();
                        form.Show();
                        form.TopMost = true; form.TopMost = false;
                        this.Close();

                        // Убираем надпись о несохраненных данных
                        label_save_status.Visible = false;
                        add_new_prob = false;

                        Form8.three_or_four = true;
                        Form8 form1 = new Form8();
                        form1.Show();
                        this.Close();
                    }

                    if (result == DialogResult.No)
                    {
                        this.TopMost = true; this.TopMost = false;
                    }
                }
                label_save_status.Visible = false;
            }
        }

        //==========================================================================================================

        // нажатие по списку альтернатив
        private void list_solution_MouseDown(object sender, MouseEventArgs e)
        {
            //обработка нажатия меню при отсутствии альтернатив в списке
            if (list_solution.Items.Count == 0)
                list_menu.Enabled = false;
            else
                list_menu.Enabled = true;

            //при нажатии ПКМ для вызова меню элемент будет выбираться и подсвечиваться автоматически
            if (list_solution.Items.Count != 0 && e.Button == MouseButtons.Right)
            {
                int index = list_solution.IndexFromPoint(e.X, e.Y);
                if (index != -1)
                {
                    list_solution.SetSelected(index, true);
                }
            }
        }

        // кнопка ДОБАВИТЬ АЛЬТЕРНАТИВУ
        private void btn_alter_add_Click(object sender, EventArgs e)
        {
            // Возникает при нажатии на кнопку "Добавить альтернативу"
            if (Convert.ToString(comboBox_problems.SelectedItem) == "")
            {
                MessageBox.Show(
                    "Сначала выберите или добавьте проблему!",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
                this.TopMost = true; this.TopMost = false;
            }
            else
            {
                Regex rgx1 = new Regex(@"^\s+\w*\s*", RegexOptions.IgnoreCase);   // пробел1+ слово0 пробел0+
                Regex rgx2 = new Regex(@"\s*\d+\s*", RegexOptions.IgnoreCase);   // пробел1+ число0 пробел0+
                Regex rgx3 = new Regex(@"\w+\s*", RegexOptions.IgnoreCase); // слово1+ пробел0+
                MatchCollection matches1 = rgx1.Matches(txt_solution.Text); // число совпадений с rgx1
                MatchCollection matches2 = rgx2.Matches(txt_solution.Text); // число совпадений с rgx2
                MatchCollection matches3 = rgx3.Matches(txt_solution.Text);  // число совпадений с rgx3


                if (txt_solution.Text == "")  // АЛЬТЕРНАТИВА ТОЛЬКО ИЗ ЧИСЕЛ ВОЗМОЖНА (доработать)
                {
                    MessageBox.Show(
                    "Не введенна альтернатива для добавления!",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
                    this.TopMost = true; this.TopMost = false;
                }
                else
                if (matches1.Count > 0)
                {
                    MessageBox.Show(
                    "Уберите лишние пробелы в начале ввода альтернативы!",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
                    this.TopMost = true; this.TopMost = false;
                }
                else
                if (matches2.Count > 0)
                {
                    MessageBox.Show(
                    "Альтернатива введена некорректно!",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
                    this.TopMost = true; this.TopMost = false;
                }
                else
                if (matches3.Count < 1)
                {
                    MessageBox.Show(
                    "Альтернатива введена некорректно!",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
                    this.TopMost = true; this.TopMost = false;
                }
                else
                {
                    /*int n = list_solution.Items.Count;
                    string selectedState = comboBox_problems.SelectedItem.ToString();
                    string[] words = selectedState.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    string path = directory + "matrix" + words[0] + ".txt";

                    string text = "";
                    if (File.Exists(path))
                    {
                        using (StreamReader sr = new StreamReader(path, System.Text.Encoding.UTF8))
                        {
                            text = sr.ReadToEnd();
                        }
                    }
                    if (text.Length != 0)
                    {
                        String[] numbs;
                        string[,] arr = new string[n + 1, n + 1];
                        string line;
                        int i = 0;

                        using (StreamReader sr = new StreamReader(path, System.Text.Encoding.UTF8))
                        {

                            while ((line = sr.ReadLine()) != null)
                            {
                                //line = line.Replace(',', '.');
                                numbs = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                                for (int j = 0; j < n + 1; j++)
                                {
                                    if (j == n)
                                    {
                                        arr[i, j] = "-1";
                                    }
                                    else
                                    {
                                        arr[i, j] = numbs[j];
                                    }
                                }
                                i++;
                            }
                        }
                        for (int j = 0; j < n + 1; j++)
                        {
                            arr[i, j] = "-1";
                            if (i == n && j == n)
                                arr[i, j] = "9";
                        }

                        FileInfo fileInf = new FileInfo(path);
                        if (fileInf.Exists)
                            fileInf.Delete();

                        using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.UTF8))
                        {
                            string matrix = "";
                            for (i = 0; i < n + 1; i++)
                            {
                                for (int j = 0; j < n + 1; j++)
                                {
                                    matrix += arr[i, j];
                                    matrix += ' ';
                                }
                                sw.WriteLine(matrix);
                                matrix = "";
                            }
                        }
                    }*/

                    list_solution.Items.Add(txt_solution.Text);
                    /*txt_solution.Text = "";

                    int num = comboBox_problems.SelectedIndex;
                    path = directory + "solutions" + Convert.ToString(num) + ".txt";

                    using (StreamWriter sr = new StreamWriter(path, false, System.Text.Encoding.UTF8))
                    {
                        for (int i = 0; i < list_solution.Items.Count; i++)
                        {
                            string line = list_solution.Items[i].ToString();
                            string[] line1 = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            line = "";
                            for (int j = 0; j < line1.Length; j++)
                            {
                                line += line1[j];
                                line += " ";
                            }
                            sr.WriteLine(line);

                        }
                    }
                    if(add_new_prob == true)
                        add_new_sol = true;

                    this.ActiveControl = txt_solution;
                    this.TopMost = true; this.TopMost = false;
                    //label_save_status.Visible = true;
                    */
                }
                
            }
        }

        // кнопка в МЕНЮ: РЕДАКТИРОВАТЬ АЛЬТЕРНАТИВУ
        private void list_menu_edit_Click(object sender, EventArgs e)
        {
            // Обеспечивает редактирование альтернативы с помощью контекстного меню
            // принажатии на кнопку "Редактировать альтернативу" 

            int index = list_solution.SelectedIndex; // Получаем номер альтернативы в списке
            if (index != -1)
            {
                Form_lsolution_edit f_edit = new Form_lsolution_edit();
                f_edit.Owner = this;
                f_edit.ShowDialog();

                string selectedState = comboBox_problems.SelectedItem.ToString();
                string[] words = selectedState.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string path = directory + "solutions" + words[0] + ".txt";
                FileInfo fileInf = new FileInfo(path);
                if (fileInf.Exists)
                {
                    using (StreamWriter sr = new StreamWriter(path, false, System.Text.Encoding.UTF8))
                    {
                        for (int i = 0; i < list_solution.Items.Count; i++)
                        {
                            string line = list_solution.Items[i].ToString();
                            sr.WriteLine(line);
                        }
                    }
                }
            }
        }

        // кнопка в МЕНЮ: УДАЛИТЬ АЛЬТЕРНАТИВУ
        private void list_menu_delete_Click(object sender, EventArgs e)
        {
            // Обеспечивает удаление альтернативы с помощью контекстного меню
            // принажатии на кнопку "Удалить альтернативу"
            int index = list_solution.SelectedIndex;
            if (index != -1)
            {


                /*int n = list_solution.Items.Count;
                string[,] arr1 = new string[n, n];
                string[,] arr2 = new string[n - 1, n - 1];
                string selectedState = comboBox_problems.SelectedItem.ToString();
                string[] words = selectedState.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string path = directory + "matrix" + words[0] + ".txt";

                string text = "";
                if (System.IO.File.Exists(path))
                {
                    using (StreamReader sr = new StreamReader(path, System.Text.Encoding.UTF8))
                    {
                        text = sr.ReadToEnd();
                    }
                }
                if (text.Length != 0)
                {
                    String[] numbs;

                    string line;
                    int i = 0;

                    using (StreamReader sr = new StreamReader(path, System.Text.Encoding.UTF8))
                    {

                        while ((line = sr.ReadLine()) != null)
                        {
                            //line = line.Replace(',', '.');
                            numbs = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                            for (int j = 0; j < n; j++)
                            {
                                arr1[i, j] = numbs[j];
                            }
                            i++;
                        }
                    }

                    if (index == n - 1) // если удалить последнюю альтернативу
                    {
                        for (i = 0; i < n - 1; i++)
                        {
                            for (int j = 0; j < n - 1; j++)
                            {
                                arr2[i, j] = arr1[i, j];

                            }
                        }
                    }
                    else
                    {

                        for (i = 0; i < index; i++)  // переписываем элементы верх-слева
                        {
                            for (int j = 0; j < index; j++)
                            {
                                arr2[i, j] = arr1[i, j];

                            }
                        }

                        for (i = index + 1; i < n; i++)// переписываем элементы низ-слева
                        {
                            for (int j = 0; j < index; j++)
                            {
                                arr2[i - 1, j] = arr1[i, j];
                            }
                        }

                        for (i = 0; i < index; i++)// переписываем элементы верх справа
                        {
                            for (int j = index + 1; j < n; j++)
                            {
                                arr2[i, j - 1] = arr1[i, j];
                            }
                        }

                        for (i = index + 1; i < n; i++)// переписываем элементы низ справа
                        {
                            for (int j = index + 1; j < n; j++)
                            {
                                arr2[i - 1, j - 1] = arr1[i, j];
                            }
                        }
                    }

                    FileInfo fileInf = new FileInfo(path);
                    if (fileInf.Exists)
                        fileInf.Delete();

                    using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.UTF8))
                    {
                        string matrix = "";
                        for (i = 0; i < n - 1; i++)
                        {
                            for (int j = 0; j < n - 1; j++)
                            {
                                matrix += arr2[i, j];
                                matrix += ' ';
                            }
                            sw.WriteLine(matrix);
                            matrix = "";
                        }
                    }*/

                    list_solution.Items.RemoveAt(index);

                   /* int num = comboBox_problems.SelectedIndex;
                    path = directory + "solutions" + Convert.ToString(num) + ".txt";

                    using (StreamWriter sr = new StreamWriter(path, false, System.Text.Encoding.UTF8))
                    {
                        for (i = 0; i < list_solution.Items.Count; i++)
                        {
                            line = list_solution.Items[i].ToString();
                            string[] line1 = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            //line1[0] = Convert.ToString(i);
                            line = "";
                            for (int j = 0; j < line1.Length; j++)
                            {
                                line += line1[j];
                                line += " ";
                            }
                            sr.WriteLine(line);

                        }
                    }
                }*/
            }
        }

        //==========================================================================================================

        // при ЗАГРУЗКЕ ФОРМЫ
        private void form3_analyst_add_Load(object sender, EventArgs e)
        {
            list_solution.Items.Clear();

            add_new_sol = false;
            add_new_prob = false;

            String[] words;              //СТРОКА.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);  // это на всякий под рукой
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
                            a.fio = words[1] + " " + words[2].First() + "." + words[3].First() + ".";
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

                //======== экспертов запомнили ==============

                alter_list = new List<solutions>();  // память для списка где харанится альтернативы для проблем
                solutions sol = new solutions();  // вспомогательная переменная для строки выше
                //====== читаем проблемы ==========
                prob_list = new List<st_problem>();
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
                    using (StreamReader sr = new StreamReader(directory + "problems.txt", System.Text.Encoding.UTF8))
                    {
                        string line = "";
                        prob_count = 0;
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
                            //считываем шкалу
                            if (File.Exists(directory + "scale" + prob.num_prob + ".txt") == true)
                            {
                                using (StreamReader sr5 = new StreamReader(directory + "scale" + prob.num_prob + ".txt", System.Text.Encoding.UTF8))
                                {
                                    text = sr5.ReadToEnd();
                                }
                                int.TryParse(text, out prob.scale);
                            }
                            else
                                prob.scale = 100;
                            //=========================================
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
                                    while ((line = sr1.ReadLine()) != null)
                                    {
                                        sol.alters[i] = line;
                                        i++;
                                    }
                                }
                                alter_list.Add(sol);
                                prob.status_prob = 0; // 0 значит альтернативы считались ,
                                                      // то есть теперь считываем экспертов и их пока 0
                            }
                            else // ХЗ  НАДО ПОДУМАТЬ (ВООБЩЕ ТАКОГО НЕ МОЖЕТ БЫТЬ)
                            {
                                alter_list.Add(sol);
                            }
                            // ========= альтернативы считали ==============

                            // теперь считываем group
                            text = "";
                            line = "";
                            exp_count = 0;

                            FileInfo fileInf4 = new FileInfo(directory + "group" + prob.num_prob + ".txt");
                            if (fileInf4.Exists)  // если  альтернативы считались файл существует вообще
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
                                prob.m0 = new metod0_inf[exp_count];
                                prob.m1 = new metod1_inf[exp_count];
                                prob.m2 = new metod2_inf[exp_count];
                                prob.m3 = new metod3_inf[exp_count];
                                prob.m4 = new metod4_inf[exp_count];
                                // ====================================================================
                                using (StreamReader sr1 = new StreamReader(directory + "group" + prob.num_prob + ".txt", System.Text.Encoding.UTF8))
                                {
                                    for (int k = 0; k < exp_count; k++)
                                    {
                                        if ((line = sr1.ReadLine()) != null)
                                        {
                                            words = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                            // ==== Запоминаем для метода 0 ===================
                                            prob.m0[k].id_exp = Convert.ToInt32(words[0]); // запомнили id эксперта
                                            prob.m0[k].status = Convert.ToInt32(words[2]); // статус прохождения опроса
                                            prob.m0[k].matr = new float[alter_count, alter_count]; // память под оценки эксперта k
                                            load_m0(k); // загрузка метода 0 для k эксперта
                                                        // ==== Метод 0 запомнили =======================
                                                        // ==== Запоминаем для метода 1 ===================
                                            prob.m1[k].id_exp = Convert.ToInt32(words[0]); // запомнили id эксперта
                                            prob.m1[k].comp = Convert.ToSingle(words[1]); // запомнили компетентность эксперта
                                            prob.m1[k].status = Convert.ToInt32(words[3]); // статус прохождения опроса
                                            prob.m1[k].marks = new float[alter_count]; // память под оценки эксперта k
                                            load_m1(k); // загрузка метода 1 для k эксперта
                                                        // ==== Метод 1 запомнили =======================
                                                        // ==== Запоминаем для метода 2 ===================
                                            prob.m2[k].id_exp = Convert.ToInt32(words[0]); // запомнили id эксперта
                                            prob.m2[k].status = Convert.ToInt32(words[4]); // статус прохождения опроса
                                            prob.m2[k].marks = new float[alter_count]; // память под оценки эксперта k
                                            load_m2(k); // загрузка метода 2 для k эксперта
                                                        // ==== Метод 2 запомнили =======================
                                                        // ==== Запоминаем для метода 3 ===================
                                            prob.m3[k].id_exp = Convert.ToInt32(words[0]); // запомнили id эксперта
                                            prob.m3[k].status = Convert.ToInt32(words[5]); // статус прохождения опроса
                                            prob.m3[k].marks = new float[alter_count]; // память под оценки эксперта k
                                            load_m3(k); // загрузка метода 3 для k эксперта
                                                        // ==== Метод 3 запомнили =======================
                                                        // ==== Запоминаем для метода 4 ===================
                                            prob.m4[k].id_exp = Convert.ToInt32(words[0]); // запомнили id эксперта
                                            prob.m4[k].status = Convert.ToInt32(words[6]); // статус прохождения опроса
                                            prob.m4[k].matr = new float[alter_count, alter_count]; // память под оценки эксперта k
                                            load_m4(k); // загрузка метода 4 для k эксперта
                                                        // ==== Метод 4 запомнили =======================
                                        }
                                    }
                                }
                                //====================================================
                                prob.status_prob = exp_count;
                            }
                            prob_list.Add(prob);
                            prob_count++;
                            comboBox_problems.Items.Add(prob.txt_prob);
                        }
                    }
                    // файл problems закрылся
                }
                else
                {
                    prob_count = 0;
                    lbl_notprob.Visible = true;
                    btn_alter_add.Cursor = Cursors.No;
                    box_open_close.Visible = false;
                }

                if (comboBox_problems.Items.Count > 0)
                {
                    comboBox_problems.SelectedIndex = 0;
                }
            }
        }

        // для ЗАГРУЗКИ МЕТОДА 0
        public void load_m0(int k)
        {
            if (prob.m0[k].status == 1 || prob.m0[k].status == -1)
            {
                // если статус пройден или не до конца пройден то считываем матрицу
                if (File.Exists(directory + "matrix" + prob.num_prob + "m0e" + prob.m0[k].id_exp + ".txt") == true)
                {
                    using (StreamReader sr2 = new StreamReader(directory + "matrix" + prob.num_prob + "m0e" + prob.m0[k].id_exp + ".txt", System.Text.Encoding.UTF8))
                    {
                        string[] words1;
                        string line = "";
                        for (int i = 0; i < alter_count; i++)
                        {
                            if ((line = sr2.ReadLine()) != null)
                            {
                                words1 = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                for (int j = 0; j < alter_count; j++)
                                {
                                    prob.m0[k].matr[i, j] = Convert.ToSingle(words1[j]);
                                }
                            }
                        }
                    }
                    // == считали из файла матрицу и если статус 1 то запомнили веса ===
                }
            }
            else if (prob.m0[k].status == 0)
            {
                for (int i = 0; i < alter_count; i++)
                {
                    for (int j = 0; j < alter_count; j++)
                    {
                        if (i == j)
                            prob.m0[k].matr[i, j] = 9;
                        else
                            prob.m0[k].matr[i, j] = -1;
                    }
                }

            }
        }

        // для ЗАГРУЗКИ МЕТОДА 1
        public void load_m1(int k)
        {
            if (prob.m1[k].status == 1 || prob.m1[k].status == -1)
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
                            if (Convert.ToInt32(words1[0]) == prob.m1[k].id_exp)
                            {
                                for (int j = 0; j < alter_count; j++)
                                {
                                    prob.m1[k].marks[j] = Convert.ToSingle(words1[j + 1]);
                                }
                            }
                        }

                    }
                    // == считали из файла матрицу и если статус 1 то запомнили веса ===
                }
            }
            else if (prob.m1[k].status == 0)
            {
                for (int j = 0; j < alter_count; j++)
                {
                    prob.m1[k].marks[j] = -1;
                }
            }
        }

        // для ЗАГРУЗКИ МЕТОДА 2
        public void load_m2(int k)
        {
            if (prob.m2[k].status == 1 || prob.m2[k].status == -1)
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
                            if (Convert.ToInt32(words1[0]) == prob.m2[k].id_exp)
                            {
                                for (int j = 0; j < alter_count; j++)
                                {
                                    prob.m2[k].marks[j] = Convert.ToSingle(words1[j + 1]);
                                }
                            }
                        }
                    }
                    // == считали из файла матрицу и если статус 1 то запомнили веса ===
                }
            }
            else if (prob.m2[k].status == 0)
            {
                for (int j = 0; j < alter_count; j++)
                {
                    prob.m2[k].marks[j] = -1;
                }
            }
        }

        // для ЗАГРУЗКИ МЕТОДА 3
        public void load_m3(int k)
        {
            if (prob.m3[k].status == 1 || prob.m3[k].status == -1)
            {
                // если статус пройдeн у эксперта то читаем его оценочки
                if (File.Exists(directory + "matrix" + prob.num_prob + "m3.txt") == true)
                {
                    using (StreamReader sr2 = new StreamReader(directory + "matrix" + prob.num_prob + "m3.txt", System.Text.Encoding.UTF8))
                    {
                        string[] words1;
                        string line = "";

                        while ((line = sr2.ReadLine()) != null)
                        {
                            words1 = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            if (Convert.ToInt32(words1[0]) == prob.m3[k].id_exp)
                            {
                                for (int j = 0; j < alter_count; j++)
                                {
                                    prob.m3[k].marks[j] = Convert.ToSingle(words1[j + 1]);
                                }
                            }
                        }

                    }
                    // == считали из файла матрицу и если статус 1 то запомнили веса ===
                }
            }
            else if (prob.m3[k].status == 0)
            {
                for (int j = 0; j < alter_count; j++)
                {
                    prob.m3[k].marks[j] = -1;
                }
            }
        }

        // для ЗАГРУЗКИ МЕТОДА 4
        public void load_m4(int k)
        {
            if (prob.m4[k].status == 1 || prob.m4[k].status == -1)
            {
                // если статус пройден или не до конца пройден то считываем матрицу
                if (File.Exists(directory + "matrix" + prob.num_prob + "m4e" + prob.m4[k].id_exp + ".txt") == true)
                {
                    using (StreamReader sr2 = new StreamReader(directory + "matrix" + prob.num_prob + "m4e" + prob.m4[k].id_exp + ".txt", System.Text.Encoding.UTF8))
                    {
                        string[] words1;
                        string line = "";
                        for (int i = 0; i < alter_count; i++)
                        {
                            if ((line = sr2.ReadLine()) != null)
                            {
                                words1 = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                for (int j = 0; j < alter_count; j++)
                                {
                                    prob.m4[k].matr[i, j] = Convert.ToSingle(words1[j]);
                                }
                            }
                        }
                    }
                    // == считали из файла матрицу и если статус 1 то запомнили веса ===
                }
            }
            else if (prob.m4[k].status == 0)
            {
                for (int i = 0; i < alter_count; i++)
                {
                    for (int j = 0; j < alter_count; j++)
                    {
                        if (i == j)
                            prob.m4[k].matr[i, j] = 9;
                        else
                            prob.m4[k].matr[i, j] = -1;
                    }
                }

            }
        }

        //==========================================================================================================

        // кнопка + (ДОБАВИТЬ ПРОБЛЕМУ)
        private void btn_add_problem_Click(object sender, EventArgs e)
        {
            if (comboBox_problems.SelectedIndex > 0 && comboBox_problems.SelectedIndex < prob_count)
            {
                /*MessageBox.Show(
                "У проблемы недостаточно альтернатив!\nУдалите проблему или добавьте как минимум 2 альтернативы!",
                "Предупреждение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
                this.TopMost = true; this.TopMost = false;*/

                
                    Form_problem_add f_edit = new Form_problem_add();
                    edit_or_add = false;
                    f_edit.Owner = this;
                    f_edit.ShowDialog();
                    //comboBox_problems.SelectedIndex = comboBox_problems.Items.Count - 1;

                    // Надпись, что есть несохраненные данные
                    label_save_status.Visible = true;
                    add_new_prob = true;

                    // ОНО НИЖЕ ЗАКОМЕНЧЕНО, ТАК И НАДО
                    // ПРИ ДОБАВЛЕНИИ ПРОБЛЕМЫ, НАДО САМИМ СОХРАНЯТЬ
                    // т.к. раньше после добавлении была возможна ситация: проблема есть, а альтернатив к ней нет

                    //сохраняем проблемы в файл  //ПОСЛЕДНЯЯ ДОРАБОТКА БЫЛА, ЧТОБ НЕ НАЖИМАТЬ КНОПКУ СОХРАНИТЬ
                    //================================
                    /*string selectedState = comboBox_problems.SelectedItem.ToString();
                    string[] words = selectedState.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    string path = directory + "problems.txt";

                    using (StreamWriter sr = new StreamWriter(path, false, System.Text.Encoding.UTF8))
                    {
                        for (int i = 0; i < comboBox_problems.Items.Count; i++)
                        {
                            string line = comboBox_problems.Items[i].ToString();
                            string[] line1 = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            line1[0] = Convert.ToString(i);
                            line = "";
                            for (int j = 0; j < line1.Length; j++)
                            {
                                line += line1[j];
                                line += " ";
                            }
                            sr.WriteLine(line);

                        }
                    }*/
                    //===============================
                
            }
        }

        // кнопка ✖ (УДАЛИТЬ ПРОБЛЕМУ)
        private void btn_problem_delete_Click(object sender, EventArgs e)
        {
            DialogResult result;
            int index = comboBox_problems.SelectedIndex; ;
            if (index != -1)
            {
                result = MessageBox.Show(
                "Данные оценивания удалятся вместе с проблемой!\nУдалить?",
                "Предупреждение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
                this.TopMost = true; this.TopMost = false;
                if (result == DialogResult.Yes)
                {
                    string selectedState;
                    String[] words;
                    selectedState = comboBox_problems.SelectedItem.ToString();
                    words = selectedState.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    string path = directory + "solutions" + words[0] + ".txt";
                    FileInfo fileInf = new FileInfo(path);
                    if (fileInf.Exists)
                        fileInf.Delete();

                    path = directory + "matrix" + words[0] + ".txt";
                    fileInf = new FileInfo(path);
                    if (fileInf.Exists)
                        fileInf.Delete();

                    comboBox_problems.Items.RemoveAt(index);
                    //if (comboBox_problems.Items.Count == 0)
                    comboBox_problems.SelectedText = "";

                    list_solution.Items.Clear();

                    //сохраняем проблемы в файл 
                    path = directory + "problems.txt";

                    using (StreamWriter sr = new StreamWriter(path, false, System.Text.Encoding.UTF8))
                    {
                        for (int i = 0; i < comboBox_problems.Items.Count; i++)
                        {
                            string line = comboBox_problems.Items[i].ToString();
                            string[] line1 = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            line1[0] = Convert.ToString(i);
                            line = "";
                            for (int j = 0; j < line1.Length; j++)
                            {
                                line += line1[j];
                                line += " ";
                            }
                            sr.WriteLine(line);
                        }
                    }

                    for (int i = Convert.ToInt32(words[0]); i < comboBox_problems.Items.Count; i++)
                    {
                        path = directory + "matrix"/* + i + ".txt"*/;
                        string path2 = directory + "solutions";
                        int m = i + 1;
                        File.Move(path + m + ".txt", path + i + ".txt");
                        File.Move(path2 + m + ".txt", path2 + i + ".txt");
                    }

                    comboBox_problems.Items.Clear();
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
                                comboBox_problems.Items.Add(line);
                            }
                        }
                    }
                    // Убираем запись, о несохраненных данных
                    label_save_status.Visible = false;
                    //===============================
                }
            }

            if (comboBox_problems.Items.Count != 0)
            {
                comboBox_problems.SelectedIndex = 0;
            }

            if (comboBox_problems.Items.Count == 0)
            {
                using (StreamWriter sr = new StreamWriter(directory + "problems.txt", false, System.Text.Encoding.UTF8))
                {
                    sr.Write("");
                }
            }

        }

        // кнопка ✎ (РЕДАКТИРОВАТЬ ПРОБЛЕМУ)
        private void btn_problem_edit_Click(object sender, EventArgs e)
        {
            if (comboBox_problems.SelectedIndex >= 0 && comboBox_problems.SelectedIndex < prob_count)
            {          
            
            
                index_prob = comboBox_problems.SelectedIndex;
                Form_problem_add f_edit = new Form_problem_add();
                edit_or_add = true;
                f_edit.Owner = this;
                f_edit.ShowDialog();

                //comboBox_problems.SelectedIndex = index_prob;

                // Надпись, что есть несохраненные данные
                label_save_status.Visible = true;

                // ОНО НИЖЕ ЗАКОМЕНЧЕНО, ТАК И НАДО
                // ПРИ РЕДАКТИРОВАНИИ ПРОБЛЕМЫ, НАДО САМИМ СОХРАНЯТЬ
                // т.к. раньше после добавления проблемы и сразу же ее редактирования была возможна ситация:
                // проблема есть, а альтернатив к ней нет

                //сохраняем проблемы в файл  //ПОСЛЕДНЯЯ ДОРАБОТКА БЫЛА, ЧТОБ НЕ НАЖИМАТЬ КНОПКУ СОХРАНИТЬ
                //================================
                /*string selectedState = comboBox_problems.SelectedItem.ToString();
                string[] words = selectedState.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string path = directory + "problems.txt";

                using (StreamWriter sr = new StreamWriter(path, false, System.Text.Encoding.UTF8))
                {
                    for (int i = 0; i < comboBox_problems.Items.Count; i++)
                    {
                        string line = comboBox_problems.Items[i].ToString();
                        string[] line1 = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        line1[0] = Convert.ToString(i);
                        line = "";
                        for (int j = 0; j < line1.Length; j++)
                        {
                            line += line1[j];
                            line += " ";
                        }
                        sr.WriteLine(line);

                    }
                }*/
                //===============================
            }
        }

        // ВЫБОР ПРОБЛЕМЫ В КОМБОБОКС
        private void comboBox_problems_SelectedIndexChanged(object sender, EventArgs e)
        {
            index_prob = comboBox_problems.SelectedIndex;
            if(index_prob >= 0 && index_prob < prob_count)
            {
                //====== галочку открыта/закрыта =============
                if (prob_list[index_prob].open_close == true)
                    box_open_close.Checked = true;
                else
                    box_open_close.Checked = false;
                //============================================
                txt_scale.Text = prob_list[index_prob].scale.ToString();
                //============================================

                //===== выводим альтернативы из alters =======
                int alt_index = 0;
                for(int i = 0; i < prob_count; i++)
                {
                    if(alter_list[i].id_prob == prob_list[index_prob].num_prob)
                    {
                        alt_index = i;
                    }
                }

                list_solution.Items.Clear();
                for (int i = 0; i < alter_list[index_prob].alters.Count(); i++)
                {
                    list_solution.Items.Add(alter_list[alt_index].alters[i]);
                }
                //============================================
                DataTable table = new DataTable("Эксперты");
                table.Clear();
                table.Columns.Clear();
                table.Rows.Clear();
                table.Columns.Add(new DataColumn("ФИО"));
                table.Columns.Add(new DataColumn("Компетентность"));
                table.Columns.Add(new DataColumn("Должность"));
                table.Columns.Add(new DataColumn("M1"));
                table.Columns.Add(new DataColumn("M2"));
                table.Columns.Add(new DataColumn("M3"));
                table.Columns.Add(new DataColumn("M4"));
                table.Columns.Add(new DataColumn("M5"));
                int index;
                for (int j = 0; j < exp_list.Count; j++)
                {
                    index = 0;
                    bool flag = false;
                    for(int i = 0; i < prob_list[index_prob].m0.Count(); i++)
                    {
                        if(prob_list[index_prob].m0[i].id_exp == exp_list[j].id_exp)
                        {
                            index = i;
                            flag = true;
                        }
                    }
                    DataRow dr = table.NewRow();
                    dr[0] = exp_list[j].fio;
                    if (flag == true)
                        dr[1] = prob_list[index_prob].m1[index].comp;
                    else
                        dr[1] = "";
                    dr[2] = exp_list[j].position;
                    dr[3] = prob_list[index_prob].m0[index].status;
                    dr[4] = prob_list[index_prob].m1[index].status;
                    dr[5] = prob_list[index_prob].m2[index].status;
                    dr[6] = prob_list[index_prob].m3[index].status;
                    dr[7] = prob_list[index_prob].m4[index].status;

                    table.Rows.Add(dr);
                }

                dataGridView1.DataSource = table;
                dataGridView1.Columns[0].Width = 50;
                dataGridView1.Columns[1].Width = 120;
                dataGridView1.Columns[2].Width = 95;
                dataGridView1.Columns[3].Width = 237;
                dataGridView1.Columns[4].Width = 30;
                dataGridView1.Columns[5].Width = 30;
                dataGridView1.Columns[6].Width = 30;
                dataGridView1.Columns[7].Width = 30;
                dataGridView1.Columns[8].Width = 30;
                for (int i = 0; i < dataGridView1.Columns.Count;i++)
                {
                    dataGridView1.Columns[i].ReadOnly = true;
                    if (i == 0 || i == 2)
                        dataGridView1.Columns[i].ReadOnly = false;
                    dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                dataGridView1.AllowUserToResizeColumns = false;
                dataGridView1.AllowUserToResizeRows = false;
                // расскраска статусов методов
                string stroka;
                for (int j = 4; j < 9; j++)
                {
                    for (int i = 0; i < exp_list.Count; i++)
                    {
                        stroka = dataGridView1.Rows[i].Cells[j].Value.ToString();
                        switch (stroka)
                        {
                            case "-1":
                                {
                                    dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.FromName("Yellow");
                                    break;
                                }
                            case "0":
                                {
                                    dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.FromName("Red");
                                    break;
                                }
                            case "1":
                                {
                                    dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.FromName("Green");
                                    break;
                                }
                        }
                        dataGridView1.Rows[i].Cells[j].Value = "";
                    }
                }
                // расстановка галочек
                /*for (int k = 0; k < dataGridView1.Rows.Count; k++)
                {
                    for (int j = 0; j < exp_list.Count; j++)
                    {
                        index = 0;
                        bool flag = false;
                        for (int i = 0; i < prob_count; i++)
                        {
                            if (prob_list[index_prob].m0[i].id_exp == exp_list[j].id_exp)
                            {
                                index = i;
                                flag = true;
                            }
                        }
                    }
                        if (exp_list[i].choice == true)
                    {
                        dataGridView1.Rows[i].Cells[0].Value = true;
                    }
                }*/
                //============================================
            }
        }
    }
}
