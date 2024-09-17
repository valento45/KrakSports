$(document).ready(() => {


});

let messageSuccess = false;

function onClickInicio(e) {

    activeInactiveButton(e);

    util.ajax.get("../Administrador/Home", null, onClickMenuSucesso, onClickMenuError);
}

function onClickPatrocinadores(e) {
    activeInactiveButton(e);

    util.ajax.get("../Administrador/Patrocinadores", null, onClickMenuSucesso, onClickMenuError);

}



function onClickMenuSucesso(result) {
    if (result) {
        $("#mainMenuContent").empty();
        $("#mainMenuContent").html(result);
    } else {
        alert("Não foi possível realizar a operação, por favor tente mais tarde.");
    }
}

function onClickMenuError(erro) {

    alert("Não foi possível realizar a operação, por favor tente mais tarde.");
}

function onClickClubes(e) {


    var obj = $(`#${e}`);
    activeInactiveButton(obj);

    util.ajax.get("../Administrador/ClubesAdmin", null, onClickMenuSucesso, onClickMenuError);

    
}

function onClickAtletas(e) {
    activeInactiveButton(e);


}

function onClickConfiguracoes(e) {
    activeInactiveButton(e);


}

function activeInactiveButton(e) {
    $(".nav-link").removeClass('active');
    $(".nav-link").addClass('text-white');

    $(e).addClass('active');
    $(e).removeClass('text-white');

}


function hideTabs() {
    $("#pnlPatrocinadoresAtivos").addClass("d-none");
    $("#pnlSolicitacoes").addClass("d-none");
    $("#pnlRendimentos").addClass("d-none");
    $("#pnlInativos").addClass("d-none");

    $("#tabPatrocinadoresAtivos").removeClass("active");
    $("#tabSolicitacoes").removeClass("active");
    $("#tabRendimentos").removeClass("active");
    $("#tabInativos").removeClass("active");


    $(".tab-clubes-adm").addClass("d-none");
    $(".clubes-adm").removeClass("active");

    $(".message-success").addClass("d-none");
    $(".message-warning-pnl").addClass("d-none");

}

function onClickTab(option) {
    hideTabs();


    $(`#tab${option}`).addClass("active");
    $(`#pnl${option}`).removeClass("d-none");
}


function onClickDetalhesPatrocinador(e) {

    util.ajax.post("../Administrador/DetalhesPatrocinadorPartialView", e,
        onClickMenuSucesso,
        onClickMenuError);

}


function onClickDetalhesClube(e) {

    util.ajax.post("../Administrador/DetalhesClubePartialView", e,
        onClickMenuSucesso,
        onClickMenuError);

}

function aceitarSolicitacaoClube(clube) {



    var idClube = clube.Id;

    util.ajax.post("../Administrador/AceitarClube", idClube,
        onClickOperacaoSucesso,
        onClickOperacaoErro);


}

function removerSolicitacaoClube(clube) {
    var idClube = clube.Id;

    util.ajax.post("../Administrador/RemoverClube", idClube,
        onClickOperacaoSucesso,
        onClickOperacaoErro);
}


function onClickOperacaoSucesso(e) {

    if (e) {
        if (e.stackTrace) {
            $(".message-warning-text").text(e.message);
            $(".message-warning").removeClass("d-none");
            return;
        }
        else if (e.id) {        
            $(`#clube_adm_${e.id}`).remove();
        }       

        $(".message-success").removeClass("d-none");


    } else {
        $(".message-warning-text").text("Não foi possível concluir a operação! Por favor, tente mais tarde.");

        $(".message-warning").removeClass("d-none");
    }

}

function onClickOperacaoErro(e) {

    if (e) {

        $(".message-warning-text").text(e.message);

        $(".message-warning").removeClass("d-none");
    } else {
        $(".message-warning").removeClass("d-none");
    }


}



function criarUsuarioRedirectAdmin(e) {
    var idClub = $("#inputIdClubeDetalhes").val();

    if (idClub) {
        var redirectUrl = `../Club/CadastroClubSemUsuario?idClub=${idClub}`;
        window.location.href = redirectUrl;
    } else {
        alert("Não foi possível obter o código do Clube! Contate o desenvolvedor.");
    }
}