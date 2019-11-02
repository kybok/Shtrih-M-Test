using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DrvFRLib;
using LiteDB;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form 
    {
        public Form1()
        {
            InitializeComponent();
            Driver = new DrvFR();
        }
        DrvFR Driver;

        public class CollProduct
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public double Amount { get; set; }
        }

        private void UpdateResult()
        {
            labelStatus.Text = string.Format("Результат: {0}, {1}", Driver.ResultCode, Driver.ResultCodeDescription);
        }
        private void btnShowProperties_Click(object sender, EventArgs e)
        {
            Driver.ShowProperties();
            UpdateResult();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text != "" && textBoxAmount.Text != "" && textBoxPass.Text != "")
            {
                Driver.Password = Convert.ToInt32(textBoxPass.Text);
                labelStatus.Text = "Порядковый номер оператора: " + Driver.OperatorNumber.ToString();
                Driver.Sale();
                if(Driver.GetECRStatus() == 0)
                {
                    using (var db = new LiteDatabase(@"TestDB.db"))
                    {
                        var collectionProduct = db.GetCollection<CollProduct>("products");
                        var product = new CollProduct { Name = textBoxName.Text, Amount = Convert.ToDouble(textBoxAmount.Text) };
                        collectionProduct.Insert(product);
                        
                    }

                }
                else labelStatus.Text = "Платеж не выполнен!"; 
                
            }
            else labelStatus.Text = "Неверно введены данные!";
        }

        private void textBoxAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)) && !((e.KeyChar == ',') && (textBoxAmount.Text.IndexOf(",") == -1) && (textBoxAmount.Text.Length != 0)))
            {
                if (e.KeyChar != (char)Keys.Back) e.Handled = true;
            }
        }

        private void textBoxPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)) && !((e.KeyChar == ',') && (textBoxAmount.Text.IndexOf(",") == -1) && (textBoxAmount.Text.Length != 0)))
            {
                if (e.KeyChar != (char)Keys.Back) e.Handled = true;
            }
        }
        
        private void button1_Click_1(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            frm.LoadData();
                frm.Show();
        }

        
    }
}
