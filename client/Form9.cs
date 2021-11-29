using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client
{
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.BackColor = Color.Red;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            this.BackColor = Color.Green;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            this.BackColor = Color.Blue;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            label3.Text = "你喜欢的颜色：";
            foreach (var a in groupBox2.Controls)
                if(a is CheckBox)
                    if (((CheckBox) a).Checked)
                {
                        label3.Text += ((CheckBox)a).Text + ' ';
                }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            label3.Text = "你喜欢的颜色：";
            foreach (var a in groupBox2.Controls)
                if (a is CheckBox)
                    if (((CheckBox)a).Checked)
                    {
                        label3.Text += ((CheckBox)a).Text + ' ';
                    }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            label3.Text = "你喜欢的颜色：";
            foreach (var a in groupBox2.Controls)
                if (a is CheckBox)
                    if (((CheckBox)a).Checked)
                    {
                        label3.Text += ((CheckBox)a).Text + ' ';
                    }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            label3.Text = "你喜欢的颜色：";
            foreach (var a in groupBox2.Controls)
                if (a is CheckBox)
                    if (((CheckBox)a).Checked)
                    {
                        label3.Text += ((CheckBox)a).Text + ' ';
                    }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            label3.Text = "你喜欢的颜色：";
            foreach (var a in groupBox2.Controls)
                if (a is CheckBox)
                    if (((CheckBox)a).Checked)
                    {
                        label3.Text += ((CheckBox)a).Text + ' ';
                    }
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            label3.Text = "你喜欢的颜色：";
            foreach (var a in groupBox2.Controls)
                if (a is CheckBox)
                    if (((CheckBox)a).Checked)
                    {
                        label3.Text += ((CheckBox)a).Text + ' ';
                    }
        }
    }
}
