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

        // ЗАКРЫТИЕ ФОРМЫ
        private void button_cross_Click(object sender, EventArgs e)
        {
            // Обеспечивает закрытие формы с матрицей
            form5_analyst_report form5 = this.Owner as form5_analyst_report;
            //form5.Show();
            //form5.TopMost = true; form5.TopMost = false;
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
            form5_analyst_report form5 = this.Owner as form5_analyst_report;
            if (form5 != null)
            {
                this.TopMost = true; this.TopMost = false;
                




                   // dataGridView1.DataSource = form5_analyst_report.matrix; // выводим матрицу в dataGridView

                    foreach (DataGridViewColumn column in dataGridView1.Columns)
                    {
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }

                    //ширина столбцов
                    for (int i = 0; i < form5.alter_count; i++)
                    {
                        dataGridView1.Columns[i].Width = 35;
                    }


                
            }
        }
    }
}
