using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bootstrapWeb
{
    public partial class kayitol : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_kaydol_Click(object sender, EventArgs e)
        {
            //Buraları hep güncellemişiz
            vtIslemleri vt = new vtIslemleri();
            //Boşsan giriş yapma. Ayıp günah
            if (txt_Sifre.Text != null || txt_Email.Text != null || txt_SifreTekrar != null)
            {
                //Bu email işleri beni çok yordu
                bool isler = System.Text.RegularExpressions.Regex.IsMatch(txt_Email.Text,
                    @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
                if ((txt_Sifre.Text == txt_SifreTekrar.Text) && isler)
                {
                    //Nede güzel sistem ama dimi
                    bool x = vt.kullaniciKayit(
                        txt_Isim.Text.TrimEnd(' ').TrimStart(' '),
                        txt_Email.Text.TrimEnd(' ').TrimStart(' '),
                        txt_Sifre.Text.TrimEnd(' ').TrimStart(' '));
                    kayitBilgi(x);
                }
                else
                {
                    lbl_user.Text = "Alanları kontrol ederek tekrar giriniz.";
                }
            }
        }
        //İnternet yavaşsa kullanıcıya bilgi gitsin
            private void kayitBilgi(bool x)
            {
                if(x)
                {
                    lbl_user.Text="Kayıt başarılı";
                    Response.Redirect("giris.aspx");
                }
                else //Güzel çözüm ama kabul et :)
                    lbl_user.Text="Kayıt başarısız.E-posta kullanımda olabilir.";
                
            }
            protected void btn_giris_Click(object sender, EventArgs e)
            {
                Response.Redirect("giris.aspx");
            }
    }
}