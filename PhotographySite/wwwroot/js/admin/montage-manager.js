import { Montage } from "../classes/Montage.js";

let callingElement;
let origin = null;
const montage = new Montage();

$(document).ready(function () {

    document.onclick = hideMenu; 
 
    setMontageMenuItem();     
 
    $(".draggable-left").sortable({

        connectWith: ".connected-sortable",
        stack: ".connected-sortable ul",
        start: function (e, ui) {
            origin = $(this);
        },
        stop: function (event, ui) {  

            let order = ui.item.index() + 1;
            let li = $(ui.item[0]);
            let column = $(ui.item).closest('.column').attr("data-column"); 
            let id = $(li).attr("data-id");

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

$("#contextMenu ul li").on("click", function (e) {

    e.preventDefault();

    let column = $(callingElement).closest('.column').attr("data-column");
    let order = $(callingElement).closest('li').attr("data-order");
    let photoOrientation = parseInt($(e.currentTarget).attr("data-id"));

    order = parseInt(order) + 1;

    montage.saveTemplatImageAdd(column, order, photoOrientation).then(function (response) {

        let html = '<li data-column="' + column
            + '" data-order="' + order
            + '" data-id="' + response.id
            + '"><div class="image-container"><img class="child'
            + '" src="/images/'
            + getMontageTemplateImage(photoOrientation) + '">'
            + '<div class="delete-template"> <i class="bi bi-trash3"></i></div></div></li >';

        $(html).insertAfter('#monatageColumn' + column + 'List li:eq(' + (order - 2) + ')');

        updateListItemOrderAttribute($(callingElement).closest('ul'));
    });
});

$("#add-montage-template-image").on("click", function (event) {

    let column = $("#montage-column").val();
    let photoOrientation = parseInt($("#montage-photo-orientation").val());
    let numberOfPhotosInColumn = $("#monatageColumn" + column + " > ul").children('li').length;

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

    let list = $(this).closest('ul');
    let li = $(this).closest('li');
    let dataId = $(li).attr("data-id");
    li.fadeOut('slow', function () {
        montage.deleteTemplateImage(dataId).then(function () {
            li.remove();
            updateListItemOrderAttribute($(list));
        });
    });
});

function hideMenu() {
    document.getElementById("contextMenu")
        .style.display = "none"
}

function rightClick(e) {
    e.preventDefault();

    callingElement = e.target;

    if (document.getElementById("contextMenu").style.display == "block") {
        hideMenu();
    } else {
        var menu = document.getElementById("contextMenu")
        menu.style.display = 'block';
        menu.style.left = e.pageX + "px";
        menu.style.top = e.pageY + "px";
    }
}

function setMontageMenuItem() {
    $("nav ul li").removeClass("active");    
    var menuItem = $('[data-montage-menu-id="1"]');
    $(menuItem).addClass("active");
     
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