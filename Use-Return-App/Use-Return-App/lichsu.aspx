<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="lichsu.aspx.cs" Inherits="Use_Return_App.lichsu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container py-4">
        <h3>Lịch sử thuê của bạn</h3>
        <asp:GridView ID="gvLichSu" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
            <Columns>
                <asp:BoundField DataField="TieuDe" HeaderText="Tên đồ dùng" />
                <asp:BoundField DataField="NgayBatDau" HeaderText="Từ ngày" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:BoundField DataField="NgayKetThuc" HeaderText="Đến ngày" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:BoundField DataField="SoNgay" HeaderText="Số ngày" />
                <asp:BoundField DataField="TongTien" HeaderText="Tổng tiền (VNĐ)" DataFormatString="{0:N0}" />
                <asp:BoundField DataField="TrangThai" HeaderText="Trạng thái" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
