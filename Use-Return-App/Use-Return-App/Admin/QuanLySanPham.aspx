<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Dashboard.Master" AutoEventWireup="true" CodeBehind="QuanLySanPham.aspx.cs" Inherits="Use_Return_App.Admin.QuanLySanPham" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
     <Columns>
         <asp:BoundField DataField="MaDoDung" HeaderText="Mã Đồ Dùng" />
         <asp:BoundField DataField="MaDanhMuc" HeaderText="Mã Danh Mục" />
         <asp:BoundField DataField="TieuDe" HeaderText="Tiêu Đề" />
         <asp:BoundField DataField="MoTa" HeaderText="Mô Tả" />
         <asp:TemplateField HeaderText="Ảnh Đồ Dùng">
             <ItemTemplate>
                 <asp:Image ID="Image1" runat="server" Height="150px" ImageUrl='<%# Eval("AnhDoDung") %>' Width="130px" />
             </ItemTemplate>
         </asp:TemplateField>
         <asp:BoundField DataField="GiaMoiNgay" HeaderText="Gía Mỗi Ngày" />
         <asp:BoundField DataField="SoLuong" HeaderText="Số Lượng" />
         <asp:BoundField DataField="TrangThai" HeaderText="Trạng Thái" />
         <asp:BoundField DataField="NgayTao" HeaderText="Ngày Tạo" />
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
