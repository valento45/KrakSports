$(document).ready(() => {


});


function onChangeFiltros(e) {

    var periodoRanking = $("#cmbFiltrarRanking").val();

    window.location.href = `../Ranking/FiltrarRankingAtletas?periodoRanking=${periodoRanking}`;
}