<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Chat.aspx.cs" Inherits="Use_Return_App.Chat" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
    <%--    <script src="<%= ResolveClientUrl("Scripts/jquery-1.6.4.min.js") %>"></script>
    <script src="<%= ResolveClientUrl("Scripts/jquery.signalR-2.4.3.min.js") %>"></script>
    <script src="<%= ResolveClientUrl("/signalr/hubs") %>"></script>--%>
    <style>
        .unread-badge {
            width: 20px;
            height: 20px;
            font-size: 10px;
            padding: 0;
        }
    </style>
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
                        <a id="<%# Eval("UserId") %>" href="/messages/u/<%# Eval("UserId") %>" class="d-flex align-items-center justify-content-between text-decoration-none text-reset p-2 rounded-2">
                            <div class="hstack align-items-center justify-content-center gap-1">


                                <div style="width: 50px; height: 50px; position: relative">
                                    <img src='<%# string.IsNullOrEmpty(Eval("AnhDaiDien") as string) ? "https://placehold.co/600x400/green/white?text=avatar" : Eval("AnhDaiDien") %>'
                                        class="w-100 h-100 object-fit-cover rounded-circle" />

                                    <%# Convert.ToBoolean(Eval("TrangThai")) 
        ? "<span style='background-color: forestgreen; width: 12px; height: 12px; position: absolute; bottom: 0; right: 0; border: 2px solid white; border-radius: 50%; display: inline-block;'><span class='visually-hidden'>Online</span></span>" 
        : "" %>
                                </div>


                                <div class="ms-2">
                                    <strong class="fw-bold"><%# Eval("HoTen") %></strong><br />
                                    <%# (int)Eval("UnreadCount") > 0
           ? $"<span class='text-dark fw-bold conversation-last-message text-nowrap text-truncate d-block' style='max-width: 320px;'>{Eval("LastMessage")}</span>"
           : $"<span class='text-muted conversation-last-message text-nowrap text-truncate d-block' style='max-width: 220px;'>{Eval("LastMessage")}</span>" %>
                                </div>
                            </div>
                            <div class="d-flex flex-column align-items-end justify-content-between">
                                <span style="font-size: 12px" class="text-muted small"><%# HumanizeTime(Eval("LastTime")) %></span>


                                <%# (int)Eval("UnreadCount") > 0 ? 
$"<span id='unread_{Eval("UserId")}' class='badge bg-danger rounded-circle d-flex justify-content-center align-items-center unread-badge'>{((int)Eval("UnreadCount") > 99 ? "99+" : Eval("UnreadCount"))}</span>" 
: "" %>
                            </div>

                        </a>
                    </ItemTemplate>
                </asp:Repeater>
            </div>

        </div>

        <!-- Chat Area -->
        <div class="border flex-grow-1 rounded-2" style="flex-basis: 65%; height: calc(100vh - 60px - 1rem);">

            <div class="w-100 d-flex flex-column" style="height: calc(100vh - 60px - 1rem);" runat="server" id="chatArea" visible="false">
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
                <div id="messageContainer" class="flex-grow-1 overflow-y-auto p-2 d-flex flex-column gap-2"></div>


                <!-- Message Input -->
                <div class="input-group p-2 align-items-end">
                    <span class="input-group-text"><i class="bi bi-geo-alt"></i></span>
                    <textarea id="messageInput" class="form-control auto-resize" rows="1" style="resize: none; max-height: 150px; overflow-y: auto;"></textarea>
                    <span id="sendMessageBtn" class="input-group-text"><i class="bi bi-send"></i></span>
                </div>
            </div>

            <div runat="server" visible="false" id="Qc" class="w-100 p-2" style="height: calc(100vh - 60px - 1rem);">
                <h3 class="text-center">Chào mừng bạn đến với chat</h3>
            </div>
        </div>
    </div>

    <script>
        const messageInput = document.querySelector('.auto-resize');
        messageInput.addEventListener('input', () => {
            messageInput.style.height = 'auto';
            messageInput.style.height = `${Math.min(messageInput.scrollHeight, 150)}px`;
        });

        const currentUserId = '<%= Session["UserID"] %>';
        const chatPartnerId = window.location.pathname.split('/').pop();


        function onGlobalMessageReceived({ userId, userName, avatar, message }) {

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
                PageMethods.MarkMessagesAsRead(currentUserId, chatPartnerId);
                $('#messageContainer').append(messageHtml);
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

        //       $.connection.hub.qs = { 'userId': currentUserId };

        $(function () {
            const chatHub = $.connection.chatHub;

            function sendMessage() {
                const message = $('#messageInput').val();
                if (!message.trim()) return;

                chatHub.server.sendToUser(chatPartnerId, currentUserId, message);
                $('#messageInput').val('');

                const container = document.getElementById("messageContainer");
                setTimeout(function () {
                    container.scrollTop = container.scrollHeight;
                }, 100);
            }

            $('#sendMessageBtn').bind('click', function () {
                sendMessage();
            });

            $('#messageInput').bind('keydown', function (e) {
                if (e.keyCode === 13 && !e.shiftKey) {
                    e.preventDefault();
                    sendMessage();
                }
            });
        });



        let skipMessages = 0;
        const messagesBatchSize = 15;
        let isLoadingMessages = false;

        let isAllMessagesLoaded = false;

        PageMethods.set_path("/Chat.aspx");

        function loadMessages(isLoadOld = true) {
            if (isLoadingMessages) return;
            isLoadingMessages = true;

            const container = document.getElementById("messageContainer");
            const previousScrollHeight = container.scrollHeight;

            PageMethods.LoadMessages(currentUserId, chatPartnerId, skipMessages, messagesBatchSize, function (messages) {
                if (!messages || messages.length === 0) {
                    isLoadingMessages = false;
                    isAllMessagesLoaded = true;
                    return;
                }

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

                    if (isLoadOld) {
                        container.prepend(messageDiv);
                    } else {
                        container.appendChild(messageDiv);
                    }
                });

                if (isLoadOld) {
                    const newHeight = container.scrollHeight;
                    container.scrollTop = newHeight - previousScrollHeight;
                } else {
                    container.scrollTop = container.scrollHeight;
                }

                if (messages.length < messagesBatchSize) {
                    isAllMessagesLoaded = true;
                }
                skipMessages += messages.length;
                isLoadingMessages = false;
            });
        }

        document.getElementById("messageContainer").addEventListener("scroll", function () {
            const container = this;
            if (container.scrollTop === 0 && !isLoadingMessages) {
                const previousScrollHeight = container.scrollHeight;

                loadMessages();

                setTimeout(() => {
                    const newScrollHeight = container.scrollHeight;
                    container.scrollTop = newScrollHeight - previousScrollHeight;
                }, 100);
            }
        });



        window.onload = function () {
            const path = window.location.pathname;
            const regex = /^\/messages\/u\/[a-fA-F0-9-]+$/;

            if (regex.test(path)) {
                loadMessages(false);
                PageMethods.MarkMessagesAsRead(currentUserId, chatPartnerId);
                const badge = document.getElementById(`unread_${chatPartnerId}`);
                if (badge) badge.style.display = "none";

                const activeConversation = document.getElementById(chatPartnerId);
                if (activeConversation) activeConversation.classList.add("bg-primary-subtle");


                setTimeout(() => {
                    const container = document.getElementById("messageContainer");
                    container.scrollTop = container.scrollHeight;
                }, 300);
            }
        };

    </script>
</asp:Content>
