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
    public partial class form6_matrix : Form
    {
        public form6_matrix()
        {
            InitializeComponent();
        }

        //======================================================
        // ПУТЬ К ОСНОВНОМУ РАБОЧЕМУ КАТАЛОГУ
        // ЕСЛИ МЕНЯТЬ ПУТЬ, ТО ТОЛЬКО В 1 ФОРМЕ 
        private string directory = global_class.main_directory;
        //======================================================
        public struct exp2 //  структура для хранения одного эксперта
        {
            public int id_exp;
            public string fio; // сокращенное ФИО
        }
        public List<exp2> f6list_exp;
        // ========== ГЛОБАЛЬНЫЕ переменные =============
        public int method_N;
        public int height_start;
        public int exp_count;
        //public int prob_num;

        //===============================================

        // ЗАКРЫТИЕ ФОРМЫ
        private void button_cross_Click(object sender, EventArgs e)
        {
            // Обеспечивает закрытие формы с матрицей
            form5_analyst_report form = this.Owner as form5_analyst_report;
            form.matr_count--;

            switch (method_N)
            {
                case 0:
                {
                    form.matr0_btnON = true;
                    form.btn_matrix0.Cursor = Cursors.Hand;
                    break;
                }
                case 1:
                {
                    form.matr1_btnON = true;
                    form.btn_matrix1.Cursor = Cursors.Hand;
                    break;
                }
                case 2:
                {
                    form.matr2_btnON = true;
                    form.btn_matrix2.Cursor = Cursors.Hand;
                    break;
                }
                case 3:
                {
                    form.matr3_btnON = true;
                    form.btn_matrix3.Cursor = Cursors.Hand;
                    break;
                }
                case 4:
                {
                    form.matr4_btnON = true;
                    form.btn_matrix4.Cursor = Cursors.Hand;
                    break;
                }
                default:
                    break;
            }

            this.Close();
        }

        // перемещение формы по экрану
        private void form6_matrix_MouseDown(object sender, MouseEventArgs e)
        {
            base.Capture = false;
            Message m = Message.Create(base.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }

        // при ЗАГРУЗКЕ ФОРМЫ   
        private void form6_matrix_Load(object sender, EventArgs e)
        {
            form5_analyst_report form = this.Owner as form5_analyst_report;
            exp_count = form.prob_list[form.index_prob].m0.inf.Count();

            switch (method_N)
            {
                case 0:
                    {
                        comboBox_exp.Visible = true;
                        height_start = 100;
                        f6list_exp = new List<exp2>(exp_count);
                        exp2 tmp;
                        for (int i = 0; i < exp_count; i++)
                        {
                            if (form.prob_list[form.index_prob].m0.inf[form.index_exp].status == 1)
                            {
                                tmp = new exp2();
                                tmp.id_exp = form.prob_list[form.index_prob].m0.inf[form.index_exp].id_exp;
                                //tmp.fio = form.prob_list[form.index_prob].m0.inf[form.index_exp]
                            }
                        }
                        break;
                    }

                case 2:
                    {
                        comboBox_exp.Visible = false;
                        height_start = 60;
                        break;
                    }

                case 4:
                    {
                        comboBox_exp.Visible = true;
                        height_start = 100;
                        break;
                    }
            }

                




                   // dataGridView1.DataSource = form5_analyst_report.matrix; // выводим матрицу в dataGridView

                    foreach (DataGridViewColumn column in dataGridView1.Columns)
                    {
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }

                    //ширина столбцов
                    for (int i = 0; i < form.alter_count; i++)
                    {
                        dataGridView1.Columns[i].Width = 35;
                    }

        }
    }
}
