using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinformPoject0527.EFModels;

namespace WinformPoject0527
{
    public partial class BuyerAdd : Form
    {
        public int _userIdAdd;

        public readonly int _ProductId;
        public BuyerAdd(int productId)
        {
            InitializeComponent();
            _ProductId = productId;
            this.Load += BuyerAdd_Load;

        }

        private void BuyerAdd_Load(object sender, EventArgs e)
        {
            var item = new AppDbContext().Products.AsNoTracking()
                                        .Where(x => x.ProductID == _ProductId).FirstOrDefault();

            if (item == null)
            {
                MessageBox.Show("record not found");
                return;
            }

            textBoxProductNameAdd.Text = item.ProductName;
            textBoxProductPriceAdd.Text = item.ProductPrice.ToString();
        }

        private void buttonAddCart_Click(object sender, EventArgs e)
        {
            string name = textBoxProductNameAdd.Text;
            decimal quantity = numericUpDown1.Value;
            decimal price = Decimal.Parse(textBoxProductPriceAdd.Text);
            
            var db = new AppDbContext();


            var shoppingCart=new ShoppingCart()
            {
                UserID = _userIdAdd,
            };
            db.ShoppingCarts.Add(shoppingCart);
            db.SaveChanges();



            
            var shoppingCartDetail = new ShoppingCartDetail()
            {
                //ProductName = name,
                ProductID= _ProductId,
                Quantity = (int)quantity,
                //TotalProductPrice = price,
            };
            db.ShoppingCartDetails.Add(shoppingCartDetail);
            db.SaveChanges();
               
            
            
            if (numericUpDown1.Value == 0)
            {
                MessageBox.Show("請輸入數量");
            }
            else
            {
                MessageBox.Show($"品名：{textBoxProductNameAdd.Text}\r\n共計：{price * numericUpDown1.Value}\r\n已加入購物車");
                Close();
            }
        }
    }
}
