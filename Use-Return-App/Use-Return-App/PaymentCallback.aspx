<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentCallback.aspx.cs" Inherits="Use_Return_App.PaymentCallback" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <title>Kiểm tra giao dịch</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <link href="Content/bootstrap.min.css" rel="stylesheet" />

    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.13.1/font/bootstrap-icons.min.css" />

    <link href="Content/styles.css" rel="stylesheet" />

    <script src="Scripts/bootstrap.bundle.min.js"></script>

    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/swiper@11/swiper-bundle.min.css" />

    <script src="https://cdn.jsdelivr.net/npm/swiper@11/swiper-bundle.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
             <nav class="navbar bg-body-tertiary">
                <div class="container-fluid" style="max-width: 1280px">
                    <a class="navbar-brand fs-4 text-primary"><i class="bi bi-gem"></i> <span class="px-1">|</span>Kiểm tra giao dịch</a>
                </div>
            </nav>
            <div>
                <div>
                    <div class="hstack gap-3 justify-content-center  align-items-center">
                       <i class="bi bi-check-circle fs-1 text-success"></i>   <i class="bi bi-x-circle fs-1 text-danger"></i>
                <%--        <asp:Label ID="txtMes" runat="server" CssClass="fs-3 text-success" Text="Thanh toán thành công"></asp:Label>--%>
                    </div>
                  <div class="container">
    <div class="header clearfix">
        
        <h3 class="text-muted">Kết quả thanh toán</h3>
    </div>
    <div ">
         <div runat="server" id="displayMsg"></div>
    </div> 
    <div">
         <div runat="server" id="displayTmnCode"></div>
    </div>
     <div >
         <div runat="server" id="displayTxnRef"></div>
    </div> 
     <div >
         <div runat="server" id="displayVnpayTranNo"></div>
    </div> 
    <div >
         <div runat="server" id="displayAmount"></div>
    </div
    <div >
         <div runat="server" id="displayBankCode"></div>
    </div> 
</div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
