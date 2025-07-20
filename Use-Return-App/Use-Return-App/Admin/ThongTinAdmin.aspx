<%@ Page Title="Thông Tin Quản Trị Viên" Language="C#" MasterPageFile="~/Admin/Dashboard.Master" AutoEventWireup="true" CodeBehind="ThongTinAdmin.aspx.cs" Inherits="Use_Return_App.Admin.ThongTinAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .admin-info-box {
            max-width: 800px;
            margin: auto;
            padding: 30px;
            background-color: #f8f8f8;
            border-radius: 15px;
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
            font-family: Arial, sans-serif;
        }

        .admin-info-box h2 {
            text-align: center;
            color: #333;
        }

        .admin-info {
            display: flex;
            align-items: center;
            justify-content: space-between;
            margin-top: 30px;
        }

        .admin-avatar {
            width: 150px;
            height: 150px;
            border-radius: 50%;
            object-fit: cover;
            border: 3px solid #ccc;
        }

        .admin-details {
            flex: 1;
            margin-left: 30px;
        }

        .admin-details table {
            width: 100%;
            font-size: 16px;
        }

        .admin-details td {
            padding: 10px 5px;
        }

        .label {
            font-weight: bold;
            width: 150px;
        }

        .edit-button {
            margin-top: 20px;
            text-align: center;
        }

        .btn-edit {
            padding: 10px 20px;
            background-color: #007bff;
            border: none;
            color: white;
            border-radius: 5px;
            cursor: pointer;
        }

        .btn-edit:hover {
            background-color: #0056b3;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="admin-info-box">
        <h2>Thông Tin Quản Trị Viên</h2>

        <div class="admin-info">
            <asp:Image ID="imgAvatar" runat="server" CssClass="admin-avatar" ImageUrl='<%# ResolveUrl("~/ImageUsers/" + Eval("AnhDaiDien")) %>'
 />
            <div class="admin-details">
                <table>
                    <tr>
                        <td class="label">Họ và tên:</td>
                        <td><asp:Label ID="lblHoTen" runat="server" Text="Nguyễn Văn Admin"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="label">Email:</td>
                        <td><asp:Label ID="lblEmail" runat="server" Text="admin@email.com"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="label">Số Điện Thoại</td>
                        <td><asp:Label ID="lblPhone" runat="server" Text="0905..."></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="label">Chức vụ:</td>
                        <td><asp:Label ID="lblRole" runat="server" Text="Quản trị viên"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="label">Ngày tạo:</td>
                        <td><asp:Label ID="lblCreatedDate" runat="server" Text="01/01/2024"></asp:Label></td>
                    </tr>
                </table>

                <div class="edit-button">
                    <asp:Button ID="btnEdit" runat="server" CssClass="btn-edit" Text="Chỉnh sửa thông tin" OnClick="btnEdit_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
