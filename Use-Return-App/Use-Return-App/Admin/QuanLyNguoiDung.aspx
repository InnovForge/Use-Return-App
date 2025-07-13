<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Dashboard.Master" AutoEventWireup="true" CodeBehind="QuanLyNguoiDung.aspx.cs" Inherits="Use_Return_App.Admin.QuanLyNguoiDung" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Height="262px">
        <Columns>
            <asp:BoundField DataField="UserID" HeaderText="ID User" />
            <asp:BoundField DataField="FullName" HeaderText="Full Name" />
            <asp:BoundField DataField="UserName" HeaderText="Tên Đăng Nhập" />
            <asp:BoundField DataField="Email" HeaderText="Email" />
            <asp:BoundField DataField="PasswordHash" HeaderText="Password" />
            <asp:BoundField DataField="Phone" HeaderText="Phone" />
            <asp:BoundField DataField="RegisteredDate" HeaderText="Ngày Đăng Ký" />
            <asp:TemplateField HeaderText="Action">
                <ItemTemplate>
                    <asp:Button ID="Button1" runat="server" Text="Thêm" />
                    &nbsp;
                    <asp:Button ID="Button2" runat="server" Text="Sửa" />
                    &nbsp;
                    <asp:Button ID="Button3" runat="server" Text="Xóa" />
                    <br />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
