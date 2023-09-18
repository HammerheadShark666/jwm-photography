
const photo = new Photo();
const lookUp = new LookUp();

const menuItemIndex = 2;
const subMenuItemIndex = 2.2;

let countries = [],
    categories = [],
    palettes = [],
    pageSize = 10,
    azureStoragePhotosContainerUrl = "",
    photoCatalogueAlerts = $("#photo-catalogue-alert"),
    cataloguePhotosGrid = $("#catalog-photos-grid"),
    photoCatalogueResultsPagination = $('#photo-catalogue-results-pagination');

$(function () {

    setAdminMenuItem(menuItemIndex, subMenuItemIndex);

    loadLookUps().then(function () {
        intialisePhotoCatalogGrid();
    });   

    initialisePhotosCatalogueResultsPagination(1, false);
    history.pushState("", document.title, window.location.pathname);
});

function initialisePhotosCatalogueResultsPagination(numberOfPhotos, showPaginator) {

    photoCatalogueResultsPagination.pagination({
        items: numberOfPhotos,
        itemsOnPage: pageSize,
        cssStyle: 'dark-theme',
        onPageClick: function (pageNumber) { 
            cataloguePhotosGrid.jsGrid("openPage", pageNumber);           
        }
    });

    showPaginationController(showPaginator);
}

function showPaginationController(showPaginator) {
    if (showPaginator)
        photoCatalogueResultsPagination.show();
    else
        photoCatalogueResultsPagination.hide();
} 

function loadLookUps() {
     
    return new Promise(function (resolve, reject) {

        lookUp.getPhotoCatalogueLookups().then(function (response) {             
            initialisePhotoCatalogLookupArrays(response.data);
            resolve();
        }).catch((error) => { 
            error.response.data.messages.forEach(function (i) { addAlert(i, photoCatalogueAlerts); });
            reject()
        });
    });
} 

function initialisePhotoCatalogLookupArrays(response) {
    countries = initialiseLookupsArray(response.countries);
    categories = initialiseLookupsArray(response.categories);
    palettes = initialiseLookupsArray(response.palettes); 
}

function initialiseLookupsArray(array) {
    array.push({ id: 0, name: "" });
    array = sortLookupArray(array);
    return array;
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
     
    cataloguePhotosGrid.jsGrid({
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
               return loadPhotos(filter);
            },
            updateItem: function (item) {
                return updatePhotoDetails(item);                
            }           
        },      
        noDataContent: "No Photos Found",
        loadIndication: true,
        loadIndicationDelay: 500,
        loadMessage: "Please, wait...",
        loadShading: true,
        fields: photosCatalogueFields()
    }); 
}

function loadPhotos(filter) {

    let pageIndex = filter.pageIndex;
    photoCatalogueAlerts.empty();
    $("#photos-catalogue-results-pagination").hide();

    return photo.getPhotos(filter).then(function (response) {

        if (response.data.itemsCount > 0) {
            initialisePhotosCatalogueResultsPagination(response.data.itemsCount, true);
            photoCatalogueResultsPagination.pagination('drawPage', pageIndex);
        }

        azureStoragePhotosContainerUrl = response.data.azureStoragePhotosContainerUrl

        return response.data;
    }).catch(function (error) {
        error.response.data.messages.forEach(function (i) { addAlert(i, photoCatalogueAlerts); });
    });
}

function updatePhotoDetails(item) {
    photoCatalogueAlerts.empty();

    photo.savePhotoDetails(item.id, item.title, item.country.id, item.category.id, item.palette.id).then(function (response) {
        return response;
    }).catch((error) => {
        error.response.data.messages.forEach(function (i) { addAlert(i, photoCatalogueAlerts); });
    });
}


function photosCatalogueFields() {
    return [
        {
            name: "fileName",
            width: 83,
            title: "",
            itemTemplate: function (val, item) {
                return $("<img>").attr({ src: azureStoragePhotosContainerUrl + item.fileName }).css({ 'width': '100%' }).on("click", function () {   //css({ width: 150 }).
                    setModal(item.fileName, azureStoragePhotosContainerUrl + item.fileName);
                });
            }
        },
        { name: "id", title: "ID", type: "number", visible: false, editing: false, },
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
        {
            name: "dateTaken", title: "Date Taken", type: "text", width: 80, editing: false,
            itemTemplate: function (val, item) {
                if (val != null)
                    return new Date(val).toLocaleDateString();
            }
        },
    ]
}