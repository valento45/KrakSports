$(document).ready(() => {

    $("#txtMensagem").on('keyup', onKeyUpTextMensagem);
    $("#btnEnviarPatrocinador").on('click', enviarSolicitacao);
    $("#rdbPessoaJuridica").on('change', onChangePessoaJuridica);
});

const limiteCaractereMensagem = 3000;

function onKeyUpTextMensagem(e) {
    var $lblLimite = $("#lblLimiteCaractere");
    var $txtMensagem = $("#txtMensagem");


    var totalCaracteres = +$txtMensagem.val().length;

    if (totalCaracteres > 0) {

        if (totalCaracteres == limiteCaractereMensagem) {
            $txtMensagem.attr('readonly', 'readonly');
        }

        var caracteresDisponiveis = limiteCaractereMensagem - totalCaracteres;
        $lblLimite.text('Limite de caracteres ' + caracteresDisponiveis);
    }
    else {
        $lblLimite.text('Limite de caracteres ' + limiteCaractereMensagem);
    }
}

function onChangePessoaJuridica(e) {
    var isChecked = $("#rdbPessoaJuridica").is(":checked");

    if (isChecked) {
        $("#txtCpfCnpj").attr('data-mask','00.000.000/0000-00');
    } else {
        $("#txtCpfCnpj").attr('data-mask','000.000.000-00');
    }
}


function criarSolicitacao() {
    var patrocinador = new Object();
    patrocinador.IdPatrocinador = 0;
    patrocinador.NomeOuRazaoSocial = $("#txtNomeRazaoSocial").val();
    patrocinador.Email = $("#txtEmail").val();


    var valCpfInput = $("#txtCpfCnpj").val();

    if (valCpfInput !== "")
        patrocinador.CpfCnpj = $("#txtCpfCnpj").val();
    else
        patrocinador.CpfCnpj = 0;

    patrocinador.Telefone = $("#txtTelefone").val();
    patrocinador.Celular = $("#txtCelular").val();
    patrocinador.LinkInstagram = $("#txtInstagram").val();
    patrocinador.LinkLinkedin = $("#txtLinkedin").val();
    patrocinador.LinkSite = $("#txtSite").val();
    patrocinador.LogoTipoBase64 = output.value;
    patrocinador.Mensagem = $("#txtMensagem").val();
    patrocinador.IsPj = $("#rdbPessoaJuridica").is(":checked");

    return patrocinador;

}

function enviarSolicitacao() {
    var request = criarSolicitacao();

    util.ajax.post("../Colaborador/NovoPatrocinador", request, novoPatrocinadorSucesso, novoPatrocinadorErro);
}

function novoPatrocinadorSucesso(e) {
    
    document.open();
    document.write(e);
    document.close();
}
function novoPatrocinadorErro(erro) {
    alert("Erro ao cadastrar patrocinador")

}

