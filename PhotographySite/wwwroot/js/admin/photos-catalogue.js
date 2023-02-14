import { Photo } from "../classes/Photo.js";
import { LookUp } from "../classes/LookUp.js";
import * as main from '../main.js'

const photo = new Photo();
const lookUp = new LookUp();

let countries = [];
let categories = [];
let palettes = [];

let pageSize = 10;
let azureStoragePhotosContainerUrl = "";

$(document).ready(function () {
    setPhotoMenuItem();
    loadLookUps().then(function () {
        intialisePhotoCatalogGrid();
    });   
    initialisePhotosCatalogueResultsPagination(1, false);
    history.pushState("", document.title, window.location.pathname);
});

function initialisePhotosCatalogueResultsPagination(numberOfPhotos, showPaginator) {

    $('#photo-catalogue-results-pagination').pagination({
        items: numberOfPhotos,
        itemsOnPage: pageSize,
        cssStyle: 'dark-theme',
        onPageClick: function (pageNumber) { 
            $("#catalogPhotosGrid").jsGrid("openPage", pageNumber);           
        }
    });

    showPaginationController(showPaginator);
}

function showPaginationController(showPaginator) {
    if (showPaginator)
        $('#photo-catalogue-results-pagination').show();
    else
        $('#photo-catalogue-results-pagination').hide();
} 

function loadLookUps() {
     
    return new Promise(function (resolve, reject) {

        lookUp.getPhotoCatalogueLookups().then(function (response) {             
            initialisePhotoCatalogLookupArrays(response);
            resolve();
        }).catch((response) => {
            main.displayAlert(response, "#photo-catalogue-alert");
            reject()
        });
    });
}

function setPhotoMenuItem() {
    $("nav ul li").removeClass("active");

    var selectedPhotoId = $("#selected-photo-id").val();
    var menuItem = $('[data-photo-menu-id="' + selectedPhotoId + '"]');

    $(menuItem).addClass("active");
    $('nav ul li ul.item-show-1').toggleClass("show");
    $('nav ul li #1 span').toggleClass("rotate");
}

function initialisePhotoCatalogLookupArrays(response) {

    countries = response.countries;
    categories = response.categories;
    palettes = response.palettes; 

    countries.push({ id: 0, name: "" });
    categories.push({ id: 0, name: "" });
    palettes.push({ id: 0, name: "" });

    countries = sortLookupArray(countries);
    categories = sortLookupArray(categories);
    palettes = sortLookupArray(palettes);
}

function sortLookupArray(array) {

    array.sort((a, b) => {
        if (a.name < b.name) {
            return -1;
        }
        if (a.name > b.name) {
            return 1;
        }
        return 0;
    });

    return array;
}

function intialisePhotoCatalogGrid() {
     
    $("#catalogPhotosGrid").jsGrid({
        width: "100%", 
        autoload: true, 
        editing: true,
        sorting: true,
        filtering: true,
        pageLoading: true,
        pageSize: 10,
        pageIndex: 1,
        controller: {
            loadData: function (filter) {    

                let pageIndex = filter.pageIndex;
                $("#photos-catalogue-results-pagination").hide();

                return photo.getPhotos(filter).then(function (response) {

                    if (response.itemsCount > 0) {                     
                        initialisePhotosCatalogueResultsPagination(response.itemsCount, true);
                        $('#photo-catalogue-results-pagination').pagination('drawPage', pageIndex); 
                    }

                    azureStoragePhotosContainerUrl = response.azureStoragePhotosContainerUrl

                    return response;

                }).catch((response) => {
                    main.displayAlert(response, "#photo-catalogue-alert");   
                }); 
            },
            updateItem: function (item) {
                 
                photo.savePhotoDetails(item.id, item.title, item.country.id,
                                       item.category.id, item.palette.id).then(function (response) {
                    return response;
                }).catch((response) => {
                    main.displayAlert(response, "#photo-catalogue-alert");
                });
            }           
        },      
        noDataContent: "No Photos Found",
        loadIndication: true,
        loadIndicationDelay: 500,
        loadMessage: "Please, wait...",
        loadShading: true,
        fields: [
            {
                name: "fileName",
                width: 83,
                title: "",
                itemTemplate: function (val, item) {
                    return $("<img>").attr("src", azureStoragePhotosContainerUrl + item.fileName).css({ 'width': '100%' }).on("click", function () {   //css({ width: 150 }).
                        $("#photoModalLabel").text(item.fileName);
                        $("#imagePreview").attr("src", azureStoragePhotosContainerUrl + item.fileName);
                        $('#photoModal').modal('show');
                    });
                }
            },
            { name: "id", title: "ID", type: "number", visible: false, editing: false,  },
            { name: "fileName", title: "Filename", type: "text", width: 120, editing: false },
            { name: "title", title: "Title", type: "text", width: 180 },
            { name: "country.id", title: "Country", type: "select", width: 100, items: countries, valueField: "id", textField: "name", align: "left" },
            { name: "category.id", title: "Category", type: "select", width: 100, items: categories, valueField: "id", textField: "name", align: "left" },
            { name: "palette.id", title: "Palette", type: "select", width: 100, items: palettes, valueField: "id", textField: "name", align: "left" },
            { type: "control", deleteButton: false, editButton: false },
            { name: "camera", title: "Camera", type: "text", width: 100, editing: false },
            { name: "lens", title: "Lens", type: "text", width: 130, editing: false },
            { name: "focalLength", title: "Focal Length", type: "text", width: 60, editing: false },
            { name: "exposureTime", title: "Exposure", type: "text", width: 70, editing: false },
            { name: "aperturValue", title: "Aperture", type: "text", width: 60, editing: false },
            {
                name: "iso",
                title: "ISO",
                type: "number",
                width: 70,
                editing: false,
                itemTemplate: function (val, item) {
                    if (val === 0)
                        return "";
                    else
                        return val;
                }
            },
            { name: "dateTaken", title: "Date Taken", type: "text", width: 80, editing: false,
                itemTemplate: function (val, item) {
                    if(val != null)
                        return new Date(val).toLocaleDateString();
                }
            },            
        ]
    }); 
}