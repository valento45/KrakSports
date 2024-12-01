$(document).ready(() => {

    $("#txtCep").on('change', onChangeCEP);

});


function onChangeCEP(e) {


    var cep = $("#txtCep").val();

    if (cep) {

        var url = `../Home/ObterDadosCEP?CEP=${cep}`;

        util.ajax.get(url, null, onChangeCEPSuccess, onChangeCEPError);
    }

}

function onChangeCEPSuccess(result) {
    if (result) {
        $("#txtLogradouro").val(result.logradouro);
        $("#txtCidade").val(result.localidade);
        $("#txtUF").val(result.uf);        

    }
}

function onChangeCEPError(error) {
    
}



var atletaModal = {};

function onClickConfirmarExclusao() {

    util.ajax.get(`../Administrador/ExcluirAtleta?idAtleta=${atletaModal.Id}`, null,
        (result) => {
         /*   window.location.reload();*/

            document.write(result);
        },

        (error) => {
            alert("Não foi possível efetuar exclusão do produto, tente mais tarde.")
        });
}


function onClickExcluirAtleta(atleta) {
    atletaModal = atleta;





    $("#modalBodyExclude").text(`Deseja excluir o atleta ${atleta.Nome}?`);
}