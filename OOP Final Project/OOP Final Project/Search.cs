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
    public partial class Search : Form
    {
        public SqlConnection con = new SqlConnection("Data Source=SP0001\\MSQLSERVER;Initial Catalog=Movie Project;Integrated Security=True");
        private ListView textListView = new ListView();
        private TextBox searchBox = new TextBox();

        public Search()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Search_Load(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        
        
        }

        private void button1_Click(object sender, EventArgs e)
        {

            
            
        }

        
    }
}
