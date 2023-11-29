'use strict';

$(function () {
    $(".form-load-image").children("input").on('change', previewFile);
    $(".form-load-image").children("textarea").on('input', previewText);    
});



function previewFile({ target }) {
    const file = target.files[0];
    const reader = new FileReader();

    reader.readAsDataURL(file);

    reader.onload = () => {
        output.value = reader.result;
        preview.src = reader.result;
    };
}

function downloadFile() {
    const link = document.createElement("a");
    link.href = output.value;
    link.download = "image.png";
    link.click();
}

function previewText({ target }) {
    let base64 = target.value.replace(/^data:image\/[a-z]+;base64,/, "");
    preview.src = `data:image/png;base64,${base64}`;
}