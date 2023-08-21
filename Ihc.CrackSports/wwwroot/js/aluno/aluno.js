$(document).ready(() => {
    

});


function apenasNumeros(string) {
    var numsStr = string.replace(/[^0-9]/g, '');
    return parseInt(numsStr);
}

function redirectDadosGerais(idAluno) {
    window.location.href = `../Aluno/DadosGerais?idAluno=${idAluno}`;
}