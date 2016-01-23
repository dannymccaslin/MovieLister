using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Movies3
{

    public class StoredProcedures
    {
        public DataTable userdt = new System.Data.DataTable();

        public void fillUserDT()
        {

            SqlConnection SqlCon = new SqlConnection(
                                    Movies3.Properties.Settings.Default.MoviesConnectionString);

            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM Users", SqlCon);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);


            userdt.Locale = System.Globalization.CultureInfo.InvariantCulture;
            userdt.Clear();
            dataAdapter.Fill(userdt);
        }

        public void DelRowStoredProc(string date, string title)
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
                    "dbo.DeleteRow", con);

                // 2. set the command object so it knows
                // to execute a stored procedure
                cmd.CommandType = CommandType.StoredProcedure;

                // 3. add parameter to command, which
                // will be passed to the stored procedure
                cmd.Parameters.Add(new SqlParameter("@Date", date));
                cmd.Parameters.Add(new SqlParameter("@Title", title));

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

        public void insertStoredProc(string date, string title)
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
                    "dbo.AddRow", con);

                // 2. set the command object so it knows
                // to execute a stored procedure
                cmd.CommandType = CommandType.StoredProcedure;

                // 3. add parameter to command, which
                // will be passed to the stored procedure
                cmd.Parameters.Add(new SqlParameter("@Date", date));
                cmd.Parameters.Add(new SqlParameter("@Title", title));

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

        public void EditTableStoredProc(string olddate, string oldtitle, string newdate, string newtitle)
        {
            SqlConnection con = null;
            SqlDataReader rdr = null;
            //  Form1 form = new Form1();

            try
            {
                con = new SqlConnection(
                        Movies3.Properties.Settings.Default.MoviesConnectionString);

                con.Open();

                // 1. create a command object identifying
                // the stored procedure
                SqlCommand cmd = new SqlCommand(
                    "dbo.EditRow2", con);

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

        public void AddUserProc(string username, string firstname, string lastname)
        {


            SqlDataReader rdr = null;
            int numUsers = 0;
            for (int i = 0; i < userdt.Rows.Count; i++)
            {
                if (Convert.ToString(userdt.Rows[i]["Username"]) == username)
                {
                    numUsers += 1;
                }
            }

            if (numUsers == 0)
            {
                using (SqlConnection con = new SqlConnection(
                         Movies3.Properties.Settings.Default.MoviesConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                           "dbo.AddUser", con);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@username", username));
                    cmd.Parameters.Add(new SqlParameter("@firstname", firstname));
                    cmd.Parameters.Add(new SqlParameter("@lastname", lastname));

                    rdr = cmd.ExecuteReader();
                }
            }
            else
            {
                MessageBox.Show("That user already exists.");
            }

        }

        public int getUserID(string username)
        {
          
            using (SqlConnection con = new SqlConnection(
                         Movies3.Properties.Settings.Default.MoviesConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                       "dbo.GetUserID", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@username", username));

                SqlParameter returnValue = new SqlParameter("@ReturnValue", DbType.Int32);
                returnValue.Direction = ParameterDirection.ReturnValue;

                int id = Int32.Parse(cmd.Parameters["returnValue"].Value.ToString());

                return id;
            }

        }

        public void loadGridView(int u)
        {
            SqlDataReader rdr = null;

            using (SqlConnection con = new SqlConnection(
                        Movies3.Properties.Settings.Default.MoviesConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                       "dbo.LoadGrid", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@userID", u));

                rdr = cmd.ExecuteReader();
            }
        }
    }
}


