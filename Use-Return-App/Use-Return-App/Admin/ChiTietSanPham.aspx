<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/QuanLyDanhMuc.Master" AutoEventWireup="true" CodeBehind="ChiTietSanPham.aspx.cs" Inherits="Use_Return_App.Admin.ChiTietSanPham" %>

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
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowFooter="True" Width="874px" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating">
            <Columns>
                <asp:TemplateField HeaderText="Mã Đồ Dùng">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBoxEditMaDoDung" runat="server" Enabled="False" Text='<%# Bind("MaDoDung") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3MDD" runat="server" Text='<%# Bind("MaDoDung") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Tiêu Đề">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBoxEditTieuDe" runat="server" Text='<%# Bind("TieuDe") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="TextBoxAddTieuDe" runat="server"></asp:TextBox>
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("TieuDe") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Hình Ảnh">
                    <ItemTemplate>
                        <asp:Repeater ID="rptImages" runat="server" DataSource='<%# GetImages(Eval("MaDoDung").ToString()) %>' OnItemDataBound="rptImages_ItemDataBound" OnItemCommand="rptImages_ItemCommand">
                            <ItemTemplate>
                                <asp:Image ID="Image1" runat="server" Height="80px" Width="80px" />
                                <asp:CheckBox ID="chkDelete" runat="server" Text="Xóa" />
                                <asp:HiddenField ID="hfImageId" runat="server" Value='<%# Eval("MaHinh") %>' />
                            </ItemTemplate>
                        </asp:Repeater>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:FileUpload ID="fuNewImages" runat="server" AllowMultiple="true" />
                        <asp:Repeater ID="rptImagesEdit" runat="server" DataSource='<%# GetImages(Eval("MaDoDung").ToString()) %>'>
                            <ItemTemplate>
                                <img src='<%# Eval("DuongDanAnh") %>' width="80px" /><br />
                                <asp:CheckBox ID="chkDelete" runat="server" Text="Xóa" />
                                <asp:HiddenField ID="hfImageId" runat="server" Value='<%# Eval("MaHinh") %>' />
                            </ItemTemplate>
                        </asp:Repeater>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:FileUpload ID="fuAddImage" runat="server" AllowMultiple="true" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Mô Tả">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBoxEditMoTa" runat="server" Text='<%# Bind("MoTa") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="TextBoxAddMoTa" runat="server"></asp:TextBox>
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("MoTa") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Giá Mỗi Ngày">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBoxEditGiaMoiNgay" runat="server" Text='<%# Bind("GiaMoiNgay") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="TextBoxAddGiaMoiNgay" runat="server"></asp:TextBox>
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("GiaMoiNgay") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Tiền Cọc">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBoxEditTienCoc" runat="server" Text='<%# Bind("TienCoc") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="TextBoxAddTienCoc" runat="server"></asp:TextBox>
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("TienCoc") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Số Lượng">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBoxEditSoLuong" runat="server" Text='<%# Bind("SoLuong") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="TextBoxAddSoLuong" runat="server"></asp:TextBox>
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("SoLuong") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Trạng Thái">
                    <EditItemTemplate>
                        <asp:DropDownList ID="DropDownEditList" runat="server">
                            <asp:ListItem>Available</asp:ListItem>
                            <asp:ListItem>Deleted</asp:ListItem>
                            <asp:ListItem>Unavailable</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="DropDownAddList" runat="server">
                            <asp:ListItem>Available</asp:ListItem>
                            <asp:ListItem>Deleted</asp:ListItem>
                            <asp:ListItem>Unavailable</asp:ListItem>
                        </asp:DropDownList>
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("TrangThai") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Ngày Tạo">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("NgayTao") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("NgayTao") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Actions" ShowHeader="False">
                    <EditItemTemplate>
                        <asp:Button ID="Button1" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
                        &nbsp;<asp:Button ID="Button2" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:Button ID="ButtonThem" runat="server" Text="Thêm" OnClick="ButtonThem_Click" />
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:Button ID="Button1" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit" />
                        &nbsp;<asp:Button ID="Button2" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
