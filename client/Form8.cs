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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                string str = string.Empty;
                
                listBox1.Items.Add("书名：" + textBox2.Text + "\t出版社：" + comboBox1.SelectedItem.ToString()  );
            }
            catch
            {
                MessageBox.Show("请将信息填完整");
                return;
            }

        }

        private void Form8_Load(object sender, EventArgs e)
        {
            
                comboBox1.Items.Add("清华大学出版社");
                comboBox1.Items.Add("北京大学出版社");
                comboBox1.Items.Add("上海交通大学出版社");
                comboBox1.Items.Add("中国铁道出版社");
            

        }
    }
}
