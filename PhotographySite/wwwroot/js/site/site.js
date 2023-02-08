document.addEventListener("DOMContentLoaded", function () { 
	document.querySelectorAll('.dropdown-menu').forEach(function (element) {
		element.addEventListener('click', function (e) {
			e.stopPropagation();
		});
	})
}); 