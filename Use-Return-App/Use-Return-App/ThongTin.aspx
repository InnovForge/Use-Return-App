<%@ Page Title="Thông tin người dùng" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="ThongTin.aspx.cs" Inherits="Use_Return_App.ThongTin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container py-4">
        <h3>Thông tin tài khoản</h3>

        <div style="display: flex; gap: 30px;">
            <asp:Image ID="imgAvatar" runat="server" Width="120px" Height="120px" Style="border-radius: 50%; border: 2px solid #ccc;" />

            <div>
                <p><b>Họ tên:</b> <asp:Label ID="lblHoTen" runat="server" /></p>
                <p><b>Email:</b> <asp:Label ID="lblEmail" runat="server" /></p>
                <p><b>SĐT:</b> <asp:Label ID="lblSoDienThoai" runat="server" /></p>
                <p><b>Ngày tạo:</b> <asp:Label ID="lblNgayTao" runat="server" /></p>
                <p><b>Trạng thái:</b> <asp:Label ID="lblTrangThai" runat="server" /></p>
            </div>
        </div>
    </div>
</asp:Content>
