$(document).ready(() => {
    $('[data-toggle="tooltip"]').tooltip();
    $(".valida-campos-obrigatorios").on("focusout", onFocusOutCamposObrigatorios);
});



const urlRefreshPaginacaoPatrocinadores = "../Colaborador/RefreshPaginacaoColaborador";
const urlRefreshPaginacaoVerTodosPatrocinadores = "../Colaborador/RefreshPaginacaoVerTodosPatrocinadores";
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

function closeModal(idModal) {
    $(`#${idModal}`).hide();
}



let _indiceItem = 0;
function onClickAvancarPaginaCarousel(totalItens) {
    //Remove active de todos itens
    $(".carousel-item").removeClass("active");
    $(".carousel-item").children("div").removeClass("d-flex");
    $(".carousel-item").children("div").addClass("d-none");
    $(".carousel-item").addClass("d-none");


    if (_indiceItem < (totalItens - 1))
        _indiceItem = _indiceItem + 1;
    else {
        _indiceItem = 0;
    }

    $(`#item_caroulsel_${_indiceItem}`).removeClass("d-none");
    $(`#item_caroulsel_${_indiceItem}`).children("div").removeClass("d-none");
    $(`#item_caroulsel_${_indiceItem}`).children("div").addClass("d-flex");
    $(`#item_caroulsel_${_indiceItem}`).addClass("active");

}


function onClickVoltarPaginaCarousel(totalItens) {
    //Remove active de todos itens
    $(".carousel-item").removeClass("active");
    $(".carousel-item").children("div").removeClass("d-flex");
    $(".carousel-item").children("div").addClass("d-none");
    $(".carousel-item").addClass("d-none");


    if (_indiceItem > 0)
        _indiceItem = _indiceItem - 1;
    else {
        _indiceItem = totalItens - 1;
    }

    $(`#item_caroulsel_${_indiceItem}`).removeClass("d-none");
    $(`#item_caroulsel_${_indiceItem}`).children("div").removeClass("d-none");
    $(`#item_caroulsel_${_indiceItem}`).children("div").addClass("d-flex");
    $(`#item_caroulsel_${_indiceItem}`).addClass("active");

}