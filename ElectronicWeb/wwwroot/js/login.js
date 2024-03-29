﻿/*********************************************************************
 * 
 * PROPRIETARY and CONFIDENTIAL
 * 
 * This is licensed from, and is trade secret of:
 * 
 *          Group 10 - PRN231 - SU23
 *          FPT University, Education and Training zone
 *          Hoa Lac Hi-tech Park, Km29, Thang Long Highway
 *          Ha Noi, Viet Nam
 *          
 * Refer to your License Agreement for restrictions on use,
 * duplication, or disclosure
 * 
 * RESTRICTED RIGHTS LEGEND
 * 
 * Use, duplication or disclosure is the
 * subject to restriction in Articles 736 and 738 of the 
 * 2005 Civil Code, the Intellectual Property Law and Decree 
 * No. 85/2011/ND-CP amending and supplementing a number of 
 * articles of Decree 100/ND-CP/2006 of the Government of Viet Nam
 * 
 * 
 * Copy right 2023 © PRN231 - SU23 - Group 10 ®. All Rights Reserved
 * 
 * Unpublished - All rights reserved under the copyright laws 
 * of the Government of Viet Nam
*********************************************************************/

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
                var allowedRoles = ["Leader", "EditorDirector", "Admin", "Writer"];
                if (allowedRoles.includes(user.role)) {
                    window.location.href = "/admin";
                } else {
                    window.location.href = "/";
                }
                localStorage.setItem("userId", user.UserId);
            } else {
                alert("Login failed!")
            }
        },
        error: function (xhr, status, error) {
            // Xử lý lỗi
            console.error(error);
        }
    });
}
function logout(event) {
    event.preventDefault();
    document.cookie = `token=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;`;
    var token = getCookie("token")
    if (token == null) {
        window.location.href = "/";
    }
}
function redirect(event) {
    event.preventDefault();
    window.location.href = "/account/login";
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

function register(event) {
    event.preventDefault();
    var username = $("#username").val();
    var fullname = $("#fullname").val();
    var dobInput = $("#dobInput").val();
    var maleRadio = $("#maleRadio").is(":checked");
    var email = $("#email").val();
    var password = $("#password").val();
    var hasError = false;

    if (!validateEmail(email)) {
        $("#emailError").text("Invalid email format.");
        event.preventDefault(); // Prevent form submission
        hasError = true;
    } else {
        $("#emailError").text(""); // Clear error message
        if (!validatePassword(password)) {
            $("#passwordError").text("Password must have at least one digit (0-9) and one uppercase letter (A-Z).");
            event.preventDefault(); // Prevent form submission
            hasError = true;
        } else {
            $("#passwordError").text(""); // Clear error message
        }
    }
    if (hasError) {
        event.preventDefault(); // Prevent form submission
        return; // Dừng việc gọi ajax
    }
    var requestBody = {
        username: username,
        fullname: fullname,
        dob: dobInput,
        gender: maleRadio? 1 : 2,
        email: email,
        password: password
    };
    $.ajax({
        url: "http://localhost:5243/api/User/register",
        type: "POST", // Change to the appropriate HTTP method (e.g., GET, POST, PUT, DELETE, etc.)
        dataType: "json", // Set the expected data type of the response
        contentType: 'application/json',
        data: JSON.stringify(requestBody),
        success: function (data) {
            // Handle the successful response from the API
            $("#genericModalLabel").text("Register");
            if (data.isSucceed) {
                $("#genericModalBody").text(data.message);
                $("#genericModalLabel").addClass("text-success");
                $("#genericModal").find(".modal-dialog").removeClass("modal-danger").addClass("modal-success");
            } else {
                $("#genericModalBody").text(data.message);
                $("#genericModalLabel").addClass("text-danger");
                $("#genericModal").find(".modal-dialog").removeClass("modal-success").addClass("modal-danger");
            }
            $("#genericModal").modal("show");
        },
        error: function (xhr, status, error) {
            $("#genericModalBody").text(error);
            $("#genericModalLabel").addClass("text-danger");
            $("#genericModal").find(".modal-dialog").removeClass("modal-success").addClass("modal-danger");
        }
    });
}
function validateEmail(email) {
    // This is a simple email validation regex
    var emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailRegex.test(email);
}
function validatePassword(password) {
    // This is a password validation regex that checks for at least one digit and one uppercase letter
    var passwordRegex = /^(?=.*\d)(?=.*[A-Z])/;
    return passwordRegex.test(password);
}