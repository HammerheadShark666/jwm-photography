import { Photo } from "../classes/Photo.js";
const photo = new Photo();

let contextMenuAction = "";
let callingElement;

$(document).ready(function () {
    document.onclick = hideMenu;
}); 

export function rightClick(e, actionName, target) {
    e.preventDefault();

    contextMenuAction = actionName;

    callingElement = target;

    if (document.getElementById("contextMenu").style.display == "block") {
        hideMenu();
    } else {
        var menu = document.getElementById("contextMenu")
        menu.style.display = 'block';
        menu.style.left = e.pageX + "px";
        menu.style.top = e.pageY + "px";
    }
}
function hideMenu() {
    document.getElementById("contextMenu")
        .style.display = "none"
}

$("#contextMenu ul li").on("click", function (e) {

    e.preventDefault();

    if (contextMenuAction == "addPhotoToFavourites")
        addPhotoToFavourites();
    else if(contextMenuAction == "deletePhotoFromFavourites")
        deletePhotoFromFavourites(); 
});

function addPhotoToFavourites() {
    let photoId = $(callingElement).attr("id");
    photo.addPhotoToFavourites(photoId).then(function (response) {
        $(callingElement).parent().append("<img class='home-image-favourite' src='images/icons8-orange-heart-48.png' class='like-icon'>"); 
    }).catch((response) => {
        showModalMessage("Favourites", "Photo failed to be added to favourites.");
    });
}

function deletePhotoFromFavourites() {
    let photoId = $(callingElement).attr("id");

    photo.deletePhotoFromFavourites(photoId).then(function (response) {
        $(callingElement).closest('img').remove(); 
    }).catch((response) => {
        showModalMessage("Favourites", "Photo failed to be deleted from favourites.");
    });
}