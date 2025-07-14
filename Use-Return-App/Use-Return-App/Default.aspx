<%@ Page Title="" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Default.aspx.cs" Inherits="Use_Return_App.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <div id="card-container" style="display: grid; grid-template-columns: repeat(auto-fill, minmax(200px, 1fr)); grid-gap: 0.5rem;">
    </div>
    <div id="loading" style="text-align: center; margin: 1rem; font-weight: bold;">Đang tải...</div>

    <script>
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
                        <div class="card" style="width: 200px; margin: 10px;">
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
