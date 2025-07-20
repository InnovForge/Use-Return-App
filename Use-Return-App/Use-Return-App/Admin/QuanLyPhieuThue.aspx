<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Dashboard.Master" AutoEventWireup="true" CodeBehind="QuanLyPhieuThue.aspx.cs" Inherits="Use_Return_App.Admin.QuanLyDonHang" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
    .grid-scroll {
        max-height: 600px; 
        overflow-y: auto; 
        overflow-x: auto; 
        border: 1px solid #999;
    }

        .grid-scroll table {
            width: 100%;
        }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="grid-scroll">
       <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  BorderStyle="Solid"  BorderWidth="1px"  GridLines="Both"  OnRowCommand="GridView1_RowCommand">
        <Columns>
            <asp:BoundField DataField="MaPhieuThue" HeaderText="Mã Phiếu Thuê" />
            <asp:BoundField DataField="MaDoDung" HeaderText="Mã Đồ Dùng" />
            <asp:BoundField DataField="MaNguoiThue" HeaderText="Mã Người Thuê" />
           <%-- <asp:BoundField DataField="NgayBatDau" HeaderText="Ngày Bắt Đầu" />
            <asp:BoundField DataField="NgayKetThuc" HeaderText="Ngày Kết Thúc" />
            <asp:BoundField DataField="SoNgay" HeaderText="Số Ngày" />
            <asp:BoundField DataField="TongTien" HeaderText="Tổng Tiền" />--%>
            <asp:BoundField DataField="TrangThai" HeaderText="Trạng Thái" />
            <asp:BoundField DataField="NgayTao" HeaderText="Ngày Tạo" />
            <asp:TemplateField HeaderText="Action">
                <ItemTemplate>
                <asp:Button ID="Button1" runat="server" Text="Xem Chi Tiết" CommandName="ChiTiet" CommandArgument='<%# Eval("MaPhieuThue") %>' OnClick="Button1_Click" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
            </div>
</asp:Content>
