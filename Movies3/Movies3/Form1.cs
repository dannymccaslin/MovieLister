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
        StoredProcedures proc = new StoredProcedures();
        




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
            proc.insertStoredProc(movieDate.Text, movieTitle.Text);



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

    

        private void saveButton_Click(object sender, EventArgs e)
        {
            
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            

        }

        public void deleteRow(int i)
        {

            string date = Convert.ToString(dt.Rows[i]["Date"]);
            string title = Convert.ToString(dt.Rows[i]["Title"]);

            proc.DelRowStoredProc(date, title);
            loadDataGridView();


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
            proc.fillUserDT();
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
                    loadDataGridView();

                }

            }
        }

        private void newUserCreator_Click(object sender, EventArgs e)
        {
            Form3 newUserForm = new Form3();
            newUserForm.ShowDialog(this);
            proc.fillUserDT();
        }

        private void usersDropBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            proc.loadGridView(proc.getUserID(usersDropBox.Text));
        }
    }
}

