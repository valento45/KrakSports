 $(document).ready(() => {


});


function onClickInicio(e) {

    activeInactiveButton(e);

    util.ajax.get("../Administrador/Home", null, onClickMenuSucesso, onClickMenuError);
}

function onClickPatrocinadores(e) {
    activeInactiveButton(e);

    util.ajax.get("../Administrador/Patrocinadores", null, onClickMenuSucesso, onClickMenuError);

}

function onClickMenuSucesso(result) {
    if (result) {
        $("#mainMenuContent").empty();
        $("#mainMenuContent").html(result);
    } else {
        alert("Não foi possível realizar a operação, por favor tente mais tarde.");
    }
}

function onClickMenuError(erro) {

    alert("Não foi possível realizar a operação, por favor tente mais tarde.");
}

function onClickClubes(e) {
    activeInactiveButton(e);


}

function onClickAtletas(e) {
    activeInactiveButton(e);


}

function onClickConfiguracoes(e) {
    activeInactiveButton(e);


}

function activeInactiveButton(e) {
    $(".nav-link").removeClass('active');
    $(".nav-link").addClass('text-white');

    $(e).addClass('active');
    $(e).removeClass('text-white');

}


function hideTabs() {
    $("#pnlPatrocinadoresAtivos").addClass("d-none");
    $("#pnlSolicitacoes").addClass("d-none");
    $("#pnlRendimentos").addClass("d-none");
    $("#pnlInativos").addClass("d-none");

    $("#tabPatrocinadoresAtivos").removeClass("active");
    $("#tabSolicitacoes").removeClass("active");
    $("#tabRendimentos").removeClass("active");
    $("#tabInativos").removeClass("active");
}

function onClickTab(option) {
    hideTabs();


    $(`#tab${option}`).addClass("active");
    $(`#pnl${option}`).removeClass("d-none");
}


function onClickDetalhesPatrocinador(e) {

    util.ajax.post("../Administrador/DetalhesPatrocinadorPartialView", e,
        onClickMenuSucesso,
        onClickMenuError);     

}



