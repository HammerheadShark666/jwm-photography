class Country {

    getCountries() {
        return new Promise(function (resolve, reject) {
            return new Promise(function (resolve, reject) {
                get("/admin/country/countries").then(function (response) {
                    return resolve(response);
                }).catch((error) => {
                    return reject(error);
                });
            });    
        });
    }
}