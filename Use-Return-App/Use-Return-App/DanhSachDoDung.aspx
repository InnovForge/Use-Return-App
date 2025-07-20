<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DanhSachDoDung.aspx.cs" Inherits="Use_Return_App.DanhSachDoDung" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Danh sách đồ dùng</title>
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
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Danh sách đồ dùng</h2>
            <asp:Repeater ID="rptDoDung" runat="server">
                <ItemTemplate>
                    <div class="item-card">
                        <div class="item-title"><%# Eval("TieuDe") %></div>
                        <div>Mô tả: <%# Eval("MoTa") %></div>
                        <div>Giá mỗi ngày: <%# Eval("GiaMoiNgay") %> VND</div>
                        <asp:Button ID="btnLuu" runat="server" Text="🔖 Lưu"
                            CommandArgument='<%# Eval("MaDoDung") %>'
                            OnCommand="btnLuu_Command" />
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <div class="clear"></div>
        </div>
    </form>
</body>
</html>
