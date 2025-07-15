<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DANGNHAP.aspx.cs" Inherits="Use_Return_App.DANGNHAP" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Đăng nhập</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background: #f1f1f1;
        }

        .login-container {
            max-width: 400px;
            margin: 60px auto;
            padding: 25px;
            background: #fff;
            border-radius: 15px;
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
        }

        .login-container h2 {
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
        <div class="login-container">
            <h2>Đăng nhập</h2>
            <asp:Label ID="lblMessage" runat="server" CssClass="message-label" />
            <asp:TextBox ID="txtTenDangNhap" runat="server" CssClass="form-control" Placeholder="Tên đăng nhập" />
            <asp:TextBox ID="txtMatKhau" runat="server" CssClass="form-control" TextMode="Password" Placeholder="Mật khẩu" />
            <asp:Button ID="btnLogin" runat="server" Text="Đăng nhập" CssClass="btn-primary" OnClick="btnLogin_Click" />
        </div>
    </form>
</body>
</html>
