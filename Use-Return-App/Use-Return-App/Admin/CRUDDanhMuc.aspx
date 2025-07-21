<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/QuanLyDanhMuc.Master" AutoEventWireup="true" CodeBehind="CRUDDanhMuc.aspx.cs" Inherits="Use_Return_App.Admin.CRUDDanhMuc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="MaDanhMuc" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" ShowFooter="True">
        <Columns>
            <asp:TemplateField HeaderText="Mã Danh Mục">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBoxEditMaDanhMuc" runat="server" Text='<%# Bind("MaDanhMuc") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("MaDanhMuc") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Tên Danh Mục">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBoxEditTenDanhMuc" runat="server" Text='<%# Bind("TenDanhMuc") %>'></asp:TextBox>
                    <br />
                    <asp:Label ID="LabelLoi" runat="server" Text="Label"></asp:Label>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="TextBoxAddTenDanhMuc" runat="server" Text='<%# Bind("MaDanhMuc") %>'></asp:TextBox>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("TenDanhMuc") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False">
                <EditItemTemplate>
                    <asp:Button ID="Button1" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
                    &nbsp;<asp:Button ID="Button2" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:Button ID="ButtonAdd" runat="server" OnClick="ButtonAdd_Click" Text="Thêm" />
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Button ID="Button1" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit" />
                    &nbsp;<asp:Button ID="Button2" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
</asp:GridView>
</asp:Content>
