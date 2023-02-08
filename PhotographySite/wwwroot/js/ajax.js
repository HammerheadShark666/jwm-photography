function ajaxCall(type, url, data) {

    return new Promise(function (resolve, reject) {

        $.ajax({
            type: type,
            url: url,
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            data: data,
            async: false,
            beforeSend: function (jqXHR, settings) {
                jqXHR.url = settings.url;
            },
            success: function (jqXHR) {
                resolve(jqXHR);
            },
            error: function (jqXHR) {
                reject(jqXHR);
            }
        });
    });
}

export { ajaxCall };