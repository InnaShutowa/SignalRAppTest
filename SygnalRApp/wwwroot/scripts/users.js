
var selectedUserName = "";

function getSelected(userId) {
    $.getJSON("/Messenger/GetMessages", { "recipientUserId": userId }, getMessages);
}

function getMessages(data) {
    var elem = document.getElementById("selectedUser");
    var avatarElem = document.getElementById("selectedUserAvatar");

    elem.innerText = data.recipientName;
    avatarElem.src = data.recipientJpegPhoto;

    selectedUserName = data.recipientUserName;

    if (data.messages !== null && data.messages !== undefined) {
        dropElementsInside("chatroom");

        for (var i = 0; i < data.messages.length; i++) {
            if (data.messages[i].isOutgoing) {
                addOutgoingMess(data.messages[i].sendDate, data.messages[i].text, "");
            } else {
                addIncomingMess(data.messages[i].sendDate, data.messages[i].text, data.recipientName, data.recipientJpegPhoto);
            }
        }
    }
}

function addIncomingMess(time, text, userName, photo) {
    var elem = document.createElement("div");
    elem.className = "chat-message-left pb-4";

    var partOneElem = document.createElement("div");

    var imgElem = document.createElement("img");
    imgElem.className = "rounded-circle mr-1";
    imgElem.width = 40;
    imgElem.height = 40;
    imgElem.src = photo !== null && photo !== undefined && photo !== "" ? photo : "https://bootdey.com/img/Content/avatar/avatar1.png";

    var timeElem = document.createElement("div");
    timeElem.className = "text-muted small text-nowrap mt-2";
    timeElem.appendChild(document.createTextNode(time));

    partOneElem.appendChild(imgElem);
    partOneElem.appendChild(timeElem);

    var partTwoElem = document.createElement("div");
    partTwoElem.className = "flex-shrink-1 bg-light rounded py-2 px-3 mr-3";

    var fromElem = document.createElement("div");
    fromElem.className = "font-weight-bold mb-1";
    fromElem.appendChild(document.createTextNode(userName));

    partTwoElem.appendChild(fromElem);
    partTwoElem.appendChild(document.createTextNode(text));

    elem.appendChild(partOneElem);
    elem.appendChild(partTwoElem);

    addElementAfter("chatroom", elem);
}

function addOutgoingMess(time, text, photo) {
    var elem = document.createElement("div");
    elem.className = "chat-message-right mb-4";

    var partOneElem = document.createElement("div");

    var imgElem = document.createElement("img");
    imgElem.className = "rounded-circle mr-1";
    imgElem.width = 40;
    imgElem.height = 40;
    imgElem.src = photo !== null && photo !== undefined && photo !== "" ? photo : "https://bootdey.com/img/Content/avatar/avatar1.png";

    var timeElem = document.createElement("div");
    timeElem.className = "text-muted small text-nowrap mt-2";
    timeElem.appendChild(document.createTextNode(time));

    partOneElem.appendChild(imgElem);
    partOneElem.appendChild(timeElem);

    var partTwoElem = document.createElement("div");
    partTwoElem.className = "flex-shrink-1 bg-light rounded py-2 px-3 mr-3";

    var fromElem = document.createElement("div");
    fromElem.className = "font-weight-bold mb-1";
    fromElem.appendChild(document.createTextNode("You"));

    partTwoElem.appendChild(fromElem);
    partTwoElem.appendChild(document.createTextNode(text));

    elem.appendChild(partOneElem);
    elem.appendChild(partTwoElem);

    addElementAfter("chatroom", elem);
}

function addElementAfter(elementName, elem) {
    var firstElem = document.getElementById(elementName).lastChild;

    if (firstElem == null || firstElem == undefined) {
        document.getElementById(elementName).insertBefore(elem, firstElem);
    } else {
        firstElem.after(elem);
    }
}

function dropElementsInside(elementName) {
    var container = document.getElementById(elementName);

    while (container.firstChild) {
        container.removeChild(container.firstChild);
    }
}

