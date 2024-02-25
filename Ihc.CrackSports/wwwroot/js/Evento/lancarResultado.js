$(document).ready(() => {



});



function onAdicionarGol(e, nmrTime) {

    var model = new Object();

    model.evento = JSON.parse($("#inputEvento").val());
    model.aluno = JSON.parse($(`#cmbJogadorTime${nmrTime}`).val());
    model.gols = $(`#txtQtdGolsTime${nmrTime}`).val();


    insertGol(model);
}


function insertGol(model) {
    util.ajax.post("../Evento/AdicionarGol", model,

        (result) => {
            window.location.href = `../Evento/LancarResultado?idEvento=${model.evento.IdEvento}`;

        },
        (error) => {
            alert("Erro ao adicionar plarcar! Por favor, tente mais tarde.");
        }
    );
}


function onClickRemoverGol(idLancamento) {

    var url = `../Evento/RemoverGol`;

    util.ajax.post(url, idLancamento,

        (result) => {
            refreshThisPage();
        },
        (error) => {
            alert("Não foi possível excluir o lançamento! Por favor, tente mais tarde.");
        });

    
}


function refreshThisPage() {
    var evento = JSON.parse($("#inputEvento").val());

    window.location.href = `../Evento/LancarResultado?idEvento=${evento.IdEvento}`;
}