$(document).ready(function () {      

    $('#image-list-container > ul > li > img').each(function (index) {
        $(this).delay(Math.random() * 500 + 100).fadeTo('slow', 1);
    }); 
});