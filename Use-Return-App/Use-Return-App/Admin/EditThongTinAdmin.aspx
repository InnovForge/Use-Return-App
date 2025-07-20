<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Dashboard.Master" AutoEventWireup="true" CodeBehind="EditThongTinAdmin.aspx.cs" Inherits="Use_Return_App.Admin.EditThongTinAdmin" %>
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

       <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" ShowFooter="True" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" ForeColor="Black">
           <Columns>
               <asp:TemplateField HeaderText="Mã Người Dùng">

                   <%-- <EditItemTemplate>
                   <asp:TextBox ID="EditMaNguoiDung" runat="server" Text='<%# Bind("MaNguoiDung") %>'></asp:TextBox>
               </EditItemTemplate>--%>
                   <FooterTemplate>
                       <asp:TextBox ID="txtMaNguoiDung" runat="server" Visible="False"></asp:TextBox>
                   </FooterTemplate>
                   <ItemTemplate>
                       <asp:Label ID="LabelMaNguoiDung" runat="server" Text='<%# Bind("MaNguoiDung") %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Họ Tên">

                   <EditItemTemplate>
                       <asp:TextBox ID="EditHoTen" runat="server" Text='<%# Bind("HoTen") %>'></asp:TextBox>
                   </EditItemTemplate>
                   <FooterTemplate>
                       <asp:TextBox ID="txtHoTen" runat="server"></asp:TextBox>
                   </FooterTemplate>
                   <ItemTemplate>
                       <asp:Label ID="LabelHoTen" runat="server" Text='<%# Bind("HoTen") %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Ảnh Đại Diện">
                   <EditItemTemplate>
                       <asp:Image ID="imgAvatarEdit" runat="server"
                           ImageUrl='<%# ResolveUrl("~/ImageUsers/" + Eval("AnhDaiDien")) %>'
                           Width="130px" Height="150px" /><br />
                       <asp:FileUpload ID="fuAvatarEdit" runat="server" />
                       <asp:HiddenField ID="hfOldAvatar" runat="server"
                           Value='<%# Eval("AnhDaiDien") %>' />
                   </EditItemTemplate>
                   <FooterTemplate>
                       <asp:FileUpload ID="fuAvatar" runat="server" />
                   </FooterTemplate>
                   <ItemTemplate>
                       <asp:Image ID="Image1" runat="server" Height="150px" ImageUrl='<%# "~/ImageUsers/" + Eval("AnhDaiDien") %>' Width="130px" />
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Email">
                   <EditItemTemplate>
                       <asp:TextBox ID="EditEmail" runat="server" Text='<%# Bind("Email") %>'></asp:TextBox>
                   </EditItemTemplate>
                   <FooterTemplate>
                       <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                   </FooterTemplate>
                   <ItemTemplate>
                       <asp:Label ID="LabelEmail" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Password">

                   <%-- <EditItemTemplate>
                   <asp:TextBox ID="EditMatKhau" runat="server" Text='<%# Bind("MatKhauHash") %>'></asp:TextBox>
               </EditItemTemplate>--%>
                   <FooterTemplate>
                       <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
                   </FooterTemplate>
                   <ItemTemplate>
                       <span title='<%# Eval("MatKhauHash") %>'>
                           <%# ShortHash(Eval("MatKhauHash")) %>
                       </span>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Phone">
                   <EditItemTemplate>
                       <asp:TextBox ID="EditPhone" runat="server" Text='<%# Bind("SoDienThoai") %>'></asp:TextBox>
                   </EditItemTemplate>
                   <FooterTemplate>
                       <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
                   </FooterTemplate>
                   <ItemTemplate>
                       <asp:Label ID="LabelPhone" runat="server" Text='<%# Bind("SoDienThoai") %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:BoundField DataField="NgayTao" HeaderText="Ngày Đăng Ký" />
               <asp:BoundField DataField="DangHoatDong" HeaderText="Hoạt Động" />

               <asp:TemplateField HeaderText="Tùy Chọn" ShowHeader="False">
                   <EditItemTemplate>
                       <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update" Text="Update"></asp:LinkButton>
                       &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                   </EditItemTemplate>
                   <FooterTemplate>
                       <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Thêm" />
                   </FooterTemplate>
                   <ItemTemplate>
                       <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit"></asp:LinkButton>
                       &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete"></asp:LinkButton>
                   </ItemTemplate>
               </asp:TemplateField>

           </Columns>
           <FooterStyle BackColor="#CCCCCC" />
           <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
           <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
           <RowStyle BackColor="White" />
           <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
           <SortedAscendingCellStyle BackColor="#F1F1F1" />
           <SortedAscendingHeaderStyle BackColor="Gray" />
           <SortedDescendingCellStyle BackColor="#CAC9C9" />
           <SortedDescendingHeaderStyle BackColor="#383838" />
       </asp:GridView>
   </div>
</asp:Content>

