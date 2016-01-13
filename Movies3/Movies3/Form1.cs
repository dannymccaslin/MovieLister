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

           

            for (int i = 0; i < this._names.Count; i++)
            {
                string name = this._names[i];
                dt.Columns.Add(name);


            }
            ds.Tables.Add(dt);
            //Render the DataGridView
            dataGridView1.DataSource = dt;
            dataGridView1.Columns["Movie Title"].Width = 200;
            dataGridView1.MultiSelect = false;

            openFileDialog1.Filter = "xml files (*.xml)|*.xml";
        }

  
        public void submitButton_Click(object sender, EventArgs e)
        {


            if (movieTitle.Text != null && movieTitle.Text != "")
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
            }
            movieTitle.Text = null;

            dataGridView1.DataSource = dt;
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

                if (Convert.ToString(dt.Rows[i]["Movie Title"]) == movieSearch.Text)
                 {
                     
                     dataGridView1.Rows[i].Selected = true;
                 } 


             }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)== DialogResult.Yes)
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
    }
}

