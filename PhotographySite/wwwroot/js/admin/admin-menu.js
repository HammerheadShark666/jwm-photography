
const gallery = new Gallery();

let navMenuListItem = $('nav ul li'),
    newGalleryName = $("#new-gallery-name"),
    newGalleryAlerts = $("#new-gallery-alert");

$(function () {
    selectMenuItem();
});

$('.sidebar ul li a').click(function () { 
    var id = $(this).attr('id');
    navMenuListItem.find('ul.item-show-' + id).toggleClass("show");
    navMenuListItem.find('#' + id + ' span').toggleClass("rotate"); 
}); 

$('nav ul li a').click(function () {
    navMenuListItem.find('a').removeClass("active"); 
    $(this).addClass("active");
});
 
$(document).on('click', '#new-gallery', function () {
    newGalleryName.val(""); 
    newGalleryAlerts.empty();
    $("#new-gallery-modal").modal('show');  
});

$(document).on('click', '#save-new-gallery', function () {

    let galleryName = newGalleryName.val();

    newGalleryAlerts.empty();

    if (galleryName != "") {
        gallery.saveNewGallery(galleryName).then(function (response) {
            $("#selected-gallery-id").val(response.data.id);
            window.location = baseUrl + "/admin/gallery/" + response.data.id;

        }).catch((error) => {
            error.response.data.messages.forEach(function (i) { addAlert(i, newGalleryAlerts); }); 
        }); 
    }
});

document.addEventListener("DOMContentLoaded", function () {
    stopClickEventPropagation('.dropdown-menu');
});
 
function selectMenuItem() {
     
    const menuItem = new URLSearchParams(window.location.search).get('menu');

    if (menuItem === "galleries")
        $('.menu li a[id="2"]').click();
};