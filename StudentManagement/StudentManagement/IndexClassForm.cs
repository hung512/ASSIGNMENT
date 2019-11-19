using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentManagement
{
    public partial class IndexClassForm : Form
    {
        private ClassManagement Business;
        public IndexClassForm()
        {
            InitializeComponent();
            this.Business = new ClassManagement();
            this.Load += IndexClassForm_Load;
            this.btnCreate.Click += btnCreate_Click;
            this.btnDelete.Click += btnDelete_Click;
            this.grddataClasses.DoubleClick += grddataClasses_DoubleClick;
        }

        void grddataClasses_DoubleClick(object sender, EventArgs e)
        {
            var @class = (Class)this.grddataClasses.SelectedRows[0].DataBoundItem;
            var updateForm = new UpdateClassForm(@class.ID);
            updateForm.ShowDialog();
            this.LoadAllClasses();
        }

        void btnDelete_Click(object sender, EventArgs e)
        {
           if(this.grddataClasses.SelectedRows.Count == 1)
           {
               if(MessageBox.Show("Do you want to delete this?", "Confirm",
                   MessageBoxButtons.YesNo) ==System.Windows.Forms.DialogResult.Yes)
               {
                   var @class = (Class)this.grddataClasses.SelectedRows[0].DataBoundItem;
                   this.Business.DeleteClass(@class.ID);
                   MessageBox.Show("Delete class successfully.");
                   this.LoadAllClasses();
               }
           }
        }

        void btnCreate_Click(object sender, EventArgs e)
        {
            var createForm = new CreateClassForm();
            createForm.ShowDialog();
            this.LoadAllClasses();
        }

        void IndexClassForm_Load(object sender, EventArgs e)
        {
            this.LoadAllClasses();
        }
        private void LoadAllClasses()
        {
            var classes = this.Business.GetClasses();
            this.grddataClasses.DataSource = classes;
        }
    }
}
