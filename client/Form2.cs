using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        static string downloadPath = "";
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
            comboBox2.Items.Add("all");
            comboBox2.SelectedIndex = 0;
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
            if (comboBox2.SelectedIndex < 0 || comboBox2.SelectedIndex == comboBox2.Items.Count - 1)
            {
                MessageBox.Show("请在列表中选择好友或all", "提示");
                return;
            }
            string ob = comboBox2.SelectedItem.ToString().Trim();
            //if (ob == "全部")
            //{
                try
                {
                    string data = "$a" + '|' + Form1.account + ":" + textSend.Text + "\r\n" + '|'+ob+'|';
                    byte[] massage = System.Text.Encoding.UTF8.GetBytes(data);
                    tcpClient.Send(massage);
                }
                catch (Exception)
                {
                    MessageBox.Show("发送失败", "提示");
                    return;
                }
            if(ob!="all")
            {
                this.chatBox.Invoke(new Action(() =>
                {
                    this.chatBox.AppendText("(私聊@"+ob+")" + Form1.account + ":" + textSend.Text);
                }));
            }
            //}
            //else
            //{
            //    try
            //    {
            //        string data = "$private" + '|' + Form1.account + ":" + textSend.Text + "\r\n" + '|'+ob+'|';
            //        byte[] massage = System.Text.Encoding.UTF8.GetBytes(data);
            //        tcpClient.Send(massage);
            //    }
            //    catch (Exception)
            //    {
            //        MessageBox.Show("发送失败", "提示");
            //        return;
            //    }
            //}
            
            
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
                   
                    Byte[] msg_receive = new Byte[1024*2];
                    String data = null;
                    int length = -1;
                    try
                    {
                        length = tcpClient.Receive(msg_receive); 
                    }
                    catch (SocketException)
                    {
                        return;
                    }
                    catch (Exception)
                    {
                        return;
                    }
 //                   ShowMsg(strMsg);
                    //                    stream.Read(msg_receive, 0, msg_receive.Length);
                    data = System.Text.Encoding.UTF8.GetString(msg_receive, 0, msg_receive.Length);
                    string[] spl = data.Split('|');

					if (spl[0] == "$xx")
                    {
                        MessageBox.Show("该用户名已被其它用户使用，请更换账号", "警告");
                        Form1.boo = 0;
                        tcpClient.Close();
                        this.Invoke(new Action(() =>
                        {
                            this.Close();
                        }));
                        break;
                    }

                    if (spl[0] == "$a")
                    {
                        this.chatBox.Invoke(new Action(() =>
                        {
                            if(spl[2]!="all") this.chatBox.AppendText("(私聊)");
                            this.chatBox.AppendText(spl[1]);
                        }));
                    }

                    //更新在线用户列表
                    if (spl[0] == "$b")
                    {
                        this.listBox1.Invoke(new Action(() =>
                        {
                            listBox1.Items.Clear();
                            comboBox2.Items.Clear();
                            comboBox2.Items.Add("all");
                        }));
                        foreach (var a in spl)
                        {
                            a.Trim();
                            System.Diagnostics.Debug.WriteLine(a);
                            System.Diagnostics.Debug.WriteLine(a.Length);
                            if (a != "$b")
                                this.listBox1.Invoke(new Action(() =>
                                {
                                    listBox1.Items.Add(a);
                                    if (a != Form1.account)
                                    {
                                        comboBox2.Items.Add(a);
                                    }
                                   
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

                    if (spl[0] == "$v")
                    {

                        MessageBox.Show(spl[2], "来自对手的提示");
                        
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
                        
                        byte[] buffer = new byte[1024 * 2];
                        string fileName = spl[1];
                        long filelength = Convert.ToInt64(spl[2]);
                        long receive = 0L;
                        //string path = "D:/fileshare/";
                        using (FileStream writer = new FileStream(Path.Combine(downloadPath, fileName), FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            int received;
                            while (receive < filelength)
                            {
                                received = tcpClient.Receive(buffer);
                                writer.Write(buffer, 0, received);
                                writer.Flush();
                                receive += (long)received;
                            }
                        }
                        this.chatBox.Invoke(new Action(() =>
                        {
                            this.chatBox.AppendText("download success...\r\n");
                        }));
                    }
                    if (spl[0] == "$getFilelist")
                    {
                        this.comboBox_filelist.Invoke(new Action(() =>
                        {
                            comboBox_filelist.Items.Clear();
                        }));
                        foreach (var a in spl)
                        {
                            a.Trim();
                            if (a != "$getFilelist")
                                this.comboBox_filelist.Invoke(new Action(() =>
                                {
                                    comboBox_filelist.Items.Add(a);
                                }));
                        }
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
            if (listBox1.SelectedIndex<0|| listBox1.SelectedIndex==listBox1.Items.Count-1)
            {
                MessageBox.Show("请在在线列表中选择一个好友", "提示");
                return;
            }
            friendname = listBox1.SelectedItems[0].ToString().Trim();
            if (friendname == Form1.account)
            {
                MessageBox.Show("请选择好友！", "提示");
                return;
            }

                try
                {
                    string data = "$s" + '|' + friendname + '|' + Form1.account + '|';
                    byte[] massage = System.Text.Encoding.UTF8.GetBytes(data);
                    tcpClient.Send(massage);
                }
                catch (Exception)
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
            catch (Exception)
            {
                MessageBox.Show("连接错误", "提示");
                return false;
            }
            return true;
        }


        static public bool sendM(string msg)
        {
            try
            {
                string data = "$v" + '|' + friendname + '|' +msg+ '|';
                byte[] massage = System.Text.Encoding.UTF8.GetBytes(data);
                tcpClient.Send(massage);
            }
            catch (Exception)
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
            catch (Exception)
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
            catch (Exception)
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
            catch (Exception)
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
                if (string.IsNullOrEmpty(ofd.FileName))
                {
                    MessageBox.Show("请选择要发送的文件！！！");
                }
                else
                {
                    // 用文件流打开用户要发送的文件；
                    //using (System.IO.FileStream fs = new FileStream(ofd.FileName, FileMode.Open))
                    //{
                    //    //在发送文件以前先给好友发送这个文件的名字+扩展名，方便后面的保存操作；
                    //    string fileName = System.IO.Path.GetFileName(ofd.FileName);
                    //    string fileExtension = System.IO.Path.GetExtension(ofd.FileName);
                    //    string strMsg = "$f" + '|'+fileName + '|';
                    //    byte[] arrMsg = System.Text.Encoding.UTF8.GetBytes(strMsg);
                    //    //byte[] arrSendMsg = new byte[arrMsg.Length + 1];
                    //    //arrSendMsg[0] = 0; // 用来表示发送的是消息数据
                    //    //Buffer.BlockCopy(arrMsg, 0, arrSendMsg, 1, arrMsg.Length);
                    //    tcpClient.Send(arrMsg); // 发送消息；


                    //    byte[] arrFile = new byte[1024 * 1024 * 2];
                    //    int length = fs.Read(arrFile, 0, arrFile.Length);  // 将文件中的数据读到arrFile数组中；
                    //    byte[] arrFileSend = new byte[length + 1];
                    //    arrFileSend[0] = 1; // 用来表示发送的是文件数据；
                    //    Buffer.BlockCopy(arrFile, 0, arrFileSend, 1, length);
                    //    // 还有一个 CopyTo的方法，但是在这里不适合； 当然还可以用for循环自己转化；
                    //    tcpClient.Send(arrFileSend);// 发送数据到服务端；
                    //    //txtSelectFile.Clear();
                    //}

                    string path = ofd.FileName;
                    using (FileStream reader = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None))
                    {
                        long send = 0L, length = reader.Length;
                        string sendStr = "$f" +'|' + Path.GetFileName(path) + '|' + length.ToString();

                        string fileName = Path.GetFileName(path);
                        tcpClient.Send(System.Text.Encoding.UTF8.GetBytes(sendStr));

                        int BufferSize = 1024*2;
                        //byte[] buffer = new byte[32];
                        //tcpClient.Receive(buffer);
                        //string mes = Encoding.Default.GetString(buffer);

                       
                            byte[] fileBuffer = new byte[BufferSize];
                            int read, sent;
                            while ((read = reader.Read(fileBuffer, 0, BufferSize)) != 0)
                            {
                                sent = 0;
                                while ((sent += tcpClient.Send(fileBuffer, sent, read, SocketFlags.None)) < read)
                                {
                                    send += (long)sent;
                                }
                            }
                            
                    
                    }
                }
            }
        }

        private void comboBox_filelist_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox_filelist_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                string data = "$getFilelist" + '|';
                byte[] massage = System.Text.Encoding.UTF8.GetBytes(data);
                tcpClient.Send(massage);
            }
            catch (Exception)
            {
                MessageBox.Show("连接错误", "提示");
            }
        }

        private void download_Click(object sender, EventArgs e)
        {
            if (comboBox_filelist.SelectedIndex < 0 || comboBox_filelist.SelectedIndex ==comboBox_filelist.Items.Count - 1)
            {
                MessageBox.Show("请在共享文件列表中选择一个文件", "提示");
                return;
            }
            string filename = comboBox_filelist.SelectedItem.ToString().Trim();
            SaveFileDialog sfd = new SaveFileDialog();
            //if (sfd.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            //    String downloadPath = sfd.FileName;
            FolderBrowserDialog folder = new FolderBrowserDialog();
            
            if (folder.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                downloadPath = folder.SelectedPath;
            }
            else
            {
                return;
            }
            if(downloadPath=="")
            {
                MessageBox.Show("文件路径选择不能为空", "提示");
                return;
            }
            try
            {
                string data = "$download" + '|'+filename+'|';
                byte[] massage = System.Text.Encoding.UTF8.GetBytes(data);
                tcpClient.Send(massage);
            }
            catch (Exception)
            {
                MessageBox.Show("请求失败", "提示");
                return;
            }
            
        }
    }
}


