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
                    <a class="navbar-brand fs-4 text-primary"><i class="bi bi-gem"></i><span class="px-1">|</span>Kiểm tra giao dịch</a>
                </div>
            </nav>
            <div>
                <div style="max-width: 600px; margin: 0 auto;" class="p-2">
                    <div class="hstack gap-2 justify-content-center align-items-center">
                        <i runat="server" id="iconSuccess" class="bi bi-check-circle fs-1 text-success" visible="false"></i>
                        <i runat="server" id="iconFail" class="bi bi-x-circle fs-1 text-danger" visible="false"></i>
                        <asp:Label ID="lbMessage" runat="server" Text="Giao dịch Không hợp lệ" CssClass="fs-3 fw-bold"></asp:Label>
                    </div>
                    <div class="fw-bold mt-2 d-flex align-items-start justify-content-around" visible="false" runat="server" id="errMsg"></div>
                    <div runat="server" id="containerTb" visible="false" class="vstack gap-1 justify-content-center align-items-center border border-1 mt-2">

                        <div class="hstack gap-2 justify-content-center align-items-lg-center">
                            <span class="fw-medium">Ngân hàng thanh toán: </span>
                            <div runat="server" id="displayBankCode"></div>
                        </div>



                        <div class="hstack gap-2 justify-content-center align-items-lg-center">
                            <span class="fw-medium">Mã giao dịch: </span>
                            <div runat="server" id="displayOrderId"></div>
                        </div>

                        <div class="hstack gap-2 justify-content-center align-items-lg-center">
                            <span class="fw-medium">Mã giao dịch tại VNPAY: </span>
                            <div runat="server" id="displayTxnRef"></div>
                        </div>

                        <div class="hstack gap-2 justify-content-center align-items-lg-center">
                            <span class="fw-medium">Số tiền thanh toán: </span>
                            <div runat="server" id="displayAmount"></div>
                        </div>
                    </div>
                    <div class="mt-2">
                  <a href="/" class="btn btn-primary">Quay về</a>

                    </div>
                       
                </div>
             
            </div>
            
        </div>
    </form>
</body>
</html>
