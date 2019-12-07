"use strict"

var connection = new signalR.HubConnectionBuilder().withUrl("/api/chathub").build();

connection.on("ReceiveMessage", function(message) {
    var elem = document.getElementById("text-center");
    var i = document.createElement("div");
    i.textContent = ":" + message;
    elem.appendChild(i);
});

connection.start();

document.getElementById("loginButton").addEventListener("click", function(event) {
    var user = document.getElementById("login").value;
    var message = document.getElementById("password").value;
    connection.invoke("Login", user, message).catch(function(err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});