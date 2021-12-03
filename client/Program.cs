using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1.boo = 0;



            //           System.Diagnostics.Debug.WriteLine("1:" + Form1.boo + "     " + Thread.CurrentThread.ManagedThreadId.ToString());
            //Application.Run(new Form2());
            //Application.Run(new Form10());



            do
            {
                if (Form1.boo == 0)
                {
                    Application.Run(new Form1());
                    System.Diagnostics.Debug.WriteLine("4:" + Form1.boo);
                }
                else
                    Application.Run(new Form2());

            } while (Form1.boo != -1);



            //while (Form1.boo != -1)
            //{
            //    if (Form1.boo == 0)
            //    {
            //        Application.Run(new Form1());
            //        System.Diagnostics.Debug.WriteLine("4:" + Form1.boo);
            //    }
            //    else
            //Application.Run(new Form4());

            
        //};

        }
    }
}
