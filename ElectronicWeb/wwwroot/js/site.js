/*********************************************************************
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
$(document).ready(function () {
    var token = getCookie("token");
    if (token != null) {
        $.ajaxSetup({
            headers: {
                "Authorization": "Bearer " + token // Gán token vào tiêu đề yêu cầu
            }
        });
        $("#profile").show();
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

function rediectToCategory(id,name) {
    localStorage.setItem("categoryName", name);
    window.location.href = "/Post/CategoryPost?id=" + id;
}

function redirectTo() {
    window.location.href = "/user/userprofile";
}