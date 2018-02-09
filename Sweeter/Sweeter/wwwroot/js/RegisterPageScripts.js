function upload() {
	var preview = document.querySelector('img');
	var file = document.querySelector('input[type=file]').files[0];
	var reader = new FileReader();

	reader.onloadend = function () { preview.src = reader.result; }

	if (file) {
		reader.readAsDataURL(file);
	} else { preview.src = "~/lib/img/avatar.jpeg" }
}

function check() {
	var pas1 = document.getElementById('password');
	var pas2 = document.getElementById('password2');
	var ermes = document.getElementById('error');
	var button = document.getElementById('register');
	if (pas1.value != pas2.value) {
		pas1.classList.add("notright");
		pas2.classList.add("notright");
		ermes.innerHTML = "Passwords are not simmilar!";
		button.disabled = true;
	}
	else {
		pas1.classList.remove("notright");
		pas2.classList.remove("notright");
		ermes.innerHTML = "";
		button.disabled = false;
	}
}

function clearerror() {
	var ermes = document.getElementById('error');
	ermes.innerHTML = "";
}

function checkemail() {
	var email = document.getElementById('email').value;
	var ermes = document.getElementById('error');
	if (email.indexOf(".", email.indexOf("@"))==-1) ermes.innerHTML = "Email is invalid!";
	else ermes.innerHTML = "";
}