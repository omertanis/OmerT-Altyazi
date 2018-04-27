<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="kayitol.aspx.cs" Inherits="bootstrapWeb.kayitol" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <meta charset="utf-8"/>
<link href="css/styleLogin.css" rel='stylesheet' type='text/css' />
<meta name="viewport" content="width=device-width, initial-scale=1"/>
<script type="application/x-javascript"> addEventListener("load", function() { setTimeout(hideURLbar, 0); }, false); function hideURLbar(){ window.scrollTo(0,1); } </script>
<!--webfonts-->
<link href='http://fonts.googleapis.com/css?family=Oxygen:400,300,700' rel='stylesheet' type='text/css'/>
<!--//webfonts-->
</head>
<body>
    <div class="copy-right">
        <asp:Label runat="server" ID="lbl_slogan" Text="We got Company !" style="font-family:'Footlight MT';color:ActiveCaption;font-size:25px"></asp:Label>
        <br />
        <asp:Image runat="server" ID="img_logo" ImageUrl="~/images/logo.png" Height="25px"/>
    </div>
   <form id="form1" runat="server">
<div class="main" style="background-image:url(images/transparent.png); background-repeat:repeat; padding:10px 10px 10px 10px; border-radius:15px; margin-top:1px">
        <div  class="col_1_of_f span_1_of_f" style="font-size:large;">
            <h2>Kullanıcı Bilgilerin ile</h2>
        </div>
    <div class="lable-2" >
        <div>
            <asp:TextBox ID="txt_Isim" runat="server" placeholder="Adınız"></asp:TextBox>
        </div>
        <div>
            <asp:TextBox ID="txt_Email" runat="server"  ValidationGroup="email" placeholder="E-Mail Örneğin:(hede@hede.hede)"></asp:TextBox>
        </div>
    </div>

    <div class="lable">
        <div class="col_1_of_f span_1_of_2">	
            <asp:TextBox ID="txt_Sifre" runat="server"  ValidationGroup="sifre" placeholder="Şifre"></asp:TextBox>
        </div>
        <div class="col_1_of_f span_1_of_2">
            <asp:TextBox ID="txt_SifreTekrar" runat="server"  ValidationGroup="sifre" placeholder="Şifre Tekrar"></asp:TextBox>                              
        </div>
        <div class="clear"> 
            <asp:Label ID="lbl_user" runat="server" Text=""></asp:Label> 
        </div>
    </div>
    <h3>Kayıt ol tuşuna basarak <span class="term"><a href="#">Hizmet şartları ve Gizlilik politikasını</a> kabul etmiş olursun.</span></h3>
    <div class="submit">
        <asp:Button ID="btn_kaydol" runat="server" Text="Kayıt Ol" CssClass="submit" OnClick="btn_kaydol_Click" Width="49%"/>
        <asp:Button ID="btn_giris" runat="server" Text="Zaten Hesabım Var" CssClass="submit" style="margin-right:2%; font-size:initial"  Width="49%" OnClick="btn_giris_Click"  />
    </div>
    <div class="clear"> 
    <table>
        <tr>
            <td>
                <asp:RequiredFieldValidator ID="reqfieldEmail" runat="server"  
                        Font-Names="comic sans ms"
                        ValidationGroup="email"
                        ControlToValidate="txt_Email" ErrorMessage ="Email Giriniz" Display="Dynamic">
                </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:RequiredFieldValidator ID="reqfieldSifre" runat="server" 
                        Font-Names="comic sans ms"
                        ControlToValidate="txt_Sifre"
                        ValidationGroup="sifre"
                        ErrorMessage="Şifre Eksik" Display="Dynamic">
                </asp:RequiredFieldValidator>
            </td>
            </tr>
         <tr>
             <td>
                 <asp:CompareValidator ID="validatorSifre" runat="server" Font-Names="comic sans ms"
                        ControlToCompare="txt_Sifre"
                        ControlToValidate="txt_SifreTekrar"
                        ValidationGroup="sifre"
                        ErrorMessage="Şifreleriniz Eşleşmiyor" Display="Dynamic">
                 </asp:CompareValidator>
            </td>
         </tr>
        <tr>
            <td>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" 
                        runat="server" ErrorMessage="hede@hede.hede Şeklinde Eposta Gir"
                        ValidationGroup="email" ControlToValidate="txt_Email" Display="Dynamic" 
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                </asp:RegularExpressionValidator>
            </td>
        </tr>
        </table> 
    </div>
</div>
   </form><!-----//end-main---->
</body>
</html>
