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
    public partial class Form4 : Form
    {
        Timer t = new Timer();
        string str = "";
        String DirPath = "";
        public Form4()
        {
            t.Start();
            Form1.boo = -1;
            InitializeComponent();
            this.BackgroundImage = Image.FromFile("../../res/backImg_2.jpg");
            panel1.BackColor = Color.FromArgb(150, 204, 212, 230);
            panel2.BackColor = Color.FromArgb(150, 204, 212, 230);
            panel3.BackColor = Color.FromArgb(150, 204, 212, 230);
            panel4.BackColor = Color.FromArgb(150, 204, 212, 230);
            panel5.BackColor = Color.FromArgb(150, 204, 212, 230);
            panel6.BackColor = Color.FromArgb(150, 204, 212, 230);
            panel7.BackColor = Color.FromArgb(150, 204, 212, 230);

            
            System.Timers.Timer timersTimer = new System.Timers.Timer();
            timersTimer.Interval = 1000;
            timersTimer.Enabled = true;
            timersTimer.Elapsed += progressbar_up;
            timersTimer.Start();

            progressBar1.Maximum = 100;
            progressBar1.Minimum = 0;                    
            progressBar1.Value = 0;     

        }

        private void progressbar_up(object sender, System.Timers.ElapsedEventArgs e)
        {
            
                this.BeginInvoke(new Action(() =>
                {
                    
                    if (this.progressBar1.Value == 100)
                        this.progressBar1.Value = 0;
                    this.progressBar1.Value++;
                    this.time_output.Text = "现在时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }), null);


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Mousemove(object sender, EventArgs e)
        {
            string gen="";
            if (radioButton2.Checked)
                gen = radioButton2.Text;
            if (radioButton1.Checked)
                gen = radioButton1.Text;
            
            

            str = "姓名：" + textBox2.Text + "\n性别：" + gen + "\n专业：" + comboBox1.Text + "\n生日：" + dateTimePicker1.Text + "\n爱好：";

            List<string> strList = new List<string>();
            if (checkBox1.Checked)
                str+=checkBox1.Text+"  ";
            if (checkBox2.Checked)
                str += checkBox1.Text + "  ";
            if (checkBox3.Checked)
                str+=checkBox3.Text+"  ";
            if (checkBox4.Checked)
                str += checkBox4.Text + "  ";

            //MessageBox.Show(str, "学生信息");
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                DirPath = f.FileName;
                this.pictureBox1.BackgroundImage = Image.FromFile(DirPath);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form6 f6 = new Form6();
            f6.label1.Text = str;
            f6.panel1.BackgroundImage = Image.FromFile(DirPath);
            f6.ShowDialog();

        }
    }
}

