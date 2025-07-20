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
    <div class="container">
        <h2>Danh sách đồ dùng đã lưu</h2>
        
        <!-- Hiển thị thông tin debug -->
        <div id="debugContainer" runat="server" class="alert alert-info mb-3"></div>
        
        <asp:Label ID="lblEmptyCart" runat="server" 
            CssClass="alert alert-info" 
            Visible="false" />
            
        <div class="row">
            <asp:Repeater ID="rptCart" runat="server">
                <ItemTemplate>
                    <div class="col-md-4 mb-4">
                        <div class="card item-card">
                            <div class="card-body">
                                <h5 class="card-title"><%# Eval("TieuDe") %></h5>
                                <p class="card-text"><%# Eval("MoTa") %></p>
                                <p class="text-price">
                                    <%# Eval("GiaMoiNgay", "{0:N0}") %> VND/ngày
                                </p>
<asp:Button runat="server" 
    Text="Xóa khỏi giỏ" 
    CssClass="btn btn-danger"
    CommandArgument='<%# Eval("MaDoDung") %>' 
    OnClick="btnRemove_Click" />
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>