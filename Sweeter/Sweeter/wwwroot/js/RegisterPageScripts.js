function checkpass() {
    var pas = document.getElementById("password");
    var pas2 = document.getElementById("password2");
    var ermes = document.getElementById('error');
    var regexp = /^[a-zA-Z0-9]*$/;
    if (pas.value.length < 6) { // length of password 1
        pas.classList.add("notright");
        ermes.innerText = "This password is too short, try somethin bigger than 6 symbols.";
        pas2.classList.remove("notright");
        pas2.value = "";
        pas2.disabled = true; 
    }
    else if (!regexp.test(pas.value)) { // characters in password 1
        pas.classList.add("notright");
        ermes.innerText = "This password contains not allowed characters.";
        pas2.classList.remove("notright");
        pas2.value = "";
        pas2.disabled = true; 
    }
    else {
        pas.classList.remove("notright");
        ermes.innerText = "";
        pas2.disabled = false; 
    }
}

function checkpasswords() { 
	var pas1 = document.getElementById('password');
	var pas2 = document.getElementById('password2');
	var ermes = document.getElementById('error');
	if (pas1.value !== pas2.value) { // if passwords match 
		pas1.classList.add("notright");
		pas2.classList.add("notright");
        ermes.innerText = "Passwords are not simmilar!";
	}
	else {
		pas1.classList.remove("notright");
		pas2.classList.remove("notright");
        ermes.innerText = "";
        register();
	}
}

function checkusername() {
    var username = document.getElementById('username');
    var regexp = /^[a-zA-Z0-9]*$/;
    var regexp2 = /^[0-9]*$/;
    var ermes = document.getElementById('error');
    if (!regexp.test(username.value)) { // if username contains only symbols listed in regexp
        ermes.innerText = "Username may contain only letters from English alphabet and numbers!";
        username.classList.add("notright");
    }
    else if (username.value.length > 50) {
        ermes.innerText = "Username must not be longer than 50 characters.";
        username.classList.add("notright");
    }
    else if (regexp2.test(username.value)) { // if username contains only numbers
        ermes.innerText = "Username must containe at least 1 character from English alphabet!";
        username.classList.add("notright");
    }
    else {
        ermes.innerText = "";
        username.classList.remove("notright");
        register();
    }
}

function checkemail() {
	var email = document.getElementById('email');
	var ermes = document.getElementById('error');
	var emailval = document.getElementById('email').value;
	var len = emailval.length - 1; // length of email
	var ind = emailval.indexOf(".", emailval.indexOf("@")); //index of . after @ 
	var indat = emailval.indexOf("@"); // index of @
    var regexp = /[a-z0-9]/i; // all allowed symbols for the end of an email
	if (ind === -1) { // check if there is a .
		ermes.innerText = "Email is invalid!";
		email.classList.add("notright");
	}
	else if (ind - indat === 1) { // if user wrote . right after the @
		ermes.innerText = "Email is invalid!";
		email.classList.add("notright");
    }
    else if (!regexp.test(emailval[emailval.length-1])) { // if email end with smth else then regexp
        ermes.innerText = "Email is invalid!";
        email.classList.add("notright");
    }
	else { // allowed email
		ermes.innerText = "";
        email.classList.remove("notright");
        register();
	}
}

function register() { 
    var username = document.getElementById('username');
    var email = document.getElementById('email');
    var pass = document.getElementById('password');
    var button = document.getElementById('register');
    if ((username.value.length !== 0) && (email.value.length !== 0) && (pass.value.length !== 0)) { // if all textinputs are filled
        if (!email.classList.contains("notright") && !username.classList.contains("notright") && !pass.classList.contains("notright")) button.disabled = false; // if out scripts decided that everthing is good
    }
}

function checkemailbool() {
    var email = document.getElementById('email').value;
    var ermes = document.getElementById('error');
    var foo = document.getElementById("foo")
    foo.innerText = '<p><label for="password" style="text-align:center">Your Password* </label> <p> <input type="password" name="password" id="password" class="textin" required></input>';
    ermes.innerText = "Write your password"
    if (email.indexOf(".", email.indexOf("@")) === -1) ermes.innerText = "Email is invalid!";
    else ermes.innerText = "";

}
