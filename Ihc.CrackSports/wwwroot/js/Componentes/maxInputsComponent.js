$(document).ready(() => {


    $(".txt-max-input").on('keyup', onKeyUpTextMensagem);
});

const limiteCaractereMensagem = 3000;

function onKeyUpTextMensagem(e) {
    var $lblLimite = $(".lblLimiteCaractere");
    var $txtMensagem = $(".txt-max-input");


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