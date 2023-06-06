$(document).ready(function () {
  
    $('#favourites-images > img').each(function (index) {
        $(this).delay(Math.random() * 500 + 100).fadeTo('slow', 1);
    });
});