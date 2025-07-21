<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="404.aspx.cs" Inherits="Use_Return_App._404" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="d-flex flex-column align-items-center justify-content-center gap-3 " style="height: calc(100vh - 60px - 1rem);">
    <div>
        <h1 class="text-center">Ôi hỏng</h1>
        <p>404 - Không tìm thấy trang</p>
    </div>
    <a href="/" class="text-center btn btn-primary">Quay lại trang chủ</a>
</div>

</asp:Content>
