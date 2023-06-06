let modalMessage;
document.addEventListener("DOMContentLoaded", function () { 
	document.querySelectorAll('.dropdown-menu').forEach(function (element) {
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