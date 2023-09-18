$(function () {
  
    $('.latest-photo-thumbnail').each(function (index) {
        $(this).delay(Math.random() * 500 + 100).fadeTo('slow', 1);
    }); 

    setAdminMenuItem(1, 0);
});