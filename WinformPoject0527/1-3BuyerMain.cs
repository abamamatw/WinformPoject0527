using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using WinformPoject0527.EFModels;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinformPoject0527
{
    public partial class BuyerMain : Form
    {
        List<ProductDto> data;
        List<ProductDto> data2;
        string selectedMainItem = "";
        public int _userIdMain;
        public BuyerMain()
        {
            InitializeComponent();
            this.Load += BuyerMain_Load;

        }

        private void BuyerMain_Load(object sender, EventArgs e)
        {
            Display();
            ComboBoxItems();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            Display();
        }

        public void ComboBoxItems()
        {
           
            var query1=new AppDbContext().ProductMainCategories.Select(x=>x.CategoryName).ToList();
            foreach (var item in query1)
            {
                comboBoxMain.Items.Add(item);
            }

            
        }

       

        public void Display()
        {
            bool isInt = int.TryParse(this.textBoxProductID.Text, out int productID);
            int? sId = isInt ? productID : (int?)null;

            string name = textBoxProductName.Text;

            IQueryable<Product> query = new AppDbContext().Products.AsNoTracking();

            if (sId.HasValue)
            {
                query = query.Where(x => x.ProductID == sId.Value);
            }

            if (string.IsNullOrEmpty(name) == false)
            {
                query = query.Where(x => x.ProductName.Contains(name));
            }

            query = query.OrderBy(x => x.ProductID);

            this.data = query.ToList()
                                .Select(x => new ProductDto()
                                {
                                    ProductId=x.ProductID,
                                    CategoryId=x.CategoryID,
                                    ProductName=x.ProductName,
                                    ProductDescription=x.ProductDescription,
                                    ProductPrice = (decimal)x.ProductPrice
                                }).ToList();
            this.dataGridView1.DataSource = data;
                                                
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == 0) return;

            int productID = this.data[e.RowIndex].ProductId;

            var frm = new BuyerAdd(productID);
            frm.Owner = this;
            frm._userIdAdd = _userIdMain;
            frm.ShowDialog();
        }

        private void buttonshoppingcart_Click(object sender, EventArgs e)
        {
            var frm = new BuyerShoppingCart();
            frm.MdiParent = Login.ActiveForm;
            frm.Show();
        }

        private void buttonSignout_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBoxMain_SelectedValueChanged(object sender, EventArgs e)
        {
            comboBoxDetail.Items.Clear();
            if(comboBoxMain.Items.Count > 0 )
            {
                List<string> productsName = new List<string>();

                string sParentID = comboBoxMain.Text;

                //var mainCategory = new AppDbContext().ProductMainCategories
                //    .Where(x => x.CategoryName == sParentID)
                //    .Select(x => x.CategoryID);

                //foreach (var item in mainCategory)
                //{
                //    productsName.Add(item);
                //}

                string sTempID = productsName[0].ToString();

                var query2 = new AppDbContext().ProductCategories
                                            .Where(x => x.ParentCategoryID == sTempID)
                                            .Select(x => x.CategoryName).ToList();
                foreach (var item in query2)
                {
                    comboBoxDetail.Items.Add(item);
                }

            }
        }

        private void buttonSearch2_Click(object sender, EventArgs e)
        {
            Display2();
        }

        public void Display2()
        {
            string _categoryName = comboBoxDetail.Text;


            List<string> productsName2 = new List<string>();
            var items = new AppDbContext().ProductCategories
                    .Where(x => x.CategoryName == _categoryName)
                    .Select(x => x.CategoryID);
            foreach (var item in items)
            {
                productsName2.Add(item);
            }
            string sTempID2 = productsName2[0].ToString();



            IQueryable<Product> query2 = new AppDbContext().Products.AsNoTracking();

            if (string.IsNullOrEmpty(_categoryName) == false)
            {
                query2 = query2.Where(x => x.CategoryID == sTempID2);
            }

            query2 = query2.OrderBy(x => x.ProductID);

            this.data2 = query2.ToList()
                                .Select(x => new ProductDto()
                                {
                                    ProductId = x.ProductID,
                                    CategoryId = x.CategoryID,
                                    ProductName = x.ProductName,
                                    ProductDescription = x.ProductDescription,
                                    ProductPrice = (decimal)x.ProductPrice
                                }).ToList();
            this.dataGridView1.DataSource = data2;


        }

        
    }
}
