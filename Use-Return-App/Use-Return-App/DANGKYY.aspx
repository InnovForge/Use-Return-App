<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DANGKYY.aspx.cs" Inherits="Use_Return_App.DANGKYY" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Đăng ký tài khoản</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background: #f1f1f1;
        }

        .form-container {
            max-width: 400px;
            margin: 60px auto;
            padding: 25px;
            background: #fff;
            border-radius: 15px;
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
        }

        .form-container h2 {
            text-align: center;
            margin-bottom: 20px;
        }

        .form-control {
            width: 100%;
            padding: 10px;
            margin-bottom: 12px;
            border: 1px solid #ccc;
            border-radius: 6px;
        }

        .btn-primary {
            background-color: #007bff;
            color: white;
            border: none;
            padding: 10px;
            width: 100%;
            border-radius: 6px;
            cursor: pointer;
        }

        .btn-primary:hover {
            background-color: #0056b3;
        }

        .message-label {
            display: block;
            margin-bottom: 12px;
            text-align: center;
            color: red;
        }
    </style>
</head>
<body>
<form id="form1" runat="server" autocomplete="off">
    <div class="form-container">
        <h2>Đăng ký tài khoản</h2>
        <asp:Label ID="lblMessage" runat="server" CssClass="message-label" />
        <asp:TextBox ID="txtHoTen" runat="server" CssClass="form-control" Placeholder="Họ tên" />
        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Placeholder="Email" />
        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" Placeholder="Mật khẩu" />
        <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" TextMode="Password" Placeholder="Xác nhận mật khẩu" />
        <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" Placeholder="Số điện thoại" />
        <asp:DropDownList ID="ddlVaiTro" runat="server" CssClass="form-control">
            <asp:ListItem Text="-- Chọn vai trò --" Value="0" />
            <asp:ListItem Text="Người dùng" Value="1" />
            <asp:ListItem Text="Quản trị" Value="2" />
        </asp:DropDownList>
        <asp:Button ID="btnRegister" runat="server" Text="Đăng ký" CssClass="btn-primary" OnClick="btnRegister_Click" />
    </div>
</form>

</body>
</html>
