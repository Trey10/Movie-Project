using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace WindowsFormsApplication9
{
    public partial class Form1 : Form
    {
        public SqlConnection con = new SqlConnection("Data Source=SP0001\\MSQLSERVER;Initial Catalog=Movie Project;Integrated Security=True");


        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           


        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            string rating = this.comboBox1.GetItemText(this.comboBox1.SelectedItem);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            string category = this.comboBox2.GetItemText(this.comboBox2.SelectedItem);

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

            string quantity = textBox2.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
          
            Home nav = new Home();
            Form1 close = new Form1();

            if (nav.listView1.SelectedIndices.Count > -1)
            {
                editMovie();
                MessageBox.Show("Movie Edited Successfully");
                Close();
                nav.Focus();
                nav.fillList();
                
                
                nav.listView1.SelectedItems[0].Text = textBox1.Text;


            }
            else
            {

                if ((textBox1.Text == null) || (comboBox1.SelectedIndex == null) || (comboBox2.SelectedIndex == null) || (textBox2.Text == null))
                {
                    MessageBox.Show("Invalid information given");
                }
                else
                {
                    Home edit = new Home();


                    SqlCommand cmd = new SqlCommand("INSERT INTO [Movie Project].[dbo].[movie_id] ([Title] ,[categoryLookup_id],[ratingLookup_id] ,[Quantity])VALUES(@title, @category,@rating, @quantity)", con);

                    cmd.Parameters.AddWithValue("@title", textBox1.Text);
                    cmd.Parameters.AddWithValue("@category", comboBox2.SelectedItem);
                    cmd.Parameters.AddWithValue("@rating", comboBox1.SelectedItem);
                    cmd.Parameters.AddWithValue("@quantity", textBox2.Text);

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }

                    try
                    {
                        cmd.Connection = con;
                        con.Open();
                        int rows = cmd.ExecuteNonQuery();
                        con.Close();


                        if (textBox1.Text == null)
                        {
                            MessageBox.Show("Error: No title was entered", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        else if (comboBox2.SelectedItem == null)
                        {
                            MessageBox.Show("Error: No category was chosen", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        else if (comboBox1.SelectedItem == null)
                        {
                            MessageBox.Show("Error: No rating was chosen");
                        }


                        else
                        {


                            if (rows > 0)
                            {
                                nav.Focus();

                                MessageBox.Show("Movie Added Successfully", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Error", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            Close();
                        }


                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }


        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        public void editMovie()
        {
            
            Home edit = new Home();
            
           string  textBox_1 = textBox1.Text;
           string comboBox_2= comboBox2.Text;
           string comboBox_1 = comboBox1.Text;
           string textBox_2 = textBox2.Text;
            

            SqlCommand cm = new SqlCommand("UPDATE [Movie Project].[dbo].[movie_id]   SET Title = @title ,categoryLookup_id = @category ,ratingLookup_id = @rating ,Quantity = @quantity WHERE Title = '"+ textBox1.Text  + "'", con);
            


            cm.Parameters.AddWithValue("@title", textBox_1);
            cm.Parameters.AddWithValue("@category", comboBox_2);
            cm.Parameters.AddWithValue("@rating", comboBox_1);
            cm.Parameters.AddWithValue("@quantity", textBox_2);

            /*textBox1.Text = textBox1.Text;
            comboBox2.Text = comboBox2.Text;
            comboBox1.Text = comboBox1.Text;
            textBox2.Text = textBox2.Text;
            */
            


            if (con.State == ConnectionState.Closed) 
            {
                con.Open();
            }

           try
                {


                    cm.ExecuteNonQuery();
                    edit.fillList();

                }

                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

           edit.fillList();

            




        }
    }
}
  
