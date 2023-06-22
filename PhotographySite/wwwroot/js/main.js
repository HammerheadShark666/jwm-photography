function showAlert(response, alertName) {

    if (response.message !== undefined) {
        $(alertName).html(response.message);
    } else {

        let status = response.status;

        if (status === 404) {
            $(alertName).html("Unable to find url - " + response.url);
        } else {

            let data = JSON.parse(response.responseText)
            if (response.status === 500)
                $(alertName).html(data.Title);
            else if (response.status === 400)
                $(alertName).html(data.errors[0].errorMessage);
        }        
    };

    $(alertName).show();
}

function hideAlert(alertName) {
    $(alertName).hide();
}

export { showAlert, hideAlert };