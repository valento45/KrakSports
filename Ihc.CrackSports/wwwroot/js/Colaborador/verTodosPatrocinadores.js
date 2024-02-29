$(document).ready(() => {


});
function refreshPaginacao(obj) {


    util.ajax.post(urlRefreshPaginacaoVerTodosPatrocinadores, obj,
        (result) => {

            successRefreshPaginacao(result);


        },
        (error) => {
            alert("Erro ao processar a solicitação. Por favor, tente mais tarde.");
        });
}



function successRefreshPaginacao(result) {
    /*$("#erro-message-solicitacoes-patrocinador").addClass("d-none");*/


    if (result) {
        $("#pnlPartialViewVerTodosPatrocinadores").empty();

        $("#pnlPartialViewVerTodosPatrocinadores").html(result);
    }
}


function voltarPaginaVerTodos(model) {
    model.PreviousPage = true;
    refreshPaginacao(model);
}

function proximaPaginaVerTodos(model) {
    model.NextPage = true;

    refreshPaginacao(model);
}