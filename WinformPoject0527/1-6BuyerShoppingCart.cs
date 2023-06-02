using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformPoject0527
{
    public partial class BuyerShoppingCart : Form
    {
        public BuyerShoppingCart()
        {
            InitializeComponent();
        }

        private void buttonMain_Click(object sender, EventArgs e)
        {
            var frm = new BuyerMain();
            frm.MdiParent = Login.ActiveForm;
            frm.Show();
        }
    }
}
