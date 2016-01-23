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
    public partial class Form2 : Form
    {
        string olddate;
        string oldtitle;
        StoredProcedures proc = new StoredProcedures();


        public Form2(string date, string title)
        {
            InitializeComponent();
            olddate = date;
            oldtitle = title;
            editMovieDate.Text = date;
            editMovieTitle.Text = title;

        }

        private void editButton2_Click(object sender, EventArgs e)
        {

            proc.EditTableStoredProc(olddate, oldtitle, editMovieDate.Text, editMovieTitle.Text);

            this.Close();

        }
    }
}

