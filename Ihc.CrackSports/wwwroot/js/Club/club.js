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