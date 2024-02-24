$(document).ready(() => {



});



function onAdicionarGol(e, nmrTime) {

    if (nmrTime == 1) {
        var model = new Object();

        model.evento = JSON.parse($("#inputEvento").val());
        model.aluno = JSON.parse($("#cmbJogadorTime1").val());
        model.gols = $("#txtQtdGolsTime1").val();


        util.ajax.post("../Evento/AdicionarGol", model,

            (result) => {
                window.location.href = `../Evento/LancarResultado?idEvento=${model.evento.IdEvento}`;

            },
            (error) => {
                alert("Erro ao adicionar plarcar! Por favor, tente mais tarde.");
            }
        );
    }
}