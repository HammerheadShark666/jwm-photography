$(document).ready(function () {
  
    $('#gallery-images > img').each(function (index) {
        $(this).delay(Math.random() * 500 + 100).fadeTo('slow', 1);
    });

    $('.gallery-image-favourite').each(function (index) {
        $(this).delay(Math.random() * 500 + 100).fadeTo('slow', 1);
    });
});