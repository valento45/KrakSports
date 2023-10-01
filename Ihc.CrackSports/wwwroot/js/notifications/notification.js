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

    atualizaNotificacoes();
    refreshPartialViewNotifications();
});

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

    //    document.getElementById("pnlNotifications").innerHTML += `<div id="solicitacao" class="mt-2 card-notification  col-lg-6 col-md-6 d-inline-flex">
    //    <div class="col-lg-2 col-md-2">
    //        var imagem = String.Format("data:image/png;base64,{0}", al.From.FotoAlunoBase64);
    //        <img class="img-icon-nav ms-2" src="@imagem" style="width: 60px;    height: 60px;" />
    //    </div>

    //    <div class="ms-2 col-lg-9 col-md-9">
    //        <p><b>@al.From.Nome.ToUpper()</b> @notificacao.Notificacao</p>

    //        <a class="btn btn-block btn-confirm-radius" onclick="aceitarSolicitacaoAluno(@JsonConvert.SerializeObject( notificacao))">Confirmar</a>
    //        <a class="btn btn-block btn-confirm-radius" onclick="excluirSolicitacaoAluno(@JsonConvert.SerializeObject( notificacao))">Excluir</a>

    //    </div>

    //</div>`;

}
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


function enviaSolicitacaoClub(idClub) {
    connection.invoke("SendSolicitacaoAlunoToClub", _userLogado, idClub).catch(function (err) {
        alert("Erro ao enviar solicitação. Tente mais tarde.")
        return console.error(err.toString());
    });

    alert("solicitação enviada com sucesso!");
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
    if (!IsConnected()) {
        StartConnection();
    }


    if (isConnected) {
        connection.invoke("ExcluirSolicitacaoAluno", notificacao.IdNotificacao).catch(function (err) {
            return console.error(err.toString());

        });

        $(`#solicitacao` + notificacao.IdNotificacao).remove();
        removedNotification();

    }


}


function aceitarSolicitacaoAluno(notificacao) {

    if (!IsConnected()) {
        isConnected = StartConnection();
    }

    if (isConnected) {
        connection.invoke("AceitarSolicitacaoAluno", notificacao.IdClub, notificacao.IdAluno).catch(function (err) {
            return console.error(err.toString());
        });

        $(`#solicitacao` + notificacao.IdNotificacao).remove();
        removedNotification();
    }

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
