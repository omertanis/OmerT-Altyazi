using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//roles
using System.Web.Security;
using System.Data.SqlClient;
//facebook login
/*
 * Buralar bir zaman doluydu ancak facebook benden website adresi istediği için localhostu 
 * ayrı bir domain e taşımak istemedim.
 * FACEBOOKLOGIN yok.
 */
namespace bootstrapWeb
{
    public partial class giris : System.Web.UI.Page
    {
        vtIslemleri vt = new vtIslemleri();
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        private void girmek()
        {
            /// <returns>Giriş yapılırsa [0] id, [1] isim , [2] email, [3] rol</returns>

            try
            {
                string[] bilgiler = vt.kullaniciGiris(
                    txt_eposta.Text.TrimStart(' ').TrimEnd(' '),
                     txt_sifre.Text.TrimStart(' ').TrimEnd(' '));
                if (bilgiler != null)
                {
                    FormsAuthenticationTicket fa = new FormsAuthenticationTicket(
                                   1, //version
                                   bilgiler[2],//Username
                                   DateTime.Now,//Şu an
                                   DateTime.Now.AddMinutes(30),//Biteceği tarih timeout
                                   false,//Kalıcı olmamalı
                                   bilgiler[3],//Rol bilgisi 1 admin 2 user
                                   FormsAuthentication.FormsCookiePath //Cookie yolu
                                   );
                    //Şifreleme
                    string ticketSifre = FormsAuthentication.Encrypt(fa);
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, ticketSifre);
                    //Cookie timeout. Permanent ise süre ekleniyor
                    if (fa.IsPersistent)
                        cookie.Expires = fa.Expiration;
                    Response.Cookies.Add(cookie);
                    //Girişe gitmek
                    string returnUrl = Request.QueryString["ReturnUrl"];
                    if (returnUrl == null) returnUrl = @"profil.aspx";
                    yonlendir(bilgiler[1], bilgiler[0]);
                    lblRespond.Text = "Giriş başarılı.";
                    lblRespond.Text += "\n ID:" + bilgiler[0] + "\nAdınız:" + bilgiler[1] +
                                        "\nE-Posta:" + bilgiler[2] + "\nRolünüz:" + bilgiler[3];
                }
                else//Şifre falan hatalı ise
                    lblRespond.Text = "Eposta yada şifre hatalı. Tekrar denesene ? Yada kayıt ol daha kolay.";
            }
            catch (Exception ex)
            {
                lblRespond.Text = "Hata oluştu." + ex.Message;
            }
        }
        protected void btn_giris_Click(object sender, EventArgs e)
        {
            //Bugları düzeltelim dimi.
            if (txt_eposta.Text != "" && txt_sifre.Text != "")
            {
                girmek();
            }
            else
            {
                lblRespond.Text = "Alanları Doldurunuz.";
            }
        }
        //Sırf adını göstersin diye yapıyorum ha
        private void yonlendir(string ad, string id)
        {
            Session["ad"] = ad;
            Session["userid"] = id;
            Response.Redirect("index.aspx");
        }

        protected void btn_kayit_Click(object sender, EventArgs e)
        {
            //Kullanıcı ister de ister
            Response.Redirect("kayitol.aspx");
        }
    }
}