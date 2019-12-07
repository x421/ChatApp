"use strict"

var connection = new signalR.HubConnectionBuilder().withUrl("/api/chathub").build();

connection.on("RegAndLoginResult", function (message) {

    code = parseInt(message);
    var elem = document.getElementById("text-center");
    if (code == -1) {
        elem.textContent = "Такой аккаунт уже существует!";
    } else {
        elem.textContent = "Вы успешно зарегистрированы!";
    }
});

connection.start();

document.getElementById("loginButton").addEventListener("click", function (event) {
    var user = document.getElementById("login").value;
    var message = document.getElementById("password").value;
    connection.invoke("RegAndLogin", "0", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
})