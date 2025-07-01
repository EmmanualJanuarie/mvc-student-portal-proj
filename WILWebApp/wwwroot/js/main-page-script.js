window.addEventListener('load', function () {
    // Functionality for removing the logout button and username
    const button = document.getElementById('logout_btn');
    const user = document.getElementById('username');
    const home = document.getElementById('home_navItem');
    const chat = document.getElementById('chat_navItem');
    const sos = document.getElementById('sos_navitem');


    if (button) {
        button.remove();
    }

    if (user) {
        user.remove();
    }

    if (home) {
        home.remove();
    }
    if (chat) {
        chat.remove();
    }
    if (sos) {
        sos.remove();
    }

});
