<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="Use_Return_App.Search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .dodung-card {
            display: flex;
            gap: 16px;
            padding: 16px;
            border: 1px solid #ccc;
            border-radius: 12px;
            margin-bottom: 16px;
            background-color: #f9f9f9;
            transition: box-shadow 0.2s;
        }

        .dodung-card:hover {
            box-shadow: 0 0 10px rgba(0,0,0,0.15);
        }

        .dodung-info {
            flex: 1;
        }

        .dodung-title {
            font-size: 1.2rem;
            font-weight: 600;
            color: #0d6efd;
        }

        .dodung-status {
            font-size: 0.9rem;
            font-weight: 500;
        }

        .badge-status {
            font-size: 0.8rem;
        }

        .dodung-meta {
            font-size: 0.9rem;
            color: #555;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3 class="mb-3">🔍 Kết quả tìm kiếm:</h3>
    <asp:Literal ID="ltKetQua" runat="server" />
    <asp:Repeater ID="rptKetQua" runat="server">
        <ItemTemplate>
            <div class="dodung-card">
                <div class="dodung-info">
                    <div class="dodung-title"><%# Eval("TieuDe") %></div>
                    <div class="dodung-meta mb-1"><%# Eval("MoTa") %></div>

                    <div class="dodung-meta mb-1">
                        💵 Giá mỗi ngày: <strong><%# String.Format("{0:N0} đ", Eval("GiaMoiNgay")) %></strong> | 
                        📦 Số lượng: <%# Eval("SoLuong") %>
                    </div>

                    <div class="dodung-meta mb-1">
                        🛠️ Tình trạng: <%# Eval("TinhTrang") %>
                    </div>

                    <div class="dodung-meta mb-1">
                        🕒 Ngày đăng: <%# Eval("NgayTao", "{0:dd/MM/yyyy}") %>
                    </div>

                    <div class="dodung-status mt-1">
                        Trạng thái: 
                        <span class='badge 
                            <%# Eval("TrangThai").ToString() == "Available" ? "bg-success" : 
                                    Eval("TrangThai").ToString() == "Unavailable" ? "bg-warning text-dark" : "bg-danger" %>'>
                            <%# Eval("TrangThai") %>
                        </span>
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
