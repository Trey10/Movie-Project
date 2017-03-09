using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication9
{

    public partial class Home : Form
    {
        

        public Home()
        {
           



        }



        public void fillList()
        {

            


        }
        private void button1_Click(object sender, EventArgs e)
        {
           



        }

        private void Home_Load(object sender, EventArgs e)
        {

        }

        private void Home_Shown(object sender, EventArgs e)
        { }

        private void button6_Click(object sender, EventArgs e)
        {

            


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

          
          
        }

        public void button4_Click(object sender, EventArgs e)
        {

           

        }

        private void button2_Click(object sender, EventArgs e)
        {
          
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            

        }

        private void Update_Click(object sender, EventArgs e)
        {
            

        }

        public void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            



        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string rating = this.comboBox3.GetItemText(this.comboBox3.SelectedItem);
            string category = this.comboBox2.GetItemText(this.comboBox2.SelectedItem);





            if ((rating != "Select Rating"))
            {
                listView1.Items.Clear();
                sortMovies();
            }
            else if ((category == "Select Category") && (rating == "Select Rating"))
            {
                fillList();
            }

            else if ((category != "Select Category") && (rating == "Select Rating"))
            {
                categorySortMovies();
            }

            else if ((comboBox2.SelectedItem == "Select Category") && (rating != "Select Rating"))
            {
                comboBox3.Select();
                rateSortMovies();
            }

            else
            {
                listView1.Items.Clear();
                sortMovies();
            }


        }

        private void reset()
        {

        }

        public void button2_Click_1(object sender, EventArgs e)
        {
            Form1 add = new Form1();
            Home close = new Home();

            if (listView1.SelectedIndices.Count < 1)
            {
                MessageBox.Show("Please Select a Movie");
            }

            else
            {
                add.Show();
                string title = listView1.SelectedItems[0].Text;
                string rating;
                add.textBox1.Text = title;
                add.comboBox1.FindString(listView1.SelectedItems[0].SubItems[2].Text).ToString();
                add.comboBox2.FindString(listView1.SelectedItems[0].SubItems[1].Text);
                add.textBox2.Text = listView1.SelectedItems[0].SubItems[3].Text;
                string finaltitle = listView1.SelectedItems[0].Text;
                

            }
        }
     

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            fillList();
            
        }

      

        private void button7_Click(object sender, EventArgs e)
        {
            Form1 add = new Form1();
            if (listView1.SelectedIndices.Count < 1)
            {
                MessageBox.Show("Please select a movie");
            }
            else
            {


                SqlCommand cmd = new SqlCommand("UPDATE [Movie Project].[dbo].[movie_id] SET [Title] = @title,[categoryLookup_id] = @category,[ratingLookup_id] = @rating,[Quantity] = @quantity,[discontinueDate] = null WHERE Title = @title", con);

                cmd.Parameters.AddWithValue("@title", listView1.SelectedItems[0].Text);
                cmd.Parameters.AddWithValue("@category", listView1.SelectedItems[0].SubItems[1].Text);
                cmd.Parameters.AddWithValue("@rating", listView1.SelectedItems[0].SubItems[2].Text);
                cmd.Parameters.AddWithValue("@quantity", listView1.SelectedItems[0].SubItems[3].Text);
                cmd.Parameters.AddWithValue("null", listView1.SelectedItems[0].SubItems[4].ToString());

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                try
                {
                    cmd.ExecuteNonQuery();
                    fillList();
                }

                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}  

        

    

 