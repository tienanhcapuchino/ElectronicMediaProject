// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    var token = getCookie("token");
    if (token != null) {
        $.ajaxSetup({
            headers: {
                "Authorization": "Bearer " + token // Gán token vào tiêu đề yêu cầu
            }
        });
    }
});
function getCookie(token) {
    var cookie = $.cookie(token);
    if (cookie != null) {
        return decodeURIComponent(cookie);
    }
    return null;
}
function decodeToken(token) {
    var parts = token.split('.');
    var encodedPayload = parts[1];
    var payload = decodeURIComponent(atob(encodedPayload).split('').map(function (c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));

    return JSON.parse(payload);
}