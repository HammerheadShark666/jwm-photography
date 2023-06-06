import { rightClick } from './contextMenu.js';

$(document).on('contextmenu', '.image-list li', function (e) {

    let isFavourite = $(e.currentTarget).find(".home-image-container").find(".home-image-favourite").length > 0;

    if (!isFavourite)
        rightClick(e, "addPhotoToFavourites", e.target);
});