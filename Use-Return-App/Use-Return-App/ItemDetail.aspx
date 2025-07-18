<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ItemDetail.aspx.cs" Inherits="Use_Return_App.Item" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .container-swiper {
            position: relative;
            aspect-ratio: 4 / 5;
            max-width: 480px;
            width: 100%;
        }


        .swiper {
            width: 100%;
            height: 100%;
        }

        .swiper-slide {
            text-align: center;
            font-size: 18px;
            background: #444;
            display: flex;
            justify-content: center;
            align-items: center;
            border-radius: 7px;
        }

            .swiper-slide img {
                display: block;
                width: 100%;
                height: 100%;
                object-fit: cover;
                border-radius: 7px;
            }


        .swiper {
            width: 100%;
            height: 300px;
            margin-left: auto;
            margin-right: auto;
            border-radius: 7px;
        }

        .swiper-slide {
            background-size: cover;
            background-position: center;
        }

        .mySwiper2 {
            height: 80%;
            width: 100%;
        }

        .mySwiper {
            height: 20%;
            box-sizing: border-box;
            padding: 10px 0;
        }

            .mySwiper .swiper-slide {
                width: 25%;
                height: 100%;
                opacity: 0.4;
            }

            .mySwiper .swiper-slide-thumb-active {
                opacity: 1;
            }

        .swiper-slide img {
            display: block;
            width: 100%;
            height: 100%;
            object-fit: cover;
        }

        .text-price {
            color: rgb(255, 66, 78);
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />--%>

    <!-- Swiper -->
    <div class="d-flex w-100">

        <div class="container-swiper">
            <div style="--swiper-navigation-color: #fff; --swiper-pagination-color: #fff" class="swiper mySwiper2">
                <div class="swiper-wrapper">
                    <asp:Repeater ID="rptMainImages" runat="server">
                        <ItemTemplate>
                            <div class="swiper-slide">
                                <img src='<%# Eval("DuongDanAnh") %>' />
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div class="swiper-button-next"></div>
                <div class="swiper-button-prev"></div>
            </div>
            <div thumbsslider="" class="swiper mySwiper">
                <div class="swiper-wrapper">
                    <asp:Repeater ID="rptThumbImages" runat="server">
                        <ItemTemplate>
                            <div class="swiper-slide">
                                <img src='<%# Eval("DuongDanAnh") %>' />
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>


            <p>
                <span class="fw-bold fs-4">Mô tả: </span>
                <br />
                <asp:Literal ID="litMoTa" runat="server" />
            </p>
        </div>
        <div class="ms-5 flex-fill">
            <asp:Label ID="lblTieuDe" runat="server" CssClass="fw-bold fs-4" />
            <div class="d-flex gap-3 bg-body-tertiary p-2 rounded-2">


                <div class="hstack gap-2 ">
                    <span>Giá thuê</span>
                    <div>
                        <asp:Label ID="lblGiaMoiNgay" runat="server" CssClass="text-price fw-bold fs-3" />
                        <span class="text-price">/ngày</span>
                    </div>
                </div>
                <div class="hstack gap-2">
                    <span>Tiền cọc</span>
                    <asp:Label ID="lblTienCoc" runat="server" CssClass="text-success fw-bold fs-4" />
                </div>
            </div>


            <%--   <div class="border p-2 rounded-2 d-flex justify-content-between align-items-lg-center mb-2">
                <div class="d-flex gap-3 justify-content-center">
                    <div class="" style="width: 48px; height: 48px">
                        <img class="w-100 h-100 object-fit-cover rounded-circle" src="https://placehold.co/600x400/green/white?text=avatar" />
                    </div>
                    <div class="d-flex flex-column gap-1">
                        <p class="p-0 m-0 fw-bold">ngtuonghy</p>
                        <p class="p-0 m-0 text-secondary fs-6">5 bài đăng</p>
                    </div>
                </div>
                <div class="d-flex gap-2 align-items-center justify-content-center">
                    <button class="btn btn-warning d-flex gap-2 align-items-center justify-content-center">
                        <i class="bi bi-telephone"></i>
                        <div>Gọi điện</div>
                    </button>
                    <button class="btn btn-info d-flex gap-2 align-items-center justify-content-center">
                        <i class="bi bi-chat"></i>
                        <div>Nhắn tin</div>
                    </button>
                </div>
            </div>--%>
            <div class="d-flex gap-2 align-items-center justify-content-start mt-2">
                <button class="btn btn-warning d-flex gap-2 align-items-center justify-content-center">
                    <i class="bi bi-telephone"></i>
                    <div>Gọi điện</div>
                </button>
                <a id="lnkMessage" runat="server" class="btn btn-info d-flex gap-2 align-items-center justify-content-center">
                    <i class="bi bi-chat"></i>
                    <div>Nhắn tin</div>
                </a>
            </div>
            <hr />
            <div class="d-flex gap-2 justify-content-end">
                <button onclick="addToCart('P001')" class="btn btn-outline-primary  btn-lg" type="button">
                    <i class="bi bi-bookmark-plus"></i>
                </button>

                <asp:LinkButton
                    ID="btnThueNgay"
                    runat="server"
                    CssClass="btn btn-primary btn-lg"
                    OnClick="btnThueNgay_Click">
    Thuê Ngay
                </asp:LinkButton>



            </div>
        </div>

    </div>
    <script>

        function addToCart(productId) {
            PageMethods.AddToCart(productId, function (newTotal) {
                document.getElementById("CartBadge").innerText = newTotal > 99 ? "99+" : newTotal;
            });
        }

        var swiper = new Swiper(".mySwiper", {
            spaceBetween: 10,
            slidesPerView: 4,
            freeMode: true,
            watchSlidesProgress: true,
        });
        var swiper2 = new Swiper(".mySwiper2", {
            spaceBetween: 10,
            navigation: {
                nextEl: ".swiper-button-next",
                prevEl: ".swiper-button-prev",
            },
            thumbs: {
                swiper: swiper,
            },
        });
    </script>
</asp:Content>
