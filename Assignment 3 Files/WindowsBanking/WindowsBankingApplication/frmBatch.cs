using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WindowsBankingApplication
{
    public partial class frmBatch : Form
    {

        public frmBatch()
        {
            InitializeComponent();
        }

        /// <summary>
        /// given - further code required
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmBatch_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0);
        }

        /// <summary>
        /// given - further code required
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkProcess_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //given - for use in encryption assignment
            if (txtKey.Text.Trim().Length != 8)
            {
                MessageBox.Show("64 Bit Decryption Key must be entered", "Enter Key");
                txtKey.Focus();
            }
        }

    }
}
