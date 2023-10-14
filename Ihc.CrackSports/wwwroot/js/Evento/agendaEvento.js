$(document).ready(() => {
    $("#btnPesquisarEvento").on('click', buscarEventosClick);
});





function buscarEventosClick(e) {
    var model = new Object();
    model.mes = $("#cmbConsultarMesAgenda").val();
    model.ano = $("#txtAnoFiltroEvento").val();

    util.ajax.post("../Evento/ListarEventosPartialView", model, buscarEventosClickSuccesso, buscarEventosClickError);
}

function buscarEventosClickSuccesso(data) {
    $("#pnlListaEventosPartial").empty();

    if (data) {
        $("#pnlListaEventosPartial").html(data);
    } else {
        alert("Não foram encontrados registros para sua busca .");
    }


}
function buscarEventosClickError(error) {

    alert("Erro ao buscar eventos !");
}

function onClickExcluirAgendaEvento(idEvento) {
    $("#modal").load("../Evento/ConfimarExclusaoEvento?idEvento=" + idEvento, function () {
        $("#modal").modal();

        $("#modal").show();

    });
}

function closeModalExcluirEvento() {
    $("#modal").hide();
}


function confirmarExclusaoEvento(idEvento) {

    var eventoModel = new Object();
    eventoModel.idEvento = idEvento;

    util.ajax.post("../Evento/ConfimarExclusaoEvento", eventoModel, excluirAgendaEventoSucesso, excluirAgendaEventoError);
}


function excluirAgendaEventoSucesso(data) {
    if (data) {
        $("#btnPesquisarEvento").click();
    }
    else {
        alert("Não foi possível excluir o evento nesse momento. Por favor, tente novamente em alguns instantes.");
    }

}
function excluirAgendaEventoError(erro) {
    alert("Não foi possível excluir o evento nesse momento. Por favor, tente novamente em alguns instantes.");
}

