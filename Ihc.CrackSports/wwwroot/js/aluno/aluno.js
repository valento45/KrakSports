$(document).ready(() => {


});


function apenasNumeros(string) {
    var numsStr = string.replace(/[^0-9]/g, '');
    return parseInt(numsStr);
}



function onClickCarregarImagem(e) {

    document.getElementById('campoArquivo').click();

}

function onChangeImage(e) {
    saveImage();
}

function saveImage() {
    document.getElementById('btnSalvarDadosAluno').click();
}

function redirectDadosGerais(idAluno) {
    window.location.href = `../Aluno/DadosGerais?idAluno=${idAluno}`;
}

