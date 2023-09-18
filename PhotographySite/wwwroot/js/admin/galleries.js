const menuItemIndex = 3; 

let pageSize = 20, 
    azureStoragePhotosContainerUrl = "",
    gallerySearchAlerts = $("#gallery-search-alerts"),
    galleryModalAlerts = $("#gallery-modal-alerts"),
    editGalleryName = $("#edit-gallery-name"),
    deleteGalleryModal = $("#delete-gallery-modal"),
    deleteGalleryAlerts =  $('#delete-gallery-alerts'),
    editGalleryNameModal = $("#edit-gallery-name-modal"),
    gallerySearchPhotosResultsPagination = $('#gallery-search-photos-results-pagination'),
    gallerySource = $('#gallery-source');

$(function () {
    gallerySearchAlerts.empty();
    galleryModalAlerts.empty();
    initialiseDropDowns();  
    setAdminMenuItem(menuItemIndex); 
    initialiseDragAndDrop();   
    initialiseGallerySearchPhotosResultsPagination(1, false);
    history.pushState("", document.title, window.location.pathname);
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
    $("#photo-modal-label").text($(this).attr("data-title"));
    $("#imagePreview").attr({ src: $(this).attr("src") });
    $('#photo-modal').modal('show');
});

$(document).on('click', '.edit-gallery-name', function () {    
    editGalleryName.val(""); 
    galleryModalAlerts.empty();
    editGalleryNameModal.modal('show');
});

$(document).on('click', '.delete-gallery', function () {
    deleteGalleryModal.modal('show');
});

$(document).on('click', '#delete-gallery', function () {

    let galleryId = $("#selected-gallery-id").val(); 

    gallery.deleteGallery(galleryId).then(function () {
        deleteGalleryModal.modal('hide');
        window.location = baseUrl + "/admin?menu=galleries";
    }).catch((error) => {
        error.response.data.messages.forEach(function (i) { addAlert(i, deleteGalleryAlerts); }); 
    });
});

$(document).on('click', '#save-gallery-name', function () { 

    let galleryId = $("#selected-gallery-id").val(),  
        galleryName = editGalleryName.val(); 

    if (galleryName != "") { 

        gallery.saveGalleryName(galleryId, galleryName).then(function () {

            $('li[data-gallary-menu-id="' + galleryId + '"]').find("a").text(galleryName);
            $("#galley-name-title").text("Gallery (" + galleryName + ")"); 
            editGalleryName.val("");
            editGalleryNameModal.modal('hide');     
            sortGalleryMenuItems();
        }).catch((error) => { 
            error.response.data.messages.forEach(function (i) { addAlert(i, galleryModalAlerts); });
        });
    }
});

$(document).on('click', '.gallery-search-photos-search-button', function () {     
    gallerySearchPhotosResultsPagination.hide();
    searchPhotosForGallery(1);
});

function initialiseGallerySearchPhotosResultsPagination(numberOfPhotos, showPaginator) {

    gallerySearchPhotosResultsPagination.pagination({
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
        gallerySearchPhotosResultsPagination.show();
    else
        gallerySearchPhotosResultsPagination.hide();
}

function sortGalleryMenuItems() {

    var menuGalleryList = $('.menu-galleries-list'),
        menuGalleryListItems = $('li', menuGalleryList).get(),
        newGalleryItem = menuGalleryListItems[0];

    menuGalleryListItems.shift();

    menuGalleryListItems.sort(function (a, b) {
        var compA = $(a).text().toUpperCase(),
            compB = $(b).text().toUpperCase();
        return (compA < compB) ? -1 : 1;
    });

    menuGalleryListItems.splice(0, 0, newGalleryItem);

    $.each(menuGalleryListItems, function (i, item) {
        menuGalleryList.append(item);
    });
} 
 
function searchPhotosForGallery(pageNumber) {

    gallerySource.empty(); 
    gallerySearchAlerts.empty(); 

    let countryId = parseInt($("#search-country-select").val()),
        categoryId = parseInt($("#search-category-select").val()),
        paletteId = parseInt($("#search-palette-select").val()),
        title = $("#gallery-search-title").val();     

    gallery.searchPhotos(title, countryId, categoryId, paletteId, pageNumber, pageSize).then(function (response) {    
        azureStoragePhotosContainerUrl = response.data.azureStoragePhotosContainerUrl;
        gallerySource.append(getPhotoElements(response.data.photos)); 
        initialiseGallerySearchPhotosResultsPagination(response.data.numberOfPhotos, true); 
        gallerySearchPhotosResultsPagination.pagination('drawPage', pageNumber); 
    }).catch((error) => { 
        error.response.data.messages.forEach(function (i) { addAlert(i, gallerySearchAlerts); });
    });
}

function getPhotoElements(photos) {

    let photoElements = "";

    photos.forEach(function (photo) {
        if (!photoInGallery(photo.id))  
            photoElements = photoElements + getLiImageElement(photo); 
    });

    return photoElements;
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
        galleryId = $("#selected-gallery-id").val();

    if (source === 'gallery-source' && destination === 'gallery-destination') {
        movePhotoFromSourceToGallery(ui.item, galleryId, photoId);
    } else if (source === 'gallery-destination' && destination === '') {
        movePhotoFromGalleryToGallery(ui.item, galleryId, photoId);
    } else if (source === 'gallery-destination' && destination === 'gallery-source') {
        movePhotoFromGalleryToSource(ui.item, galleryId, photoId);
    }
}

function movePhotoFromSourceToGallery(liElement, galleryId, photoId) {
   
    var order = $(liElement).prevAll().length + 1;
    gallery.savePhotoToGallery(galleryId, photoId, order).then(function (id) {
        updateOrderOfPhotos(liElement, order);
    }).catch((response) => {
        addAlert(response, "#gallery-alert"); 
    });
}

function movePhotoFromGalleryToGallery(liElement, galleryId, photoId) {

    var order = $(liElement).prevAll().length + 1;
    gallery.movePhotoInGallery(galleryId, photoId, order).then(function (id) {
        updateOrderOfPhotos(liElement);
    }).catch((response) => {
        addAlert(response, "#gallery-alert"); 
    });
}

function movePhotoFromGalleryToSource(liElement, galleryId, photoId) {

    var order = $(liElement).prevAll().length;
    gallery.removePhotoInGallery(galleryId, photoId, order).then(function () {
        updateOrderOfPhotos(liElement);
    }).catch((response) => {
        addAlert(response, "#gallery-alert");
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

function getLiImageElement(photo) {      
    return "<li><img data-photo-id='" + photo.id + "' data-title='" + photo.title + "' class='draggable-img gallery-thumbnail' src='" + azureStoragePhotosContainerUrl + photo.fileName + "' /></li>";
} 

function photoInGallery(sourcePhotoId) {

    let photoInGallery = false;

    $('#gallery-destination li img').each(function (index, element) {
        let galleryPhotoId = parseInt($(element).attr("data-photo-id"));

        if (galleryPhotoId === sourcePhotoId) {
            photoInGallery = true;
            return;
        }            
    });

    return photoInGallery;
}