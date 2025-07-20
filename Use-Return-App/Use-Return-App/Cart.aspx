<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="Use_Return_App.Cart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <style>
        .item-card {
            border: 1px solid #ccc;
            border-radius: 8px;
            padding: 10px;
            margin: 10px;
            width: 300px;
            float: left;
        }
        .item-title {
            font-weight: bold;
            font-size: 18px;
        }
        .clear { clear: both; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div>
            <h2>Danh sách đồ dùng đã lưu</h2>
            <asp:Repeater ID="rptCart" runat="server">
                <ItemTemplate>
                    <div class="item-card">
                        <div class="item-title"><%# Eval("TieuDe") %></div>
                        <div>Mô tả: <%# Eval("MoTa") %></div>
                        <div>Giá mỗi ngày: <%# Eval("GiaMoiNgay") %> VND</div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <div class="clear"></div>
        </div>
</asp:Content>