$(document).ready(() => {


});


function onClickInicio(e) {

    activeInactiveButton(e);



}
function onClickPatrocinadores(e) {
    activeInactiveButton(e);


}
function onClickClubes(e) {
    activeInactiveButton(e);


}
function onClickAtletas(e) {
    activeInactiveButton(e);


}
function onClickConfiguracoes(e) {
    activeInactiveButton(e);


}


function activeInactiveButton(e) {
    $(".nav-link").removeClass('active');
    $(".nav-link").addClass('text-white');

    $(e).addClass('active');
    $(e).removeClass('text-white');

}