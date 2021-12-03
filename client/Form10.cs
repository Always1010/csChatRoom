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
    public partial class Form10 : Form
    {

        int unit = 40;//格子单位长度

        int ori = 25;//原点

        int tail = 585;//结束点

        int posi = 25;//用于绘制棋盘点位

        ///////////以上为棋盘基本数据//////////////

        int half = 20;
        //static int blackTime = 0;
        //static int whiteTime = 0;
        int[,] chessColor;//记录棋子颜色
        public bool isBlack;//黑白执棋
        public bool isBegin1;//准备游戏
        public bool isBegin2;//准备游戏
        public bool isBegin ;
        public bool[,] hasChess;//用于记录棋盘点位是否落子
        public bool isFirst;
        public bool isEnd;//比赛结束
        public Pen pen = new Pen(Color.Black, 2);//棋盘画笔黑，用于画棋盘
        
        public Point tailChessPosition;//上一颗黑棋
        public Point tailChessPosition2; //上一颗白棋
        public Point tailChessPosition3;
        public Point tailChessPosition4;
        public bool host;

        ////////////////////////////11.19标注，状态判断///////////////
        public bool isOwnRound;//是否自己回合
        System.Timers.Timer timersTimer;
        //orrPanel panel1;


        public Form10()
        {
            
            InitializeComponent();
            this.Show();
            //drawBoard();
            //isOwnRound = a;

            //isBlack = true;//黑白执棋
            //isBegin1 = false;
            //isBegin2 = false;
            //isBegin = false;
            //isEnd = false;
            //label2.Text = Form2.friendname;
            //label1.Text = "未准备";

            //button1.Enabled = false;
            //button4.Enabled = false;
            //button3.Enabled = false;
            //isFirst=false;

        //System.Timers.Timer timersTimer = new System.Timers.Timer();
        //timersTimer.Interval = 1000;
        //timersTimer.Enabled = true;
        //timersTimer.Elapsed += progressbar_up;
        //timersTimer.Start();

        //progressBar1.Maximum = 30;
        //progressBar1.Minimum = 0;
        //progressBar2.Maximum = 30;
        //progressBar2.Minimum = 0;
        //if(a)
        //{
        //    progressBar2.Value = 30;
        //}
        //else
        //    progressBar1.Value = 30;

    }
        //public void setpanel() 
        //{
        //    panel1 = new orrPanel();
        ////board.OnGameEnd += board_OnGameEnd;
        //    this.Controls.Add(panel1);
        //    panel1.Left = 10;
        //    panel1.Top = 10;
        ////}

        private void progressbar_up(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (!isEnd)
            {
                this.Invoke(new Action(() =>
            {

                if (isOwnRound)
                {
                    //this.progressBar2.Value = 30;
                    if (this.progressBar2.Value != 0)
                    {
                        this.progressBar2.Value--;
                        this.label4.Text = this.progressBar2.Value.ToString() + 's';
                    }
                }
                else
                {
                    if (this.progressBar1.Value != 0)
                    {
                        this.progressBar1.Value--;
                        this.label3.Text = this.progressBar1.Value.ToString() + 's';
                    }
                }

            }), null);


            }
            else
            {

                
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            start();
        }

        public bool chess(int x, int y)
        {
            if (hasChess[x, y]) return false;
            int cenX = ori + x * unit;
            int cenY = ori + y * unit;

            SolidBrush sb;
            Graphics g = panel1.CreateGraphics();
            if (isBlack == true)
            {
                sb = new SolidBrush(Color.Black);
                //blackTime = 0;
            }
            else
            {
                sb = new SolidBrush(Color.White);
                //whiteTime = 0;
            }
            if (isBlack) chessColor[x, y] = 1;
            else chessColor[x, y] = 2;
            // r = new (new Point(cenX - half +, cenY - half + 3), new Size(unit - 6, unit - 6));
            g.FillEllipse(sb, cenX-15, cenY-15 , 30, 30);
            hasChess[x, y] = true;
            if (isBlack)
            {
                if (tailChessPosition != null)
                    tailChessPosition3 = tailChessPosition;
                tailChessPosition = new Point(x, y);
            }
            if (!isBlack)
            {
                if (tailChessPosition2 != null)
                    tailChessPosition4 = tailChessPosition2;
                tailChessPosition2 = new Point(x, y);
            }
            if (isBlack) isEnd=endJudge(x, y, 0);
            else isEnd = endJudge(x, y, 1);
            if (isOwnRound && isEnd) 
            {
                MessageBox.Show("恭喜恭喜，你获胜了！", "恭喜");
            }
            if (!isOwnRound && isEnd)
            {
                MessageBox.Show("不要气馁，下次你一定可以打败他！", "很遗憾");
            }
            isBlack = !isBlack;
            isOwnRound = !isOwnRound;

            if (isOwnRound)
            {
                this.progressBar2.Value = 30;
                this.progressBar1.Value = 0;
                this.label4.Visible = true;
                this.label3.Visible = false;
                if (!isFirst)
                    button3.Enabled = true;
            }
            else
            {
                this.progressBar1.Value = 30;
                this.progressBar2.Value = 0;
                this.label3.Visible = true;
                this.label4.Visible = false;
                button3.Enabled = false;
            }


                return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            isBegin2 = true;
            Form2.sendStatus();
            start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button4.Enabled = false;
            isEnd = true;
            Form2.giveup();
        }

        //棋盘点击事件
        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (!isBegin)
            {
                MessageBox.Show("请先开始游戏！", "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (isEnd)
            {
                MessageBox.Show("比赛已结束！！！", "注意");
                return;
            }
            //isOwnRound = true;
            if (!isOwnRound)
                return;
            //button3.Enabled = button4.Enabled = button5.Enabled = true;

            //判断鼠标点击是否落在棋盘上
            if (e.X < ori - half || e.X > tail + half || e.Y < ori - half || e.Y > tail + half)
            {
                return;
            }

            int x = (e.X - ori) / unit;
            int y = (e.Y - ori) / unit;
            int leftX = (e.X - ori) % unit;
            int leftY = (e.Y - ori) % unit;
            if (leftX > unit / 2)
                x += 1;
            if (leftY > unit / 2)
                y += 1;
            
            if(!chess(x, y)) return;
            else
            {
                sendP(x, y);
            }
            //takeTurns();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Repentance();
            button3.Enabled = false;
            isFirst = true;
            Form2.re();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(textBox1.Text!=null&&textBox1.Text.Trim()!="")
                Form2.sendM(textBox1.Text);
            textBox1.Text = "";
        }




        //用于判断是否已经连成五子
        public bool endJudge(int x, int y, int chessState)
        {
            int i, j, k, color;
            if (chessState == 0) color = 1; else color = 2;


            //竖直遍历
            i = y;
            j = 1;
            while (i > 0 && chessColor[x, --i] == color) ++j;
            i = y;
            while (i < 14 && chessColor[x, ++i] == color) ++j;
            if(j>4) return true;


            //横排遍历
            i = x;
            j = 1;
            while (i > 0 && chessColor[--i, y] == color) ++j;
            i = x;
            while (i < 14 && chessColor[++i, y] == color) ++j;
            if (j > 4) return true;


            //左斜
            i = x;
            j = y;
            k = 1;
            while (i > 0 && j > 0 && chessColor[--i, --j] == color) ++k;
            i = x;
            j = y;
            while (i < 14 && j < 14 && chessColor[++i, ++j] == color) ++k;
            if (k > 4) return true;



            //右斜
            i = x;
            j = y;
            k = 1;
            while (i < 14 && j > 0 && chessColor[++i, --j] == color) ++k;
            i = x;
            j = y;
            while (i > 0 && j < 14 && chessColor[--i, ++j] == color) ++k;
            if (k > 4) return true;

            return false;
        }



        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
            //drawBoard();

        }



        



        private void Form10_Load(object sender, EventArgs e)
        {

            drawBoard();


            isBlack = true;//黑白执棋
            isBegin1 = false;
            isBegin2 = false;
            isBegin = false;
            isEnd = false;
            label2.Text = Form2.friendname;
            label1.Text = "未准备";

            button1.Enabled = false;
            button4.Enabled = false;
            button3.Enabled = false;
            isFirst = false;
            
        }



        private void drawBoard()
        {
            //Graphics GPS = panel1.CreateGraphics();
            //Pen MyPen = new Pen(Color.Black, 2f);
            //for (int i = 0; i < 15; i++)
            //{
            //    //用于画网格
            //    GPS.DrawLine(MyPen, posi, ori, posi, tail);
            //    GPS.DrawLine(MyPen, ori, posi, tail, posi);

            //    posi += unit;
            //}
            //GPS.Save();

            Bitmap bitm = new Bitmap(this.panel1.Width, this.panel1.Height);
            Graphics g = Graphics.FromImage(bitm);
            g.Clear(panel1.BackColor);


            Pen MyPen = new Pen(Color.Black, 2f);
            for (int i = 0; i < 15; i++)
            {
                //用于画网格
                g.DrawLine(MyPen, posi, ori, posi, tail);
                g.DrawLine(MyPen, ori, posi, tail, posi);

                posi += unit;
            }
            //g.DrawLine(p, 20, 20, 20, this.panel1.Height - 20);
            this.panel1.BackgroundImage = bitm;
            MyPen.Dispose();
            g.Dispose();
        }


        public void start()
        {
            if (isBegin1)
            {
                label1.Text = "已准备";
                label1.ForeColor = Color.Green;
            }
            if (isBegin1 && isBegin2)
            {
                isBegin = true;
                isEnd = false;

                button2.Enabled = false;
                button4.Enabled = true;
                button3.Enabled = true;

                System.Timers.Timer timersTimer = new System.Timers.Timer();
                timersTimer.Interval = 1000;
                timersTimer.Enabled = true;
                timersTimer.Elapsed += progressbar_up;
                timersTimer.Start();

                progressBar1.Maximum = 30;
                progressBar1.Minimum = 0;
                progressBar2.Maximum = 30;
                progressBar2.Minimum = 0;
                if (isOwnRound)
                {
                    progressBar2.Value = 30;
                }
                else
                    progressBar1.Value = 30;


                MessageBox.Show("游戏开始！", "加油", MessageBoxButtons.OK, MessageBoxIcon.Information);
                hasChess = new bool[15, 15];
                chessColor = new int[15, 15];
            }
        }


        //发送坐标
        public bool sendP(int x, int y)
        {
            //Form2 a = (Form2)this.Owner;
            return Form2.sendP(x,y);
        }
        
        
        
        //悔棋
        public void Repentance()
        {
            int x = tailChessPosition.X * unit + ori;
            int y = tailChessPosition.Y * unit + ori;
            int t = 15;
            hasChess[tailChessPosition.X, tailChessPosition.Y] = false;
            chessColor[tailChessPosition.X, tailChessPosition.Y] = 0;
            Rectangle r = new Rectangle(new Point(x - 15, y -15), new Size(30,30));
            SolidBrush sb = new SolidBrush(panel1.BackColor);
            Graphics g = panel1.CreateGraphics();
            g.FillEllipse(sb, r);
            g.DrawLine(pen, x, y - t >= ori? y - t : ori, x, y + t <= tail? y + t : tail);
            g.DrawLine(pen, x - t >= ori? x - t : ori, y, x + t <= tail? x + t : tail, y);
            


            x = tailChessPosition2.X* unit + ori;
            y = tailChessPosition2.Y* unit + ori;
            hasChess[tailChessPosition2.X, tailChessPosition2.Y] = false;
            chessColor[tailChessPosition2.X, tailChessPosition2.Y] = 0;
            r = new Rectangle(new Point(x - 15, y - 15), new Size(30, 30));
            sb = new SolidBrush(panel1.BackColor);
            g = panel1.CreateGraphics();
            g.FillEllipse(sb, r);
            g.DrawLine(pen, x, y - t >= ori? y - t : ori, x, y + t <= tail? y + t : tail);
            g.DrawLine(pen, x - t >= ori? x - t : ori, y, x + t <= tail? x + t : tail, y);

            tailChessPosition = tailChessPosition3;
            tailChessPosition2 = tailChessPosition4;
        }
    }
}
