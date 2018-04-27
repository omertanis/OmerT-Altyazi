<%@ Page Title="" Language="C#" MasterPageFile="~/MasterIndex.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="bootstrapWeb.index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <asp:Label runat="server" ID="lbl_baslik" CssClass="sub-header text-center" Text="<h2>Film Listesi</h2>">
            </asp:Label>
            <asp:Label ID="lbl_alert"  CssClass="alert alert-info center-block" runat="server"></asp:Label>
        <div class="table-responsive scroll" id="div_film" runat="server" visible="true">
        <asp:GridView ID="grid_filmList" runat="server"
             CssClass="table  table-hover table-condensed table-bordered table-striped"
            AutoGenerateSelectButton="True"
            SelectedRowStyle-BackColor="LightGreen" OnSelectedIndexChanged="grid_filmList_SelectedIndexChanged">
        </asp:GridView>
        </div>

        <div class="table-responsive scroll" id="div_altyazi" runat="server" visible="true">
         <asp:GridView ID="grid_altyaziList" runat="server"
             CssClass="table  table-hover table-condensed table-bordered table-striped"
            AutoGenerateSelectButton="True"
            SelectedRowStyle-BackColor="LightGreen" OnSelectedIndexChanged="grid_altyaziList_SelectedIndexChanged">
        </asp:GridView>
        </div>

</asp:Content>
