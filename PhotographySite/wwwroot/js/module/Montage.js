class Montage {
        
    saveTemplatImageAdd(column, order, orientation) {

        const image = {
            column: parseInt(column),
            order: order,
            orientation: parseInt(orientation)
        }; 

        return new Promise(function (resolve, reject) {

            post("/admin/montage/add-image", image).then(function (response) {
                return resolve(response);
            }).catch((error) => {
                return reject(error);
            });
        });         
    }

    saveTempateImageMove(id, column, order) {

        const image = {
            id: id,
            column: column,
            order: order
        };

        return new Promise(function (resolve, reject) {

            post("/admin/montage/move-image", image).then(function (response) {
                return resolve(response);
            }).catch((error) => {
                return reject(error);
            });
        });
    }

    deleteTemplateImage(id) {
                
        return new Promise(function (resolve, reject) {

            deletion("/admin/montage/delete-image/" + id).then(function (response) {
                return resolve(response);
            }).catch((error) => {
                return reject(error);
            });
        });
    }
}