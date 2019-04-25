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
    public partial class Form_lsolution_edit : Form
    {
        public Form_lsolution_edit()
        {
            InitializeComponent();
        }

        private void btn_edit_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form_lsolution_edit_Load(object sender, EventArgs e)
        {
            form3_analyst_add owner = this.Owner as form3_analyst_add;

            if (owner != null)
            {
                this.TopMost = true; this.TopMost = false;
                this.textBox1.Text = Convert.ToString(owner.list_solution.Items[owner.list_solution.SelectedIndex]);

            }
        }

        private void btn_edit_ok_Click(object sender, EventArgs e)
        {
            form3_analyst_add owner = this.Owner as form3_analyst_add;
            if (owner != null)
            {
                owner.TopMost = true; owner.TopMost = false;
                owner.list_solution.Items[owner.list_solution.SelectedIndex] = this.textBox1.Text;
                this.Close();
            }
        }
    }
}
