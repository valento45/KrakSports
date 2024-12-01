$(document).ready(function () {


});



function onLeaveValidarSenhas() {

    $("#btnSalvarUsuario").attr('disabled', 'disabled');
    $("#lblErroUsuario").addClass('d-none');


    if ($("#txtSenha").val() !== "") {
        if ($("#txtSenha").val() == $("#txtConfirmarSenha").val()) {
            $("#btnSalvarUsuario").removeAttr('disabled');
        }
        else {
            $("#btnSalvarUsuario").attr('disabled', 'disabled');


            $("#lblErroUsuario").text('As senhas não conferem, verifique');
            $("#lblErroUsuario").removeClass('d-none');
        }
    }
    else {
        $("#btnSalvarUsuario").attr('disabled', 'disabled');


        $("#lblErroUsuario").text('Para prosseguir, informe a sua senha e a confirmação.');
        $("#lblErroUsuario").removeClass('d-none');
    }
}