using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace Server
{

    public partial class Form1 : Form
    {
        IPEndPoint listenPort = null;

        Thread threadWatch = null;
        Socket socket = null;
        Dictionary<string, Socket> sockets = new Dictionary<string, Socket>();
        Dictionary<string, Thread> dictThread = new Dictionary<string, Thread>();
        Dictionary<string, string> users = new Dictionary<string, string>();

        public Form1()
        {
            InitializeComponent();
            Port.Text = "10086";
            //System.Diagnostics.Debug.WriteLine("jjjjj................." + Thread.CurrentThread.ManagedThreadId.ToString());
        }

        private void Clean_Click(object sender, EventArgs e)
        {
            ChatBox.Text = "";
        }

        private void Start_Click(object sender, EventArgs e)
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            int port = Convert.ToInt32(Port.Text);
            listenPort = new IPEndPoint(IPAddress.Any, port);
            socket.Bind(listenPort);
            socket.Listen(10000);
            threadWatch = new Thread(Listen);
            threadWatch.IsBackground = true;
            threadWatch.Start();
            Start.Enabled = false;
        }
        public void Listen()
        {
            //System.Diagnostics.Debug.WriteLine("jjjjj.！....." + Thread.CurrentThread.ManagedThreadId);
            while (true)
            {
                Socket sokConnection = socket.Accept();
                //this.listBox1.Invoke(new Action(() =>
                //{
                //    listBox1.Items.Add(sokConnection.RemoteEndPoint.ToString());
                //}));
                this.listBox1.Invoke(new Action(() =>
                {
                    sokConnection.RemoteEndPoint.ToString();
                }));
                sockets.Add(sokConnection.RemoteEndPoint.ToString(), sokConnection);

               
                //System.Diagnostics.Debug.WriteLine("jjjjj.客户端连接成功！....." + Thread.CurrentThread.Name);
                
                //foreach (Socket s in sockets.Values)
                //{
                //    byte[] massage = System.Text.Encoding.UTF8.GetBytes("$b"+'|'+sokConnection.RemoteEndPoint.ToString()+'|');
                //    s.Send(massage);
                //}


                Thread thr = new Thread(msgRec);
                thr.IsBackground = true;
                thr.Start(sokConnection);
                dictThread.Add(sokConnection.RemoteEndPoint.ToString(), thr);
            }
        }

        public void msgRec(object sokConnectionparn)
        {
            Socket sokClient = sokConnectionparn as Socket;
            while (true)
            {
                byte[] arrMsgRec = new byte[1024*2];
                int length = -1;
                try
                {
                    length = sokClient.Receive(arrMsgRec);
                }
                catch (SocketException se)
                {
                    sockets.Remove(sokClient.RemoteEndPoint.ToString());
                    dictThread.Remove(sokClient.RemoteEndPoint.ToString());
                    deleteUser(sokClient.RemoteEndPoint.ToString());
                    updateUsers();
                    break;
                }
                catch (Exception e)
                {
                    sockets.Remove(sokClient.RemoteEndPoint.ToString());
                    dictThread.Remove(sokClient.RemoteEndPoint.ToString());
                    deleteUser(sokClient.RemoteEndPoint.ToString());
                    updateUsers();
                    break;
                }

                if (arrMsgRec != null)
                {
                    string strMsg = System.Text.Encoding.UTF8.GetString(arrMsgRec, 0, length);
                    string[] spl = strMsg.Split('|');

                    //待优化
                    //群发消息
                    if (spl[0] == "$a")
                    {
                        this.ChatBox.Invoke(new Action(() =>
                        {
                            this.ChatBox.AppendText(spl[1]);
                        }));

                        if (spl[2] == "all")
                        {
                            foreach (Socket s in sockets.Values)
                            {
                                s.Send(arrMsgRec);
                            }
                        }
                        else
                        {
                            sockets[users[spl[2]]].Send(arrMsgRec);
                        }
                    }


                    //接入用户
                    if (spl[0] == "$b")
                    {
                        try
                        {
                            users.Add(spl[1], sokClient.RemoteEndPoint.ToString());
                        }
                        catch(Exception e)
                        {
                            byte[] massage = System.Text.Encoding.UTF8.GetBytes("$xx" + '|');
                            sockets[sokClient.RemoteEndPoint.ToString()].Send(massage);
                        }
                        this.listBox1.Invoke(new Action(() =>
                        {
                            listBox1.Items.Add(spl[1]);
                        }));
                        updateUsers();
                    }


                    //下棋
                    if (spl[0] == "$c" )
                    {
                        //foreach(var e in sockets)
                        //{
                        //    System.Diagnostics.Debug.WriteLine(e.Key);
                        //}
                        //System.Diagnostics.Debug.WriteLine(spl[1]);
                        sockets[users[spl[1]]].Send(arrMsgRec);

                    }


                    //发起五子棋连接
                    if (spl[0] == "$s")
                    {
                        //string data = "$s" + '|'  +  + '|';
                        //arrMsgRec = System.Text.Encoding.UTF8.GetBytes(data);
                        sockets[users[spl[1]]].Send(arrMsgRec);
                    }

                    if (spl[0] == "$g")
                    {
                        sockets[users[spl[1]]].Send(arrMsgRec);
                    }

                    //双方准备
                    if (spl[0] == "$z"||spl[0]=="$v")
                    {
                        sockets[users[spl[1]]].Send(arrMsgRec);
                    }

                    if (spl[0] == "$r")
                    {
                        sockets[users[spl[1]]].Send(arrMsgRec);
                    }


                    //文件接收
                    if (spl[0] == "$f")
                    {
                        
                        byte[] buffer = new byte[1024 * 2];
                        string fileName = spl[1];
                        long filelength = Convert.ToInt64(spl[2]);
                        long receive = 0L;
                        string path = "D:/fileshare/";
                        using (FileStream writer = new FileStream(Path.Combine(path, fileName), FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            int received;
                            while (receive < filelength)
                            {
                                received = sokClient.Receive(buffer);
                                writer.Write(buffer, 0, received);
                                writer.Flush();
                                receive += (long)received;
                            }
                        }
                        this.ChatBox.Invoke(new Action(() =>
                        {
                            this.ChatBox.AppendText("someone share a file...");
                        }));
                        string data = "$a" + '|' + "someone share a file..." + '|' +"all"+'|';
                        byte[] arrMsg = System.Text.Encoding.UTF8.GetBytes(data);
                        foreach (Socket s in sockets.Values)
                        {
                            s.Send(arrMsg);
                        }

                    }

                    if (spl[0] == "$getFilelist")
                    {
                        String path = @"D:\fileshare";
                        var files = Directory.GetFiles(path, "*");
                        String msg = "$getFilelist" + '|';
                        foreach (var file in files)
                            msg = msg + Path.GetFileName(file) + '|';
                        byte[] massage = System.Text.Encoding.UTF8.GetBytes(msg);
                        sokClient.Send(massage);
                    }


                    if (spl[0] == "$download")
                    {
                        
                        String path = @"D:\fileshare\"+spl[1];

                        //string path = ofd.FileName;
                        using (FileStream reader = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None))
                        {
                            long send = 0L;
                            long filelength = reader.Length;
                            string sendStr = "$f" + '|' + Path.GetFileName(path) + '|' + filelength.ToString();

                            string fileName = Path.GetFileName(path);
                            sokClient.Send(System.Text.Encoding.UTF8.GetBytes(sendStr));

                            int BufferSize = 1024 * 2;
                            //byte[] buffer = new byte[32];
                            //tcpClient.Receive(buffer);
                            //string mes = Encoding.Default.GetString(buffer);


                            byte[] fileBuffer = new byte[BufferSize];
                            int read, sent;
                            while ((read = reader.Read(fileBuffer, 0, BufferSize)) != 0)
                            {
                                sent = 0;
                                while ((sent += sokClient.Send(fileBuffer, sent, read, SocketFlags.None)) < read)
                                {
                                    send += (long)sent;
                                }
                            }


                        }

                    }

                    //this.ChatBox.Invoke(new Action(() =>
                    //{
                    //    this.ChatBox.AppendText(strMsg);
                    //}));

                    //if (length != -1)
                    //    foreach (Socket s in sockets.Values)
                    //    {
                    //        s.Send(arrMsgRec);
                    //    }
                }
            }

        }

        
        public void deleteUser(string str)
        {
            
            string strr = "";
            foreach (var item in users)
            {
                if (item.Value == str)
                {
                    strr = item.Key;
                    break;
                }
                
            }
            this.listBox1.Invoke(new Action(() =>
            {
                listBox1.Items.Remove(strr);
            }));
            users.Remove(strr);
        }

        public string getUsers()
        {
            string strr = "$b"+'|';
            foreach (var item in users)
            {
                strr += item.Key + '|';
            }
            //strr += "E";
            return strr;
        }


        //更新在线用户名单
        public void updateUsers()
        {
            string usersName = getUsers();
            byte[] data = System.Text.Encoding.UTF8.GetBytes(usersName);
            foreach (Socket s in sockets.Values)
            {
                s.Send(data);
            }
        }
    }
}
