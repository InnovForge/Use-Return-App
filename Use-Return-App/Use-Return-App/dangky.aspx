<%@ Page Title="Đăng ký" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="dangky.aspx.cs" Inherits="Use_Return_App.dangky" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .form-container {
            max-width: 400px;
            margin: 40px auto;
            padding: 20px;
            border: 1px solid #ccc;
            border-radius: 12px;
            background: #f9f9f9;
        }

        .form-container h2 {
            text-align: center;
        }

        .form-control {
            width: 100%;
            padding: 8px;
            margin-bottom: 12px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="form-container">
        <h2>Đăng ký tài khoản</h2>
        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" />
        <asp:TextBox ID="txtHoTen" runat="server" CssClass="form-control" Placeholder="Họ tên" />
        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Placeholder="Email" />
        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" Placeholder="Mật khẩu" />
        <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" Placeholder="Số điện thoại" />
        <asp:DropDownList ID="ddlVaiTro" runat="server" CssClass="form-control">
            <asp:ListItem Text="--Chọn vai trò--" Value="0" />
            <asp:ListItem Text="Người dùng" Value="1" />
            <asp:ListItem Text="Quản trị" Value="2" />
        </asp:DropDownList>
        <asp:Button ID="btnRegister" runat="server" Text="Đăng ký" CssClass="btn btn-primary form-control" OnClick="btnRegister_Click" />
    </div>
</asp:Content>
