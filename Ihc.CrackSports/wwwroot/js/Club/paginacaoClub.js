$(document).ready(() => {

});


function voltarPagina(e) {
    _paginacaoClub.previousPage = true;

    refreshPaginacao();

}
function proximaPagina(e) {
    _paginacaoClub.nextPage = true;

    refreshPaginacao();

}

function refreshPaginacao() {
    

    util.ajax.post(urlRefreshPaginacao, _paginacaoClub, successRefreshPaginacao, erroRefreshPaginacao);
}
function successRefreshPaginacao(data) {
    $("#pnlPartialClubs").empty();

    if (!data) {
        alert("Erro")
    }
    else {

        $("#pnlPartialClubs").html(data);


        if (_paginacaoClub.nextPage) {
            _paginacaoClub.nextPage = false;

            if (_paginacaoClub.pageNumber < (_paginacaoClub.pageCount - 1))
                _paginacaoClub.pageNumber += 1;
        }

        if (_paginacaoClub.previousPage) {
            _paginacaoClub.previousPage = false;

            if (_paginacaoClub.pageNumber > 0)
                _paginacaoClub.pageNumber -= 1;
        }
    }
}
function erroRefreshPaginacao(e) {
    alert("Erro");
}

function redirectApresentacaoClube(idClub) {
    window.location.href = `../Club/ApresentacaoClub?idClub=${idClub}`;
}