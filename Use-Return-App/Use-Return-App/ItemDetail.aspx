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
        }

            .swiper-slide img {
                display: block;
                width: 100%;
                height: 100%;
                object-fit: cover;
            }


        .swiper {
            width: 100%;
            height: 300px;
            margin-left: auto;
            margin-right: auto;
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />

    <!-- Swiper -->
    <div class="d-flex w-100">

        <div class="container-swiper">
            <div style="--swiper-navigation-color: #fff; --swiper-pagination-color: #fff" class="swiper mySwiper2">
                <div class="swiper-wrapper">
                    <div class="swiper-slide">
                        <img src="https://swiperjs.com/demos/images/nature-1.jpg" />
                    </div>
                    <div class="swiper-slide">
                        <img src="https://swiperjs.com/demos/images/nature-2.jpg" />
                    </div>
                    <div class="swiper-slide">
                        <img src="https://swiperjs.com/demos/images/nature-3.jpg" />
                    </div>
                    <div class="swiper-slide">
                        <img src="https://swiperjs.com/demos/images/nature-4.jpg" />
                    </div>
                    <div class="swiper-slide">
                        <img src="https://swiperjs.com/demos/images/nature-5.jpg" />
                    </div>
                    <div class="swiper-slide">
                        <img src="https://swiperjs.com/demos/images/nature-6.jpg" />
                    </div>
                    <div class="swiper-slide">
                        <img src="https://swiperjs.com/demos/images/nature-7.jpg" />
                    </div>
                    <div class="swiper-slide">
                        <img src="https://swiperjs.com/demos/images/nature-8.jpg" />
                    </div>
                    <div class="swiper-slide">
                        <img src="https://swiperjs.com/demos/images/nature-9.jpg" />
                    </div>
                    <div class="swiper-slide">
                        <img src="https://swiperjs.com/demos/images/nature-10.jpg" />
                    </div>
                </div>
                <div class="swiper-button-next"></div>
                <div class="swiper-button-prev"></div>
            </div>
            <div thumbsslider="" class="swiper mySwiper">
                <div class="swiper-wrapper">
                    <div class="swiper-slide">
                        <img src="https://swiperjs.com/demos/images/nature-1.jpg" />
                    </div>
                    <div class="swiper-slide">
                        <img src="https://swiperjs.com/demos/images/nature-2.jpg" />
                    </div>
                    <div class="swiper-slide">
                        <img src="https://swiperjs.com/demos/images/nature-3.jpg" />
                    </div>
                    <div class="swiper-slide">
                        <img src="https://swiperjs.com/demos/images/nature-4.jpg" />
                    </div>
                    <div class="swiper-slide">
                        <img src="https://swiperjs.com/demos/images/nature-5.jpg" />
                    </div>
                    <div class="swiper-slide">
                        <img src="https://swiperjs.com/demos/images/nature-6.jpg" />
                    </div>
                    <div class="swiper-slide">
                        <img src="https://swiperjs.com/demos/images/nature-7.jpg" />
                    </div>
                    <div class="swiper-slide">
                        <img src="https://swiperjs.com/demos/images/nature-8.jpg" />
                    </div>
                    <div class="swiper-slide">
                        <img src="https://swiperjs.com/demos/images/nature-9.jpg" />
                    </div>
                    <div class="swiper-slide">
                        <img src="https://swiperjs.com/demos/images/nature-10.jpg" />
                    </div>
                </div>
            </div>


            <p>
                <span class="fw-bold fs-4">Mô tả: </span>
                <br />
                Tivi Samsung QLED 4K 65 inch QA65Q70D là sự kết hợp hoàn hảo giữa thiết kế tinh tế và công nghệ tiên tiến, mang đến cho người dùng trải nghiệm giải trí đỉnh cao. Với màn hình 65 inch rộng lớn và độ phân giải 4K sắc nét, tivi này không chỉ đáp ứng nhu cầu xem phim, chơi game mà còn làm nổi bật không gian sống. Được tích hợp nhiều tiện ích thông minh như tìm kiếm bằng giọng nói, điều khiển qua ứng dụng di động,... Smart Tivi Samsung chắc chắn sẽ là sự lựa chọn tuyệt vời cho những ai yêu thích công nghệ hiện đại và tiện nghi.

Viền màn hình mỏng với trải nghiệm xem mở rộng
Smart Tivi Samsung QLED 4K 65 inch QA65Q70D nổi bật với thiết kế đơn giản, thanh lịch, tạo điểm nhấn cho không gian sống. Với viền màn hình mỏng, tivi mang đến trải nghiệm xem mở rộng, giúp người dùng tận hưởng trọn vẹn nội dung mà không bị xao nhãng bởi các yếu tố xung quanh.

Kích thước 65 inch rất lý tưởng cho những không gian có diện tích vừa và lớn như phòng khách, phòng họp hay phòng làm việc, tạo cảm giác hiện đại và phong cách.

Chân đế tivi được chế tác từ kim loại chắc chắn, đảm bảo độ bền và ổn định khi đặt trên các mặt phẳng khác nhau. Đặc biệt, người dùng có thể tháo rời chân đế để dễ dàng treo tivi lên tường, tiết kiệm không gian và tạo ra một góc nhìn tối ưu hơn.
            </p>
        </div>
        <div class="ms-5 flex-fill">
            <h3 class="fs-4 m-0 p-0">Samsung Smart TV QLED 65 inch 4K QA65Q70D</h3>
            <p class="text-primary fw-bold fs-5 m-0 p-0"><i class="bi bi-clock"></i> Thời gian thuê: <span>1 Ngày</span></p>
            <p class="text-danger fw-bold fs-3 m-0 p-0">10.000 đ</p>
            <div class="border p-2 rounded-2 d-flex justify-content-between align-items-lg-center mb-2">
                <div class="d-flex gap-3 justify-content-center">
                    <div class="" style="width: 48px; height: 48px">
                        <img class="w-100 h-100 object-fit-cover rounded-circle" src="https://placehold.co/600x400/green/white?text=avatar" />
                    </div>
                    <div class="d-flex flex-column gap-1">
                        <p class="p-0 m-0 fw-bold">ngtuonghy</p>
                        <p class="p-0 m-0 text-secondary fs-6">5 bài đăng</p>
                    </div>
                </div>
                    <div class="d-flex gap-2">
                        <button class="btn btn-warning">
                          <i class="bi bi-telephone"></i>  Gọi điện
                        </button>
                             <button class="btn btn-info">
                         <i class="bi bi-chat"></i>   Nhắn tin
                        </button>
                    </div>
            </div>

            <div class="d-flex gap-2 justify-content-end">
                <button onclick="addToCart('P001')" class="btn btn-outline-primary  btn-lg" type="button">
                    <i class="bi bi-bookmark-plus"></i>
                </button>


                <button type="button" class="btn btn-primary btn-lg">Mua Ngay</button>
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
