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

        private bool add_new_prob;
        private bool add_new_sol;
        
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

        // кнопка СОХРАНИТЬ
        private void btn_save_all_Click(object sender, EventArgs e)
        {

            if (comboBox_problems.Items.Count == 0)
            {
                MessageBox.Show(
                "Проблемы отсутствуют!",
                "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
                this.TopMost = true; this.TopMost = false;
            }
            else
            // Обеспечивает сохранение проблемы и альтернатив в файл
            // Возникает при нажатии на кнопку "Сохранить"
            if (list_solution.Items.Count < 2)
            {
                MessageBox.Show(
                "Альтернатив в списке должно быть как минимум 2!",
                "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
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
        }

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
        private void btn_save_Click(object sender, EventArgs e)
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
                    int n = list_solution.Items.Count;
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
                    }

                    list_solution.Items.Add(txt_solution.Text);
                    txt_solution.Text = "";

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


                int n = list_solution.Items.Count;
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
                    }

                    list_solution.Items.RemoveAt(index);

                    int num = comboBox_problems.SelectedIndex;
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
                }
            }
        }

        // при загрузке формы
        private void form3_analyst_add_Load(object sender, EventArgs e)
        {
            list_solution.Items.Clear();
            add_new_sol = false;
            add_new_prob = false;

            // Убираем надпись о несохраненных данных
            label_save_status.Visible = false;

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
            list_solution.Items.Clear();
        }

        public bool edit_or_add;  // флаг для ред/добав проблемы

        // кнопка + (ДОБАВИТЬ ПРОБЛЕМУ)
        private void btn_add_problem_Click(object sender, EventArgs e)
        {
            if (comboBox_problems.SelectedIndex != -1 && list_solution.Items.Count < 2)
            {
                MessageBox.Show(
                "У проблемы недостаточно альтернатив!\nУдалите проблему или добавьте как минимум 2 альтернативы!",
                "Предупреждение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
                this.TopMost = true; this.TopMost = false;
            }
            else
            {
                Form_problem_add f_edit = new Form_problem_add();
                edit_or_add = false;
                f_edit.Owner = this;
                f_edit.ShowDialog();
                comboBox_problems.SelectedIndex = comboBox_problems.Items.Count - 1;

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
                "Альтернативы удалятся вместе с проблемой!\nУдалить?",
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
            if (comboBox_problems.Items.Count == 0 || comboBox_problems.SelectedIndex == -1)
            {
                MessageBox.Show(
                "Сначала выберите проблему для редактирования!",
                "",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
                this.TopMost = true; this.TopMost = false;
            }
            else
            {
                int num = comboBox_problems.SelectedIndex;
                Form_problem_add f_edit = new Form_problem_add();
                edit_or_add = true;
                f_edit.Owner = this;
                f_edit.ShowDialog();

                comboBox_problems.SelectedIndex = num;

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


        public int predN_prob; // предыдущий номер проблемы, используется в comboBox_problems_SelectedIndexChanged
        
        public bool mem = false;  // флаг, если ничего не выведено в список альтернатив, то не сохраняем их

        // когда выбираем проблему в списке
        private void comboBox_problems_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedState;
            String[] words;
            string path;
            if (mem == true && list_solution.Items.Count != 0)
            {
                selectedState = comboBox_problems.SelectedItem.ToString();
                words = selectedState.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                path = directory + "solutions" + Convert.ToString(predN_prob) + ".txt";

                using (StreamWriter sr = new StreamWriter(path, false, System.Text.Encoding.UTF8))
                {
                    for (int i = 0; i < list_solution.Items.Count; i++)
                    {
                        string line = list_solution.Items[i].ToString();
                        sr.WriteLine(line);

                    }
                }
            }

            predN_prob  = comboBox_problems.SelectedIndex;
            selectedState = comboBox_problems.SelectedItem.ToString();
            words = selectedState.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            list_solution.Items.Clear();
            path = directory + "solutions" + words[0] + ".txt";
            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists)
            {

                using (StreamReader sr = new StreamReader(path, System.Text.Encoding.UTF8))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        list_solution.Items.Add(line);
                    }
                }
                mem = true;
            }

        }
    }
}
