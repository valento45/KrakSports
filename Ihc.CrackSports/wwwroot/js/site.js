$(document).ready(() => {
    $('[data-toggle="tooltip"]').tooltip();
    $(".valida-campos-obrigatorios").on("focusout", onFocusOutCamposObrigatorios);
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