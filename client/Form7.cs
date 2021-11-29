using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client
{
    public partial class Form7 : Form
    {
        string filePath;
        string savePath;
        string line;
        public Form7()
        {
            InitializeComponent();
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "文本文件|*.txt|C#文件|*.cs|所有文件|*.*";
            //openFileDialog.RestoreDirectory = false;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);
                while ((line = sr.ReadLine()) != null)
                    textBox1.Text += line+"\r\n";
                fs.Close();
                sr.Close();
            }
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
                FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(textBox1.Text);
                sw.Close();
                fs.Close();
            
        }

        private void 另存为ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "文本文件|*.txt|C#文件|*.cs|所有文件|*.*";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                savePath = dialog.FileName;
                FileStream fs = new FileStream(savePath, FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(textBox1.Text);
                sw.Close();
                fs.Close();
            }
        }

        private void 文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Form7_Load(object sender, EventArgs e)
        {

        }

        private void 字体ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 斜体ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!textBox1.Font.Italic)
            {
                textBox1.Font = new Font(textBox1.Font, FontStyle.Italic);
            }
            else
            {
                textBox1.Font = new Font(textBox1.Font, FontStyle.Regular);
            }
        }

        private void 加粗ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!textBox1.Font.Bold)
            {
                textBox1.Font = new Font(textBox1.Font, FontStyle.Bold);
            }
            else
            {
                textBox1.Font = new Font(textBox1.Font, FontStyle.Regular);
            }
        }
    }
}
