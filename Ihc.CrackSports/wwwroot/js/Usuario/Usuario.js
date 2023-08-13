$(document).ready(() => {


});

let nome, cpfCnpj, email;

var model = new Object();

function redirectCadastro() {

    window.location.href = "../Usuario/Index";
}

function continuarCadastro() {

    nome = $("#txtNomeCompleto").val();
    cpfCnpj = $("#txtCpfCnpj").val();
    email = $("#txtEmail").val();


    var url = `../Usuario/Cadastro?nome=${nome}&cpfCnpj=${cpfCnpj}&email=${email}`;

    window.location.href = url;
}

function validarCampos() {
    return $("#txtNomeResponsavel").val() != '' && $("#txtDocumentoResponsavel").val() != '' &&
        $("#txtCelularResponsavel").val() != '' && $("#txtNomeAluno").val() != '' && $("#txtDocumentoAluno").val() != '' && $("#txtDataNascAluno").val() != '' &&
        $("#txtCep").val() != '' && $("#txtUsuario").val() != '' && $("#txtSenha").val() != '';
}
function criarConta() {

    if (validarCampos()) {
        /*Dados responsavel*/
        model.idAluno = 0;
        model.idUsuario = 0;

        model.nomeResponsavel = $("#txtNomeResponsavel").val();
        model.documentoResponsavel = $("#txtDocumentoResponsavel").val();
        model.cpfResponsavel =+ $("#txtCpfResponsavel").val();
        model.grauParentesco = $('#cmbGrauParentesco').val();;
        model.telefoneResponsavel = $("#txtTelefoneResponsavel").val();
        model.celularResponsavel = $("#txtCelularResponsavel").val();

        /*Dados Aluno*/
        model.nomeAluno = $("#txtNomeAluno").val();
        model.documentoAluno = $("#txtDocumentoAluno").val();
        model.cpfAluno =+ $("#txtCpfAluno").val();
        model.dataNascimento = new Date();

        /*Dados Endereco*/
        model.endereco = new Object();

        model.endereco.cep = $("#txtCep").val();
        model.endereco.logradouro = $("#txtLogradouro").val();
        model.endereco.numero = $("#txtNumero").val();
        model.endereco.cidade = $("#txtCidade").val();
        model.endereco.uf = $("#txtUF").val();
        model.endereco.complemento = $("#txtComplemento").val();

        /*Dados Usuario*/
        model.login = $("#txtUsuario").val();
        model.senha = $("#txtSenha").val();
        model.email = email;
        model.tipo = 0;


        util.ajax.post("../Usuario/Cadastro", model, cadastroSuccess, cadastroError);

    } else {
        alert("Verifique o preenchimento dos campos obrigatórios !");
    }

}

function cadastroSuccess(e) { }
function cadastroError(error) { }