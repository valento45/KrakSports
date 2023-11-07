"use strict";
let isConnected = false;
var connection = new signalR.HubConnectionBuilder().withUrl("/notificationHub").build();

connection.on("refreshNotification", (user, title, message, link) => {

    $("#notifications").html("ADDED by <b>" + user + "</b>");
    window.setTimeout(function () {
        $("#notifications").html("NOTIFICATIONS");
    }, 10000);

    atualizaNotificacoes();
    refreshPartialViewNotifications();
});

function StartConnection() {
    connection.start().then(function () {
        isConnected = true;
        return isConnected;
    }).catch(function (err) {
        return console.error(err.toString());
    });
}
function IsConnected() {
    return connection._connectionState == "Connected";
}



function refreshPartialViewNotifications() {
    util.ajax.get("../Notificacao/RefreshNotificationsPartialView", null, refreshPartialViewNotificationsSuccesso, refreshPartialViewNotificationsErro);
}

function refreshPartialViewNotificationsSuccesso(data) {
    if (data) {
        $("#pnlNotificacoes").empty();

        $("#pnlNotificacoes").html(data);
    }
}
function refreshPartialViewNotificationsErro(data) {
    alert("Erro ao atualizar as notificações.");
}

function atualizaNotificacoes() {
    var txtNotificacoesNaoVistas = document.getElementById("txtNotificationsNVistas");

    if (txtNotificacoesNaoVistas && txtNotificacoesNaoVistas.innerText) {
        var notifys = + txtNotificacoesNaoVistas.innerText;
        notifys += 1;

        txtNotificacoesNaoVistas.innerText = notifys;
        $("#pnlNotificacoesNVistas").removeClass("d-none");
    }
    else {
        $("#pnlNotificationsNaoVistas").empty();
        $("#pnlNotificationsNaoVistas").html(`<div style="background-color: red;color: ghostwhite; border-radius: 50px; width: 15px; height: 15px; display: initial;
position: absolute; margin-left: 20px;" class="text-center">
            <span id="txtNotificationsNVistas" style="font-size: 10px; display: block;">1</span>
        </div>`);
    }

}



function enviaSolicitacaoClub(idClub) {
    //connection.invoke("SendSolicitacaoAlunoToClub", _userLogado, idClub).catch(function (err) {
    //    alert("Erro ao enviar solicitação. Tente mais tarde.")
    //    return console.error(err.toString());
    //});

    var model = {
        idUsuario = _userLogado,
        idClub = idClub
    };
    util.ajax.post("../Notificacao/EnviaSolicitacaoClub", model, enviaSolicitacaoClubSucess, enviaSolicitacaoClubError);

}

function enviaSolicitacaoClubSucess(data) {

    alert("solicitação enviada com sucesso!");
}
function enviaSolicitacaoClubError(erro) {
    alert("Erro ao enviar solicitação, por favor tente mais tarde.");
}




function openCloseNotificacoes() {
    if ($("#pnlNotificacoes").hasClass("d-none")) {
        $("#pnlNotificacoes").removeClass("d-none");
        $("#pnlNotificacoes").show();
    } else {
        $("#pnlNotificacoes").addClass("d-none");
        $("#pnlNotificacoes").hide();
    }

}

function excluirSolicitacaoAluno(notificacao) {
    //if (!IsConnected()) {
    //    StartConnection();
    //}


    //if (isConnected) {
    //    connection.invoke("ExcluirSolicitacaoAluno", notificacao.IdNotificacao).catch(function (err) {
    //        return console.error(err.toString());

    //    });

    //    $(`#solicitacao` + notificacao.IdNotificacao).remove();
    //    removedNotification();

    //}
    util.ajax.post("../Notificacao/ExcluirNotificacao", notificacao.IdNotificacao, excluirSolicitacaoAlunoSucess, excluirSolicitacaoAlunoError);

}

function excluirSolicitacaoAlunoSucess(data) {

    removedNotification();
}
function excluirSolicitacaoAlunoError(erro) { }


function aceitarSolicitacaoAluno(notificacao) {

    //if (!IsConnected()) {
    //    isConnected = StartConnection();
    //}

    //if (isConnected) {
    //    connection.invoke("AceitarSolicitacaoAluno", notificacao.IdClub, notificacao.IdAluno).catch(function (err) {
    //        return console.error(err.toString());
    //    });

    //    $(`#solicitacao` + notificacao.IdNotificacao).remove();
    //    removedNotification();
    //}
    util.ajax.post("../Notificacao/AceitarSolicitacaoAluno", notificacao.IdNotificacao, aceitarSolicitacaoAlunoSucesso, aceitarSolicitacaoAlunoError);
}

function aceitarSolicitacaoAlunoSucesso(data) {
    removedNotification();
}
function aceitarSolicitacaoAlunoError(erro) {
    alert("Não foi possível aceitar a solicitação, por favor tente novamente mais tarde.")
}

function removedNotification() {
    var txtNotificacoesNaoVistas = document.getElementById("txtNotificationsNVistas");

    if (txtNotificacoesNaoVistas.innerText) {
        var notifys = + txtNotificacoesNaoVistas.innerText;
        notifys -= 1;

        if (notifys == 0) {
            $("#pnlNotificacoesNVistas").addClass("d-none");
        }

        else
            txtNotificacoesNaoVistas.innerText = notifys;
    }
}

function redirectNotification(linkRedirect) {
    if (linkRedirect)
        window.location.href = linkRedirect;
}
