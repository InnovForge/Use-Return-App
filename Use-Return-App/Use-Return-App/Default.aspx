<%@ Page Title="" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Default.aspx.cs" Inherits="Use_Return_App.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <style>

        .container-banner {
            height: 330px;
            width: auto;
            position: relative;
            
            
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

    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server" EnablePageMethods="true"></asp:ScriptManager>
      <div class="container-banner">
                <div class="swiper bannerSwiper">
                    <div class="swiper-wrapper">
                        <div class="swiper-slide">
                           <img src="https://placehold.co/600x400/orange/white?text=ngtuonghy" />
                        </div>
                        <div class="swiper-slide">
                             <img src="https://placehold.co/600x400/red/white?text=name" />
                        </div>
                        <div class="swiper-slide">
                             <img src="https://placehold.co/600x400/blue/white?text=banner-quảng-cáo" />
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

    <script>

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
                    container.insertAdjacentHTML("beforeend", `
                        <div class="card">
                            <img src="${card.ImageUrl}" class="card-img-top" alt="${card.Title}" />
                            <div class="card-body">
                                <h5 class="card-title">${card.Title}</h5>
                                <p class="card-text">${card.Description}</p>
                                <a href="${card.LinkUrl}" class="btn btn-primary">Go</a>
                            </div>
                        </div>
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
