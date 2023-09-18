class LookUp {

    getPhotoCatalogueLookups() {

        return new Promise(function (resolve, reject) {             
            get("/admin/lookup/photo-catalogue").then(function (response) {
                    return resolve(response);
                }).catch((error) => {
                    return reject(error);
                });
        });
    }
}