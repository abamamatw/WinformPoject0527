using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinformPoject0527.EFModels;

namespace WinformPoject0527
{
    
    public partial class BuyerLogin : Form
    {
        string _account = "";
        string _pw = "";
        public int _userIdlogin;
       

        public BuyerLogin()
        {
            InitializeComponent();
        }

        private void buttonexit_Click(object sender, EventArgs e)
        {
            
            Application.Exit();
        }
       

        private void buttonDemo_Click(object sender, EventArgs e)
        {           
            _account= "Jason@123.com";
            _pw= "1234";
            textBoxaccount.Text = _account;
            textBoxPw.Text = _pw;
        }


        public void buttonlogin_Click(object sender, EventArgs e)
        {
            
            try 
            {
                List<int> userID = new List<int>();
                var query1 = new AppDbContext().Users
                                            .Where(x => x.UserAccount == _account && x.UserPassword == _pw)
                                            .Select(x=>x.UserID)/*.FirstOrDefault()*/;
                foreach (var item in query1)
                {
                    userID.Add(item);
                }
                _userIdlogin = userID[0];


                if (query1 != null)
                {
                    
                    var frm = new BuyerMain();
                    frm.MdiParent = Login.ActiveForm;
                    frm._userIdMain=_userIdlogin;
                    frm.Show();
                }
                else
                {
                    MessageBox.Show("請輸入正確帳戶與密碼");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("請輸入正確帳戶與密碼");
            }
            
        }

        
    }
}
