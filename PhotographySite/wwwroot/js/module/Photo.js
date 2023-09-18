class Photo {

    getPhotos(filter) {

        let photoFilter = {
            pageSize: filter.pageSize,
            pageIndex: filter.pageIndex,
            sortField: filter.sortField === undefined ? "" : filter.sortField,
            sortOrder: filter.sortOrder === undefined ? "" : filter.sortOrder,
            title: filter.title,
            countryId: filter.country.id,
            categoryId: filter.category.id,
            paletteId: filter.palette.id,
            aperturValue: filter.aperturValue,
            camera: filter.camera,
            dateTaken: filter.dateTaken,
            exposureTime: filter.exposureTime,
            fileName: filter.fileName,
            focalLength: filter.focalLength,
            id: filter.id === undefined ? 0 : filter.id,
            iso: filter.iso === undefined ? 0 : filter.iso,
            lens: filter.lens 
        };
          
        return new Promise(function (resolve, reject) {
            post("/admin/photo/catalog", photoFilter).then(function (response) {
                return resolve(response);
            }).catch((error) => {
                return reject(error);
            });
        });         
    };

    savePhotoDetails(id, title, countryId, categoryId, paletteId) {

        const photo = {
            id: id,
            title: title,
            countryId: countryId,
            categoryId: categoryId,
            paletteId: paletteId
        };

        return new Promise(function (resolve, reject) {
            post("/admin/photo/catalog/update-photo", photo).then(function (response) {
                return resolve(response);
            }).catch((error) => {
                return reject(error);
            });
        });   
    };

    addPhotoToGallerys(photoId) {
        return new Promise(function (resolve, reject) {
            post("/favourites/add-photo/" + photoId).then(function (response) {
                return resolve(response);
            }).catch((error) => {
                return reject(error);
            });
        });
    };

    deletePhotoFromGallerys(photoId) {
        return new Promise(function (resolve, reject) {
            deletion("/favourites/delete-photo/" + photoId).then(function (response) {
                return resolve(response);
            }).catch((error) => {
                return reject(error);
            });
        });
    };

    importPhotos(formData) {
        return new Promise(function (resolve, reject) {
            post("/admin/photo/import", formData).then(function (response) {
                return resolve(response);
            }).catch((error) => {
                return reject(error);
            }); 
        });
    }; 

    updateCataloguePhoto(photo) { 
        return new Promise(function (resolve, reject) {
            post("/admin/photo/catalog/update-photo", photo).then(function (response) {
                return resolve(response);
            }).catch((error) => {
                return reject(error);
            }); 
        });
    };
}