using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bootstrapWeb
{

    public partial class profilim : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //giriş kontrol
            if (Session["userid"] == null)
                Response.Redirect("giris.aspx");

            if(User.IsInRole("admin"))
            {
                //Site yavaş çalışıyorsa ekrana boş boş bakma .d
            lbl_bilgi.Text="Admin sayfasına gidiyorsun adaş."; 
            Response.Redirect(@"siteAdmin\profilim.aspx");
            }
            else if(User.IsInRole("user"))
            {
                //Site yavaş çalışıyorsa ekrana boş boş bakma .d
                lbl_bilgi.Text="Kullanıcı sayfasına gidiyorsun.";
                Response.Redirect(@"siteUser\profilim.aspx");
            }
            else
            {
                //Bir hata varmış demekki
                lbl_bilgi.Text = "sen bi giriş yapsana önce";
                Response.Redirect(@"giris.aspx");
            }
 
        }
    }
}