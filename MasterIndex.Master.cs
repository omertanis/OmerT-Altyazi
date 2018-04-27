using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.Security;

namespace bootstrapWeb
{
    public partial class MasterIndex : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Giriş yapmadıysan başa git
            if (HttpContext.Current.User == null)
                Response.Redirect("giris.aspx");


            //Giriş yapılı ise adını yazalım şöyle güzelce
            if (Session["ad"] != null)
                lbl_kullaniciadi.Text = "Hoşgeldiniz Sayın : " + Session["ad"].ToString();
            else
                lbl_kullaniciadi.Text = "GODMODE !!!";
        }

        protected void btn_ara_Click(object sender, EventArgs e)
        {
            //Arama yaptıysan ona göre film gelsin
            if (txt_ara.Text.TrimStart(' ').TrimEnd(' ') != "")
            {
                Session["ara"] = txt_ara.Text.TrimStart(' ').TrimEnd(' ');
                Response.Redirect("index.aspx");
            }
                //Yapmadıysan hepsini gömerim ama
            else
            {
                Session["ara"] = null;
                txt_ara.Focus();
                Response.Redirect("index.aspx");
            }
                
        }

        protected void btn_profilim_Click(object sender, EventArgs e)
        {
            //Profiline gitsin o zaman. Adminmi user mi onu içeride görücez
            Response.Redirect("profilim.aspx");
        }

        protected void btn_exit_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            Response.Redirect("giris.aspx");
        }

        protected void btn_anasayfa_Click(object sender, EventArgs e)
        {
            Session["ara"] = null;
            Response.Redirect("/index.aspx");
        }
    }
}