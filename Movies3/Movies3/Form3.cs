using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Movies3
{
    public partial class Form3 : Form
    {
        StoredProcedures proc = new StoredProcedures();
        string username;
        string firstname;
        string lastname;

        public Form3()
        {
            InitializeComponent();
             username = userName.Text;
             firstname = firstName.Text;
             lastname = lastName.Text;

            
        }

        private void addUserButton_Click(object sender, EventArgs e)
        {
            proc.AddUserProc(username, firstname, lastname);
            this.Close();
        }
    }
}
