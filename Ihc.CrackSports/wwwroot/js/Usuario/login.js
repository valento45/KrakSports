$(document).ready(() => {

    $("#btnLogar").on("click", onClickLogar);
});


function onClickLogar() {

    var loginModel = new Object();
    loginModel.UserName = $("#txtUserNameLogin").val();
    loginModel.Password = $("#txtSenhaLogin").val();
    loginModel.PasswordHash = $("#txtSenhaLogin").val();

    util.ajax.post("../Home/Login", loginModel, loginSucesso, loginErro);
}

function loginSucesso(e) {
    $("#pnlMsgTryLogin").addClass("d-none");

    if (e.message) {

        $("#msgTryLoginUser").text(e.message);
        $("#pnlMsgTryLogin").removeClass("d-none");
    } else {
        var newDoc = document.open("text/html", "replace");
        newDoc.write(e);
        newDoc.close();
    }
}

function loginErro(e) {
    alert("O processo demorou mais que o esperado e por isso foi cancelado. Por favor, tente mais tarde");
}

function redirectCadastro() {

    window.location.href = "../Usuario/Index";
}


function apenasNumeros(string) {
    var numsStr = string.replace(/[^0-9]/g, '');
    return parseInt(numsStr);
}