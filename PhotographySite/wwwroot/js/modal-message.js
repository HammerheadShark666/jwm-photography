let modalMessage;

document.addEventListener("DOMContentLoaded", function () {
	  
	modalMessage = new bootstrap.Modal(document.getElementById('modal-message-popup'), {
		keyboard: false
	});
}); 

function showModalMessage(title, message) {

	$("#model-message-title").text(title);
	$("#model-message").text(message);
	modalMessage.show();
}