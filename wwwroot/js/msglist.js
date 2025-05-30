"use strict"

const hub = new signalR.HubConnectionBuilder()
    .withUrl("/dmhub")
    .build();   
hub.start();

hub.on("Receive", (msg, dt) => getMsg(msg, dt, false));

hub.on("ReceiveSelf", (msg, dt) => getMsg(msg, dt, true));

function getMsg(text, dtv, isSelf) {
    var flex = document.createElement("div");
    var e = document.createElement("div");
    var t = document.createElement("div");
    var br = document.createElement("br");
    t.textContent = text;
    if (isSelf) {
        flex.className = "flex justify-end";
        e.className = "message-self";
    }
    else
    {
        flex.className = "flex justify-start";
        e.className = "message-other";
    }
    e.appendChild(br);
    e.appendChild(t);
    var dt = document.createElement("div");
    dt.className = "text-xs text-gray-500 dark:text-gray-400 mt-1";
    dt.textContent = dtv;
    e.appendChild(dt);
    flex.appendChild(e);
    document.getElementById("msg-list").appendChild(flex);
}

function send() {
    var id = document.getElementById("user-id").textContent.trim();
    var text = document.getElementById("msg-input").value;
    fetch(`/Direct/Send?id=${id}`, {
        method: "POST",
        body: text
    })
    .then(res => res.text())
    .then(txt => hub.invoke("Send", id, text, txt));
}

