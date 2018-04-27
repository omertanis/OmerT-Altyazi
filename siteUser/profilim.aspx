<%@ Page Title="" Language="C#" MasterPageFile="../MasterProfil.Master" AutoEventWireup="true" CodeBehind="profilim.aspx.cs" Inherits="bootstrapWeb.siteUser.profilim" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- USER PAGE -->
    <div><h1 class="dropdown-header"><asp:Label runat="server" ID="lbl_bilgi"></asp:Label></h1></div>
    <!-- ADMIN PAGE -->
    <!-- Sayfa değişme. TAB İLE YAPMAK ISTEMEDIM ! -->
    <div class="btn-group ">
        <asp:Button ID="btn_filmEkle" CssClass="btn btn-info" runat="server" Text="Film Ekle" OnClick="btn_filmEkle_Click" Visible="false"/>
        <asp:Button ID="btn_altyaziEkle" CssClass="btn btn-info" runat="server" Text="Altyazı Ekle" OnClick="btn_altyaziEkle_Click" />
        <asp:Button ID="btn_altyazilarim" CssClass="btn btn-info" runat="server" Text="Altyazılarımı Listele" OnClick="btn_altyazilarim_Click" />
        <asp:Button ID="btn_bilgilerim" CssClass="btn btn-info" runat="server" Text="Bilgilerimi Güncelle" OnClick="btn_bilgilerim_Click" />
    </div>

    <!-- Altyazılarını düzenleyebilsin tablosu -->
    <div id="div_altyazi" runat="server" visible="false" class="scroll">
        <h4 class="dropdown-header"><asp:Label runat="server" ID="lbl_altyaziBilgi"></asp:Label></h4>
        <h2><asp:Label runat="server" Text="Altyazı Bilgileri"></asp:Label></h2>
        <asp:GridView ID="grid_altyazi" runat="server" AutoGenerateColumns="false"
            CssClass="table table-responsive table-hover table-bordered"
            OnPageIndexChanging="grid_altyazi_PageIndexChanging"
            OnRowCancelingEdit="grid_altyazi_RowCancelingEdit"
            OnRowDeleting="grid_altyazi_RowDeleting"
            OnRowEditing="grid_altyazi_RowEditing"
            OnRowUpdating="grid_altyazi_RowUpdating"
            PageSize="10"
            AllowPaging="true">
    <Columns>
        <asp:BoundField DataField="SubtitlesID" HeaderText="Altyazı No No" ReadOnly="true" />
        <asp:BoundField DataField="sName" HeaderText="Altyazı Adı" />
        <asp:BoundField DataField="Directory" HeaderText="Kayıt Yeri" ReadOnly="true" />
        <asp:BoundField DataField="fName" HeaderText="Film Adı" ReadOnly="true"/>
        <asp:BoundField DataField="uName" HeaderText="Yükleyen Kullanıcı" ReadOnly="true"/>
        <asp:CommandField ShowEditButton="true" ButtonType="Button" ControlStyle-CssClass="btn btn-primary btn-block" />
        <asp:CommandField ShowDeleteButton="true" ButtonType="Button" ControlStyle-CssClass="btn btn-primary btn-block" />
        </Columns>
        </asp:GridView>
    </div>

    <!-- Bir de film ekleyebilsin yeter ! -->
    <div class="table table-responsive table-condensed table-hover scroll" id="div_filmekle" runat="server" visible="false">
        <h4 class="dropdown-header"><asp:Label runat="server" ID="lbl_film"></asp:Label></h4>
        <asp:Table runat="server" ID="tbl_film">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="Film ADI  &nbsp"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" ID="txt_filmAdi" placeholder="Film Adı..." Text=""></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Button ID="btn_film" runat="server" 
                        Text="Filmi Ekle" CssClass="btn btn-danger" OnClick="btn_film_Click"/>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
    <!-- Alt Yazıyı unutmuşum özür dilerim -->
    <div class="table table-responsive table-condensed table-hover scroll" id="div_altyaziekle" runat="server" visible="true">
        <h4 class="dropdown-header"><asp:Label runat="server" ID="lbl_altyazi"></asp:Label></h4>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:SqlDataSource ID="data_filmList" runat="server" ConnectionString="<%$ ConnectionStrings:Oracle %>" ProviderName="System.Data.OracleClient" SelectCommand="SELECT FilmsID, Name FROM Films ORDER BY Name"></asp:SqlDataSource>
        
        <asp:Table runat="server" ID="tbl_altyazi">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="Altyazı Adı &nbsp"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" ID="txt_altyazi_altyaziAdi" placeholder="Altyazı Adı..." Text=""></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" ID="lbl_dosyasec" Text="Dosya Seç"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:FileUpload ID="fu_altyazi_filmkaynak" runat="server" />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="Film Adı &nbsp"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList ID="ddl_altyazi_filmAdi" runat="server" 
                        DataSourceID="data_filmList" DataTextField="Name" DataValueField="FilmsID"></asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Button ID="btn_altyazi" runat="server" 
                        Text="Altyazı Ekle" CssClass="btn btn-danger" OnClick="btn_altyazi_Click"/>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        </div>
    <div class="table table-responsive table-condensed table-hover scroll" id="div_bilgilerim" runat="server" visible="true">
        <h4 class="dropdown-header"><asp:Label runat="server" ID="lbl_bilgilerim"></asp:Label></h4>
        <asp:Table runat="server" ID="tbl_bilgilerim">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="Kullanıcı No"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" ID="txt_bilgilerim_id" ReadOnly="true" Enabled="false" Text=""></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="İsim"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" ID="txt_bilgilerim_isim" Text=""></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="Şifre"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" ID="txt_bilgilerim_sifre" TextMode="Password" Text=""></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Button ID="btn_bilgilerim_guncelle" runat="server" 
                        Text="Bilgilerimi Güncelle" CssClass="btn btn-danger" OnClick="btn_bilgilerim_guncelle_Click"/>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
</asp:Content>
