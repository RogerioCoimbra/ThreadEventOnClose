using System;
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

namespace ThreadEventOnClose
{
    public partial class Form1 : Form
    {
        readonly ManualResetEvent resetEvent = new(false);

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (File.Exists(@"C:\Temp\ThreadEventOnClose.log"))
                File.Delete(@"C:\Temp\ThreadEventOnClose.log");
            
            File.AppendAllText(@"C:\Temp\ThreadEventOnClose.log", string.Empty);

            new Thread(new ThreadStart(() => {
                var listCount = 100;

                for (int i = 1; i <= listCount && !resetEvent.WaitOne(10); i++)
                {
                    File.AppendAllText(@"C:\Temp\ThreadEventOnClose.log", $"Inicio do processamento do arquivo {i}." + DateTime.Now.ToString() + Environment.NewLine);

                    File.AppendAllText(@"C:\Temp\ThreadEventOnClose.log", $"Executando operação 01 - " + DateTime.Now.ToString() + Environment.NewLine);
                    Thread.Sleep(1000);

                    File.AppendAllText(@"C:\Temp\ThreadEventOnClose.log", $"Executando operação 02 - " + DateTime.Now.ToString() + Environment.NewLine);
                    Thread.Sleep(1000);

                    File.AppendAllText(@"C:\Temp\ThreadEventOnClose.log", $"Executando operação 03 - " + DateTime.Now.ToString() + Environment.NewLine);
                    Thread.Sleep(1000);

                    File.AppendAllText(@"C:\Temp\ThreadEventOnClose.log", $"Executando operação 04 - " + DateTime.Now.ToString() + Environment.NewLine);
                    Thread.Sleep(1000);

                    File.AppendAllText(@"C:\Temp\ThreadEventOnClose.log", $"Executando operação 05 - " + DateTime.Now.ToString() + Environment.NewLine);
                    Thread.Sleep(1000);

                    File.AppendAllText(@"C:\Temp\ThreadEventOnClose.log", $"Fim do processamento do arquivo {i}." + DateTime.Now.ToString() + Environment.NewLine);


                    File.AppendAllText(@"C:\Temp\ThreadEventOnClose.log", Environment.NewLine);
                }
                
            })).Start();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            File.AppendAllText(@"C:\Temp\ThreadEventOnClose.log", "*********************************************" + Environment.NewLine);
            File.AppendAllText(@"C:\Temp\ThreadEventOnClose.log", "Form1_FormClosed" + Environment.NewLine);
            File.AppendAllText(@"C:\Temp\ThreadEventOnClose.log", "*********************************************" + Environment.NewLine);

            resetEvent.Set();
        }
    }
}
