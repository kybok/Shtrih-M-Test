using LiteDB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static WindowsFormsApplication1.Form1;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        public void LoadData()
        {
            using (var db = new LiteDatabase(@"TestDB.db"))
            {
                var collectionProduct = db.GetCollection<CollProduct>("products");
                var result = collectionProduct.FindAll();
                
                int i = 0;
                foreach (CollProduct c in result)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = c.Name;
                    dataGridView1.Rows[i].Cells[1].Value = c.Amount;
                    i++;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var db = new LiteDatabase(@"TestDB.db"))
            {
                db.DropCollection("products");
            }
            ActiveForm.Close();
        }
    }
}
