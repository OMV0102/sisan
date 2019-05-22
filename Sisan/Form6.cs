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

        private void button_cross_Click(object sender, EventArgs e)
        {
            // Обеспечивает закрытие формы с матрицей
            form5_analyst_report owner = this.Owner as form5_analyst_report;
            if (owner != null)
            {
                owner.TopMost = true; owner.TopMost = false;
                owner.Show();
                this.Close();
            }
        }

        private void form6_matrix_MouseDown(object sender, MouseEventArgs e)
        {
            // обеспечивает перемещение формы без рамки
            base.Capture = false;
            Message m = Message.Create(base.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }

        private void form6_matrix_Load(object sender, EventArgs e)
        {
            form5_analyst_report owner = this.Owner as form5_analyst_report;
            if (owner != null)
            {
                this.TopMost = true; this.TopMost = false;




                   // dataGridView1.DataSource = form5_analyst_report.matrix; // выводим матрицу в dataGridView

                    foreach (DataGridViewColumn column in dataGridView1.Columns)
                    {
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }

                    //ширина столбцов
                    /*for (int i = 0; i < form5_analyst_report.count; i++)
                    {
                        dataGridView1.Columns[i].Width = 35;
                    }*/


                
            }
        }
    }
}
