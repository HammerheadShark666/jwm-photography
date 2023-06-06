import * as ajax from '../ajax.js'

class Photo {

    getPhotos(filter) {

        const photoFilter = {
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

        let data = JSON.stringify(photoFilter);

        return new Promise(function (resolve, reject) {

            ajax.ajaxCall("POST", "/admin/photo/catalog", data).then(function (jqXHR) {
                return resolve(jqXHR);
            }).catch((jqXHR) => {
                return reject(jqXHR);
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

        let data = JSON.stringify(photo);

        return new Promise(function (resolve, reject) {
            ajax.ajaxCall("POST", "/admin/photo/catalog/update-photo", data).then(function (jqXHR) {
                return resolve(jqXHR);
            }).catch((jqXHR) => {
                return reject(jqXHR);
            });
        });
    };

    addPhotoToFavourites(photoId) {
        return new Promise(function (resolve, reject) {

            ajax.ajaxCall("POST", "/favourites/add-photo/" + photoId).then(function (jqXHR) {
                return resolve(jqXHR);
            }).catch((jqXHR) => {
                return reject(jqXHR);
            });
        });
    };

    deletePhotoFromFavourites(photoId) {
        return new Promise(function (resolve, reject) {

            ajax.ajaxCall("POST", "/favourites/delete-photo/" + photoId).then(function (jqXHR) {
                return resolve(jqXHR);
            }).catch((jqXHR) => {
                return reject(jqXHR);
            });
        });
    };
}

export { Photo };