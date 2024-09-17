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