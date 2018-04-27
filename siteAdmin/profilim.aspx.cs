using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bootstrapWeb.siteAdmin
{
    public partial class profilim : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Giriş yapmadıysan başa git
            if(Session["userid"]==null)
                Response.Redirect(@"..\giris.aspx");
            //Bilgilendirelim
            lbl_bilgi.Text = "EPosta, Film Adı, Kayıt yeri aynı olamaz. Değişiklik göremiyorsanız sebebi budur.";
            if (!IsPostBack) //Sabitleyelim
                gvbind();
        }

        //Önceden yazmıştım. Yoksa bunları diğer fonksiyona da gömebilirim.
        private void filmListesi()
        {
            vtIslemleri vt = new vtIslemleri();
            System.Data.DataTable dt;
            if (Session["ara"] == null)
                dt = vt.filmListesi();
            else
                dt = vt.filmListesi(Session["ara"].ToString());
            if (dt.Rows.Count > 0)
            {
                grid_Films.DataSource = dt;
                grid_Films.DataBind();
            }
            else
            {
                lbl_filmBilgi.Text = "Tabloda veri yok";
            }
        }

        //İşe yaradığını farkettim
        private void kullaniciListesi()
        {
            System.Data.DataTable dt;
            vtIslemleri vt = new vtIslemleri();
            //Burada tecrübe yatıyor !1!!11111birbirbir!!!!!
            if (Session["ara"] == null)
                dt = vt.kullaniciListesi();
            else
                dt = vt.kullaniciListesi(Session["ara"].ToString());

            //Buralar data varmı yokmu onun için geçerli. Kod şişik dursun
            if (dt.Rows.Count > 0)
            {
                grid_kullanicilar.DataSource = dt;
                grid_kullanicilar.DataBind();
            }
            else
            {
                lbl_altyaziBilgi.Text = "Tabloda veri yok";
            }
        }
        //Modülerlik kazandırıyormuş
        private void altyaziListesi()
        {
            System.Data.DataTable dt;
            vtIslemleri vt = new vtIslemleri();

            //Burada tecrübe yatıyor !1!!11111birbirbir!!!!!
            if (Session["ara"] == null)
                dt = vt.altyaziListesi();
            else
                dt = vt.altyaziListesi(Session["ara"].ToString());
            //Buralar data varmı yokmu onun için geçerli. Kod şişik dursun
            if (dt.Rows.Count > 0)
            {
                grid_altyazi.DataSource = dt;
                grid_altyazi.DataBind();
            }
            else
            {
                lbl_altyaziBilgi.Text = "Tabloda veri yok";
            }
        }

        //dropdownlist update
        private void ddlFilmListesi()
        {
            ddl_altyazi_filmAdi.DataBind();
        }

        private void bilgilerimDoldur()
        {
            txt_bilgilerim_id.Text = Session["userid"].ToString();
            txt_bilgilerim_isim.Text = Session["ad"].ToString();
        }

        //Bunlar hep güncellemeler
        protected void gvbind()
        {
            filmListesi();
            altyaziListesi();
            kullaniciListesi();
            ddlFilmListesi();
            bilgilerimDoldur();
        }


        //Buralarda kafayı yaktım
        protected void grid_Films_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = grid_Films.Rows[e.RowIndex].Cells[0].Text.ToString();
            string name = grid_Films.Rows[e.RowIndex].Cells[1].Text;


            string sorgu = "delete FROM Films where FilmsID='"
                + id
                + "'";
            bool sil = new vtIslemleri().sil(sorgu);
            lbl_filmBilgi.Focus();
            lbl_filmBilgi.Text = sil ? "ID :" + id + " Film ADI :" + name + " başarı ile silindi." : "Silme Başarısız.";
            gvbind();
        }

        // Şu mesela. Aşırı can sıkıcıydı
        protected void grid_Films_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grid_Films.Rows[e.NewEditIndex].Focus();
            grid_Films.EditIndex = e.NewEditIndex;
            gvbind();
        }

        //O Controls[0]'ı keşfedene kadar kaç ATP harcandı
        protected void grid_Films_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            //Bu kısımda o edite e basınca textbox geliyor ya
            //onlardan dataları çekiyoruz
            string id, filmadi;
            TextBox txt;
            id = grid_Films.Rows[e.RowIndex].Cells[0].Text;
            txt = (TextBox)grid_Films.Rows[e.RowIndex].Cells[1].Controls[0];
            filmadi = txt.Text;

            grid_Films.EditIndex = -1;

            string sorgu = "update Films set " +
                "Name = '" + filmadi + "'" +
                "where FilmsID = " + id;
            bool guncelle = new vtIslemleri().guncelle(sorgu);
            lbl_filmBilgi.Focus();
            lbl_filmBilgi.Text = guncelle ? "Güncelleme Basarili" : "Güncelleme Basarisiz";
            gvbind();
        }

        //Paging bile ekledim
        protected void grid_Films_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grid_Films.Focus();
            grid_Films.PageIndex = e.NewPageIndex;
            gvbind();
        }

        //Kullanıcı ya işte. Vazgeçtim de diyebilir.
        protected void grid_Films_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grid_Films.Rows[e.RowIndex].Focus();
            grid_Films.EditIndex = -1;
            gvbind();
        }


        //BURADAN SONRAKI AÇIKLAMALARA GEREK YOK. ÜSTTEKİ EŞDEĞERLERİNİ OKU
        protected void grid_altyazi_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grid_altyazi.Focus();
            grid_altyazi.PageIndex = e.NewPageIndex;
            gvbind();
        }

        protected void grid_altyazi_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grid_altyazi.Rows[e.RowIndex].Focus();
            grid_altyazi.EditIndex = -1;
            gvbind();
        }

        protected void grid_altyazi_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = grid_altyazi.Rows[e.RowIndex].Cells[0].Text.ToString();
            string name = grid_altyazi.Rows[e.RowIndex].Cells[1].Text;


            string sorgu = "delete FROM Subtitles where SubtitlesID='"
                + id
                + "'";
            bool sil = new vtIslemleri().sil(sorgu);
            lbl_altyaziBilgi.Focus();
            lbl_altyaziBilgi.Text = sil ? "ID :" + id + " Altyazı Adı :" + name + " başarı ile silindi." : "Silme Başarısız.";
            gvbind();
        }

        protected void grid_altyazi_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grid_altyazi.Rows[e.NewEditIndex].Focus();
            grid_altyazi.EditIndex = e.NewEditIndex;
            gvbind();
        }

        protected void grid_altyazi_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string id, altyaziadi, altyaziHedef;
            TextBox txt;
            id = grid_altyazi.Rows[e.RowIndex].Cells[0].Text;

            txt = (TextBox)grid_altyazi.Rows[e.RowIndex].Cells[1].Controls[0];
            altyaziadi = txt.Text;
            txt.Text = grid_altyazi.Rows[e.RowIndex].Cells[2].Text;
            altyaziHedef = txt.Text;

            grid_altyazi.EditIndex = -1;

            string sorgu = "update Subtitles set " +
                "Name = '" + altyaziadi + "', " +
                "Directory = '"+altyaziHedef+"' "+
                "where SubtitlesID = " + id;
            bool guncelle = new vtIslemleri().guncelle(sorgu);
            grid_altyazi.Rows[e.RowIndex].Focus();
            lbl_altyaziBilgi.Text = guncelle ? "Güncelleme Basarili" : "Güncelleme Basarisiz";
            gvbind();
        }


        //AYNEN BURADADA ÖYLE YAP
        protected void grid_kullanicilar_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grid_kullanicilar.Focus();
            grid_kullanicilar.PageIndex = e.NewPageIndex;
            gvbind();
        }

        protected void grid_kullanicilar_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grid_kullanicilar.Rows[e.RowIndex].Focus();
            grid_kullanicilar.EditIndex = -1;
            gvbind();
        }

        protected void grid_kullanicilar_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = grid_kullanicilar.Rows[e.RowIndex].Cells[0].Text.ToString();
            string name = grid_kullanicilar.Rows[e.RowIndex].Cells[1].Text;
            string eposta = grid_kullanicilar.Rows[e.RowIndex].Cells[2].Text;


            string sorgu = "delete FROM Users where UsersID='"
                + id
                + "'";
            bool sil = new vtIslemleri().sil(sorgu);
            lbl_kullaniciBilgi.Focus();
            lbl_kullaniciBilgi.Text = sil ? "ID :" + id + " Kullanıcı Adı :" + name + " E-Posta : " + eposta + " başarı ile silindi." : "Silme Başarısız.";
            gvbind();
        }

        protected void grid_kullanicilar_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grid_kullanicilar.Rows[e.NewEditIndex].Focus();
            grid_kullanicilar.EditIndex = e.NewEditIndex;
            gvbind();
        }

        protected void grid_kullanicilar_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //En sonda farkettim ama
            //adi = ((TextBox)grid_kullanicilar.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
            //Böyle de yapılabilir.
            string id, adi, email, sifre, rol;
            TextBox txt;
            id = grid_kullanicilar.Rows[e.RowIndex].Cells[0].Text;

            txt = (TextBox)grid_kullanicilar.Rows[e.RowIndex].Cells[1].Controls[0];
            adi = txt.Text;
            txt = (TextBox)grid_kullanicilar.Rows[e.RowIndex].Cells[2].Controls[0];
            email = txt.Text;
            txt = (TextBox)grid_kullanicilar.Rows[e.RowIndex].Cells[3].Controls[0];
            sifre = txt.Text;
            txt = (TextBox)grid_kullanicilar.Rows[e.RowIndex].Cells[4].Controls[0];
            rol = txt.Text;

            grid_kullanicilar.EditIndex = -1;

            string sorgu = "update Users set " +
                "Name = '" + adi + "'," +
                "Email = '" + email + "', " +
                "Password = '" + sifre + "', " +
                "Roles = '" + rol + "' " +
                "where UsersID = " + id;
            bool guncelle = new vtIslemleri().guncelle(sorgu);
            grid_kullanicilar.Rows[e.RowIndex].Focus();
            lbl_kullaniciBilgi.Text = guncelle ? "Güncelleme Basarili" : "Güncelleme Basarisiz";
            gvbind();
        }

        //Radio button list çalışmadı
        //Radio button da çalışmadı
        //Manuel çözdüm
        protected void btn_filmBilgi_Click(object sender, EventArgs e)
        {
            div_film.Visible = true;
            div_altyazi.Visible = false;
            div_kullanicilar.Visible = false;
            div_filmekle.Visible = false;
            div_altyaziekle.Visible = false;
            div_bilgilerim.Visible = false;
        }

        protected void btn_altyaziBilgi_Click(object sender, EventArgs e)
        {
            div_film.Visible = false;
            div_altyazi.Visible = true;
            div_kullanicilar.Visible = false;
            div_filmekle.Visible = false;
            div_altyaziekle.Visible = false;
            div_bilgilerim.Visible = false;
        }

        protected void btn_kullaniciBilgi_Click(object sender, EventArgs e)
        {
            div_film.Visible = false;
            div_altyazi.Visible = false;
            div_kullanicilar.Visible = true;
            div_filmekle.Visible = false;
            div_altyaziekle.Visible = false;
            div_bilgilerim.Visible = false;
        }

        protected void btn_filmEkle_Click(object sender, EventArgs e)
        {
            div_film.Visible = false;
            div_altyazi.Visible = false;
            div_kullanicilar.Visible = false;
            div_filmekle.Visible = true;
            div_altyaziekle.Visible = false;
            div_bilgilerim.Visible = false;
        }

        protected void btn_altyaziEkle_Click(object sender, EventArgs e)
        {
            div_film.Visible = false;
            div_altyazi.Visible = false;
            div_kullanicilar.Visible = false;
            div_filmekle.Visible = false;
            div_altyaziekle.Visible = true;
            div_bilgilerim.Visible = false;
        }

        protected void btn_bilgilerim_Click(object sender, EventArgs e)
        {
            div_film.Visible = false;
            div_altyazi.Visible = false;
            div_kullanicilar.Visible = false;
            div_filmekle.Visible = false;
            div_altyaziekle.Visible = false;
            div_bilgilerim.Visible = true;
        }



        //Film Eklemek için
        protected void btn_film_Click(object sender, EventArgs e)
        {
            if(txt_filmAdi.Text != "")
            {
                bool durum = new vtIslemleri().filmEkle(txt_filmAdi.Text.TrimEnd(' ').TrimStart(' '));
                lbl_film.Text = durum ? "Ekleme başarılı" : "Ekleme başarısız";
                lbl_film.Focus();
                gvbind();
            }
            else
            {
                lbl_film.Text = "Adsız film olmaz.";
                lbl_film.Focus();
            }
        }

        //Altyazı Eklemek için
        protected void btn_altyazi_Click(object sender, EventArgs e)
        {

            if (ddl_altyazi_filmAdi.SelectedIndex != -1)
            {
                //Seçmemişse uyarsın
                if (!fu_altyazi_filmkaynak.HasFile)
                { lbl_altyazi.Text = "DOSYA SEÇİNİZ."; lbl_dosyasec.Focus(); }
                else
                {
                    fu_altyazi_filmkaynak.SaveAs(Request.PhysicalApplicationPath + @"/altyazilar/" + fu_altyazi_filmkaynak.FileName);
                    string ad = txt_altyazi_altyaziAdi.Text.TrimEnd(' ').TrimStart(' ');
                    string kaynak = @"/altyazilar/" + fu_altyazi_filmkaynak.PostedFile.FileName;
                    string filmid = ddl_altyazi_filmAdi.SelectedValue;
                    string userid = Session["userid"].ToString();
                    bool ekleme = new vtIslemleri().altyaziEkle(ad, kaynak, filmid, userid) ;
                    lbl_altyazi.Focus();
                    lbl_altyazi.Text = ekleme?"Ekleme başarılı" : "Ekleme başarısız";
                    gvbind();
                }
            }
            else
            {
                lbl_altyazi.Text = "Film seçiniz."; lbl_dosyasec.Focus();
            }

        }

        protected void btn_bilgilerim_guncelle_Click(object sender, EventArgs e)
        {
            string ad = txt_bilgilerim_isim.Text.TrimEnd(' ').TrimStart(' ');
            string id = txt_bilgilerim_id.Text.TrimEnd(' ').TrimStart(' ');
            string sifre = txt_bilgilerim_sifre.Text.TrimEnd(' ').TrimStart(' ');
            bool guncelle = new vtIslemleri().bilgilerimiGuncelle(ad, sifre, id);
            if(guncelle)
            {
                Session["ad"] = ad;
            }
            lbl_bilgilerim.Text = guncelle ? "Güncelleme başarılı." : "Güncelleme başarısız.";
            gvbind();
        }
    }
}
