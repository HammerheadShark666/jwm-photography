import { Gallery } from "../classes/Gallery.js";
import * as main from '../main.js'

const gallery = new Gallery();

$(document).ready(function () {
    selectMenuItem();
});

$(document).on('click', '.photoModalClose, .photoModalCloseSmall', function () {
    $("#photoModal").modal('hide');
});
 
$('.sidebar ul li a').click(function () {
    var id = $(this).attr('id');
    $('nav ul li ul.item-show-' + id).toggleClass("show");
    $('nav ul li #' + id + ' span').toggleClass("rotate");

});

$('nav ul li').click(function () {
    $("nav ul li").removeClass("active");
    $(this).addClass("active");
}); 

$(document).on('click', '#new-gallery', function () {
    $("#newGalleryModal").modal('show');
    $("#new-gallery-name").val("");
    $("#new-gallery-alert").hide();
});

$(document).on('click', '#save-new-gallery', function () {
        
    let galleryName = $("#new-gallery-name").val();

    if (galleryName != "") {
        gallery.saveNewGallery(galleryName).then(function (gallery) {           
            $("#selected-gallery-id").val(gallery.id);
            window.location = "https://localhost:7166/admin/gallery/" + gallery.id;

        }).catch((response) => {    
            main.showAlert(response, "#new-gallery-alert");
        });
    }
});

document.addEventListener("DOMContentLoaded", function () {
    document.querySelectorAll('.dropdown-menu').forEach(function (element) {
        element.addEventListener('click', function (e) {
            e.stopPropagation();
        });
    })
}); 

function selectMenuItem() {

    const queryString = window.location.search;
    const urlParams = new URLSearchParams(queryString);
    const menuItem = urlParams.get('menu');

    if (menuItem === "galleries")
        $('.menu li a[id="2"]').click();
}