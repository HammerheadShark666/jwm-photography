const photo = new Photo();
const menuItemIndex = 2;
const subMenuItemIndex = 2.1;

let importAlerts = $("#import-alerts");
 
$(function () {  
    importAlerts.empty();
    intialiseImportedPhotosGridContainer('');  
    setAdminMenuItem(menuItemIndex, subMenuItemIndex); 
});

$(document).on('click', '#photos-import-upload', function () { 
    intialiseImportedPhotosGridContainer(''); 
});

$(document).on("submit", "form#import-photos", function (e) {

    e.preventDefault();          
    importAlerts.empty(); 

    photo.importPhotos(new FormData(this)).then(function (response) { 
        intialiseImportedPhotosGridContainer(response.data);
        intialiseImportedPhotosGrid();
        intialiseFailedImportPhotosGrid();
        resetForm();
    }).catch((error) => {
        error.response.data.messages.forEach(function (i) { addAlert(i, importAlerts); }); 
    });
}); 

function resetForm() { 
    $("#import-photos").trigger("reset");
}

function intialiseImportedPhotosGridContainer(response) {
    $("#photos-grid-container").empty();
    $("#photos-grid-container").append(response);
    importAlerts.empty();
} 
 
function intialiseImportedPhotosGrid() {

    $("#import-photos-grid").jsGrid({
        width: "100%",
        autoload: true, 
        editing: true,
        sorting: true,
        paging: true,
        data: savedPhotos,
        controller: {             
            updateItem: function (item) {  
                return updateImportedPhotosDetails(item); 
            } 
        },
        noDataContent: "No Photos Imported",
        loadIndication: true,
        loadIndicationDelay: 500,
        loadMessage: "Please, wait...",
        loadShading: true,
        fields: importGridPhotosFields()
    });    
}

function updateImportedPhotosDetails(item) {
    photo.updateCataloguePhoto(getPhotoToUpdate(item)).then(function (response) {
        return;
    }).catch((error) => {
        error.response.data.messages.forEach(function (i) { addAlert(i, importAlerts); });
    });

    return;
} 

function importGridPhotosFields() {
    return [
        {
            name: "FileName",
            title: "",
            width: 90,
            itemTemplate: function (val, item) {
                return $("<img>").attr({ src: azureStoragePhotosContainerUrl + item.fileName }).css({ 'width': '100%' }).on("click", function () {   //css({ width: 150 }).
                    setModal(item.fileName, azureStoragePhotosContainerUrl + item.fileName);
                });
            }
        },
        { name: "id", title: "ID", type: "text", width: 50, editing: false },
        { name: "fileName", title: "Filename", type: "text", width: 140, editing: false },
        { name: "title", title: "Title", type: "text", width: 200 },
        { name: "country.id", title: "Country", type: "select", width: 100, items: countries, valueField: "id", textField: "name", align: "left" },
        { name: "category.id", title: "Category", type: "select", width: 100, items: categories, valueField: "id", textField: "name", align: "left" },
        { name: "palette.id", title: "Palette", type: "select", width: 100, items: palettes, valueField: "id", textField: "name", align: "left" },
        { type: "control", deleteButton: false, editButton: false },
        { name: "camera", title: "Camera", type: "text", width: 100, editing: false },
        { name: "lens", title: "Lens", type: "text", width: 90, editing: false },
        { name: "focalLength", title: "Focal Length", type: "text", width: 70, editing: false },
        { name: "exposureTime", title: "Exposure", type: "text", width: 70, editing: false },
        { name: "aperturValue", title: "Aperture", type: "text", width: 65, editing: false },
        { name: "iso", title: "ISO", type: "text", width: 50, editing: false },
        {
            name: "dateTaken", title: "Date Taken", type: "text", width: 80, editing: false,
            itemTemplate: function (val, item) {
                return new Date(val).toLocaleDateString();
            }
        },
    ]
}

function getPhotoToUpdate(item) {

    return {
        id: item.id,
        title: item.title,
        countryId: item.country.id,
        categoryId: item.category.id,
        paletteId: item.palette.id
    };
}


function intialiseFailedImportPhotosGrid() {

    $("#failed-to-import-photos-grid").jsGrid({
        width: "100%",
        autoload: true,
        sorting: true,
        data: existingPhotos,
        noDataContent: "No Photos Failed To Import",
        loadIndication: true,
        loadIndicationDelay: 500,
        loadMessage: "Please, wait...",
        loadShading: true,
        fields: failedToImportGridFields()
    });
}

function failedToImportGridFields() {
    return [
        {
            name: "FileName",
            title: "",
            width: 30,
            itemTemplate: function (val, item) {
                return $("<img>").attr({ src: azureStoragePhotosContainerUrl + item.fileName }).css({ 'width': '100%' }).on("click", function () {
                    setModal(item.fileName, azureStoragePhotosContainerUrl + item.fileName);
                });
            }
        },
        { name: "fileName", title: "Filename", type: "text", width: 140, editing: false },
        { name: "title", title: "Title", type: "text", width: 200 }
    ]
}