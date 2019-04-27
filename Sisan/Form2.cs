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

    public partial class form2_opros0 : Form
    {
        //======================================================
        // ПУТЬ К ОСНОВНОМУ РАБОЧЕМУ КАТАЛОГУ
        // ЕСЛИ МЕНЯТЬ ПУТЬ, ТО ТОЛЬКО В 1 ФОРМЕ 
        private string directory = global_class.main_directory;
        //======================================================

        // МЕТОД 0 (МЕТОД ПАРНЫХ СРАВНЕНИЙ)

        private int N = 0; // количество вопросов
        private bool start = false;
        private int current = 1;
        private bool alter;

        public struct question
        {
            public string A;
            public string B;
            public double result;
        }

        List<question> q;

        public form2_opros0()
        {
            InitializeComponent();
        }

        // кнопка НАЗАД
        private void button_back_Click(object sender, EventArgs e)
        {
            Form9_expert form = this.Owner as Form9_expert;
            if (start == true)
            {
                DialogResult otvet = MessageBox.Show(
                "Все несохраненные изменения будут потеряны.\n" +
                "Закрыть оценивание?",
                "Внимание",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2,
                MessageBoxOptions.DefaultDesktopOnly);

                if (otvet == DialogResult.Yes)
                {
                    form.Show();  // Показываем форму эксперта
                    form.TopMost = true; form.TopMost = false;
                    this.Close();
                }

                if (otvet == DialogResult.No)
                {
                    this.TopMost = true; this.TopMost = false;
                }
            }
            else
            {
                form.Show();  // Показываем форму эксперта
                form.TopMost = true; form.TopMost = false;
                this.Close();
            }
        }

        // кнопка СВЕРНУТЬ
        private void button_minimize_Click(object sender, EventArgs e)
        {
            // обеспечивает сворачивание формы при нажатии на тире
            this.WindowState = FormWindowState.Minimized;
        }

        // кнопка ЗАКРЫТЬ ОКНО
        private void button_cross_Click(object sender, EventArgs e)
        {
            button_back_Click(null, null);
        }

        // перетаскивание окна по экрану
        private void form2_opros0_MouseDown(object sender, MouseEventArgs e)
        {
            // обеспечивает перемещение формы без рамки
            base.Capture = false;
            Message m = Message.Create(base.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }

        // при загрузке формы
        private void form2_opros0_Load(object sender, EventArgs e)
        {
            Form9_expert form = this.Owner as Form9_expert;
            //String[] words = СТРОКА.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);  // это на всякий под рукой
            list_solution.Items.Clear();

            //При загрузке radiobutton неактивны
            radioButton1.Enabled = false;
            radioButton2.Enabled = false;
            radioButton3.Enabled = false;

            // Заполняем проблему
            label_problems.Text = form.problem;

            string text = "";
            // считываем альтернативы
            FileInfo fileInf = new FileInfo(directory + "solutions" + form.num_problem + ".txt");
            if (fileInf.Exists)  // если файл существует вообще
            {
                using (StreamReader sr = new StreamReader(directory + "solutions" + form.num_problem + ".txt", System.Text.Encoding.UTF8))
                {
                    text = sr.ReadToEnd();
                }
            }

            if (text.Length != 0)
            {
                int i = 0;

                using (StreamReader sr = new StreamReader(directory + "solutions" + form.num_problem + ".txt", System.Text.Encoding.UTF8))
                {
                    while ((text = sr.ReadLine()) != null)
                    {
                        list_solution.Items.Add(text);
                        i++;
                    }
                }

                alter = true;


                // считаем количество вопросов
                N = (i * i - i) / 2;
            }
            else
            {
                alter = false;
                btn_start.Enabled = false;
            }
        }

        //Кнопка НАЧАТЬ ОЦЕНИВАНИЕ 
        private void btn_start_Click(object sender, EventArgs e)
        {
            Form9_expert form = this.Owner as Form9_expert;
            question a;  // впомогательная переменная
            q = new List<question>(); // список вопросов
            List<string> alt = new List<string>();
            int n = 0;

            if (alter)
            {


                //Делаем активными radiobutton
                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
                radioButton3.Enabled = true;


                label3.Text = " из " + Convert.ToString(N);
                start = true;

                // добавляем из списка на форме в список alt
                for (int i = 0; i < list_solution.Items.Count; i++)
                {
                    alt.Add(list_solution.Items[i].ToString());
                    n++;
                }

                for (int i = 0; i < n; i++)
                {
                    for (int j = i + 1; j < n; j++)
                    {
                        a.A = alt[i];
                        a.B = alt[j];
                        a.result = -1;
                        q.Add(a);
                    }
                }

                // тута добавляется номер вопроса в комбобокс
                for (int i = 0; i < N; i++)
                {
                    comboBox_number.Items.Add(i + 1);
                }

                comboBox_number.Text = "1";
                textBox2.Text = q[current - 1].A;
                textBox3.Text = q[current - 1].B;

                string path;

                path = directory + "matrix" + form.num_problem + "m0e" + form1_main.num_expert + ".txt";

                string text = "";
                if (System.IO.File.Exists(path))
                {
                    using (StreamReader sr = new StreamReader(path, System.Text.Encoding.UTF8))
                    {
                        text = sr.ReadToEnd();
                    }
                }

                if (text.Length > 0)
                {
                    String[] numbs;
                    string[,] arr = new string[n, n];

                    using (StreamReader sr = new StreamReader(path, System.Text.Encoding.UTF8))
                    {
                        string line;
                        int i = 0;
                        while ((line = sr.ReadLine()) != null)
                        {
                            //line = line.Replace(',', '.');
                            numbs = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                            for (int j = 0; j < n; j++)
                            {
                                arr[i, j] = numbs[j];
                            }
                            i++;
                        }
                    }

                    int count = 0;
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < n; j++, count++)
                        {
                            if (i == j)
                            {
                                count--;
                            }
                            else
                            {
                                if (i > j)
                                {
                                    count--;
                                }
                                else
                                {
                                    question b = q[count];
                                    b.result = Convert.ToDouble(arr[i, j]);
                                    q[count] = b;

                                }
                            }
                        }
                    }

                    switch (q[current - 1].result)
                    {
                        case 1:
                            radioButton1.Checked = true;
                            break;
                        case 0:
                            radioButton2.Checked = true;
                            break;
                        case 0.5:
                            radioButton3.Checked = true;
                            break;
                        default:
                            break;
                    }

                }

                // так как начался опрос, блочим кнопку "начать опрос"
                btn_start.Enabled = false;
            }
        }
    
        // при выборе ПУНКТ1
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (start)
            {
                question a = q[current - 1];
                a.result = 1;
                q[current - 1] = a;
            }
        }

        // при выборе ПУНКТ2
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (start)
            {
                question a = q[current - 1];
                a.result = 0;
                q[current - 1] = a;
            }
        }

        // при выборе ПУНКТ3
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (start)
            {
                question a = q[current - 1];
                a.result = 0.5;
                q[current - 1] = a;
            }
        }

        // кнопка > (СЛЕДУЮЩИЙ ВОПРОС)
        private void btn_next_Click(object sender, EventArgs e)
        {
            if (start)
            {
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                radioButton3.Checked = false;

                if (current < N)
                    current++;
                else
                    current = 1;

                textBox2.Text = q[current - 1].A;
                textBox3.Text = q[current - 1].B;
                //label10.Text = "Вопрос " + Convert.ToString(current) + " из " + Convert.ToString(N);
                comboBox_number.Text = current.ToString();
               
                switch (q[current - 1].result)
                {
                    case 1:
                        radioButton1.Checked = true;
                        break;
                    case 0:
                        radioButton2.Checked = true;
                        break;
                    case 0.5:
                        radioButton3.Checked = true;
                        break;
                    default:
                        break;
                }
            }
        }

        // кнопка < (ПРЕДЫДУЩИЙ ВОПРОС)
        private void btn_prev_Click(object sender, EventArgs e)
        {
            if (start)
            {
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                radioButton3.Checked = false;

                if (current != 1)
                    current--;
                else
                    current = N;

                textBox2.Text = q[current - 1].A;
                textBox3.Text = q[current - 1].B;
                comboBox_number.Text = current.ToString();

                switch (q[current - 1].result)
                {
                    case 1:
                        radioButton1.Checked = true;
                        break;
                    case 0:
                        radioButton2.Checked = true;
                        break;
                    case 0.5:
                        radioButton3.Checked = true;
                        break;
                    default:
                        break;
                }
            }
        }

        // ФУНКЦИЯ СОХРАНЕНИЯ в файл matrix...
        private void save()
        {
            Form9_expert form = this.Owner as Form9_expert;

            string path = directory + "matrix" + form.num_problem + "m0e" + form1_main.num_expert + ".txt";
            //System.IO.File.Create(path);
            
            int n = list_solution.Items.Count;

            string[,] arr = new string[n, n];
            int count = 0;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++, count++)
                {
                    if (i == j)
                    {
                        arr[i, j] = "9";
                        count--;
                    }
                    else
                    {
                        if (i > j)
                        {
                            count--;
                            if (arr[j, i] == "1")
                                arr[i, j] = "0";

                            if (arr[j, i] == "0")
                                arr[i, j] = "1";

                            if (arr[j, i] == "0,5" || arr[j, i] == "0.5")
                                arr[i, j] = "0,5";

                            if (arr[j, i] == "-1")
                                arr[i, j] = "-1";
                            count--;

                        }
                        else
                            arr[i, j] = Convert.ToString(q[count].result);
                    }
                }
            }
            
            //  замена 0.5 на 0,5 (с запятой)
            //=======================
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    if (arr[i, j] == "0.5" || arr[i, j] == "0,5")
                        arr[i, j] = "0,5";
            //===========================
            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists)
                fileInf.Delete();

            using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.UTF8))
            {
                string matrix = "";
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        matrix += arr[i, j];
                        matrix += ' ';
                    }
                    sw.WriteLine(matrix);
                    matrix = "";
                }
            }
        }

        // кнопка СОХРАНИТЬ
        private void btn_save_Click(object sender, EventArgs e)
        {
            if (start)
            {
                Form9_expert form = this.Owner as Form9_expert;

                bool end = true;
                for (int i = 0; i < q.Count; i++)
                    if (q[i].result == -1)
                        end = false;

                if (!end)
                {
                    this.Hide();
                    DialogResult result = MessageBox.Show(
                    "Вы не прошли опрос до конца.\n" +
                    "Можете вернуться и продолжить в любое время.\n" +
                    "Сохранить?",
                    "Внимание",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
                    if (result == DialogResult.Yes)
                    {
                        start = false;
                        // сохраняем в файл matrix...
                        save();
                        //============================================
                        // в форме эксперта обновляем данные на ней
                        form.list_prob[form.N].exp[form.E].m0 = -1; // не закончил проходить
                        form.save_group(); // сохраняем измененное в файл group...
                        form.update(form.N, form.E);  // обновляем на 9 форме 
                        //============================================
                        radioButton1.Checked = false;
                        radioButton2.Checked = false;
                        radioButton3.Checked = false;

                        label3.Text = " из N";
                        textBox2.Text = "";
                        textBox3.Text = "";

                        //==================================
                        form.Show();
                        form.TopMost = true; form.TopMost = false;
                        this.Close();
                        //===================================
                    }
                    if (result == DialogResult.No)
                    {
                        this.Show();
                        this.TopMost = true; this.TopMost = false;
                    }
                        
                }
                else
                {
                    start = false;
                    // сохраняем в файл matrix...
                    save();
                    //============================================
                    // в форме эксперта обновляем данные на ней
                    form.list_prob[form.N].exp[form.E].m0 = 1; // не закончил проходить
                    form.save_group(); // сохраняем измененное в файл group...
                    form.update(form.N, form.E);  // обновляем на 9 форме 
                    //============================================

                    MessageBox.Show("Изменения сохранены!", "Сохранено",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                    radioButton3.Checked = false;

                    label10.Text = " из N";
                    textBox2.Text = "";
                    textBox2.Text = "";


                    //==================================
                    form.Show();
                    form.TopMost = true; form.TopMost = false;
                    this.Close();
                    //===================================

                }
                btn_start.Enabled = true;
            }
        }

        // при выборе НОМЕРА ВОПРОСА 
        private void comboBox_number_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedState = comboBox_number.SelectedItem.ToString();
            current = Convert.ToInt32(selectedState);

            if (start)
            {
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                radioButton3.Checked = false;

                textBox2.Text = q[current - 1].A;
                textBox3.Text = q[current - 1].B;

                switch (q[current - 1].result)
                {
                    case 1:
                        radioButton1.Checked = true;
                        break;
                    case 0:
                        radioButton2.Checked = true;
                        break;
                    case 0.5:
                        radioButton3.Checked = true;
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
