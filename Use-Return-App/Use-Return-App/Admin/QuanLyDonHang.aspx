<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Dashboard.Master" AutoEventWireup="true" CodeBehind="QuanLyDonHang.aspx.cs" Inherits="Use_Return_App.Admin.QuanLyDonHang" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Height="262px"  Width="904px">
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
                    <asp:Button ID="Button2" runat="server" Text="Sửa" CommandName="Sua" CommandArgument='<%# Eval("MaPhieuThue") %>' />
                    &nbsp;
                <asp:Button ID="Button3" runat="server" Text="Xóa" CommandName="Xoa" CommandArgument='<%# Eval("MaPhieuThue") %>' />
                    &nbsp;
                <asp:Button ID="Button1" runat="server" Text="Xem Chi Tiết" CommandName="ChiTiet" CommandArgument='<%# Eval("MaPhieuThue") %>' />
                    <br />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
