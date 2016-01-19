using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace Movies3
{
    public partial class Form1 : Form
    {
        //create a list for my columns
        List<string> _names = new List<string>();

        List<string> _dates = new List<string>();
        List<string> _titles = new List<string>();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();




        public Form1()
        {
            InitializeComponent();

            //Add columns
            _names.Add("Date");
            _names.Add("Movie Title");



            /*  for (int i = 0; i < this._names.Count; i++)
              {
                  string name = this._names[i];
                  dt.Columns.Add(name);


              }
              ds.Tables.Add(dt);
              //Render the DataGridView
              dataGridView1.DataSource = dt;
              dataGridView1.Columns["Movie Title"].Width = 200;
              dataGridView1.MultiSelect = false;

              openFileDialog1.Filter = "xml files (*.xml)|*.xml"; */
        }


        public void submitButton_Click(object sender, EventArgs e)
        {
            // insertToTable(movieDate.Text, movieTitle.Text);
            insertStoredProc(movieDate.Text, movieTitle.Text);



            /* if (movieTitle.Text != null && movieTitle.Text != "")
            {
                DataRow dtRow;
                dtRow = dt.NewRow();
                dtRow["Date"] = movieDate.Text;
                dtRow["Movie Title"] = movieTitle.Text;
                dt.Rows.Add(dtRow);
            }
            else
            {
                MessageBox.Show("You need to type a movie title to save a movie to your list.");
            }*/
            movieTitle.Text = null;
            loadDataGridView();

        }

        private static void insertStoredProc(string date, string title)
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


        static void insertToTable(string date, string name)
        {
            using (SqlConnection con = new SqlConnection(
                Movies3.Properties.Settings.Default.MoviesConnectionString))
            {
                con.Open();

                using (SqlCommand command = new SqlCommand(
                    "INSERT INTO MovieTable VALUES (@Date, @Title)", con))
                {
                    command.Parameters.Add(new SqlParameter("Date", date));
                    command.Parameters.Add(new SqlParameter("Title", name));
                    command.ExecuteNonQuery();
                }

            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();

        }

        public void deleteRow(int i)
        {

            DataRow dr = dt.Rows[i];
            dr.Delete();

        }
        private void searchButton_Click(object sender, EventArgs e)
        {

            /*   

           int index = dt.Rows.IndexOf(foundRow[0]);
           */

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                if (Convert.ToString(dt.Rows[i]["Title"]) == movieSearch.Text)
                {

                    dataGridView1.Rows[i].Selected = true;
                }


            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // Do stuff if yes
                //Get the selected row by looping through all of the rows and finding one that has been selected
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Selected == true)
                    {
                        deleteRow(i);
                    }

                }
            }
            else
            {
                // Close the window
            }
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string name = saveFileDialog1.FileName;

            ds.WriteXml(name);
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string name = openFileDialog1.FileName;

            ds.ReadXml(name);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            loadDataGridView();

        }

        public void loadDataGridView()
        {
            SqlConnection SqlCon = new SqlConnection(
                                    Movies3.Properties.Settings.Default.MoviesConnectionString);

            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM MovieTable", SqlCon);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);


            dt.Locale = System.Globalization.CultureInfo.InvariantCulture;
            dt.Clear();
            dataAdapter.Fill(dt);
            dbBindSource.DataSource = dt;

            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = dbBindSource;

        }

        private void Form1_FormClosing(Object sender, FormClosingEventArgs e)
        {
            

        }

        private void editButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Selected == true)
                {
                    string date = Convert.ToString(dt.Rows[i]["Date"]);
                    string title = Convert.ToString(dt.Rows[i]["Title"]);

                    Form2 editForm = new Form2(date, title);
                    editForm.ShowDialog(this);
                    

                }

            }
        }
    }
}

