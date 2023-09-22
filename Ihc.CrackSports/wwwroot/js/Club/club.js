$(document).ready(() => {

});


function onClickCarregarImagemClub() {
    document.getElementById('campoArquivoClub').click();
}

function onChangeImageClub(e) {
    saveImageClub();
}

function saveImageClub() {
    document.getElementById('btnSalvarDadodsClub').click();
}

function continuarCadastroClub() {
    nome = $("#txtNomeClub").val();
    email = $("#txtEmailClub").val();
    cpfCnpj = 0;
    tipoUsuario = 1;


    var url = `../Usuario/Cadastro?nome=${nome}&cpfCnpj=${cpfCnpj}&email=${email}&tipoUsuario=${tipoUsuario}`;

    window.location.href = url;
}

function redirectDadosClub(idClub) {
    window.location.href = `../Club/Cadastro?idUsuario=${idClub}`;
}

function onClickAlterarCamisa(idAluno) {
    $("#modal").load("../Club/AtualizarCamisa?idAluno=" + idAluno, function () {
        $("#modal").modal();

        $("#modal").show();

    });
}


function closeModalAlterarCamisa() {
    $("#modal").hide();
}

function hideTabs() {
    $("#pnlSobreClube").addClass("d-none");
    $("#pnlAtletasClube").addClass("d-none");
    $("#pnlContatosClube").addClass("d-none");

    $("#tabSobre").removeClass("active");
    $("#tabAtletas").removeClass("active");
    $("#tabContatos").removeClass("active");
}

function onClickTabSobre() {
    hideTabs();

    $("#pnlSobreClube").removeClass("d-none");
    $("#tabSobre").addClass("active");
}
function onClickTabAtletas() {
    hideTabs();

    $("#pnlAtletasClube").removeClass("d-none");
    $("#tabAtletas").addClass("active");
}
function onClickTabContatos() {
    hideTabs();

    $("#pnlContatosClube").removeClass("d-none");
    $("#tabContatos").addClass("active");
}