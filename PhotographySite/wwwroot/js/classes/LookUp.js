import * as ajax from '../ajax.js'

class LookUp {

    getPhotoCatalogueLookups() {

        return new Promise(function (resolve, reject) {

            ajax.ajaxCall("GET", "/admin/lookup/photo-catalogue", "").then(function (jqXHR) {
                return resolve(jqXHR);             
            }).catch((jqXHR) => {
                return reject(jqXHR); 
            });           
        });
    }
}

export { LookUp };