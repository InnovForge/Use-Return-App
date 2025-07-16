<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="Use_Return_App.Checkout" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Thanh Toán</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <link href="Content/bootstrap.min.css" rel="stylesheet" />

    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.13.1/font/bootstrap-icons.min.css" />

    <link href="Content/styles.css" rel="stylesheet" />

    <script src="Scripts/bootstrap.bundle.min.js"></script>

    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/swiper@11/swiper-bundle.min.css" />

    <script src="https://cdn.jsdelivr.net/npm/swiper@11/swiper-bundle.min.js"></script>
    <style>
        .text-price {
            color: rgb(255, 66, 78);
            font-weight: bold;
        }

        .lazy-img {
            background-color: #eee;
            min-height: 70px;
            display: block;
            object-fit: cover;
            border-radius: 6px;
            transition: opacity 0.3s ease;
        }
        input[type=number]::-webkit-inner-spin-button,
input[type=number]::-webkit-outer-spin-button {
    -webkit-appearance: none;
    margin: 0;
}

/* Firefox */
input[type=number] {
    -moz-appearance: textfield;
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <nav class="navbar bg-body-tertiary">
                <div class="container-fluid" style="max-width: 1280px">
                    <a class="navbar-brand fs-4 text-primary"><i class="bi bi-gem"></i> <span class="px-1">|</span>Thanh Toán</a>
                </div>
            </nav>
            <div style="max-width: 1280px; margin: 0 auto" class="p-2">
                <div class="border border-1 d-flex gap-2 rounded-2 p-1 align-items-center align-items-center">

                    <div class="" style="width: 70px; height: 70px">
                        <asp:Image ID="img" runat="server" CssClass="w-100 h-100 object-fit-cover lazy-img" />
                    </div>

                    <div>
                        <asp:Label ID="lblTieuDe" runat="server" CssClass="fw-bold fs-5" />
                        <div class="hstack gap-3 align-items-center align-items-center">
                            <div class="hstack gap-1 align-items-center align-items-center">
                                <div>Giá thuê</div>
                                <asp:Label ID="lblGiaMoiNgay" runat="server" CssClass="text-price" />
                            </div>
                            <div class="hstack gap-1 align-items-center align-items-center">
                                <div>Tiền cọc</div>
                                <asp:Label ID="lblTienCoc" runat="server" CssClass="text-success fw-bold" />
                            </div>

                        </div>

                    </div>
                </div>

                <div class="border-1 border mt-3 rounded-2 p-1">
                    <p class="fw-bold">Chọn phương thức thanh toán</p>

                    <asp:Repeater ID="rptPaymentMethods" runat="server">
                        <ItemTemplate>
                            <div class="form-check d-flex align-items-center mb-2">
                                <input class="form-check-input me-2"
                                    type="radio"
                                    name="paymentMethod"
                                    id='<%# Eval("Id") %>'
                                    value='<%# Eval("Id") %>'
                                    <%# Container.ItemIndex == 0 ? "checked='checked'" : "" %> />
                                <label class="form-check-label" for='<%# Eval("Id") %>'>
                                    <div>
                                        <img src='<%# Eval("ImageUrl") %>' alt='<%# Eval("Name") %>' style="height: 24px;" class="me-2 " />
                                        <span class="payment-name fs-6"><%# Eval("Name") %></span><br />
                                        <small class="text-muted"><%# Eval("Description") %></small>
                                    </div>
                                </label>
                            </div>
                        </ItemTemplate>


                    </asp:Repeater>

                </div>
                <div>
                </div>
                <div class="mt-3">

                    <label class="fw-bold mb-1">Số ngày thuê</label>
                    <div class="input-group" style="max-width:140px;">
                        <button class="btn btn-outline-secondary" type="button" onclick="decreaseDays()"><i class="bi bi-dash"></i></button>
                        <asp:TextBox ID="txtSoNgayThue" runat="server" CssClass="form-control text-center" Text="1" onkeyup="updateTotal()" onchange="updateTotal()" />
                        <button class="btn btn-outline-secondary" type="button" onclick="increaseDays()"><i class="bi bi-plus"></i></button>
                    </div>

                    <div class="mt-2">
    <label class="fw-bold">Tổng tiền thuê:</label>
    <span id="tongTienThue" class="text-price fs-4">0 đ</span>
</div>
                
                    <asp:Button ID="Button1" runat="server" Text="Xác nhận" CssClass="btn btn-primary btn-lg mt-2" OnClick="Button1_Click" />
                </div>

            </div>
        </div>
    </form>
</body>
</html>
<script>
    const giaMoiNgay = <%= lblGiaMoiNgay.Text.Replace(".", "").Replace("đ", "") %>;
    const giaCoc = <%= lblTienCoc.Text.Replace(".", "").Replace("đ", "") %>;

    function getSoNgay() {
        let val = parseInt(document.getElementById("<%= txtSoNgayThue.ClientID %>").value);
        return isNaN(val) || val < 1 ? 1 : val;
    }

    function updateTotal() {
        const days = getSoNgay();
        const total = days * (giaMoiNgay + giaCoc);
        document.getElementById("tongTienThue").innerText = total.toLocaleString("vi-VN") + " đ";
    }

    function increaseDays() {
        let input = document.getElementById("<%= txtSoNgayThue.ClientID %>");
        input.value = getSoNgay() + 1;
        updateTotal();
    }

    function decreaseDays() {
        let input = document.getElementById("<%= txtSoNgayThue.ClientID %>");
        const current = getSoNgay();
        if (current > 1) input.value = current - 1;
        updateTotal();
    }

    window.onload = updateTotal;
</script>

