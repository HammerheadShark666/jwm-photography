const photo = new Photo();

let contextMenuAction = "";
let callingElement;

$(document).ready(function () {
    document.onclick = hideMenu;
});

$(document).on('contextmenu', '#favourites-images img', function (e) {
    rightClick(e, "deletePhotoFromGallerys", e.target);
});

$(document).on('contextmenu', '.image-list li', function (e) { 
    let isGallery = $(e.currentTarget).find(".home-image-container").find(".home-image-favourite").length > 0;
    if (!isGallery)
        rightClick(e, "addPhotoToGallerys", e.target);
});

$(document).on('contextmenu', '#gallery-images div img', function (e) { 
    let isGallery = $(e.currentTarget).closest(".gallery-image-container").find(".gallery-image-favourite").length > 0;
    if (!isGallery)
        rightClick(e, "addPhotoToGallerys", e.target);
}); 

function rightClick(e, actionName, target) {
    e.preventDefault();

    contextMenuAction = actionName;

    callingElement = target;

    if (document.getElementById("context-menu").style.display == "block")
        hideMenu();
    else
        setMenuPosition(e);
}

function setMenuPosition(e) {
    var menu = document.getElementById("context-menu")
    menu.style.display = 'block';
    menu.style.left = e.pageX + "px";
    menu.style.top = e.pageY + "px";
}

function hideMenu() {
    document.getElementById("context-menu").style.display = "none"
}

$("#context-menu ul li").on("click", function (e) {

    e.preventDefault();

    if (contextMenuAction == "addPhotoToGallerys")
        addPhotoToGallerys();
    else if (contextMenuAction == "deletePhotoFromGallerys")
        deletePhotoFromGallerys();
});

function addPhotoToGallerys() {
    let photoId = $(callingElement).attr("id");
    photo.addPhotoToGallerys(photoId).then(function (response) {
        $(callingElement).parent().append("<img class='home-image-favourite' src='/images/icons8-orange-heart-48.png' class='like-icon'>");
        $(".home-image-favourite").delay(Math.random() * 500 + 100).fadeTo('slow', 1);
    }).catch((response) => {
        showModalMessage("Gallerys", "Photo failed to be added to favourites.");
    });
}

function deletePhotoFromGallerys() {
    let photoId = $(callingElement).attr("id");

    photo.deletePhotoFromGallerys(photoId).then(function (response) {
        $(callingElement).closest('img').remove();
    }).catch((response) => {
        showModalMessage("Gallerys", "Photo failed to be deleted from favourites.");
    });
}