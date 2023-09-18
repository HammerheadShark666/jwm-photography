let callingElement;
let origin = null;

const montage = new Montage();
const menuItemIndex = 4;
const subMenuItemIndex = 0;

$(function () {

    document.onclick = hideMenu;   
    setAdminMenuItem(menuItemIndex, subMenuItemIndex); 
 
    $(".draggable-left").sortable({

        connectWith: ".connected-sortable",
        stack: ".connected-sortable ul",
        start: function (e, ui) {
            origin = $(this);
        },
        stop: function (event, ui) {  

            let order = ui.item.index() + 1,
                li = $(ui.item[0]),
                column = $(ui.item).closest('.column').attr("data-column"), 
                id = $(li).attr("data-id");

            montage.saveTempateImageMove(id, column, order).then(function () {
                updateListItemOrderAttribute(origin);
            });
        },
        receive: receiveDroppedTemplate,

    }).disableSelection();    
});

$(document).on('contextmenu', '.image-container img', function (e) {
    rightClick(e);
});

$("#context-menu ul li").on("click", function (e) {

    e.preventDefault();

    let column = $(callingElement).closest('.column').attr("data-column"),
        order = $(callingElement).closest('li').attr("data-order"),
        photoOrientation = parseInt($(e.currentTarget).attr("data-id"));

    order = parseInt(order) + 1;

    montage.saveTemplatImageAdd(column, order, photoOrientation).then(function (response) {

        let html = '<li data-column="' + column
            + '" data-order="' + order
            + '" data-id="' + response.data.id
            + '"><div class="image-container"><img class="child'
            + '" src="/images/'
            + getMontageTemplateImage(photoOrientation) + '">'
            + '<div class="delete-template"> <i class="bi bi-trash3"></i></div></div></li >';

        $(html).insertAfter('#monatageColumn' + column + 'List li:eq(' + (order - 2) + ')');

        updateListItemOrderAttribute($(callingElement).closest('ul'));
    });
});

$("#add-montage-template-image").on("click", function (event) {

    let column = $("#montage-column").val(),
        photoOrientation = parseInt($("#montage-photo-orientation").val()),
        numberOfPhotosInColumn = $("#monatageColumn" + column + " > ul").children('li').length;

    montage.saveTemplatImageAdd(column, (numberOfPhotosInColumn + 1), photoOrientation).then(function (id) {

        let html = '<li data-column="' + column
            + '" data-order="' + (numberOfPhotosInColumn + 1)
            + '" data-id="' + id
            + '"><div class="image-container"><img class="child'
            + '" src="/images/'
            + getMontageTemplateImage(photoOrientation) + '">'
            + '<div class="delete-template"> <i class="bi bi-trash3"></i></div></div></li >';

        $("#monatageColumn" + column + " ul").append(html);
    });
});

$(".connected-sortable").on("click", ".delete-template", function (event) {

    event.preventDefault();

    let list = $(this).closest('ul'),
        li = $(this).closest('li'),
        dataId = $(li).attr("data-id");

    li.fadeOut('slow', function () {
        montage.deleteTemplateImage(dataId).then(function () {
            li.remove();
            updateListItemOrderAttribute($(list));
        });
    });
});

function hideMenu() {
    document.getElementById("context-menu")
        .style.display = "none"
}

function rightClick(e) {
    e.preventDefault();

    callingElement = e.target;

    if (document.getElementById("context-menu").style.display == "block") {
        hideMenu();
    } else {
        var menu = document.getElementById("context-menu")
        menu.style.display = 'block';
        menu.style.left = e.pageX + "px";
        menu.style.top = e.pageY + "px";
    }
}
 
function receiveDroppedTemplate(event, ui) {
    updateListItemColumnAttribute($(event.target), ui);
    updateListItemOrderAttribute($(event.target));
}

function updateListItemOrderAttribute(list) {
    let order = 1;
    list.find("li").each(function () {
        $(this).attr("data-order", order);
        order++;
    });
}

function updateListItemColumnAttribute(list, ui) {
    let newColumn = list.parent().attr("data-column");
    $(ui.item[0]).attr("data-column", newColumn);
}

function getMontageTemplateImage(photoOrientation) {
    switch (photoOrientation) {
        case 0:
            return "LandscapeTemplate.jpg";
        case 1:
            return "PortraitTemplate.jpg";
        default:
            return "SquareTemplate.jpg";
    }
}