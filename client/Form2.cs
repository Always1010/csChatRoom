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
        
        Socket tcpClient = null;

        public Form2()
        {
            Form1.boo = -1;
            tcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            InitializeComponent();
//            IP.Text = "127.0.0.1";
            Port.Text = "10086";
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
                tcpClient.Connect(IP.Text, Convert.ToInt32(Port.Text));
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }
            if (tcpClient.Connected)
            {
                connect_state.Text = "已连接";

                MessageBox.Show("连接成功", "提示");
                //               stream = tcpClient.GetStream();
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
                string data = Form1.account + ":" + textSend.Text+"\r\n";
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
                   
                    Byte[] msg_receive = new Byte[1024];
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
                    //                    chatBox.AppendText(data);
                    this.chatBox.Invoke(new Action(() =>
                    {
                        this.chatBox.AppendText(data);
                    }));
 //                   System.Diagnostics.Debug.WriteLine(".................");
                }
                catch
                {
//                    System.Diagnostics.Debug.WriteLine("ee.................");
                }
            }
        }

        private void textSend_TextChanged(object sender, EventArgs e)
        {

        }
    }
}


