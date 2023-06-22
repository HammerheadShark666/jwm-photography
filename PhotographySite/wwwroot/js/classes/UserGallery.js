import * as ajax from '../ajax.js'

class UserGallery {

    searchPhotos(title, countryId, categoryId, paletteId, pageIndex, pageSize) {
               
        let data = JSON.stringify(this.filter(title, countryId, categoryId, paletteId, pageIndex, pageSize));

        return new Promise(function (resolve, reject) {

            ajax.ajaxCall("POST", "/favourites/search", data).then(function (jqXHR) {
                return resolve(jqXHR);
            }).catch((jqXHR) => {
                return reject(jqXHR);
            });
        }); 
    }      

    savePhotoToUserGallery(userGalleryId, photoId, order) {

        let data = JSON.stringify(this.photo(userGalleryId, photoId, order));

        return new Promise(function (resolve, reject) {

            ajax.ajaxCall("POST", "/user/gallery-photos/add", data).then(function (jqXHR) {
                return resolve(jqXHR);
            }).catch((jqXHR) => {
                return reject(jqXHR);
            });
        });
    }

    movePhotoInUserGallery(userGalleryId, photoId, order) {
               
        let data = JSON.stringify(this.photo(userGalleryId, photoId, order));

        return new Promise(function (resolve, reject) {

            ajax.ajaxCall("POST", "/user/gallery-photos/move", data).then(function (jqXHR) {
                return resolve(jqXHR);
            }).catch((jqXHR) => {
                return reject(jqXHR);
            });
        });
    }

    removePhotoInUserGallery(userGalleryId, photoId, order) {
               
        let data = JSON.stringify(this.photo(userGalleryId, photoId, order));

        return new Promise(function (resolve, reject) {

            ajax.ajaxCall("POST", "/user/gallery-photos/remove", data).then(function (jqXHR) {
                return resolve(jqXHR);
            }).catch((jqXHR) => {
                return reject(jqXHR);
            });
        });
    }

    saveUserGalleryName(id, name) {

        let data = JSON.stringify(this.userGallery(id, name));

        return new Promise(function (resolve, reject) {

            ajax.ajaxCall("POST", "/user/gallery/save/name", data).then(function (jqXHR) {
                return resolve(jqXHR);
            }).catch((jqXHR) => {
                return reject(jqXHR);
            });
        });
    }

    saveNewUserGallery(name) {
              
        let data = JSON.stringify(this.userGallery(0, name));

        return new Promise(function (resolve, reject) {

            ajax.ajaxCall("POST", "/user/gallery/new/save", data).then(function (jqXHR) {
                return resolve(jqXHR);
            }).catch((jqXHR) => {
                return reject(jqXHR);
            });
        });
    }

    deleteUserGallery(id) {
         
        return new Promise(function (resolve, reject) {

            ajax.ajaxCall("DELETE", "/user/gallery/delete/" + id, "").then(function (jqXHR) {
                return resolve(jqXHR);
            }).catch((jqXHR) => {
                reject(jqXHR);
            });
        });
    }

    photo(userGalleryId, photoId, order) {
        const photo = {
            userGalleryId: userGalleryId,
            photoId: photoId,
            order: order,
        };

        return photo;
    }

    userGallery(id, name) {
        const userGallery = {
            id: id,
            name: name
        };

        return userGallery;
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

export { UserGallery };