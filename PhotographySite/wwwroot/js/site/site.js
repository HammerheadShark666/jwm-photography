import { UserGallery } from "../classes/UserGallery.js";
import * as main from '../main.js'

const userGallery = new UserGallery();

let modalMessage;
document.addEventListener("DOMContentLoaded", function () { 
	document.querySelectorAll('.site-galleries').forEach(function (element) {
		element.addEventListener('click', function (e) {
			e.stopPropagation();
		});
	}); 

	document.querySelectorAll('.user-galleries').forEach(function (element) {
		element.addEventListener('click', function (e) {
			e.stopPropagation();
		});
	}); 

	document.querySelectorAll('.new-gallery-item').forEach(function (element) {
		element.addEventListener('click', function (e) {
			e.stopPropagation();
		});
	}); 

	modalMessage = new bootstrap.Modal(document.getElementById('modal-message-popup'), {
		keyboard: false
	});
}); 

function showModalMessage(title, message) {

	$("#model-message-title").text(title);
	$("#model-message").text(message);
	modalMessage.show();
} 

$('#menu-bar li a').click(function () {
	var id = $(this).attr('id');
	$('nav ul li ul.item-show-' + id).toggleClass("show");
	$('nav ul li #' + id + ' span').toggleClass("rotate");

}); 

$('nav ul li').click(function () {
	$("nav ul li").removeClass("active");
	$(this).addClass("active");
}); 

$(document).on('click', '#new-user-gallery', function () {
	$("#newUserGalleryModal").modal('show');
	$("#new-user-gallery-name").val("");
	$("#new-user-gallery-alert").hide();
});

$(document).on('click', '#save-user-new-gallery', function () {

	let galleryName = $("#new-user-gallery-name").val();

	if (galleryName != "") {
		userGallery.saveNewUserGallery(galleryName).then(function (gallery) {
			$("#newUserGalleryModal").modal('hide'); 
			$("#user-galleries").append("<li><a href='/user/gallery/" + gallery.id + "'>" + gallery.name + "</a></li>");
			$(".site-galleries").append("<li class='user-gallery-menu-item' data-user-gallary-menu-id='" + gallery.id + "'><a class='dropdown-item' href='/gallery/user/" + gallery.id + "'>" + gallery.name + "</a></li>");
			sortUserGalleryMenuItems();
			sortGalleryUserMenuItems()
		}).catch((response) => {
			main.showAlert(response, "#new-user-gallery-alert");
		});
	}
});

function sortUserGalleryMenuItems() {

	var menuGalleryList = $('#user-galleries')
	var menuGalleryListItems = $('li', menuGalleryList).get();
	var newGalleryItem = menuGalleryListItems[0];

	menuGalleryListItems.shift();

	var sortedMenuGalleryListItems = sortList(menuGalleryListItems);

	sortedMenuGalleryListItems.splice(0, 0, newGalleryItem);

	menuGalleryList = appendItemsToList(menuGalleryList, sortedMenuGalleryListItems);
} 

function sortGalleryUserMenuItems() {

	var menuGalleryList = $('.site-galleries');	 
	var siteGalleryMenuItems = $('li.site-gallery-menu-item', menuGalleryList);
	var userGalleryMenuItems = $('li.user-gallery-menu-item', menuGalleryList);
	var seperatorMenuItem = $('li.seperator', menuGalleryList);
	var sortedUserGalleryMenuItems = sortList(userGalleryMenuItems);
	 
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