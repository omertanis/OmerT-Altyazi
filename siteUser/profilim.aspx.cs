using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bootstrapWeb.siteUser
{
    public partial class profilim : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Giriş yapmadıysan başa git
            if (Session["userid"] == null)
                Response.Redirect(@"..\giris.aspx");
            //Bilgilendirelim
            lbl_bilgi.Text = "EPosta, Film Adı, Kayıt yeri aynı olamaz. Değişiklik göremiyorsanız sebebi budur.";
            if (!IsPostBack) //Sabitleyelim
                gvbind();
        }

        //Modülerlik kazandırıyormuş
        private void altyaziListesi()
        {
            System.Data.DataTable dt;
            vtIslemleri vt = new vtIslemleri();
            if (Session["ara"] == null)
                dt = vt.altyaziListesiKullanici(Session["userid"].ToString());
            else
                dt = vt.altyaziListesiKullanici(Session["userid"].ToString(), Session["ara"].ToString());

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
            altyaziListesi();
            ddlFilmListesi();
            bilgilerimDoldur();
        }

        //BURADAN SONRAKI AÇIKLAMALARA GEREK YOK. siteAdmin\profilim.aspx EŞDEĞERLERİNİ OKU
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


            string sorgu = @"delete FROM Subtitles where SubtitlesID='"
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
            txt = (TextBox)grid_altyazi.Rows[e.RowIndex].Cells[2].Controls[0];
            altyaziHedef = txt.Text;

            grid_altyazi.EditIndex = -1;

            string sorgu = @"update Subtitles set " +
                "Name = '" + altyaziadi + "', " +
                "Directory = '" + altyaziHedef + "' " +
                "where SubtitlesID = " + id;
            bool guncelle = new vtIslemleri().guncelle(sorgu);
            grid_altyazi.Rows[e.RowIndex].Focus();
            lbl_altyaziBilgi.Text = guncelle ? "Güncelleme Basarili" : "Güncelleme Basarisiz";
            gvbind();
        }


        //Radio button list çalışmadı
        //Radio button da çalışmadı
        //Manuel çözdüm
        protected void btn_altyazilarim_Click(object sender, EventArgs e)
        {
            div_altyazi.Visible = true;
            div_filmekle.Visible = false;
            div_altyaziekle.Visible = false;
            div_bilgilerim.Visible = false;
        }
        protected void btn_filmEkle_Click(object sender, EventArgs e)
        {
            div_altyazi.Visible = false;
            div_filmekle.Visible = true;
            div_altyaziekle.Visible = false;
            div_bilgilerim.Visible = false;
        }

        protected void btn_altyaziEkle_Click(object sender, EventArgs e)
        {
            div_altyazi.Visible = false;
            div_filmekle.Visible = false;
            div_altyaziekle.Visible = true;
            div_bilgilerim.Visible = false;
        }

        protected void btn_bilgilerim_Click(object sender, EventArgs e)
        {
            div_altyazi.Visible = false;
            div_filmekle.Visible = false;
            div_altyaziekle.Visible = false;
            div_bilgilerim.Visible = true;
        }



        //Film Eklemek için
        protected void btn_film_Click(object sender, EventArgs e)
        {
            if (txt_filmAdi.Text != "")
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
                    bool ekleme = new vtIslemleri().altyaziEkle(ad, kaynak, filmid, userid);
                    lbl_altyazi.Focus();
                    lbl_altyazi.Text = ekleme ? "Ekleme başarılı" : "Ekleme başarısız";
                    gvbind();
                }
            }
            else
            {
                lbl_altyazi.Text = "Film seçiniz."; lbl_dosyasec.Focus();
            }

        }
        //bilgileri de güncellesin
        protected void btn_bilgilerim_guncelle_Click(object sender, EventArgs e)
        {
            string ad = txt_bilgilerim_isim.Text.TrimEnd(' ').TrimStart(' ');
            string id = txt_bilgilerim_id.Text.TrimEnd(' ').TrimStart(' ');
            string sifre = txt_bilgilerim_sifre.Text.TrimEnd(' ').TrimStart(' ');
            bool guncelle = new vtIslemleri().bilgilerimiGuncelle(ad, sifre, id);
            if (guncelle)
            {
                Session["ad"] = ad;
            }
            lbl_bilgilerim.Text = guncelle ? "Güncelleme başarılı." : "Güncelleme başarısız.";
            gvbind();
        }
    }
}