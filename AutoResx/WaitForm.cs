using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cuiliangbjgmailcom.AutoResx
{
    public partial class WaitForm : Form
    {
        // how many seconds left
        private int WaitSeconds = 0;

        public WaitForm(string title, int waitSeconds=10)
        {
            InitializeComponent();

            lblTitle.Text = title;
            lblWait.Text = waitSeconds.ToString();
            WaitSeconds = waitSeconds;

            //start timer
            timer1.Enabled = true;
        }



        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            WaitSeconds--;
            lblWait.Text = WaitSeconds.ToString();

            if (WaitSeconds <= 0)
            {
                Close();
            }
        }

        private void WaitForm_Load(object sender, EventArgs e)
        {
            Refresh();
        }
    }
}
