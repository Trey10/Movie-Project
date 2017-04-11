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
        public SqlConnection con = new SqlConnection("Data Source=SP0001\\MSQLSERVER;Initial Catalog=Movie Project;Integrated Security=True");

        public Home()
        {
            InitializeComponent();



        }



        public void fillList()
        {

            SqlConnection con = new SqlConnection("Data Source=SP0001\\MSQLSERVER;Initial Catalog=Movie Project;Integrated Security=True");
            listView1.Items.Clear();

            
            if (checkBox1.Checked == false)
            {
                SqlCommand cmd = new SqlCommand("SELECT TOP 1000 [Title],[categoryLookup_id] ,[ratingLookup_id],[Quantity] FROM [Movie Project].[dbo].[movie_id] WHERE discontinueDate IS NULL", con);
            
            
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                try
                {


                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }


                    SqlDataReader readData = cmd.ExecuteReader();

                    while (readData.Read())
                    {

                        ListViewItem item = new ListViewItem(readData["Title"].ToString());
                        item.SubItems.Add(readData["categoryLookup_id"].ToString());
                        item.SubItems.Add(readData["ratingLookup_id"].ToString());
                        item.SubItems.Add(readData["Quantity"].ToString());
                        
                        
                        listView1.Items.Add(item);
                        


                        
                    }

                    readData.Close();
                    readData.Dispose();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                button4.Enabled = true;
                button5.Enabled = true;
                textBox1.Enabled = true;
                Update.Enabled = true;
            }

            else if (checkBox1.Enabled == true)
            {
                discontinuedDatelist();
                button4.Enabled = false;
                button5.Enabled = false;
                textBox1.Enabled = false;
                Update.Enabled = false;

            }


        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form1 add = new Form1();
            Home close = new Home();

            add.Show();



        }

        private void Home_Load(object sender, EventArgs e)
        {

        }

        private void Home_Shown(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                fillList();
            }

            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.ExitThread();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

            fillList();
            string rating = this.comboBox3.GetItemText(this.comboBox3.SelectedItem);
            string category = this.comboBox2.GetItemText(this.comboBox2.SelectedItem);
            comboBox3.Select();
            this.comboBox3.SelectedItem = "Select Rating";
            comboBox2.Select();
            this.comboBox2.SelectedItem = "Select Category";
            listView1.Select();


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            Form1 add = new Form1();
            if (listView1.SelectedIndices.Count < 1)
            {
                MessageBox.Show("Please select a movie");
            }
            else
            {


                SqlCommand cmd = new SqlCommand("UPDATE [Movie Project].[dbo].[movie_id] SET [Title] = @title,[categoryLookup_id] = @category,[ratingLookup_id] = @rating,[Quantity] = @quantity,[discontinueDate] = GETDATE() WHERE Title = @title", con);

                cmd.Parameters.AddWithValue("@title", listView1.SelectedItems[0].Text);
                cmd.Parameters.AddWithValue("@category", listView1.SelectedItems[0].SubItems[1].Text);
                cmd.Parameters.AddWithValue("@rating", listView1.SelectedItems[0].SubItems[2].Text);
                cmd.Parameters.AddWithValue("@quantity", listView1.SelectedItems[0].SubItems[3].Text);
                

             

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

        public void button4_Click(object sender, EventArgs e)
        {

            if (listView1.SelectedIndices.Count < 1)
            {
                MessageBox.Show("Please select a movie");
            }
            
            else if(checkBox1.Enabled = true)
            {
                MessageBox.Show("These movies are DISCONTINUED. \nTo Check In/ Check Out, Please Click 'Undo Delete'");
            }
            
            else
            {
                SqlCommand cm = new SqlCommand("UPDATE [Movie Project].[dbo].[movie_id] SET [Quantity] = Quantity + 1 WHERE [Title] = '" + listView1.SelectedItems[0].Text + "' ", con);
                cm.Parameters.AddWithValue("@quantity", textBox1.Text);

                try
                {
                    cm.ExecuteNonQuery();
                    fillList();

                }

                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            Search search = new Search();
            search.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string update = textBox1.Text;

        }

        private void Update_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count < 1)
            {
                MessageBox.Show("Please select a movie");
            }
            else if (textBox1.Text.Length < 1)
            {
                MessageBox.Show("Please Enter Quantity");
            }

            else
            {
                SqlCommand cm = new SqlCommand("UPDATE [Movie Project].[dbo].[movie_id] SET [Quantity] = @quantity WHERE [Title] = '" + listView1.SelectedItems[0].Text + "' ", con);
                cm.Parameters.AddWithValue("@quantity", textBox1.Text);

                try
                {
                    cm.ExecuteNonQuery();
                    fillList();

                }

                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        public void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count < 1)
            {
                MessageBox.Show("Please select a movie");
            }
            else if (checkBox1.Enabled = true)
            {
                MessageBox.Show("These movies are DISCONTINUED. \nTo Check In/ Check Out, Please Click 'Undo Delete'");
            }
            else
            {

                //**************SQL QUERY TO UPDATE MOVIE QUANTITY***************************************

                SqlCommand cm = new SqlCommand("UPDATE [Movie Project].[dbo].[movie_id] SET [Quantity] = Quantity - 1 WHERE [Title] = '" + listView1.SelectedItems[0].Text + "' ", con);
                cm.Parameters.AddWithValue("@quantity", textBox1.Text);


                //**************EXECUTE SQL QUERY*****************************************
                try
                {
                    cm.ExecuteNonQuery();
                    fillList();

                }

                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string rating = this.comboBox3.GetItemText(this.comboBox3.SelectedItem);
            string category = this.comboBox2.GetItemText(this.comboBox2.SelectedItem);
            sortMovies();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string rating = this.comboBox3.GetItemText(this.comboBox3.SelectedItem);
            string category = this.comboBox2.GetItemText(this.comboBox2.SelectedItem);

            if ((category != "Select Category"))
            {
                listView1.Items.Clear();
                sortMovies();


            }

            else if ((rating == "Select Rating") && (category == "Select Category"))
            {
                fillList();

            }

            else if ((rating != "Select Rating") && (category == "Select Category"))
            {
                rateSortMovies();
            }
            else if ((rating == "Select Rating"))
            {
                categorySortMovies();
            }

            else
            {
                listView1.Items.Clear();
                rateSortMovies();
            }





        }


        private void sortMovies()
        {
            if (checkBox1.Checked == false)
            {
                SqlConnection con = new SqlConnection("Data Source=SP0001\\MSQLSERVER;Initial Catalog=Movie Project;Integrated Security=True");

                string rating = this.comboBox3.GetItemText(this.comboBox3.SelectedItem);
                string category = this.comboBox2.GetItemText(this.comboBox2.SelectedItem);
                SqlCommand sort = new SqlCommand("SELECT * FROM movie_id Where ratingLookup_id = '" + rating + "'  AND categoryLookup_id= '" + category + "' ", con);

                //SqlCommand sort = new SqlCommand("SELECT * FROM movie_id Where ratingLookup_id = ISNULL('" + rating + "', ratingLookup_id) AND categoryLookup_id = ISNULL('" + category + "', categoryLookup_id) ", con);


                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }


                listView1.Items.Clear();
                try
                {

                    SqlDataReader reader = sort.ExecuteReader();


                    while (reader.Read())
                    {

                        ListViewItem item = new ListViewItem(reader["Title"].ToString());
                        item.SubItems.Add(reader["categoryLookup_id"].ToString());
                        item.SubItems.Add(reader["ratingLookup_id"].ToString());
                        item.SubItems.Add(reader["Quantity"].ToString());

                        listView1.Items.Add(item);

                    }
                    con.Close();
                    con.Dispose();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            else if (checkBox1.Checked == true) 
            
            {
            
            }

        }
        private void rateSortMovies()
        {
            SqlConnection con = new SqlConnection("Data Source=SP0001\\MSQLSERVER;Initial Catalog=Movie Project;Integrated Security=True");

            string rating = this.comboBox3.GetItemText(this.comboBox3.SelectedItem);
            string category = this.comboBox2.GetItemText(this.comboBox2.SelectedItem);
            SqlCommand sort = new SqlCommand("SELECT * FROM movie_id Where ratingLookup_id = '" + rating + "'", con);

            //SqlCommand sort = new SqlCommand("SELECT * FROM movie_id Where '" + rating + "' = ISNULL('" + rating + "', ratingLookup_id) AND '" + category + "' = ISNULL('" + category + "', categoryLookup_id) ", con);


            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }


            listView1.Items.Clear();
            try
            {

                SqlDataReader reader = sort.ExecuteReader();


                while (reader.Read())
                {

                    ListViewItem item = new ListViewItem(reader["Title"].ToString());
                    item.SubItems.Add(reader["categoryLookup_id"].ToString());
                    item.SubItems.Add(reader["ratingLookup_id"].ToString());
                    item.SubItems.Add(reader["Quantity"].ToString());

                    listView1.Items.Add(item);

                }
                con.Close();
                con.Dispose();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }
        private void categorySortMovies()
        {
            SqlConnection con = new SqlConnection("Data Source=SP0001\\MSQLSERVER;Initial Catalog=Movie Project;Integrated Security=True");

            string rating = this.comboBox3.GetItemText(this.comboBox3.SelectedItem);
            string category = this.comboBox2.GetItemText(this.comboBox2.SelectedItem);
            SqlCommand sort = new SqlCommand("SELECT * FROM movie_id Where categoryLookup_id = '" + category + "'", con);

            //SqlCommand sort = new SqlCommand("SELECT * FROM movie_id Where '" + rating + "' = ISNULL('" + rating + "', ratingLookup_id) AND '" + category + "' = ISNULL('" + category + "', categoryLookup_id) ", con);


            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }


            listView1.Items.Clear();
            try
            {

                SqlDataReader reader = sort.ExecuteReader();


                while (reader.Read())
                {

                    ListViewItem item = new ListViewItem(reader["Title"].ToString());
                    item.SubItems.Add(reader["categoryLookup_id"].ToString());
                    item.SubItems.Add(reader["ratingLookup_id"].ToString());
                    item.SubItems.Add(reader["Quantity"].ToString());

                    listView1.Items.Add(item);

                }
                con.Close();
                con.Dispose();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



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
        private void checkList()
        {



        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            fillList();
            
        }

        private void discontinuedDatelist()
        {
            SqlCommand cmd = new SqlCommand("SELECT TOP 1000 [Title],[categoryLookup_id] ,[ratingLookup_id],[Quantity],[discontinueDate] FROM [Movie Project].[dbo].[movie_id] WHERE discontinueDate = ISNULL( discontinueDate, '')", con);

            try
            {


                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }


                SqlDataReader readData = cmd.ExecuteReader();

                while (readData.Read())
                {

                    ListViewItem item = new ListViewItem(readData["Title"].ToString());
                    item.SubItems.Add(readData["categoryLookup_id"].ToString());
                    item.SubItems.Add(readData["ratingLookup_id"].ToString());
                    item.SubItems.Add(readData["Quantity"].ToString());
                    item.SubItems.Add(readData["discontinueDate"].ToString());


                    listView1.Items.Add(item).ForeColor = Color.Red;




                }

                readData.Close();
                readData.Dispose();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        

    

 