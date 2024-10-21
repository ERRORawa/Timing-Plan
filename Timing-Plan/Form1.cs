using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Timing_Plan
{
    public partial class Form1 : Form
    {
        public void Cmd(string command)    //执行命令（命令，输出文件名）   需等待程序退出
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\n[执行命令]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(command);
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.StandardOutputEncoding = Encoding.UTF8;
            p.Start();
            p.StandardInput.WriteLine(command + " & exit");
            p.StandardInput.AutoFlush = true;
            string strOutput = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            p.Close();
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void leave_Leave(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                textBox4.Text = DateTime.Now.Second.ToString();
            }
            if (textBox5.Text == "")
            {
                textBox5.Text = DateTime.Now.Minute.ToString();
            }
            if (textBox6.Text == "")
            {
                textBox6.Text = DateTime.Now.Hour.ToString();
            }
            if (textBox7.Text == "")
            {
                textBox7.Text = "00";
            }
            if (textBox8.Text == "")
            {
                textBox8.Text = "00";
            }
            if (textBox9.Text == "")
            {
                textBox9.Text = "00";
            }
        }

        private void numOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8 && e.KeyChar != 46 && e.KeyChar != 13 && e.KeyChar != 27))
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int second = 0;
            if (radioButton3.Checked)
            {
                second += int.Parse(textBox9.Text) * 3600 + int.Parse(textBox8.Text) * 60 + int.Parse(textBox7.Text);
            }
            if (radioButton2.Checked)
            {
                if (int.Parse(textBox6.Text) > 23 || int.Parse(textBox5.Text) > 59 || int.Parse(textBox4.Text) > 59)
                {
                    MessageBox.Show("你输入的时间似乎不存在呢！", "难道……你活在异世界？", MessageBoxButtons.OK);
                    return;
                }
                if (int.Parse(textBox6.Text) - DateTime.Now.Hour <= 0)
                {
                    second += (24 - DateTime.Now.Hour + int.Parse(textBox6.Text)) * 3600 + (int.Parse(textBox5.Text) - DateTime.Now.Minute) * 60 + int.Parse(textBox4.Text) - DateTime.Now.Second;
                }
                else
                {

                    second += (int.Parse(textBox6.Text) - DateTime.Now.Hour) * 3600 + (int.Parse(textBox5.Text) - DateTime.Now.Minute) * 60 + int.Parse(textBox4.Text) - DateTime.Now.Second;
                }
            }
            string arg = "a";
            if (radioButton1.Checked)
            {
                arg = "s";
            }
            if (radioButton4.Checked)
            {
                arg = "r";
            }
            Cmd("shutdown -" + arg + " /t " + second);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cmd("shutdown -a");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Settings1.Default["时static"] = int.Parse(textBox6.Text);
            Settings1.Default["分static"] = int.Parse(textBox5.Text);
            Settings1.Default["秒static"] = int.Parse(textBox4.Text);
            Settings1.Default.Save();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Settings1.Default["时now"] = int.Parse(textBox9.Text);
            Settings1.Default["分now"] = int.Parse(textBox8.Text);
            Settings1.Default["秒now"] = int.Parse(textBox7.Text);
            Settings1.Default.Save();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox9.Text = Settings1.Default.时now.ToString();
            textBox8.Text = Settings1.Default.分now.ToString();
            textBox7.Text = Settings1.Default.秒now.ToString();
            textBox6.Text = Settings1.Default.时static.ToString();
            textBox5.Text = Settings1.Default.分static.ToString();
            textBox4.Text = Settings1.Default.秒static.ToString();
        }
    }
}
