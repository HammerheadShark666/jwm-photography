class Gallery {

    searchPhotos(title, countryId, categoryId, paletteId, pageIndex, pageSize) {
                
        let data = this.filter(title, countryId, categoryId, paletteId, pageIndex, pageSize);

        return new Promise(function (resolve, reject) {

            post("/admin/gallery/search", data).then(function (response) {
                return resolve(response);
            }).catch((error) => {
                return reject(error);
            });
        }); 
    }      

    savePhotoToGallery(galleryId, photoId, order) {
         
        let data = this.photo(galleryId, photoId, order);

        return new Promise(function (resolve, reject) {

            post("/admin/gallery-photos/add", data).then(function (response) {
                return resolve(response);
            }).catch((error) => {
                return reject(error);
            });
        });
    }

    movePhotoInGallery(galleryId, photoId, order) {
                
        let data = this.photo(galleryId, photoId, order);

        return new Promise(function (resolve, reject) {

            post("/admin/gallery-photos/move", data).then(function (response) {
                return resolve(response);
            }).catch((error) => {
                return reject(error);
            });
        });
    }

    removePhotoInGallery(galleryId, photoId, order) {
                
        let data = this.photo(galleryId, photoId, order);

        return new Promise(function (resolve, reject) {

            post("/admin/gallery-photos/remove", data).then(function (response) {
                return resolve(response);
            }).catch((error) => {
                return reject(error);
            });
        });
    }

    saveGalleryName(id, name) {
         
        let data = this.gallery(id, name);

        return new Promise(function (resolve, reject) {

            post("/admin/gallery/save/name", data).then(function (response) {
                return resolve(response);
            }).catch((error) => {
                return reject(error);
            });
        });
    }

    saveNewGallery(name) { 

        let data = this.gallery(0, name);

        return new Promise(function (resolve, reject) {

            post("/admin/gallery/new/save", data).then(function (response) {
                return resolve(response);
            }).catch((error) => {
                return reject(error);
            });
        });
    }

    deleteGallery(id) {
         
        return new Promise(function (resolve, reject) {

            deletion("/admin/gallery/delete/" + id).then(function (response) {
                return resolve(response);
            }).catch((error) => {
                return reject(error);
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