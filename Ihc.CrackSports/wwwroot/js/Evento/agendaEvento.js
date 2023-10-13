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