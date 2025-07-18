<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Chat.aspx.cs" Inherits="Use_Return_App.Chat" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
    <script src="<%= ResolveClientUrl("Scripts/jquery-1.6.4.min.js") %>"></script>
    <script src="<%= ResolveClientUrl("Scripts/jquery.signalR-2.4.3.min.js") %>"></script>
    <script src="<%= ResolveClientUrl("/signalr/hubs") %>"></script>
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="d-flex gap-2">
        <!-- Conversations Sidebar -->
        <div class="flex-grow-1" style="flex-basis: 35%">
            <div class="input-group mb-3">
                <span class="input-group-text"><i class="bi bi-search"></i></span>
                <input type="text" class="form-control" placeholder="Search...">
            </div>

            <div id="conversationsList">
                <asp:Repeater ID="rptConversations" runat="server">
                    <ItemTemplate>
                        <a id="<%# Eval("UserId") %>" href="/messages/u/<%# Eval("UserId") %>" class="d-flex align-items-center mb-3 text-decoration-none text-reset">
                            <div style="width: 40px; height: 40px">
                                <img src='<%# string.IsNullOrEmpty(Eval("AnhDaiDien") as string) ? "https://placehold.co/600x400/green/white?text=avatar" : Eval("AnhDaiDien") %>' class="w-100 h-100 object-fit-cover rounded-circle" />
                            </div>
                            <div class="ms-2">
                                <strong><%# Eval("HoTen") %></strong><br />
                                <small class="text-muted conversation-last-message"><%# Eval("LastMessage") %></small>
                            </div>
                        </a>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>

        <!-- Chat Area -->
        <div class="border flex-grow-1 rounded-2 d-flex flex-column" style="flex-basis: 65%; height: calc(100vh - 60px - 1rem);">

            <!-- Chat Header -->
            <div class="d-flex gap-2 align-items-center bg-body w-100 shadow-sm p-2 rounded-top-2">
                <div style="width: 40px; height: 40px">
                    <img id="userAvatar" runat="server" class="w-100 h-100 object-fit-cover rounded-circle" />
                </div>
                <div class="d-flex flex-column gap-0">
                    <p id="userName" runat="server" class="p-0 m-0 fw-bold"></p>
                    <span id="userStatus" runat="server" class="text-secondary" style="font-size: 13px"></span>
                </div>
            </div>

            <!-- Chat Messages -->
            <div id="messageContainer" class="flex-grow-1 overflow-y-auto p-2 d-flex flex-column flex-column-reverse gap-2"></div>

            <!-- Message Input -->
            <div class="input-group p-2 align-items-end">
                <span class="input-group-text"><i class="bi bi-geo-alt"></i></span>
                <textarea id="messageInput" class="form-control auto-resize" rows="1" style="resize: none; max-height: 150px; overflow-y: auto;"></textarea>
                <span id="sendMessageBtn" class="input-group-text"><i class="bi bi-send"></i></span>
            </div>
        </div>
    </div>

    <script >
        const messageInput = document.querySelector('.auto-resize');
        messageInput.addEventListener('input', () => {
            messageInput.style.height = 'auto';
            messageInput.style.height = `${Math.min(messageInput.scrollHeight, 150)}px`;
        });

        const currentUserId = '<%= Session["UserID"] %>';
        const chatPartnerId = window.location.pathname.split('/').pop();

        $(function () {
            const chatHub = $.connection.chatHub;

            chatHub.client.receiveMessage = function ({ userId, userName, avatar, message }) {

                const isIncoming = userId !== currentUserId;
                const messageHtml = isIncoming
                    ? `<div class="d-flex justify-content-start align-items-end gap-2">
                            <div style="width: 28px; height: 28px">
                                <img class="w-100 h-100 object-fit-cover rounded-circle" src="${avatar}" />
                            </div>
                            <div class="bg-light rounded p-2">${message}</div>
                       </div>`
                    : `<div class="d-flex justify-content-end gap-2 mb-2">
                            <div class="bg-primary text-white rounded p-2">${message}</div>
                       </div>`;

                if (userId === currentUserId || userId === chatPartnerId) {
                    $('#messageContainer').prepend(messageHtml);
                }

                if (isIncoming) {
                    const $conversationItem = $(`#${userId}`);
                    if ($conversationItem.length) {
                        $conversationItem.find('.conversation-last-message').text(message);
                        $conversationItem.prependTo($conversationItem.parent());
                    } else {
                        const newConversation = `
                            <a href="/messages/u/${userId}" class="d-flex align-items-center mb-3 text-decoration-none text-reset">
                                <div style="width: 40px; height: 40px">
                                    <img src="${avatar}" class="w-100 h-100 object-fit-cover rounded-circle" />
                                </div>
                                <div class="ms-2">
                                    <strong>${userName}</strong><br />
                                    <small class="text-muted conversation-last-message">${message}</small>
                                </div>
                            </a>`;
                        $('#conversationsList').prepend(newConversation);
                    }
                }
            };

            $.connection.hub.qs = { 'userId': currentUserId };
            $.connection.hub.start().done(() => {
                $('#sendMessageBtn').click(() => {
                    const message = $('#messageInput').val();
                    chatHub.server.sendToUser(chatPartnerId, currentUserId, message);
                    $('#messageInput').val('');
                });
            });
        });

        let skipMessages = 0;
        const messagesBatchSize = 20;
        let isLoadingMessages = false;

        PageMethods.set_path("/Chat.aspx");

        function loadMessages() {
            if (isLoadingMessages) return;
            isLoadingMessages = true;

            const previousScrollHeight = document.getElementById("messageContainer").scrollHeight;

            PageMethods.LoadMessages(currentUserId, chatPartnerId, skipMessages, messagesBatchSize, function (messages) {
                const container = document.getElementById("messageContainer");
                messages.forEach(msg => {
                    const isMine = msg.SenderId === currentUserId;
                    const messageDiv = document.createElement("div");

                    messageDiv.className = isMine
                        ? "d-flex justify-content-end"
                        : "d-flex justify-content-start align-items-end gap-2";

                    messageDiv.innerHTML = isMine
                        ? `<div class="bg-primary text-white rounded p-2">${msg.Content}</div>`
                        : `<div style="width: 28px; height: 28px">
                               <img class="w-100 h-100 object-fit-cover rounded-circle" src="<%= ToUserAvatar %>" />
                           </div>
                           <div class="bg-light rounded p-2">${msg.Content}</div>`;

                    container.prepend(messageDiv);
                });

                container.scrollTop = container.scrollHeight - previousScrollHeight;
                skipMessages += messages.length;
                isLoadingMessages = false;
            });
        }

      window.onload = function () {
    const path = window.location.pathname;
    const regex = /^\/messages\/u\/[a-fA-F0-9-]+$/;

    if (regex.test(path)) {
        loadMessages();

        // Cuộn xuống cuối sau khi tải
        setTimeout(() => {
            const container = document.getElementById("messageContainer");
            container.scrollTop = container.scrollHeight;
        }, 300);
    }
};

    </script>
</asp:Content>
