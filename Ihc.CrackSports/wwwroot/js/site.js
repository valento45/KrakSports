$(document).ready(() => {
    $('[data-toggle="tooltip"]').tooltip();
    $(".valida-campos-obrigatorios").on("focusout", onFocusOutCamposObrigatorios);
});



const urlRefreshPaginacaoPatrocinadores = "../Colaborador/RefreshPaginacaoColaborador";
const urlAceitarPatrocinador = "../Colaborador/AceitarPatrocinador";
const urlExcluirPatrocinador = "../Colaborador/ExcluirPatrocinador";
const urlInativarPatrocinador = "../Colaborador/InativarPatrocinador";
const urlReativarPatrocinador = "../Colaborador/ReativarPatrocinador";

function onFocusOutCamposObrigatorios(e) {

    var campo = $(`#${e.currentTarget.id}`);
    var label = $(`#${e.currentTarget.id}Label`);

    if (!campo.val()) {
        label.addClass("campos-invalidos");
        campo.addClass("campos-invalidos");
    }
    else {
        label.removeClass("campos-invalidos");
        campo.removeClass("campos-invalidos");
    }
}

function onRedirectToSejaUmColaborador() {
    window.location.href = "../Colaborador/SejaUmColaborador";
}

function onRedirectInscricoesAtleta() {
    window.location.href = "../Usuario";
}
function onRedirectInscricoesClube() {
    window.location.href = "../Club/Index";
}

function onRedirectCadastroPatrocinador() {
    window.location.href = "../Colaborador/CadastroPatrocinador";
}

function redirectDadosAluno(idAluno) {
    window.location.href = `../Aluno/DadosAluno?idAluno=${idAluno}`;
}



function onClickRedirectNovaAba(url) {
    window.open(url, '_blank');
}