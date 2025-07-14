<%@ Page Title="Đăng nhập" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Use_Return_App.login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .login-form {
            max-width: 400px;
            margin: 40px auto;
            padding: 20px;
            border: 1px solid #ccc;
            border-radius: 12px;
            background: #f9f9f9;
        }

        .form-control {
            width: 100%;
            padding: 8px;
            margin-bottom: 12px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="login-form">
        <h2>Đăng nhập</h2>
        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" />
        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Placeholder="Email" />
        <asp:TextBox ID="txtMatKhau" runat="server" CssClass="form-control" TextMode="Password" Placeholder="Mật khẩu" />
        <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-primary form-control" Text="Đăng nhập" OnClick="btnLogin_Click" />
    </div>
</asp:Content>
