$(document).ready(function () {
     
    $("#photos-import-upload").on("click", function (event) {
        $("#photosGridContainer").html('');
        $("#import-alert").hide();
    });

    $("#import-alert").hide();

    setPhotoMenuItem();
        
    $("form#import-photos").submit(function (e) {

        e.preventDefault();
        var formData = new FormData(this);         

        $.ajax({                    
            url: '/admin/photo/import',
            method: "POST",
            dataType: 'text',
            data: formData,
            processData: false,
            contentType: false,
            async: true,
            success: function (response, status) {
                $("#photosGridContainer").html('');              
                $("#photosGridContainer").append(response);
                intialiseImportedPhotosGrid();
                intialiseFailedImportPhotosGrid();
                resetForm();        
            },
            error: function (jqXHR, textStatus, errorThrown) {
                if (jqXHR.status === 400) {
                    $("#import-alert").html(jqXHR.responseText);
                    $("#import-alert").show();
                }
            },
            complete: function (data) {
                $("#import-alert").html();
            }
        });
    });    
});

function setPhotoMenuItem() {
    $("nav ul li").removeClass("active");

    var selectedPhotoId = $("#selected-photo-id").val();
    var menuItem = $('[data-photo-menu-id="' + selectedPhotoId + '"]');

    $(menuItem).addClass("active");
    $('nav ul li ul.item-show-1').toggleClass("show");
    $('nav ul li #1 span').toggleClass("rotate");
}

function resetForm() {
    var $el = $('#photos-import-upload');
    $el.wrap('<form>').closest('form').get(0).reset();
    $el.unwrap();
}
 
function intialiseImportedPhotosGrid() {

    $("#importPhotosGrid").jsGrid({
        width: "100%",
        autoload: true, 
        editing: true,
        sorting: true,
        paging: true,
        data: savedPhotos,
        controller: {             
            updateItem: function (item) {

                const photo = {
                    id: item.id,
                    title: item.title,
                    countryId: item.country.id,
                    categoryId: item.category.id,
                    paletteId: item.palette.id
                };

                return $.ajax({
                    url: "/admin/photo/catalog/update-photo",
                    type: "POST",
                    datatype: "json",
                    data: JSON.stringify(photo),
                    contentType: "application/json; charset=utf-8",
                    async: true,
                })
            } 
        },
        noDataContent: "No Photos Imported",
        loadIndication: true,
        loadIndicationDelay: 500,
        loadMessage: "Please, wait...",
        loadShading: true,
        fields: [
            {
                name: "FileName",
                title: "",
                width: 90,
                itemTemplate: function (val, item) {
                    return $("<img>").attr("src", "/photos/" + item.fileName).css({ 'width': '100%' }).on("click", function () {   //css({ width: 150 }).
                        $("#photoModalLabel").text(item.fileName);
                        $("#imagePreview").attr("src", "/photos/" + item.fileName);
                        $('#photoModal').modal('show');
                    });
                }
            },
            { name: "id", title: "ID", type: "text", width: 50, editing: false },
            { name: "fileName", title: "Filename", type: "text", width: 140, editing: false },
            { name: "title", title: "Title", type: "text", width: 200 },
            { name: "country.Id", title: "Country", type: "select", width: 100, items: countries, valueField: "id", textField: "name", align: "left" },
            { name: "category.Id", title: "Category", type: "select", width: 100, items: categories, valueField: "id", textField: "name", align: "left" },            
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
    });    
}

function intialiseFailedImportPhotosGrid() {

    $("#failedToImportPhotosGrid").jsGrid({
        width: "100%",
        autoload: true,
        sorting: true,
        data: existingPhotos,
        noDataContent: "No Photos Failed To Import",
        loadIndication: true,
        loadIndicationDelay: 500,
        loadMessage: "Please, wait...",
        loadShading: true,
        fields: [
            {
                name: "FileName",
                title: "",
                width: 30,
                itemTemplate: function (val, item) {
                    return $("<img>").attr("src", "/photos/" + item.fileName).css({ 'width': '100%' }).on("click", function () {   //css({ width: 150 }).
                        $("#photoModalLabel").text(item.fileName);
                        $("#imagePreview").attr("src", "/photos/" + item.fileName);
                        $('#photoModal').modal('show');

                    });
                }
            },             
            { name: "fileName", title: "Filename", type: "text", width: 140, editing: false },
            { name: "title", title: "Title", type: "text", width: 200 },]
    });
}