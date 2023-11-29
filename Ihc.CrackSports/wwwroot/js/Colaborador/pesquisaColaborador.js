$(document).ready(() => {


});

function voltarPagina(e, tipo) {
    e.PreviousPage = true;


    refreshPaginacao(e);

}
function proximaPagina(e, tipo) {
    e.NextPage = true;

    refreshPaginacao(e, tipo);

}

function refreshPaginacao(obj, tipo) {


    util.ajax.post(urlRefreshPaginacaoPatrocinadores, obj,
        (result) => {
            if (tipo == 'Pendente')
                successRefreshPaginacao(result);

            else if (tipo == 'Ativo')
                successRefreshPaginacaoAtivo(result);

            else
                successRefreshPaginacaoInativo(result);
        },
        (error) => {
            alert("Erro ao processar a solicitação. Por favor, tente mais tarde.");
        });
}

function successRefreshPaginacao(result) {
    $("#erro-message-solicitacoes-patrocinador").addClass("d-none");


    if (result) {
        $("#pnlSolicitacoes").empty();

        $("#pnlSolicitacoes").html(result);
    }
}

function successRefreshPaginacaoAtivo(result) {
    $("#erro-message-solicitacoes-patrocinador").addClass("d-none");


    if (result) {
        $("#pnlPatrocinadoresAtivos").empty();

        $("#pnlPatrocinadoresAtivos").html(result);
    }
}

function successRefreshPaginacaoInativo(result) {
    $("#erro-message-solicitacoes-patrocinador").addClass("d-none");


    if (result) {
        $("#pnlInativos").empty();

        $("#pnlInativos").html(result);
    }
}


function aceitarSolicitacaoPatrocinador(model) {

    util.ajax.post(urlAceitarPatrocinador, model, aceitarSolicitacaoSucesso, aceitarSolicitacaoErro);
}

function aceitarSolicitacaoSucesso(data) {

    if (data) {
        alert("Solicitação aceita com sucesso !");
        window.location.reload();
    }
    else
        responseError();
}
function aceitarSolicitacaoErro(error) {
    alert("Não foi possível aceitar a solicitação. Por favor, tente mais tarde.");
}



function excluirSolicitacaoPatrocinador(model) {
    util.ajax.post(urlExcluirPatrocinador, model, excluirSolicitacaoSucesso, responseError);
}

function excluirSolicitacaoSucesso(data) {

    if (data) {
        alert("Solicitação excluída com sucesso.");
        window.location.reload();
    }   
    else
        responseError();
}

function responseError(error) {
    alert("Ocorreu um erro ao tentar processar sua requisição. Por favor, tente mais tarde.");

}


function inativarPatrocinador(model) {
    util.ajax.post(urlInativarPatrocinador, model, inativarPatrocinadorSucesso, responseError);
}
function inativarPatrocinadorSucesso(data) {
    alert("Patrocinador inativado com sucesso!");
    window.location.reload();
}

function reativarPatrocinador(model) {
    util.ajax.post(urlReativarPatrocinador, model, reativarSucesso, responseError);
}
function reativarSucesso(data) {
    if (data) {
        alert("Patrocinador reativado com sucesso!");
        window.location.reload();
    }
    else
        responseError();
}