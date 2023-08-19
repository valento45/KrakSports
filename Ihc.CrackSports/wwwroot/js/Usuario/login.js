$(document).ready(() => {

    $("#btnLogar").on("click", onClickLogar);
});

function onFocusOutCamposObrigatorios(e) {

    var campo = $(`#${e.currentTarget.id}`);
    var label = $(`#${e.currentTarget.id}Label`);

    if (!campo.val()) {
        label.addClass("campos-invalidos");
        campo.addClass("campos-invalidos");
    }
    else {
        label.removeClass("campos-invalidos");
        campo.removeClass("campos-invalidos");
    }
}

function onClickLogar() {

    var loginModel = new Object();
    loginModel.UserName = $("#txtUserNameLogin").val();
    loginModel.Password = $("#txtSenhaLogin").val();
    loginModel.PasswordHash = $("#txtSenhaLogin").val();

    util.ajax.post("../Home/Login", loginModel, loginSucesso, loginErro);
}

function loginSucesso(e) {
   

    /*   window.location.href = "../Home/About";*/
    var newDoc = document.open("text/html", "replace");
    newDoc.write(e);
    newDoc.close();
}

function loginErro(e) {
    alert("login erro !!!!!!!!!!!!!!!!!!!");
}

function redirectCadastro() {

    window.location.href = "../Usuario/Index";
}


function apenasNumeros(string) {
    var numsStr = string.replace(/[^0-9]/g, '');
    return parseInt(numsStr);
}