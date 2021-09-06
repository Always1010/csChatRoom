﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client
{  
    public partial class Form1 : Form
    {
        public Form1()
        {   
            Form1.boo = -1;
            InitializeComponent();
        }
        bool register=false;
        public static int boo;
        public static string account;


        private void button1_Click(object sender, EventArgs e)
        {
           
 //           System.Diagnostics.Debug.WriteLine("2:" + boo + "     "+Thread.CurrentThread.ManagedThreadId.ToString());
            string account_number = textBox1.Text;
            string password = textBox2.Text;
            if (account_number == "")
            {
                MessageBox.Show("请输入用户名", "提示");
                return;
            }
            if (password == "")
            {
                MessageBox.Show("请输入密码", "提示");
                return;
            }
            if (!register)
            {
                if (!File.Exists("D:\\" + account_number + ".txt"))
                {
                    _ = MessageBox.Show("该账号不存在！！！", "提示");
                }
                else
                {
                    FileStream fs = new FileStream("D:\\" + account_number + ".txt", FileMode.Open, FileAccess.Read);
                    StreamReader sr = new StreamReader(fs);
                    if (password == sr.ReadLine())
                    {
                        _ = MessageBox.Show("登陆成功", "提示");
                        boo = 1;
                        System.Diagnostics.Debug.WriteLine("3:" + boo);
                        Form1.account = account_number;
                        this.Close();
                        
                    }
                    else
                        _ = MessageBox.Show("账号或密码错误", "提示");
                    ;//开始写入值
                    sr.Close();
                    fs.Close();
                }
            }
            else
            {
                if (!File.Exists("D:\\" + account_number + ".txt"))
                {
                    FileStream fs1 = new FileStream("D:\\" + account_number + ".txt", FileMode.Create, FileAccess.Write);//创建写入文件
                    StreamWriter sw = new StreamWriter(fs1);
                    sw.WriteLine(password);//开始写入值
                    sw.Close();
                    fs1.Close();
                    _ = MessageBox.Show("注册成功", "提示");
                }
                else
                {
                    MessageBox.Show("该账号已存在！！！", "提示");
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            if (!register)
            {
                label3.Text = "已有帐号？";
                button1.Text = "注册";
                button2.Text = "选择登录";
                register = true;
            }
            else
            {
                label3.Text = "没有帐号？";
                button1.Text = "登录";
                button2.Text = "选择注册";
                register = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox2.PasswordChar != '\0')
            {
                textBox2.PasswordChar = '\0';
                button3.Text = "隐藏密码";
            }
            else
            {
                textBox2.PasswordChar = '*';
                button3.Text = "显示密码";
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
