// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const photoPath = document.getElementById("photoPath");
// const preview = document.getElementById("photoPreview");
const photo = document.getElementById("photo");

photoPath.addEventListener("change", function () {
    var path = document.getElementById("photoPath").files;
    console.log(path);

    if (path.length > 0) {
        var loadImage = path[0];
        console.log(loadImage);

        var readFile = new FileReader();

        readFile.onload = function (e) {
            var imageBase64 = e.target.result;
            console.log(imageBase64);
            photo.value = imageBase64;
            // preview.classList.remove("d-none");
            // preview.src = imageBase64;
        }

        readFile.readAsDataURL(loadImage);
    }
})