<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Chat.aspx.cs" Inherits="Use_Return_App.Chat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="<%= ResolveClientUrl("Scripts/jquery-1.6.4.min.js") %>"></script>
    <script src="<%= ResolveClientUrl("Scripts/jquery.signalR-2.4.3.min.js")  %>"></script>
    <script src="<%= ResolveClientUrl("/signalr/hubs") %>"></script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="d-flex gap-2">

        <div class="flex-grow-1" style="flex-basis: 35%">
            <div class="input-group mb-3">
                <span class="input-group-text"><i class="bi bi-search"></i></span>
                <input type="text" class="form-control" placeholder="Search...">
            </div>

            <div id="BoxConversations">

                <asp:Repeater ID="rptConversations" runat="server">
                    <ItemTemplate>
                        <a href="<%# Eval("UserId") %>" class="d-flex align-items-center mb-3 text-decoration-none text-reset">
                            <div style="width: 40px; height: 40px">

                                <img src='<%# string.IsNullOrEmpty(Eval("AnhDaiDien") as string) ? 
    "https://placehold.co/600x400/green/white?text=avatar" : Eval("AnhDaiDien") %>'
                                    class="w-100 h-100 object-fit-cover rounded-circle" />
                            </div>
                            <div class="ms-2">
                                <strong><%# Eval("HoTen") %></strong><br />
                                <small id="lastMsg" class="text-muted"><%# Eval("LastMessage") %></small>
                            </div>
                        </a>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>

        <div class="border flex-grow-1 rounded-2 d-flex flex-column" style="flex-basis: 65%; height: calc(100vh - 60px - 1rem);">

            <!-- Header -->
            <div class="d-flex gap-2 align-items-center bg-body w-100 shadow-sm p-2 rounded-top-2">
                <div style="width: 40px; height: 40px">
                    <img id="AvatarNguoiDung" runat="server" class="w-100 h-100 object-fit-cover rounded-circle" />
                </div>
                <div class="d-flex flex-column gap-0">
                    <p id="TenNguoiDung" runat="server" class="p-0 m-0 fw-bold"></p>
                    <span id="TrangThai" runat="server" class="text-secondary" style="font-size: 13px"></span>
                </div>
            </div>

            <div id="chatBox" class="flex-grow-1 overflow-y-auto p-2 d-flex flex-column flex-column-reverse gap-2">

                <%--     <!-- Tin nhắn của người khác (trái) -->
                <div class="d-flex justify-content-start align-items-end gap-2">
                    <div style="width: 28px; height: 28px">
                        <img class="w-100 h-100 object-fit-cover rounded-circle" src="https://placehold.co/600x400/green/white?text=avatar" />
                    </div>
                    <div class="bg-light rounded p-2">Xin chào!</div>
                </div>

                <!-- Tin nhắn của mình (phải) -->
                <div class="d-flex justify-content-end">
                    <div class="bg-primary text-white rounded p-2">Làm sao để giống Messenger?</div>
                </div>--%>
            </div>

            <!-- Chat input -->
            <div class="input-group p-2 align-items-end">

                <span class="input-group-text"><i class="bi bi-geo-alt"></i></span>

                <textarea id="txtMessage" class="form-control auto-resize" rows="1"
                    style="resize: none; max-height: 150px; overflow-y: auto;"></textarea>

                <span id="sendBtn" class="input-group-text"><i class="bi bi-send"></i></span>
            </div>

        </div>

    </div>
    <script type="text/javascript">

        const textarea = document.querySelector('.auto-resize');
        textarea.addEventListener('input', () => {
            textarea.style.height = 'auto';
            textarea.style.height = `${Math.min(textarea.scrollHeight, 150)}px`;
        });

        $(function () {

            var urlParts = window.location.pathname.split('/');
            const toUserId = urlParts[urlParts.length - 1];
            const fromUser =  '<%= Session["UserID"] %>';


            const chat = $.connection.chatHub;


            chat.client.receiveMessage = function (data) {
                const { userId, userName, avatar, message } = data
                const isMe = userId !== fromUser;
                const html = isMe
                    ? `   <div class="d-flex justify-content-start align-items-end gap-2">
                    <div style="width: 28px; height: 28px">
                        <img class="w-100 h-100 object-fit-cover rounded-circle" src=${avatar}/>
                    </div>
                    <div class="bg-light rounded p-2">${message}</div>
                </div>
`
                    : `<div class="d-flex justify-content-end gap-2 mb-2">
                        <div class="bg-primary text-white rounded p-2">${message}</div>
                   </div>`;

                $('#chatBox').prepend(html);

                if (isMe) {
                    const $target = $(`a[href='${userId}']`);
                    if ($target.length) {

                        $target.find('#lastMsg').text(message);
                        $target.prependTo($target.parent());
                    } else {


                        const newItem = $(`
            <a href="${userId}" class="d-flex align-items-center mb-3 text-decoration-none text-reset">
                <div style="width: 40px; height: 40px">
                    <img src="${avatar}" class="w-100 h-100 object-fit-cover rounded-circle" />
                </div>
                <div class="ms-2">
                    <strong>${userName}</strong><br />
                    <small id="lastMsg" class="text-muted">${message}</small>
                </div>
            </a>
        `);

                        $('#BoxConversations').prepend(newItem);;
                    }
                }

            }



            $.connection.hub.qs = { 'userId': fromUser };
            $.connection.hub.start().done(function () {
                $('#sendBtn').click(function () {
                    const message = $('#txtMessage').val();
                    chat.server.sendToUser(toUserId, fromUser, message);
                    $('#txtMessage').val('');
                });
            });
        });



        let skip = 0;
        const take = 20;

        let loading = false;
        PageMethods.set_path("/Chat.aspx");
        function loadMessages() {
            if (loading) return;
            loading = true;


            var urlParts = window.location.pathname.split('/');
            const toUserId = urlParts[urlParts.length - 1];
            const fromUser = '<%= Session["UserID"] %>';
            const currentUser = fromUser
            const otherUser = toUserId


            const prevHeight = document.getElementById("chatBox").scrollHeight;

            PageMethods.LoadMessages(currentUser, otherUser, skip, take, function (messages) {

                const container = document.getElementById("chatBox");
                for (let msg of messages) {
                    const div = document.createElement("div");
                    console.log(
                        "currentUser:", currentUser,
                        "\notherUser:", otherUser,
                        "\nSenderId:", msg.SenderId,
                        "\nReceiverId:", msg.ReceiverId,
                        "\nisMine:")

                    const isMine = msg.SenderId === currentUser
                    console.log(isMine)


                    div.className = (isMine)
                        ? "d-flex justify-content-end"
                        : "d-flex justify-content-start align-items-end gap-2";

                    if (isMine) {
                        div.innerHTML = `
                        <div class="bg-primary text-white rounded p-2">${msg.Content}</div>
                    `;

                    } else {
                        div.innerHTML = `
                        <div style="width: 28px; height: 28px">
                            <img class="w-100 h-100 object-fit-cover rounded-circle" src="<%= ToUserAvatar %>" />
                        </div>
                        <div class="bg-light rounded p-2">${msg.Content}</div>
                    `;
                    }

                    container.prepend(div);
                }

                // Giữ lại vị trí cuộn
                container.scrollTop = container.scrollHeight - prevHeight;

                skip += messages.length;
                loading = false;
            });
        }

        // Cuộn lên gần đầu thì load thêm
        //document.getElementById("chatBox").addEventListener("scroll", function () {
        //    if (this.scrollTop < 100) {
        //        loadMessages();
        //    }
        //});


        window.onload = () => {
            loadMessages();

            setTimeout(() => {
                const container = document.getElementById("chatBox");
                container.scrollTop = container.scrollHeight;
            }, 300);
        };

    </script>


</asp:Content>
