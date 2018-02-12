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
	var email = document.getElementById('email');
	var ermes = document.getElementById('error');
	var emailval = document.getElementById('email').value;
	var len = emailval.length - 1;
	var ind = emailval.indexOf(".", emailval.indexOf("@"));
	var indat = emailval.indexOf("@");
	len = len - ind;
	if (ind == -1) { // *@*
		ermes.innerHTML = "Email is invalid!";
		email.classList.add("notright");
	}
	else if (ind - indat == 1) { //*@.*
		ermes.innerHTML = "Email is invalid!";
		email.classList.add("notright");
	}
	else if (len > 3) { //*@-.-
		ermes.innerHTML = "Email is invalid!";
		email.classList.add("notright");
	}
	else if (len < 1) { //*@-.-
		ermes.innerHTML = "Email is invalid!";
		email.classList.add("notright");
	}
	else {
		ermes.innerHTML = "";
		email.classList.remove("notright");
	}
}

function checkemailbool() {
    var email = document.getElementById('email').value;
    var ermes = document.getElementById('error');
    var foo = document.getElementById("foo")
    foo.innerHTML = '<p><label for="password" style="text-align:center">Your Password* </label> <p> <input type="password" name="password" id="password" class="textin" required></input>';
    ermes.innerHTML = "Write your password"
    if (email.indexOf(".", email.indexOf("@")) == -1) ermes.innerHTML = "Email is invalid!";
    else ermes.innerHTML = "";

}