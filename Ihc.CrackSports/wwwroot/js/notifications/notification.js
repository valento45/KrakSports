"use strict";
let isConnected = false;
var connection = new signalR.HubConnectionBuilder().withUrl("/notificationHub").build();


connection.start().then(function () {
    isConnected = true;
}).catch(function (err) {
    return console.error(err.toString());
});

connection.on("refreshNotification", (user, title, message, link) => {
    
    $("#notifications").html("ADDED by <b>" + user + "</b>");
    window.setTimeout(function () {
        $("#notifications").html("NOTIFICATIONS");
    }, 10000);
});


function enviaSolicitacaoClub(idClub) {
    connection.invoke("SendSolicitacaoAlunoToClub", _userLogado, idClub).catch(function (err) {
        return console.error(err.toString());
    });    
}




