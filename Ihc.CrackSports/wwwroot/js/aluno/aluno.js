$(document).ready(() => {


});


function apenasNumeros(string) {
    var numsStr = string.replace(/[^0-9]/g, '');
    return parseInt(numsStr);
}

function redirectDadosAluno(idAluno) {
    window.location.href = `../Aluno/DadosAluno?idAluno=${idAluno}`;
}

function onClickCarregarImagem() {
    document.getElementById('campoArquivo').click();
}


function onChangeImage(e) {
    saveImage();
}

function saveImage() {  
    document.getElementById('btnSalvarDadosAluno').click();
}
