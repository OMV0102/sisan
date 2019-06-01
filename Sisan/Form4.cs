using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace system_analysis
{
    public partial class form4_analyst_choice : Form
    {
        public form4_analyst_choice()
        {
            InitializeComponent();
        }

        //======================================================
        // ПУТЬ К ОСНОВНОМУ РАБОЧЕМУ КАТАЛОГУ
        // ЕСЛИ МЕНЯТЬ ПУТЬ, ТО ТОЛЬКО В 1 ФОРМЕ 
        private string directory = global_class.main_directory;
        //======================================================

        // перемещение формы по экрану
        private void form5_analyst_choice_MouseDown(object sender, MouseEventArgs e)
        {
            // обеспечивает перемещение формы без рамки
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

        // кнопка ЗАКРЫТЬ ПРИЛОЖЕНИЕ
        private void button_cross_Click(object sender, EventArgs e)
        {
            // обеспечевает закрытие всего приложения нажатии на крестик
            // вызываем главную форму приложения, главная форма всегда = 0
            Form form = Application.OpenForms[0];
            form.Close();  // Закрываем главную форму, а значит закрываем вообще всё
        }

        // кнопка ДОБАВИТЬ/РЕДАКТИРОВАТЬ ПРОБЛЕМУ
        private void btn_add_problem_Click(object sender, EventArgs e)
        {
            // переход на форму при нажатии на кнопку "Добавить проблему"
            Form form = new form3_analyst_add();
            form.Show();
            this.Close();
        }

        // кнопка ПОСМОТРЕТЬ ОПРОС
        private void btn_show_report_Click(object sender, EventArgs e)
        {
            Form form = new form5_analyst_report();
            form.Show();
            this.Close();
        }

        // кнопка НАЗАД
        private void button_back_Click(object sender, EventArgs e)
        {
            Form form = Application.OpenForms[0];
            form.Show();
            this.Close();
        }

        // кнопка СМЕНИТЬ ПАРОЛЬ аналитика
        private void btn_change_pass_Click(object sender, EventArgs e)
        {
            Form_change_pass form = new Form_change_pass();
            form.Show();
            this.Close();
        }

        // при загрузке формы
        private void form4_analyst_choice_Load(object sender, EventArgs e)
        {
            if (Form_change_pass.is_ok == true)
            {
                Form_change_pass.is_ok = false;
                MessageBox.Show(
                "Пароль успешно изменен!",
                "Изменение пароля",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
                this.TopMost = true; this.TopMost = false;
                
            }
        }

        // кнопка РЕДАКТИРОВАНИЕ СПИСКА ЭКСПЕРТОВ
        private void btn_expert_edit_Click(object sender, EventArgs e)
        {
            Form10_experts_edit form = new Form10_experts_edit();
            form.Show();
            this.Close();
        }
    }
}
