function onClickDetalhesPlacar(e) {

    /*$(`#idImgDetalhesPlacar_${e}`).toggle("img-accordion-cima");*/


    var element = document.getElementById(`idImgDetalhesPlacar_${e}`);
    element.classList.toggle("img-accordion-cima");
    element.classList.toggle("img-accordion-normal");

}