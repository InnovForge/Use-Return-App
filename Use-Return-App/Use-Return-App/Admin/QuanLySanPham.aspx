<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/QuanLyDanhMuc.Master" AutoEventWireup="true" CodeBehind="QuanLySanPham.aspx.cs" Inherits="Use_Return_App.Admin.QuanLySanPham" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
    <Columns>
        <asp:BoundField DataField="MaDanhMuc" HeaderText="Mã Danh Mục" />
        <asp:BoundField DataField="MaDoDung" HeaderText="Mã Đồ Dùng" />
        <asp:BoundField DataField="TieuDe" HeaderText="Tiêu Đề" />
        <asp:BoundField DataField="GiaMoiNgay" HeaderText="Gía Mỗi Ngày" />
        <asp:TemplateField HeaderText="Chi Tiết">
            <EditItemTemplate>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Button ID="ButtonXemChiTiet" runat="server" CommandName="XemChiTiet" CommandArgument='<%# Eval("MaDoDung") %>' Text="Xem Chi Tiết" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
</asp:Content>
