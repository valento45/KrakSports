$(document).ready(() => {


});

function voltarPagina(e) {
    _paginacaoPatrocinadores.previousPage = true;

    refreshPaginacao();

}
function proximaPagina(e) {
    _paginacaoPatrocinadores.nextPage = true;

    refreshPaginacao();

}

function refreshPaginacao() {


    util.ajax.post(urlRefreshPaginacaoPatrocinadores, _paginacaoPatrocinadores, successRefreshPaginacao, erroRefreshPaginacao);
}

function successRefreshPaginacao(result) {


}
function erroRefreshPaginacao(error) {

}