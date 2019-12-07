"use strict"

var connection = new signalR.HubConnectionBuilder().withUrl("/api/chathub").build();

var cookies = document.cookie;
var i = cookies.indexOf("=") + 1;
var name = cookies.slice(i, cookies.length);

var authBox = document.getElementById("loginBox");


if (name != "") {
    authBox.hidden = true;
    connection.invoke("GetHistory", "50");
}

connection.on("RecieveMessage", function (login, message) {
    if (name == "")
        return;
    var el = document.getElementById("text-center");
    var cr = document.createElement("div");
    cr.textContent = login + ":" + message;
    el.appendChild(cr);
});

connection.on("RegAndLoginResult", function (message) {
    var el = document.getElementById("text-center");
    var cr = document.createElement("div");
    var login = document.getElementById("login").value;

    if (parseInt(message) < 0)
        cr.textContent = "Неверные данные!";
    else {
        cr.textContent = "Вы успешно авторизовались!";
        document.cookie = "login=" + login;
        authBox.hidden = true;
        name = login;
        connection.invoke("GetHistory", "50");
    }

    el.appendChild(cr);
});

connection.on("ChatHistory", function (UserMessages) {
    var el = document.getElementById("text-center");

    var msgs = [];
    msgs = JSON.parse(UserMessages);
    for (var i = 0; i < msgs.length; i++) {
        var cr = document.createElement("div");
        cr.textContent = msgs[i].login + ":" + msgs[i].message;
        el.appendChild(cr);
    }
});

connection.start();

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = name;
    var message = document.getElementById("message").value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
})

// Auth

document.getElementById("loginButton").addEventListener("click", function (event) {
    var user = document.getElementById("login").value;
    var message = document.getElementById("password").value;
    connection.invoke("RegAndLogin", "1", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
})