function fadeInImages(element) {
    $(element).each(function () {
        $(this).delay(Math.random() * 500 + 100).fadeTo('slow', 1);
    });
}

function getAlertClass(severity) { 

    switch (severity) {

        case 0: {
            return 'alert-danger'; 
        }
        case 1: {
            return 'alert-warning'; 
        }
        case 2: {
            return 'alert-info'; 
        }
    }
}

function addAlert(message, element) {
    $(element).append('<div class="alert ' + getAlertClass(message.severity) + '" role="alert">' + message.text + '</div >');
}

function stopClickEventPropagation(selector) {

    document.querySelectorAll(selector).forEach(function (element) {
        element.addEventListener('click', function (e) {
            e.stopPropagation();
        });
    });
} 

function setAdminMenuItem(mainIndex, subIndex) {

    const selectedGalleryId = $("#selected-gallery-id").val();
    const navListItem = $("nav ul li");

    navListItem.find('a').removeClass("active-menu-item");

    if (subIndex === 0)
        subIndex = mainIndex;

    if (mainIndex === 3)
        subIndex = "3." + selectedGalleryId;

    $('[data-menu-level-index="' + subIndex + '"] a').addClass("active-menu-item");
    navListItem.find('ul.item-show-' + mainIndex).toggleClass("show");
    navListItem.find('#' + mainIndex + ' span').toggleClass("rotate");
}