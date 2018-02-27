function upload() {
    var preview = document.querySelector('img');
    var file = document.querySelector('input[type=file]').files[0];
    var reader = new FileReader();

    reader.onloadend = function () { preview.src = reader.result; }

    if (file) {
        reader.readAsDataURL(file);
    } else { preview.src = "~/lib/img/avatar.jpeg" }
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
    }
    FullCheck();
}

function checkfullname() {
    var fullname = document.getElementById('name');
    var ermes = document.getElementById('error');
    if (fullname.value.length > 50) {
        ermes.innerText = "Your Fullname is too long! Try to make it a bit shorter";
        fullname.classList.add("notright");
    }
    else {
        ermes.innerText = "";
        fullname.classList.remove("notright");
    }
    FullCheck();
}

function limitText(limitField, limitNum) {
    var ermes = document.getElementById('error');
    if (limitField.value.length > limitNum) {
        limitField.value = limitField.value.substring(0, limitNum);
        ermes.innerText = "Text limitation 300 characters";
    }
    FullCheck();
}

function CheckPasswordForDelete() {
    var password = document.getElementById('password');
    var button = document.getElementById('DeleteButton')
    var ermes = document.getElementById('DeleteError');
    if (password.value.length < 6) {
        ermes.innerText = "Unlike the truth, this password is too short.";
        button.disabled = true;
    }
    else {
        ermes.innerText = "";
        button.disabled = false;
    }
}

function FullCheck() {
    var fullname = document.getElementById('name'); 
    var username = document.getElementById('username');
    var about = document.getElementById('about');
    var button = document.getElementById('ApplyButton');
    if (!fullname.classList.contains('notright') && !username.classList.contains('notright') && !about.classList.contains('notright')) {
        button.disabled = false;
    }
    else button.disabled = true;
}

(function ClearModal() {
    var modal = document.getElementById('password');
    modal.innerText = '';
})()