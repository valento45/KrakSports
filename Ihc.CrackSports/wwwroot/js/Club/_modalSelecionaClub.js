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

    if (_numeroAdversarioCorrente == _adversarioPrimeiro) {

    }
    else if (_numeroAdversarioCorrente == _adversarioSegundo) {

    }
}


