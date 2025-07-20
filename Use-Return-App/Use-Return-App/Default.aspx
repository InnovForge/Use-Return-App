<%@ Page Title="" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Default.aspx.cs" Inherits="Use_Return_App.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <style>

        .container-banner {
            height: 310px;
            width: auto;
            position: relative;
            
            
        .swiper {
            width: 100%;
            height: 100%;
              border-radius: 7px;
        }

        .swiper-slide {
            text-align: center;
            font-size: 18px;
            background: #444;
            display: flex;
            justify-content: center;
            align-items: center;
            border-radius:6px;
           
        }

        .swiper-slide img {
                display: block;
                width: 100%;
                height: 100%;
                object-fit: cover;
               
        }

        .swiper {
            margin-left: auto;
            margin-right: auto;
        }

        }

            .lazy-img {
    background-color: #eee;
    min-height: 160px;
    display: block;
    object-fit: cover;
    border-radius: 6px 6px 0 0;
    transition: opacity 0.3s ease;
}
        

                .card {
    height: 100%; 
    display: flex;
    flex-direction: column;
}

.card-body.item {
    flex-grow: 1;
    display: flex;
    flex-direction: column;
    justify-content: space-between;
}

/* Cắt tiêu đề và mô tả */
.card-title.item {
   font-size: 1rem;
    font-weight: bold;
    overflow: hidden;
    display: -webkit-box;
    -webkit-box-orient: vertical;
    -webkit-line-clamp: 2; /* ✅ Hiển thị tối đa 2 dòng */
    line-height: 1.2;
    max-height: calc(1.2em * 2); /* giới hạn chiều cao dòng */
}

.card-text.description {
    flex-grow: 1;
    font-size: 0.875rem;
    overflow: hidden;
    text-overflow: ellipsis;
    display: -webkit-box;
    -webkit-line-clamp: 2; /* giới hạn 2 dòng */
    -webkit-box-orient: vertical;
}
            
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" >



      <div class="container-banner">
                <div class="swiper bannerSwiper">
                    <div class="swiper-wrapper">
                        <div class="swiper-slide">
                           <img src="https://placehold.co/600x400/orange/white?text=ngtuonghy" />
                        </div>
                        <div class="swiper-slide">
                             <img src="https://placehold.co/600x400/red/white?text=Thắng+cá+mập" />
                        </div>
                        <div class="swiper-slide">
                             <img src="https://placehold.co/600x400/blue/white?text=Nhật+blue" />
                        </div>
                         <div class="swiper-slide">
                             <img src="https://placehold.co/600x400/green/white?text=banner+quảng+cáo" />
                        </div>
                    <%--    <div class="swiper-slide">Slide 4</div>
                        <div class="swiper-slide">Slide 5</div>
                        <div class="swiper-slide">Slide 6</div>
                        <div class="swiper-slide">Slide 7</div>
                        <div class="swiper-slide">Slide 8</div>
                        <div class="swiper-slide">Slide 9</div>--%>
                    </div>
                    <div class="swiper-button-next"></div>
                    <div class="swiper-button-prev"></div>
                    <div class="swiper-pagination"></div>
                </div>
            </div>
    <div id="card-container" style="display: grid; grid-template-columns: repeat(auto-fill, minmax(200px, 1fr)); grid-gap: 0.5rem;" class="mt-2">
    </div>
    <div id="loading" style="text-align: center; margin: 1rem; font-weight: bold;">Đang tải...</div>

 <script type="text/javascript" >

    var swiper = new Swiper(".bannerSwiper", {
        slidesPerView: 1,
        spaceBetween: 30,
        loop: true,
          autoplay: {
        delay: 2500,
        disableOnInteraction: false,
      },
        pagination: {
            el: ".swiper-pagination",
            clickable: true,
        },
        navigation: {
            nextEl: ".swiper-button-next",
            prevEl: ".swiper-button-prev",
        },
    });


        let page = 0;
        let loading = false;

        function loadMore() {
            if (loading) return;
            loading = true;

            PageMethods.LoadCards(page, function (cards) {
                const container = document.getElementById("card-container");

                if (!cards || cards.length === 0) {
                    document.getElementById("loading").innerText = "Không còn dữ liệu.";
                    return;
                }

                cards.forEach(card => {
                    let imageUrl = "ImageDoDung/no-image.jpg";

                    if (card.DuongDanAnh) {
                        imageUrl = (card.DuongDanAnh.startsWith("http://") || card.DuongDanAnh.startsWith("https://"))
                            ? card.DuongDanAnh
                            : `/ImageDoDung/${card.DuongDanAnh}`;
                    }
                    container.insertAdjacentHTML("beforeend", `
<a class="text-decoration-none text-reset" href="${card.LinkItemDetail}" >
                        <div class="card-item card position-relative d-flex flex-cloumn">
                            <img src="${imageUrl}" class="card-img-top lazy-img" alt="${card.TieuDe}" />
                            <div class="card-body item p-2">

                                <h5 class="card-title item">${card.TieuDe}</h5>
                                <p class="card-text description">${card.MoTa}</p>
<p style="color: rgb(255, 66, 78);" class="card-text fw-bold">${Number(card.GiaMoiNgay).toLocaleString('vi-VN', { style: 'currency', currency: 'VND' })}</p>
<div class="card-footer-meta text-end mt-auto">
  <p class="card-text" style="font-size: .675rem">
    <small class="text-body-secondary">${card.NgayTaoText}</small>
  </p>
</div>

<div class="d-flex justify-content-end gap-2 position-absolute" style="top: .4rem; left: .4rem;">
                                <button onclick="event.stopPropagation(); event.preventDefault();" type="button" class="btn btn-light btn-sm">  <i class="bi bi-bookmark-plus"></i> </button>                    
</div>
                            </div>

                        </div>
</a>
                    `);
                });

                page++;
                loading = false;
            }, function (error) {
                console.error("Lỗi khi gọi WebMethod:", error.get_message());
                document.getElementById("loading").innerText = "Lỗi tải dữ liệu.";
                loading = false;
            });
        }

        window.addEventListener('scroll', function () {
            if ((window.innerHeight + window.scrollY) >= document.body.offsetHeight - 200) {
                loadMore();
            }
        });

        window.onload = loadMore;


 </script>

</asp:Content>
