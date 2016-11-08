using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SXWTaxCaculationOpen
{
    public partial class Form1 : Form
    {
        private double fdkc = 3500.00;
        private double msje = 0.00;
        private double gzze = 0.00;
        private double yjed = 0.00;
        private double gs = 0.00;
        private double sfgz = 0.00;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            fdkcTextBox.Text = this.fdkc.ToString();
            statusLabel.Text = "[ 准备就绪 ].  Powered by Postbird";
        }
        //重置
        private void resetButton_Click(object sender, EventArgs e)
        {
            this.fdkc = 3500.00;
            this.msje = 0.00;
            this.gzze = 0.00;
            this.yjed = 0.00;
            this.gs = 0.00;
            this.sfgz = 0.00;
            fdkcTextBox.Text = this.fdkc.ToString();
            msjeTextBox.Text = "";
            gzzeTextBox.Text = "";
            yjedTextBox.Text = "";
            gsTextBox.Text = "";
            sfgzTextBox.Text = "";
            statusLabel.Text = "[ 准备就绪 ].  Powered by Postbird";
        }
        //计算
        private void submitButton_Click(object sender, EventArgs e)
        {
            if (double.TryParse(gzzeTextBox.Text.ToString().Trim(), out this.gzze) && double.TryParse(msjeTextBox.Text.ToString().Trim(), out this.msje))
            {
                //计算应交税款额度
                this.yjed = Math.Round(this.gzze - this.msje - this.fdkc, 2, MidpointRounding.AwayFromZero);
                if(this.yjed <= 0)
                {
                    this.yjed = 0.00;
                }
                //计算个税
                //设置基础的税率和速算扣除数
                double tmpRate = 0.03;
                double tmpExcept = 0.00;
                double tmpYSJE = this.yjed;
                if (tmpYSJE <= 1500.00)
                {
                    tmpRate = 0.03;
                    tmpExcept = 0.00;
                }
                else if (tmpYSJE > 1500.00 && tmpYSJE <= 4500)
                {
                    tmpRate = 0.10;
                    tmpExcept = 105.00;
                }
                else if (tmpYSJE > 4500 && tmpYSJE <= 9000)
                {
                    tmpRate = 0.2;
                    tmpExcept = 555;
                }
                else if (tmpYSJE > 9000 && tmpYSJE <= 35000)
                {
                    tmpRate = 0.25;
                    tmpExcept = 1050;
                }
                else if (tmpYSJE > 35000 && tmpYSJE <= 55000)
                {
                    tmpRate = 0.30;
                    tmpExcept = 2775;
                }
                else if (tmpYSJE > 55000 && tmpYSJE <= 80000)
                {
                    tmpRate = 0.35;
                    tmpExcept = 5505;
                }
                else
                {
                    tmpRate = 0.45;
                    tmpExcept = 13505;
                }
                //公式 应税金额*税率-速算扣除数 保留中国式四舍五入
                this.gs = Math.Round(tmpYSJE * tmpRate - tmpExcept, 2, MidpointRounding.AwayFromZero);
                this.sfgz = this.gzze - this.gs;
                //输出
                gsTextBox.Text = this.gs.ToString("f2");
                yjedTextBox.Text = this.yjed.ToString("f2");
                sfgzTextBox.Text = this.sfgz.ToString("f2");
                //输出状态
                statusLabel.Text = "[ 计算完成 ].  Powered by Postbird ";
            }
            else
            {
                this.showErrorMsg("输入有误", "错误提示");
            }
            
        }
        //输出提示信息 
        // 内容 和输出框标题
        private void showErrorMsg(string content,string title)
        {
            MessageBox.Show(content,title);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string url = "www.ptbird.cn";
            System.Diagnostics.Process.Start(url);
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string url = "http://contact.ptbird.cn";
            System.Diagnostics.Process.Start(url);
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string url = "http://sxw.ptbird.cn";
            System.Diagnostics.Process.Start(url);
        }
    }
}
