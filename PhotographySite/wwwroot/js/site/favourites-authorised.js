import { rightClick } from './contextMenu.js';

$(document).on('contextmenu', '#favourites-images img', function (e) {
    rightClick(e, "deletePhotoFromFavourites", e.target);
});