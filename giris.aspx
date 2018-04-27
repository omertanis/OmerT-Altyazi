<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="giris.aspx.cs" Inherits="bootstrapWeb.giris" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <meta charset="utf-8"/>
        <link href="css/styleLogin.css" rel='stylesheet' type='text/css' />
        <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="copy-right">
            <asp:Label runat="server" ID="lbl_slogan" Text="We got Company !" style="font-family:'Footlight MT';color:ActiveCaption;font-size:25px"></asp:Label>
            <br />
            <asp:Image runat="server" ID="img_logo" ImageUrl="~/images/logo.png" Height="25px"/>
        </div>  
<div class="main" style="background-image:url(images/transparent.png); background-repeat:repeat; padding:10px 10px 10px 10px; border-radius:15px;">
    <div style="text-align:center;">
        <h2 style="font-size:large;color:yellow;font-family:Calibri;"><b>Giriş Yap</b></h2>
    </div>
		   <div class="lable-2">
               <asp:Label runat="server"><span style="color:white">E-Posta</span></asp:Label>
               <asp:TextBox ID="txt_eposta" CssClass="txt-lt" runat="server" TextMode="SingleLine"></asp:TextBox>
               <br />
               <asp:Label runat="server"><span style="color:white">Şifre</span></asp:Label>
               <asp:TextBox ID="txt_sifre" CssClass="txt-lt" runat="server" TextMode="Password"></asp:TextBox>
		   </div>
		   <div class="submit">
			   <asp:Button ID="btn_giris" runat="server" Text="Giriş Yap !" style="font-size:initial" OnClick="btn_giris_Click" Width="49%" />
               <asp:Button ID="btn_kayit" runat="server" Text="Kayıtta Olabilirsin :)" style="margin-right:2%; font-size:initial"  Width="49%" OnClick="btn_kayit_Click"  />
		   </div>
		   <div class="clear"> </div>
    		</div>
		 <!-----start-copyright--www.w3layouts.com-->
   		<div class="copy-right">
			<p>
                <asp:Label ID="lblRespond" runat="server"></asp:Label>
            </p>
        </div>
		</form>
</body>
</html>
