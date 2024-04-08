
const userGalleryService = new UserGallery();
 
let addUserGalleryNameAlerts = $("#add-user-gallery-name-alerts"),
    newUserGalleryName = $("#new-user-gallery-name"),
	newUserGalleryModal = $("#new-user-gallery-modal"),
    navLi = $('nav ul li');

$(function () {
	 
	addUserGalleryNameAlerts.empty();
});

document.addEventListener("DOMContentLoaded", function () {  
	stopClickEventPropagation('.site-galleries'); 
	stopClickEventPropagation('.user-galleries'); 
	stopClickEventPropagation('.new-gallery-item');	
}); 
 
$('#menu-bar li a').click(function () {
	var id = $(this).attr('id'); 
	navLi.find('ul.item-show-' + id).toggleClass("show");
	navLi.find('#' + id + ' span').toggleClass("rotate");  
}); 

navLi.click(function () {
	navLi.removeClass("active");
	$(this).addClass("active");
}); 

$(document).on('click', '#new-user-gallery', function () {
	newUserGalleryModal.modal('show');
	newUserGalleryName.val(""); 
	addUserGalleryNameAlerts.empty();
});

$(document).on('click', '#save-user-new-gallery', function () {

	let galleryName = newUserGalleryName.val();

	addUserGalleryNameAlerts.empty();

	if (galleryName != "") {
		userGalleryService.saveNewUserGallery(galleryName).then(function (response) {
			newUserGalleryModal.modal('hide'); 
			$("#user-galleries").append("<li><a href='/user/gallery/" + response.data.id + "'>" + response.data.name + "</a></li>");
			$(".site-galleries").append("<li class='user-gallery-menu-item' data-user-gallary-menu-id='" + response.data.id + "'><a class='dropdown-item' href='/gallery/user/" + response.data.id + "'>" + response.data.name + "</a></li>");
			sortUserGalleryMenuItems();
			sortGalleryUserMenuItems()
			window.location = baseUrl + "/user/gallery/" + response.data.id;
		}).catch((error) => {
			error.response.data.messages.forEach(function (i) { addAlert(i, addUserGalleryNameAlerts); });
		});
	}
});

function sortUserGalleryMenuItems() {

	var menuGalleryList = $('#user-galleries'),
	    menuGalleryListItems = $('li', menuGalleryList).get(),
	    newGalleryItem = menuGalleryListItems[0];

	menuGalleryListItems.shift();

	var sortedMenuGalleryListItems = sortList(menuGalleryListItems);

	sortedMenuGalleryListItems.splice(0, 0, newGalleryItem);

	menuGalleryList = appendItemsToList(menuGalleryList, sortedMenuGalleryListItems);
} 

function sortGalleryUserMenuItems() {

	var menuGalleryList = $('.site-galleries'),	 
	    siteGalleryMenuItems = $('li.site-gallery-menu-item', menuGalleryList),
	    userGalleryMenuItems = $('li.user-gallery-menu-item', menuGalleryList),
	    seperatorMenuItem = $('li.seperator', menuGalleryList),
	    sortedUserGalleryMenuItems = sortList(userGalleryMenuItems),
	 
	menuGalleryList = appendItemsToList(menuGalleryList, siteGalleryMenuItems);
	menuGalleryList.append(seperatorMenuItem);  
	menuGalleryList = appendItemsToList(menuGalleryList, sortedUserGalleryMenuItems);
} 
 
function sortList(list) {
	list.sort(function (a, b) {
		var compA = $(a).text().trim().toUpperCase();
		var compB = $(b).text().trim().toUpperCase();
		return (compA < compB) ? -1 : 1;
	});

	return list;
}

function appendItemsToList(list, items) {
	$.each(items, function (i, item) {
		list.append(item);
	});

	return list;
}
