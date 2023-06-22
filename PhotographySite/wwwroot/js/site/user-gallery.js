import { UserGallery } from "../classes/UserGallery.js";
import * as main from '../main.js'

const userGallery = new UserGallery();


let pageSize = 20;
let azureStoragePhotosContainerUrl = "";
 
$(document).ready(function () {   
    $('#search-country-select').prepend("<option value='0' selected='selected'>Select Country...</option>");        
    $('#search-category-select').prepend("<option value='0' selected='selected'>Select Category...</option>");
    $('#search-palette-select').prepend("<option value='0' selected='selected'>Select Palette...</option>"); 
    setUserGallaryMenuItem();  
    initialiseDragAndDrop();   
    initialiseUserGallerySearchPhotosResultsPagination(1, false);
    history.pushState("", document.title, window.location.pathname);

    main.hideAlert("#gallery-alert");
});

$(document).on('click', '.photoModalClose, .photoModalCloseSmall', function () {
    $("#photoModal").modal('hide');
});
 
$(document).on('click', '.gallery-thumbnail', function () {
    $("#photoModalLabel").text($(this).attr("data-title"));
    $("#imagePreview").attr("src", $(this).attr("src"));
    $('#photoModal').modal('show');
});

$(document).on('click', '.edit-user-gallery-name', function () {
    let galleryName = $(this).attr("data-gallery-name")
    $("#editUserGalleryNameModal").modal('show');
    $("#edit-user-gallery-name").val(galleryName);
    $("#edit-user-gallery-alert").hide();
});

$(document).on('click', '.delete-user-gallery', function () {
    $("#deleteUserGalleryModal").modal('show');
});

$(document).on('click', '#delete-user-gallery', function () {

    let galleryId = $("#selected-user-gallery-id").val(); 
    userGallery.deleteUserGallery(galleryId).then(function () {
        $("#deleteUserGalleryModal").modal('hide');
        window.location = "https://localhost:7166";
    }).catch((response) => {
        main.showAlert(response, "#edit-user-gallery-alert"); 
    });
});

$(document).on('click', '#save-user-gallery-name', function () { 

    let galleryId = $("#selected-user-gallery-id").val();  
    let galleryName = $("#edit-user-gallery-name").val(); 

    if (galleryName != "") { 

        userGallery.saveUserGalleryName(galleryId, galleryName).then(function () {

            $("#user-galley-name-title").text("Gallery (" + galleryName + ")"); 
            $("#editUserGalleryNameModal").modal('hide');
            $("#edit-user-gallery-name").val("");
            $(".edit-user-gallery-name").attr("data-gallery-name", galleryName);
            $('li[data-user-gallary-menu-id="' + galleryId + '"]').find("a").text(galleryName);
            sortUserGalleryMenuItems();
        }).catch((response) => {
            main.showAlert(response.responseJSON, "#edit-user-gallery-alert"); 
        });
    }
});

$(document).on('click', '.gallery-search-photos-search-button', function () {     
    $('#gallery-search-photos-results-pagination').hide();
    searchPhotosForUserGallery(1);
});

function initialiseUserGallerySearchPhotosResultsPagination(numberOfPhotos, showPaginator) {

    $('#gallery-search-photos-results-pagination').pagination({
        items: numberOfPhotos,
        itemsOnPage: pageSize,
        cssStyle: 'dark-theme',
        onPageClick: function (pageNumber) { 
            searchPhotosForUserGallery(pageNumber);               
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
 
function searchPhotosForUserGallery(pageNumber) {

    $('#gallerySource').empty(); 

    let countryId = parseInt($("#search-country-select").val());
    let categoryId = parseInt($("#search-category-select").val());
    let paletteId = parseInt($("#search-palette-select").val());
    let title = $("#gallery-search-title").val();     

    userGallery.searchPhotos(title, countryId, categoryId, paletteId, pageNumber, pageSize).then(function (response) {    
        azureStoragePhotosContainerUrl = response.azureStoragePhotosContainerUrl;
        $("#gallerySource").append(getPhotoElements(response.photos)); 
        initialiseUserGallerySearchPhotosResultsPagination(response.numberOfPhotos, true); 
        $('#gallery-search-photos-results-pagination').pagination('drawPage', pageNumber); 
    }).catch((response) => {
        main.showAlert(response, "#gallery-alert");
    });
}

function getPhotoElements(photos) {

    let photoElements = "";

    photos.forEach(function (photo) {
        if (!photoInUserGallery(photo.id)) {
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
            let galleryId = $("#selected-user-gallery-id").val();  

            if (source === 'gallerySource' && destination === 'galleryDestination') {
                movePhotoFromSourceToUserGallery(ui.item, galleryId, photoId);              
            } else if (source === 'galleryDestination' && destination === '') {    
                movePhotoFromUserGalleryToUserGallery(ui.item, galleryId, photoId);
            } else if ((source === 'galleryDestination' && destination === 'gallerySource') || (source === '' && destination === '')) {
                movePhotoFromUserGalleryToSource(ui.item, galleryId, photoId);
            }                   
        }
    });

    $("#gallerySource, #galleryDestination").disableSelection();
}

function movePhotoFromSourceToUserGallery(liElement, galleryId, photoId) {
   
    var order = $(liElement).prevAll().length + 1;
    userGallery.savePhotoToUserGallery(galleryId, photoId, order).then(function (id) {
        updateOrderOfPhotos(liElement, order);
    }).catch((response) => {
        main.showAlert(response, "#gallery-alert"); 
    });
}

function movePhotoFromUserGalleryToUserGallery(liElement, galleryId, photoId) {

    var order = $(liElement).prevAll().length + 1;
    userGallery.movePhotoInUserGallery(galleryId, photoId, order).then(function (id) {
        updateOrderOfPhotos(liElement);
    }).catch((response) => {
        main.showAlert(response, "#gallery-alert"); 
    });
}

function movePhotoFromUserGalleryToSource(liElement, galleryId, photoId) {

    var order = $(liElement).prevAll().length;
    userGallery.removePhotoInUserGallery(galleryId, photoId, order).then(function () {
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

function setUserGallaryMenuItem() {
    $("nav ul li").removeClass("active");

    var selectedUserGalleryId = $("#selected-gallery-id").val();
    var menuItem = $('[data-gallary-menu-id="' + selectedUserGalleryId + '"]');

    $(menuItem).addClass("active");
    $('nav ul li ul.item-show-2').toggleClass("show");
    $('nav ul li #2 span').toggleClass("rotate");
}

function photoInUserGallery(sourcePhotoId) {

    let photoInUserGallery = false;

    $('#galleryDestination li img').each(function (index, element) {
        let userGalleryPhotoId = parseInt($(element).attr("data-photo-id"));

        if (userGalleryPhotoId === sourcePhotoId) {
            photoInUserGallery = true;
            return;
        }            
    });

    return photoInUserGallery;
}