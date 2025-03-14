const hub = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:7251/dmhub")
    .configureLogging(signalR.LogLevel.Trace)
    .withAutomaticReconnect([0, 10, 30, 60, 90, 150])
    .build();   

async function start() {
    try {
        await hub.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

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

hub.on("Receive", (msg, dt) => getMsg(msg, dt, false));

hub.on("ReceiveSelf", (msg, dt) => getMsg(msg, dt, true));

function send() {
    var id = document.getElementById("user-id").textContent;
    var text = document.getElementById("msg-input").value;
    fetch(`/Direct/Send?id=${id}`, {
        method: "POST",
        body: text
    })
    .then(res => res.text())
    .then(txt => hub.invoke("Send", id, text, txt));
}

start();