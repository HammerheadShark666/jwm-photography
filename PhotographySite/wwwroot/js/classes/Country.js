import * as ajax from '../ajax.js'

class Country {

    getCountries() {

        return new Promise(function (resolve, reject) {

            ajax.ajaxCall("GET", "/admin/country/countries", "").then(function (jqXHR) {
                return resolve(jqXHR);
            }).catch((jqXHR) => {
                return reject(jqXHR);
            });
        });
    }
}

export { Country };