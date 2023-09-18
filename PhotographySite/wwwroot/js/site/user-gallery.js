const userGallery = new UserGallery();

let pageSize = 20, 
    azureStoragePhotosContainerUrl = "",
    galleryAlerts = $("#gallery-alerts"),
    userGallerySearchPhotosResultsPagination = $('#user-gallery-search-photos-results-pagination'),
    editUserGalleryNameAlerts = $("#edit-user-gallery-name-alerts"),    
    editUserGalleryNameModal = $("#edit-user-gallery-name-modal"), 
    editUserGalleryName = $("#edit-user-gallery-name"), 
    deleteUserGalleryModal = $("#delete-user-gallery-modal"),  
    gallerySource = $('#gallery-source');

$(function () {  
    initialiseDropDowns();
    setUserGallaryMenuItem();  
    initialiseDragAndDrop();   
    initialiseUserGallerySearchPhotosResultsPagination(1, false);
    history.pushState("", document.title, window.location.pathname);

    galleryAlerts.empty();
}); 

function initialiseDropDowns() {
    $('#search-country-select').prepend(dropdownDefaultItem("Country"));
    $('#search-category-select').prepend(dropdownDefaultItem("Category"));
    $('#search-palette-select').prepend(dropdownDefaultItem("Palette"));
}

function dropdownDefaultItem(dropdownType) {
    return "<option value='0' selected='selected'>Select " + dropdownType + "...</option>";
}  
 
$(document).on('click', '.gallery-thumbnail', function () { 
    setModal($(this).attr("data-title"), $(this).attr("src"));
});

$(document).on('click', '.edit-user-gallery-name', function () {
    let galleryName = $(this).attr("data-gallery-name");
    editUserGalleryNameModal.modal('show');
    editUserGalleryName.val(galleryName); 
    editUserGalleryNameAlerts.empty();
});

$(document).on('click', '.delete-user-gallery', function () {
    deleteUserGalleryModal.modal('show');
});

$(document).on('click', '#delete-user-gallery', function () {

    let galleryId = $("#selected-user-gallery-id").val(); 
    userGallery.deleteUserGallery(galleryId).then(function () {
        deleteUserGalleryModal.modal('hide');
        window.location = baseUrl;
    }).catch((response) => {
        response.responseJSON.messages.forEach(function (i) { addAlert(i, editUserGalleryNameAlerts); });
    });
});

$(document).on('click', '.gallery-search-photos-search-button', function () {
    userGallerySearchPhotosResultsPagination.hide();
    searchPhotosForUserGallery(1);
});

$(document).on('click', '#save-user-gallery-name', function () { 

    let galleryId = $("#selected-user-gallery-id").val(),   
        galleryName = editUserGalleryName.val();  

    editUserGalleryNameAlerts.empty();

    if (galleryName != "") {  

        userGallery.saveUserGalleryName(galleryId, galleryName).then(function () { 
            
            editUserGalleryNameModal.modal('hide');
            editUserGalleryName.val("");
            editUserGalleryName.attr("data-gallery-name", galleryName);
            $("#user-galley-name-title").text("Gallery (" + galleryName + ")");             
            $('li[data-user-gallary-menu-id="' + galleryId + '"]').find("a").text(galleryName);
            sortUserGalleryMenuItems();
        }).catch((error) => {  
            error.response.data.messages.forEach(function (i) { addAlert(i, editUserGalleryNameAlerts); });
        });
    }
}); 
 
function initialiseUserGallerySearchPhotosResultsPagination(numberOfPhotos, showPaginator) {

    userGallerySearchPhotosResultsPagination.pagination({
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
        userGallerySearchPhotosResultsPagination.show();
    else
        userGallerySearchPhotosResultsPagination.hide();
}
 
function searchPhotosForUserGallery(pageNumber) {

    gallerySource.empty(); 

    let countryId = parseInt($("#search-country-select").val()),
        categoryId = parseInt($("#search-category-select").val()),
        paletteId = parseInt($("#search-palette-select").val()),
        title = $("#gallery-search-title").val();     

    userGallery.searchPhotos(title, countryId, categoryId, paletteId, pageNumber, pageSize).then(function (response) {    
        azureStoragePhotosContainerUrl = response.data.azureStoragePhotosContainerUrl;
        gallerySource.append(getPhotoElements(response.data.photos)); 
        initialiseUserGallerySearchPhotosResultsPagination(response.data.numberOfPhotos, true); 
        userGallerySearchPhotosResultsPagination.pagination('drawPage', pageNumber); 
    }).catch((error) => {  
        error.response.data.messages.forEach(function (i) { addAlert(i, galleryAlerts); });
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

function getLiImageElement(photo) {
    return "<li><img data-photo-id='" + photo.id + "' data-title='" + photo.title + "' class='draggable-img gallery-thumbnail' src='" + azureStoragePhotosContainerUrl + photo.fileName + "' /></li>";
}

function initialiseDragAndDrop() {

    let source = "",
        destination = "";

    $("#gallery-source, #gallery-destination").sortable({
        helper: "clone",
        opacity: 0.5,
        cursor: "crosshair",
        connectWith: ".list",
        receive: function (event) {
            if(destination == "")
                destination = event.target.id;
        },
        start: function () {
            source = "";
            destination = "";
        },
        update: function (event) {           
            if(source == "")
                source = event.target.id;
        },         
        stop: function (event, ui) {
            dropPhoto(ui, source, destination); 
        }
    });

    $("#gallery-source, #gallery-destination").disableSelection();
}

function dropPhoto(ui, source, destination) {

    let photoId = $(ui.item[0]).find("img").attr("data-photo-id"),
        galleryId = $("#selected-user-gallery-id").val();

    if (source === 'gallery-source' && destination === 'gallery-destination')
        movePhotoFromSourceToUserGallery(ui.item, galleryId, photoId);
    else if (source === 'gallery-destination' && destination === '')
        movePhotoFromUserGalleryToUserGallery(ui.item, galleryId, photoId);
    else if ((source === 'gallery-destination' && destination === 'gallery-source') || (source === '' && destination === ''))
        movePhotoFromUserGalleryToSource(ui.item, galleryId, photoId);
}

function movePhotoFromSourceToUserGallery(liElement, galleryId, photoId) {
   
    var order = $(liElement).prevAll().length + 1;
    userGallery.savePhotoToUserGallery(galleryId, photoId, order).then(function (id) {
        updateOrderOfPhotos(liElement, order);
    }).catch((error) => {
        error.response.data.messages.forEach(function (i) { addAlert(i, galleryAlerts); });
    });
}

function movePhotoFromUserGalleryToUserGallery(liElement, galleryId, photoId) {

    var order = $(liElement).prevAll().length + 1;
    userGallery.movePhotoInUserGallery(galleryId, photoId, order).then(function (id) {
        updateOrderOfPhotos(liElement);
    }).catch((error) => {
        error.response.data.messages.forEach(function (i) { addAlert(i, galleryAlerts); });
    });
}

function movePhotoFromUserGalleryToSource(liElement, galleryId, photoId) {

    var order = $(liElement).prevAll().length;
    userGallery.removePhotoInUserGallery(galleryId, photoId, order).then(function () {
        updateOrderOfPhotos(liElement);
    }).catch((error) => {
        error.response.data.messages.forEach(function (i) { addAlert(i, galleryAlerts); });
    });
}

function updateOrderOfPhotos(liElement) {

    let order = 1,
        listLiElements = $(liElement).parent().children();

    listLiElements.each(function (index, element) {
        $(element).find("img").attr("data-photo-order", order);
        order++;
    });
}
 
function setUserGallaryMenuItem() {

    let navListItem = $("nav ul li");

    navListItem.removeClass("active");

    var selectedUserGalleryId = $("#selected-gallery-id").val();
    var menuItem = $('[data-gallary-menu-id="' + selectedUserGalleryId + '"]');

    $(menuItem).addClass("active");
    navListItem.find('ul.item-show-2').toggleClass("show");
    navListItem.find('#2 span').toggleClass("rotate");
}

function photoInUserGallery(sourcePhotoId) {

    let photoInUserGallery = false; 

    $('#gallery-destination li img').each(function (index, element) {

        let userGalleryPhotoId = parseInt($(element).attr("data-photo-id"));

        if (userGalleryPhotoId === sourcePhotoId) {
            photoInUserGallery = true;
            return;
        }            
    });

    return photoInUserGallery;
}