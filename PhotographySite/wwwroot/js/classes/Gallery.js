import * as ajax from '../ajax.js'

class Gallery {

    searchPhotos(title, countryId, categoryId, paletteId, pageIndex, pageSize) {
               
        let data = JSON.stringify(this.filter(title, countryId, categoryId, paletteId, pageIndex, pageSize));

        return new Promise(function (resolve, reject) {

            ajax.ajaxCall("POST", "/admin/gallery/search", data).then(function (jqXHR) {
                return resolve(jqXHR);
            }).catch((jqXHR) => {
                return reject(jqXHR);
            });
        }); 
    }      

    savePhotoToGallery(galleryId, photoId, order) {

        let data = JSON.stringify(this.photo(galleryId, photoId, order));

        return new Promise(function (resolve, reject) {

            ajax.ajaxCall("POST", "/admin/gallery-photos/add", data).then(function (jqXHR) {
                return resolve(jqXHR);
            }).catch((jqXHR) => {
                return reject(jqXHR);
            });
        });
    }

    movePhotoInGallery(galleryId, photoId, order) {
               
        let data = JSON.stringify(this.photo(galleryId, photoId, order));

        return new Promise(function (resolve, reject) {

            ajax.ajaxCall("POST", "/admin/gallery-photos/move", data).then(function (jqXHR) {
                return resolve(jqXHR);
            }).catch((jqXHR) => {
                return reject(jqXHR);
            });
        });
    }

    removePhotoInGallery(galleryId, photoId, order) {
               
        let data = JSON.stringify(this.photo(galleryId, photoId, order));

        return new Promise(function (resolve, reject) {

            ajax.ajaxCall("POST", "/admin/gallery-photos/remove", data).then(function (jqXHR) {
                return resolve(jqXHR);
            }).catch((jqXHR) => {
                return reject(jqXHR);
            });
        });
    }

    saveGalleryName(id, name) {

        let data = JSON.stringify(this.gallery(id, name));

        return new Promise(function (resolve, reject) {

            ajax.ajaxCall("POST", "/admin/gallery/save/name", data).then(function (jqXHR) {
                return resolve(jqXHR);
            }).catch((jqXHR) => {
                return reject(jqXHR);
            });
        });
    }

    saveNewGallery(name) {
              
        let data = JSON.stringify(this.gallery(0, name));

        return new Promise(function (resolve, reject) {

            ajax.ajaxCall("POST", "/admin/gallery/new/save", data).then(function (jqXHR) {
                return resolve(jqXHR);
            }).catch((jqXHR) => {
                return reject(jqXHR);
            });
        });
    }

    deleteGallery(id) {
         
        return new Promise(function (resolve, reject) {

            ajax.ajaxCall("DELETE", "/admin/gallery/delete/" + id, "").then(function (jqXHR) {
                return resolve(jqXHR);
            }).catch((jqXHR) => {
                reject(jqXHR);
            });
        });
    }

    photo(galleryId, photoId, order) {
        const photo = {
            galleryId: galleryId,
            photoId: photoId,
            order: order,
        };

        return photo;
    }

    gallery(id, name) {
        const gallery = {
            id: id,
            name: name
        };

        return gallery;
    }

    filter(title, countryId, categoryId, paletteId, pageIndex, pageSize) {
        const filter = {
            title: title,
            countryId: parseInt(countryId),
            categoryId: parseInt(categoryId),
            paletteId: parseInt(paletteId),
            pageIndex: parseInt(pageIndex),
            pageSize: parseInt(pageSize)
        };

        return filter;
    }
}

export { Gallery };