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
            EditTableStoredProc( olddate, oldtitle, editMovieDate.Text, editMovieTitle.Text);
            this.Close();

        }

        private static void EditTableStoredProc(string olddate,string oldtitle,string newdate,string newtitle)
        {
            SqlConnection con = null;
            SqlDataReader rdr = null;

            try
            {
                con = new SqlConnection(
                        Movies3.Properties.Settings.Default.MoviesConnectionString);

                con.Open();

                // 1. create a command object identifying
                // the stored procedure
                SqlCommand cmd = new SqlCommand(
                    "dbo.EditRow", con);

                // 2. set the command object so it knows
                // to execute a stored procedure
                cmd.CommandType = CommandType.StoredProcedure;

                // 3. add parameter to command, which
                // will be passed to the stored procedure
                cmd.Parameters.Add(new SqlParameter("@olddate", olddate));
                cmd.Parameters.Add(new SqlParameter("@oldtitle", oldtitle));
                cmd.Parameters.Add(new SqlParameter("@date", newdate));
                cmd.Parameters.Add(new SqlParameter("@title", newtitle));

                rdr = cmd.ExecuteReader();

            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

                if (rdr != null)
                {
                    rdr.Close();
                }

            }
        }
    }
}
