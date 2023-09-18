class UserGallery {

    searchPhotos(title, countryId, categoryId, paletteId, pageIndex, pageSize) {
                
        let data = this.filter(title, countryId, categoryId, paletteId, pageIndex, pageSize);

        return new Promise(function (resolve, reject) {

            post("/favourites/search", data).then(function (response) {
                return resolve(response);
            }).catch((error) => {
                return reject(error);
            });
        }); 
    }      

    savePhotoToUserGallery(userGalleryId, photoId, order) {
         
        let data = this.photo(userGalleryId, photoId, order);

        return new Promise(function (resolve, reject) {

            post("/user/gallery-photos/add", data).then(function (response) {
                return resolve(response);
            }).catch((error) => {
                return reject(error);
            });
        });
    }

    movePhotoInUserGallery(userGalleryId, photoId, order) {
                
        let data = this.photo(userGalleryId, photoId, order);

        return new Promise(function (resolve, reject) {

            post("/user/gallery-photos/move", data).then(function (response) {
                return resolve(response);
            }).catch((error) => {
                return reject(error);
            });
        });
    }

    removePhotoInUserGallery(userGalleryId, photoId, order) { 

        let data = this.photo(userGalleryId, photoId, order);

        return new Promise(function (resolve, reject) {

            post("/user/gallery-photos/remove", data).then(function (response) {
                return resolve(response);
            }).catch((error) => {
                return reject(error);
            });
        });
    }

    saveUserGalleryName(id, name) {
         
        let data = this.userGallery(id, name);
        let url = "/user/gallery/" + (id == 0 ? "add" : "update");
         
        return new Promise(function (resolve, reject) {

            post(url, data).then(function (response) {
                return resolve(response);
            }).catch((error) => {
                return reject(error);
            });
        });
    }

    saveNewUserGallery(name) {
               
        let data = this.userGallery(0, name);

        return new Promise(function (resolve, reject) {

            post("/user/gallery/add", data).then(function (response) {
                return resolve(response);
            }).catch((error) => {
                return reject(error);
            });
        });
    }

    deleteUserGallery(id) {
         
        return new Promise(function (resolve, reject) {

            deletion("/user/gallery/delete/" + id).then(function (response) {
                return resolve(response);
            }).catch((error) => {
                return reject(error);
            });
        });
    }

    photo(userGalleryId, photoId, order) {
        return {
            userGalleryId: userGalleryId,
            photoId: photoId,
            order: order,
        }; 
    }

    userGallery(id, name) {
        return {
            id: id,
            name: name
        }; 
    }

    filter(title, countryId, categoryId, paletteId, pageIndex, pageSize) {
        return {
            title: title,
            countryId: parseInt(countryId),
            categoryId: parseInt(categoryId),
            paletteId: parseInt(paletteId),
            pageIndex: parseInt(pageIndex),
            pageSize: parseInt(pageSize)
        }; 
    }
}

export { UserGallery }