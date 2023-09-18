$(document).on('click', '.photo-modal-close, .photo-modal-close-small', function () {
    $("#photo-modal").modal('hide');
});

function setModal(title, src) {
    $("#photo-modal-label").text(title);
    $("#imagePreview").attr({ src: src });
    $('#photo-modal').modal('show');
}