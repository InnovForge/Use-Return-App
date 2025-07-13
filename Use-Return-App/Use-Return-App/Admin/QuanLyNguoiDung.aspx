<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Dashboard.Master" AutoEventWireup="true" CodeBehind="QuanLyNguoiDung.aspx.cs" Inherits="Use_Return_App.Admin.QuanLyNguoiDung" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Height="262px" OnRowCommand="GridView1_RowCommand" Width="904px">
        <Columns>
            <asp:BoundField DataField="MaNguoiDung" HeaderText="Mã Người Dùng" />
            <asp:BoundField DataField="HoTen" HeaderText="HoTen" />
            <asp:TemplateField HeaderText="Ảnh Đại Diện">
                <ItemTemplate>
                    <asp:Image ID="Image1" runat="server" Height="150px" ImageUrl='<%# Eval("AnhDaiDien" %>' Width="130px" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Email" HeaderText="Email" />
            <asp:BoundField DataField="MatKhauHash" HeaderText="Password" />
            <asp:BoundField DataField="SoDienThoa" HeaderText="Phone" />
            <asp:BoundField DataField="NgayTao" HeaderText="Ngày Đăng Ký" />
            <asp:BoundField DataField="DangHoatDong" HeaderText="Hoạt Động" />
            <asp:TemplateField HeaderText="Action">
                <ItemTemplate>
                    <asp:Button ID="Button2" runat="server" Text="Sửa" CommandName="Sua" CommandArgument='<%# Eval("UserID") %>' />
                    &nbsp;
                    <asp:Button ID="Button3" runat="server" Text="Xóa" CommandName="Xoa" CommandArgument='<%# Eval("UserID") %>'   />
                    <br />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
