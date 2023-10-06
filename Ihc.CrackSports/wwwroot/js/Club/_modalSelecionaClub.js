$(document).ready(() => {
    
});

let _numeroAdversarioCorrente = 0;
const _adversarioPrimeiro = 1, _adversarioSegundo = 2;

function closeModalSelecionarClube() {
    $("#modalSelecionarClube").hide();
}

function onClickSelecionarClube(numeroAdversario) {
    $("#modalSelecionarClube").load("../Club/SelecionarClubePartial",
        function () {
            _numeroAdversarioCorrente = numeroAdversario;
            $("#modalSelecionarClube").modal();
            $("#modalSelecionarClube").show();

        });


}

function selectedClube(data) {

    var clubeSelecionado = instanceClubeSelecionado(data);
    setClubeSelected(clubeSelecionado);

}


function instanceClubeSelecionado(data) {

    if (data) {

        var clubeSelecionado = new Object();

        clubeSelecionado.nome = $(data).children("div").children("span").text();
        clubeSelecionado.idClube = $(data).children("div").children("input").val();
        clubeSelecionado.imagem = $(data).children("div").children("img").attr('src');

        return clubeSelecionado;
    }
}

function setClubeSelected(clube) {
    if (_numeroAdversarioCorrente == _adversarioPrimeiro) {
        setClubeAdversario(clube);
    }
    else if (_numeroAdversarioCorrente == _adversarioSegundo) {
        setClubeAdversario2(clube);
    }
    closeModalSelecionarClube();
}

function setClubeAdversario(clube) {
    $("#txtCodigoAdversario").val(clube.idClube);
    $("#lblAdversario").text(`${clube.nome}`);
    $("#imgAdversario").attr('src', `${clube.imagem}`);
}
function setClubeAdversario2(clube) {
    $("#txtCodigoAdversario2").val(clube.idClube);
    $("#lblAdversario2").text(`${clube.nome}`);
    $("#imgAdversario2").attr('src', `${clube.imagem}`);
}


function onKeyUpFiltro(event) {
    $("#listClubes").children("li").each(function () {

        if ($(event).val().toLowerCase() == "" || !$(event).val()) {
            $(this).removeClass("d-none");
            return;
        }

        else if ($(this).text().toLowerCase().includes($(event).val().toLowerCase())) {

            this.selected = true;
            $(this).removeClass("d-none");

        }
        else {
            $(this).addClass("d-none");
        }
    });
}