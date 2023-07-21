function login(event) {
    event.preventDefault();
    var username = $('#username').val()
    var password = $('#password').val()
    var data = {
        username: username,
        password: password
    };
    $.ajax({
        url: 'http://localhost:5243/api/User/login',
        method: 'POST',
        dataType: 'json',
        contentType: 'application/json',
        data: JSON.stringify(data),
        success: function (response) {
            // Xử lý phản hồi thành công
            if (response.isSucceed) {

                var serializedtoken = JSON.stringify(response.data);
                setCookie("token", serializedtoken, 1);
                var token = getCookie("token")
                var user = decodeToken(token)
                setCookie("user", JSON.stringify(user), 1);

                var allowedRoles = ["Leader", "EditorDirector", "Admin", "Writer"];
                if (allowedRoles.includes(token.role)) {
                    window.location.href = "/admin";
                } else {
                    window.location.href = "/";
                }
            } else {
                alert("Login False!")
            }
        },
        error: function (xhr, status, error) {
            // Xử lý lỗi
            console.error(error);
        }
    });
}
function setCookie(name, value, days) {
    var expires = "";
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toUTCString();
    }
    document.cookie = name + "=" + value + expires + "; path=/";
}