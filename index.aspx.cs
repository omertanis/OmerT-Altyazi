using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace bootstrapWeb
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //giriş kontrol
            if (Session["userid"] == null)
                Response.Redirect("giris.aspx");
            if(Session["ara"]!=null)
            {
                lbl_baslik.Text = "<h2>Film Listesi</h2>";
                System.Data.DataTable dt = new vtIslemleri().filmListesi(Session["ara"].ToString());
                if (dt.Rows.Count > 0)
                {
                    lbl_alert.Visible = false;
                    grid_filmList.DataSource = dt;
                    grid_filmList.DataBind();
                }
                else
                {
                    lbl_alert.Visible = true;
                    lbl_alert.Text = "Tabloda veri yok";
                }
            }
            else
            {
                lbl_baslik.Text = "<h2>Film Listesi</h2>";
                System.Data.DataTable dt = new vtIslemleri().filmListesi();
                if (dt.Rows.Count > 0)
                {
                    lbl_alert.Visible = false;
                    grid_filmList.DataSource = dt;
                    grid_filmList.DataBind();
                }
                else
                {
                    lbl_alert.Visible = true;
                    lbl_alert.Text = "Tabloda veri yok";
                }
            }
        }
        //Ona göre indir bari
        protected void grid_filmList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = grid_filmList.SelectedRow.Cells[1].Text;
            string filmIsim = grid_filmList.SelectedRow.Cells[2].Text;
            System.Data.DataTable dt = new vtIslemleri().altyaziListesiFilm(id);
            grid_altyaziList.DataSource = dt;
            grid_altyaziList.DataBind();
            if (dt.Rows.Count > 0)
            {
                grid_altyaziList.DataSource = dt;
                grid_altyaziList.DataBind();
            }
            else
            {
                lbl_alert.Text = "Tabloda veri yok";
            }
            lbl_baslik.Text = "<h2>" + filmIsim + " adlı filmin <br/> altyazı listesi</h2>";
            div_altyazi.Visible = true;
            div_film.Visible = false;
            
        }

        protected void grid_altyaziList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MSDN den çaldım.
            //İngilizcemiz var Allah'a şükür.
            try
            {
                string adres = grid_altyaziList.SelectedRow.Cells[3].Text;
                System.String filename = System.IO.Path.GetFileName(adres);

                // set the http content type to "APPLICATION/OCTET-STREAM
                Response.ContentType = "APPLICATION/OCTET-STREAM";

                // initialize the http content-disposition header to
                // indicate a file attachment with the default filename
                // "myFile.txt"
                System.String disHeader = "Attachment; Filename=\"" + filename +
                   "\"";
                //Dosya türünü aldım
                Response.AppendHeader("Content-Disposition", disHeader);
                // transfer the file byte-by-byte to the response object
                System.IO.FileInfo fileToDownload = new
                   System.IO.FileInfo(adres);
                Response.Flush();
                Response.WriteFile(adres);
                div_altyazi.Visible = true;
                div_film.Visible = false;
            }
            catch (Exception ex)
            {
                //burası işe yarıyormuş.
                RegisterStartupScript("message",
                    "<script>alert('Altyazı silinmiş olabilir.\n" + ex.Message + "')</script>");
            }
        }
    }
}