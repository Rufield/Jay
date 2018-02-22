// Write your Javascript code.
window.onload = function () {
    var hide = document.getElementById('HideForm');
    var show = document.getElementById('ShowForm');
    var form = document.getElementById('Addfeed');

    hide.onclick = function () {
        form.classList.add('hidden');
        hide.classList.add('hidden');
        show.classList.remove('hidden');
    }
    show.onclick = function () {
        form.classList.remove('hidden');
        hide.classList.remove('hidden');
        show.classList.add('hidden');
    }
}