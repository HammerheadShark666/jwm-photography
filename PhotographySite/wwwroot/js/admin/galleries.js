import { Gallery } from "../classes/Gallery.js";
import * as main from '../main.js'

const gallery = new Gallery();

let pageSize = 20;
let azureStoragePhotosContainerUrl = "";
 
$(document).ready(function () {   
    $('#search-country-select').prepend("<option value='0' selected='selected'>Select Country...</option>");        
    $('#search-category-select').prepend("<option value='0' selected='selected'>Select Category...</option>");
    $('#search-palette-select').prepend("<option value='0' selected='selected'>Select Palette...</option>"); 
    setGallaryMenuItem();  
    initialiseDragAndDrop();   
    initialiseGallerySearchPhotosResultsPagination(1, false);
    history.pushState("", document.title, window.location.pathname);
});
 
$(document).on('click', '.gallery-thumbnail', function () {
    $("#photoModalLabel").text($(this).attr("data-title"));
    $("#imagePreview").attr("src", $(this).attr("src"));
    $('#photoModal').modal('show');
});

$(document).on('click', '.edit-gallery-name', function () {
    $("#editGalleryNameModal").modal('show');
    $("#edit-gallery-name").val("");
    $("#edit-gallery-alert").hide();
});

$(document).on('click', '.delete-gallery', function () {
    $("#deleteGalleryModal").modal('show');
});

$(document).on('click', '#delete-gallery', function () {

    let galleryId = $("#selected-gallery-id").val(); 

    gallery.deleteGallery(galleryId).then(function () {
        $("#deleteGalleryModal").modal('hide');
        window.location = "https://localhost:7166/admin?menu=galleries";
    }).catch((response) => {
        main.showAlert(response, "#edit-gallery-alert"); 
    });
});

$(document).on('click', '#save-gallery-name', function () { 

    let galleryId = $("#selected-gallery-id").val();  
    let galleryName = $("#edit-gallery-name").val(); 

    if (galleryName != "") { 

        gallery.saveGalleryName(galleryId, galleryName).then(function () {

            $("#galley-name-title").text("Gallery (" + galleryName + ")"); 
            $("#editGalleryNameModal").modal('hide');
            $("#edit-gallery-name").val("");
            $('li[data-gallary-menu-id="' + galleryId + '"]').find("a").text(galleryName);
            sortGalleryMenuItems();
        }).catch((response) => {
            main.showAlert(response, "#edit-gallery-alert"); 
        });
    }
});

$(document).on('click', '.gallery-search-photos-search-button', function () {     
    $('#gallery-search-photos-results-pagination').hide();
    searchPhotosForGallery(1);
});

function initialiseGallerySearchPhotosResultsPagination(numberOfPhotos, showPaginator) {

    $('#gallery-search-photos-results-pagination').pagination({
        items: numberOfPhotos,
        itemsOnPage: pageSize,
        cssStyle: 'dark-theme',
        onPageClick: function (pageNumber) { 
            searchPhotosForGallery(pageNumber);               
        }
    }); 

    showPaginationController(showPaginator);
}

function showPaginationController(showPaginator) {
    if (showPaginator)
        $('#gallery-search-photos-results-pagination').show();
    else
        $('#gallery-search-photos-results-pagination').hide();
}

function sortGalleryMenuItems() {

    var menuGalleryList = $('.menu-galleries-list')
    var menuGalleryListItems = $('li', menuGalleryList).get();
    var newGalleryItem = menuGalleryListItems[0];

    menuGalleryListItems.shift();

    menuGalleryListItems.sort(function (a, b) {
        var compA = $(a).text().toUpperCase();
        var compB = $(b).text().toUpperCase();
        return (compA < compB) ? -1 : 1;
    });

    menuGalleryListItems.splice(0, 0, newGalleryItem);

    $.each(menuGalleryListItems, function (i, item) {
        menuGalleryList.append(item);
    });
} 
 
function searchPhotosForGallery(pageNumber) {

    $('#gallerySource').empty(); 

    let countryId = parseInt($("#search-country-select").val());
    let categoryId = parseInt($("#search-category-select").val());
    let paletteId = parseInt($("#search-palette-select").val());
    let title = $("#gallery-search-title").val();     

    gallery.searchPhotos(title, countryId, categoryId, paletteId, pageNumber, pageSize).then(function (response) {    
        azureStoragePhotosContainerUrl = response.azureStoragePhotosContainerUrl;
        $("#gallerySource").append(getPhotoElements(response.photos)); 
        initialiseGallerySearchPhotosResultsPagination(response.numberOfPhotos, true); 
        $('#gallery-search-photos-results-pagination').pagination('drawPage', pageNumber); 
    }).catch((response) => {
        main.showAlert(response, "#gallery-alert");
    });
}

function getPhotoElements(photos) {

    let photoElements = "";

    photos.forEach(function (photo) {
        if (!photoInGallery(photo.id)) {
            photoElements = photoElements + getLiImageElement(photo);
        }
    });

    return photoElements;
}

function initialiseDragAndDrop() {

    let source = "";
    let destination = "";

    $("#gallerySource, #galleryDestination").sortable({
        helper: "clone",
        opacity: 0.5,
        cursor: "crosshair",
        connectWith: ".list",
        receive: function (event) {
            if(destination == "")
                destination = event.target.id;
        },
        start() {
            source = "";
            destination = "";
        },
        update: function (event) {           
            if(source == "")
                source = event.target.id;
        },         
        stop: function (event, ui) {
             
            let photoId = $(ui.item[0]).find("img").attr("data-photo-id");
            let galleryId = $("#selected-gallery-id").val();  

            if (source === 'gallerySource' && destination === 'galleryDestination') {
                movePhotoFromSourceToGallery(ui.item, galleryId, photoId);              
            } else if (source === 'galleryDestination' && destination === '') {    
                movePhotoFromGalleryToGallery(ui.item, galleryId, photoId);
            } else if (source === 'galleryDestination' && destination === 'gallerySource') {
                movePhotoFromGalleryToSource(ui.item, galleryId, photoId);
            }                   
        }
    });

    $("#gallerySource, #galleryDestination").disableSelection();
}

function movePhotoFromSourceToGallery(liElement, galleryId, photoId) {
   
    var order = $(liElement).prevAll().length + 1;
    gallery.savePhotoToGallery(galleryId, photoId, order).then(function (id) {
        updateOrderOfPhotos(liElement, order);
    }).catch((response) => {
        main.showAlert(response, "#gallery-alert"); 
    });
}

function movePhotoFromGalleryToGallery(liElement, galleryId, photoId) {

    var order = $(liElement).prevAll().length + 1;
    gallery.movePhotoInGallery(galleryId, photoId, order).then(function (id) {
        updateOrderOfPhotos(liElement);
    }).catch((response) => {
        main.showAlert(response, "#gallery-alert"); 
    });
}

function movePhotoFromGalleryToSource(liElement, galleryId, photoId) {

    var order = $(liElement).prevAll().length;
    gallery.removePhotoInGallery(galleryId, photoId, order).then(function () {
        updateOrderOfPhotos(liElement);
    }).catch((response) => {
        main.showAlert(response, "#gallery-alert");
    });
}

function updateOrderOfPhotos(liElement) {

    let order = 1;
    let listLiElements = $(liElement).parent().children();

    listLiElements.each(function (index, element) {
        $(element).find("img").attr("data-photo-order", order);
        order++;
    });
}

function getLiImageElement(photo) {      
    return "<li><img data-photo-id='" + photo.id + "' data-title='" + photo.title + "' class='draggable-img gallery-thumbnail' src='" + azureStoragePhotosContainerUrl + photo.fileName + "' /></li>";
}

function setGallaryMenuItem() {
    $("nav ul li").removeClass("active");

    var selectedGalleryId = $("#selected-gallery-id").val();
    var menuItem = $('[data-gallary-menu-id="' + selectedGalleryId + '"]');

    $(menuItem).addClass("active");
    $('nav ul li ul.item-show-2').toggleClass("show");
    $('nav ul li #2 span').toggleClass("rotate");
}

function photoInGallery(sourcePhotoId) {

    let photoInGallery = false;

    $('#galleryDestination li img').each(function (index, element) {
        let galleryPhotoId = parseInt($(element).attr("data-photo-id"));

        if (galleryPhotoId === sourcePhotoId) {
            photoInGallery = true;
            return;
        }            
    });

    return photoInGallery;
}