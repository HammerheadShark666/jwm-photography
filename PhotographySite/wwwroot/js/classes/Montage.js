import * as ajax from '../ajax.js'

class Montage {
        
    saveTemplatImageAdd(column, order, orientation) {

        const image = {
            column: parseInt(column),
            order: order,
            orientation: parseInt(orientation)
        };

        let data = JSON.stringify(image);

        return new Promise(function (resolve, reject) {

            ajax.ajaxCall("POST", "/admin/montage/add-image", data).then(function (jqXHR) {
                return resolve(jqXHR);
            }).catch((jqXHR) => {
                return reject(jqXHR);
            });
        });         
    }

    saveTempateImageMove(id, column, order) {

        const image = {
            id: id,
            column: column,
            order: order
        };

        let data = JSON.stringify(image);

        return new Promise(function (resolve, reject) {

            ajax.ajaxCall("POST", "/admin/montage/move-image", data).then(function (jqXHR) {
                return resolve(jqXHR);
            }).catch((jqXHR) => {
                return reject(jqXHR);
            });
        });
    }

    deleteTemplateImage(id) {
                
        return new Promise(function (resolve, reject) {

            ajax.ajaxCall("DELETE", "/admin/montage/delete-image/" + id, "").then(function (jqXHR) {
                return resolve(jqXHR);
            }).catch((jqXHR) => {
                return reject(jqXHR);
            });
        });
    }
}

export { Montage };