using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace client
{
    public partial class orrPanel:Panel
    {
        public orrPanel()
        {
            SetStyle(ControlStyles.UserPaint, true);
            //// 禁止擦除背景.
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //// 双缓冲
            //SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();
            //this.Size = new Size(800, 800);
            //InitializeComponent();
        }
    }
}
