using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace 西门子重启修复
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button1.Enabled = false;
            button1.BackColor = Color.Gray;

        }

        private RegistryKey openSubKey;
        private bool _exist = false;


        private void findRegstry_Click(object sender, EventArgs e)
        {
            //打开项
            openSubKey =
                Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Session Manager", true);

            //读取值
            var o = openSubKey.GetValue("PendingFileRename Operations");
            if (o != null)
            {
                status.Items[0].Text = "发现问题";
                _exist = true;
                button1.Enabled = true;
                button1.BackColor = Color.FromArgb(210,9,98);
            }
            else
            {
                status.Items[0].Text = "无问题";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_exist)
            {
                // //删除值
                openSubKey.DeleteValue("PendingFileRename Operations");
                MessageBox.Show("成功", "结果", MessageBoxButtons.OK);
                _exist = false;

                button1.Enabled = false;
                button1.BackColor = Color.Gray;

            }
        }
    }
}