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
            var serializedtoken = JSON.stringify(response.data);
            $.cookie("token", serializedtoken);
            var token = getCookie("token")
            var allowedRoles = ["Leader", "EditorDirector", "Admin", "Writer"];
            if (allowedRoles.includes(token.role)) {
                window.location.href = "/admin";
            } else {
                window.location.href = "/";
            }
        },
        error: function (xhr, status, error) {
            // Xử lý lỗi
            console.error(error);
        }
    });
}