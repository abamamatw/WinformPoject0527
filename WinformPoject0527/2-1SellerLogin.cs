using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinformPoject0527.EFModels;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinformPoject0527
{
    public partial class SellerLogin : Form
    {
        string account = " ";
        string password = " ";
        public int  SellerIdlogin;

        public SellerLogin()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonDemo_Click(object sender, EventArgs e)
        {
            account = "Apple@123.com";
            password = "1234";
            txtAccount.Text = account;
            txtPassword.Text = password;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            try
            {
                List<int> SellerID = new List<int>();
                var query1 = new AppDbContext().Sellers
                                            .Where(x => x.SellerAccount == account && x.SellerPassword == password)
                                            .Select(x => x.SellerID)/*.FirstOrDefault()*/;
                foreach (var item in query1)
                {
                    SellerID.Add(item);
                }
                SellerIdlogin = SellerID[0];


                if (query1 != null)
                {
                    var frm = new SellerMain();
                    frm.MdiParent = Login.ActiveForm;
                    frm.Show();
                }
                else
                {
                    MessageBox.Show("請輸入正確帳號或密碼");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("請輸入正確帳號或密碼");
            }

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            var frm = new SellerRegister();
            frm.ShowDialog();
        }
    }
}
