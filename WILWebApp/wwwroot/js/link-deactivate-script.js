
window.addEventListener('load', function () { 
    const link = document.getElementById('main_logo');

    if (link) {
        link.onclick = function (event) {
            // Prevent the default action of the link
            event.preventDefault();

            // Show the alert message
            alert("Logout to Return to Login Menu");

        }

    }
});
