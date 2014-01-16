using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EnvDTE;
using EnvDTE80;

namespace cuiliangbjgmailcom.AutoResx
{
    public partial class OptionForm : Form
    {
        public OptionForm(IList<string> projects )
        {
            InitializeComponent();

            foreach (var p in projects)
            {
                ddlProjects.Items.Add(p);
            }
            ddlProjects.SelectedIndex = 0;
        }

        private void OptionForm_Load(object sender, EventArgs e)
        {
            
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ddlFileCount.Text != "ALL")
            {
                int value = 0;
                if (int.TryParse(ddlFileCount.Text, out value) == false || value <= 0)
                {
                    MessageBox.Show("Invalid max file count! Should be 'ALL' or a integer.");
                    ddlFileCount.Focus();
                    return;
                }
            }

            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        public string SelectedProject
        {
            get { return ddlProjects.Text; }
        }

        public bool RemoveOldTags
        {
            get { return chkRemoveOldTags.Checked; }
        }

        public int WaitForDesignViewSeconds
        {
            get { return (int)numWaitForView.Value; }
        }

        public int WaitForCommandReadySeconds
        {
            get { return (int) numWaitForCmd.Value; }
        }

        public int WaitForSaveSeconds
        {
            get { return (int) numWaitForSave.Value; }
        }

        public int MaxProcessFiles
        {
            get
            {
                if (ddlFileCount.SelectedText == "ALL")
                {
                    return int.MaxValue;
                }
                else
                {
                    return int.Parse(ddlFileCount.Text);
                }
            }
        }
    }
}
