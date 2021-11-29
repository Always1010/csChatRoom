using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace client
{
    public partial class Form2 : Form
    {
        
        static Socket tcpClient = null;
        public static string friendname;
        static public Form10 f10;
        public Form2()
        {
            Form1.boo = -1;
            tcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            InitializeComponent();
            IP.Text = "127.0.0.1";
            Port.Text = "10086";
            this.BackgroundImage = Image.FromFile("../../res/backImg2.jpg");
            panel1.BackColor = Color.FromArgb(150, 204, 212, 230);
            panel2.BackColor = Color.FromArgb(150, 204, 212, 230);
            panel3.BackColor = Color.FromArgb(150, 204, 212, 230);
            label8.Text = Form1.account;
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            chatBox.Text = "";
        }

        private void appExit_Click(object sender, EventArgs e)
        {
            Form1.boo = 0;
            tcpClient.Close();
            this.Close();
        }

        private void Connect_Click(object sender, EventArgs e)
        {
            
            try
            {
                //Form1.account = textBox1.Text;
                tcpClient.Connect(IP.Text, Convert.ToInt32(Port.Text));
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }
            if (tcpClient.Connected)
            {
                connect_state.Text = "已连接";


                string data = "$b" + '|' + Form1.account+'|' ;
                byte[] massage = System.Text.Encoding.UTF8.GetBytes(data);
                tcpClient.Send(massage);


                //MessageBox.Show("连接成功", "提示");

                Connect.Enabled = false;
                Thread thread = new Thread(Receive);
                thread.IsBackground = true;
                thread.Start();
            }
            else
                MessageBox.Show("连接错误", "警告");
        }

        private void Send_Click(object sender, EventArgs e)
        {
            try {
                string data ="$a"+'|'+ Form1.account + ":" + textSend.Text+"\r\n"+'|';
                byte[] massage = System.Text.Encoding.UTF8.GetBytes(data);
                tcpClient.Send(massage);
            }
            catch (Exception error)
            {
                MessageBox.Show("发送失败", "提示");
                return;
            }
            
//            stream.Write(massage, 0, massage.Length);
//            System.Diagnostics.Debug.WriteLine("a.................");
            textSend.Text = "";
//            System.Diagnostics.Debug.WriteLine("a.................");
//            stream.Close();
//            System.Diagnostics.Debug.WriteLine("a.................");
        }
        public void Receive()
        {
//            TcpClient tcp = _tcpClient as TcpClient;
 //           tcp.ReceiveTimeout
            while (true)
            {
                try
                {
                   
                    Byte[] msg_receive = new Byte[1024*1024*2];
                    String data = null;
                    int length = -1;
                    try
                    {
                        length = tcpClient.Receive(msg_receive); 
                    }
                    catch (SocketException se)
                    {
                        return;
                    }
                    catch (Exception e)
                    {
                        return;
                    }
 //                   ShowMsg(strMsg);
                    //                    stream.Read(msg_receive, 0, msg_receive.Length);
                    data = System.Text.Encoding.UTF8.GetString(msg_receive, 0, msg_receive.Length);
                    string[] spl = data.Split('|');


                    if (spl[0] == "$a")
                    {
                        this.chatBox.Invoke(new Action(() =>
                        {
                            this.chatBox.AppendText(spl[1]);
                        }));
                    }

                    //更新在线用户列表
                    if (spl[0] == "$b")
                    {
                        this.listBox1.Invoke(new Action(() =>
                        {
                            listBox1.Items.Clear();
                        }));
                        foreach (var a in spl)
                        {
                            if (a != "$b"&&a!=" "&&a!="\0")
                                this.listBox1.Invoke(new Action(() =>
                                {
                                    listBox1.Items.Add(a);
                                }));
                        }
                    }


                    if (spl[0] == "$r")
                    {
                        f10.Repentance();
                    }


                    if (spl[0] == "$c")
                    {
                        int x = Convert.ToInt32(spl[2]);
                        int y = Convert.ToInt32(spl[3]);
                        this.Invoke(new Action(() =>
                        {
                            f10.chess(x, y);
                        }));
                        
                    }

                    if (spl[0] == "$g")
                    {
                        this.Invoke(new Action(() =>
                        {
                            f10.button4.Enabled = false;
                        }));
                        f10.isEnd = true;
                        MessageBox.Show("对方已认输", "提示");
                    }

                        if (spl[0] == "$s")
                    {
                        
                        if (MessageBox.Show(spl[2] + "向你发起五子棋挑战，是否应战？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            friendname = spl[2];
                            this.Invoke(new Action(() =>
                            {
                                f10 = new Form10();
                                f10.host = false;
                                f10.isOwnRound = false;
                                f10.Show();
                            }));
                            
                        }

                    }
                    if (spl[0] == "$f")
                    {

                    }
                    if (spl[0] == "$z")
                    {
                        f10.isBegin1 = true;
                        this.Invoke(new Action(() =>
                        {
                            f10.start();
                        }));
                    }
                    //                   System.Diagnostics.Debug.WriteLine(".................");
                }
                catch
                {
//                    System.Diagnostics.Debug.WriteLine("ee.................");
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            friendname = listBox1.SelectedItems[0].ToString().Trim();
            try
            {
                string data = "$s" + '|' + friendname + '|'+ Form1.account+'|';
                byte[] massage = System.Text.Encoding.UTF8.GetBytes(data);
                tcpClient.Send(massage);
            }
            catch (Exception error)
            {
                MessageBox.Show("连接错误", "提示");
                return;
            }
            
            f10 = new Form10();
            f10.host = true;
            f10.isOwnRound = true;
            f10.Show();
        }

        static public bool sendP(int x,int y)
        {
            try
            {
                string data = "$c"+ '|' + friendname+ '|' + x + '|' + y+'|';
                byte[] massage = System.Text.Encoding.UTF8.GetBytes(data);
                tcpClient.Send(massage);
            }
            catch (Exception error)
            {
                MessageBox.Show("连接错误", "提示");
                return false;
            }
            return true;
        }

        static public bool sendStatus()
        {
            try
            {
                string data = "$z" + '|' + friendname + '|' ;
                byte[] massage = System.Text.Encoding.UTF8.GetBytes(data);
                tcpClient.Send(massage);
            }
            catch (Exception error)
            {
                MessageBox.Show("连接错误", "提示");
                return false;
            }
            return true;
        }
        static public void giveup()
        {
            try
            {
                string data = "$g" + '|' + friendname + '|';
                byte[] massage = System.Text.Encoding.UTF8.GetBytes(data);
                tcpClient.Send(massage);
            }
            catch (Exception error)
            {
                MessageBox.Show("连接错误", "提示");
            }
        }

        static public void re()
        {
            try
            {
                string data = "$r" + '|' + friendname + '|';
                byte[] massage = System.Text.Encoding.UTF8.GetBytes(data);
                tcpClient.Send(massage);
            }
            catch (Exception error)
            {
                MessageBox.Show("连接错误", "提示");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = "D:\\";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                
            }
        }
    }
}


