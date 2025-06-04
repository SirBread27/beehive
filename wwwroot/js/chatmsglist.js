"use strict"

const hub = new signalR.HubConnectionBuilder()
    .withUrl("/chathub")
    .build();
hub.start();

hub.on("Receive", (msg, dt) => getMsg(msg, dt, false));

hub.on("ReceiveSelf", (msg, dt) => getMsg(msg, dt, true));

function getMsg(text, dtv, isSelf) {
    var e = document.createElement("div");
    var t = document.createElement("div");
    var br = document.createElement("br");
    t.textContent = text;
    if (isSelf) e.className = "msg-self";
    else e.className = "msg";
    e.appendChild(br);
    e.appendChild(t);
    var dt = document.createElement("div");
    dt.className = "msg-datetime";
    dt.textContent = dtv;
    e.appendChild(dt);
    document.getElementById("msg-list").appendChild(e);
}

function send() {
    var id = document.getElementById("chat-id").textContent.trim();
    var text = document.getElementById("msg-input").value;
    fetch(`/Chat/Send?id=${id}`, {
        method: "POST",
        body: text
    })
        .then(res => res.text())
        .then(txt => hub.invoke("Send", id, text, txt));
}

