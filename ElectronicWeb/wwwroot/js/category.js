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

function getTopCategory() {
    var token = getCookie("token");
    var categoryPost = $("#categoryList");
    $.ajax({
        url: 'http://localhost:5243/api/Category/categoryParent',
        method: 'GET',
        dataType: 'json',
        contentType: 'application/json',
        success: function (response) {
            const $navigationMenu = $("#parents");
            response.forEach(item => {
                const $li = $("<li>");
                const $a = $("<a>").text(item.name).click(function () {
                    rediectToCategory(item.id, item.name);
                });
                $li.append($a);
                if (item.childrens && item.childrens.length > 0) {
                    $li.addClass("has-children");
                    buildMenu(item.childrens, $li);
                }
                $navigationMenu.append($li);
            });
            const $log = $("<li>");
            if (token != null) {
                var user = decodeToken(token);
                const $logOut = $("<a>").text("LogOut").addClass("log-link").click(function () {
                    logout(event);
                });
                $log.append($logOut)
                var allowedRoles = ["Leader", "EditorDirector", "Admin", "Writer"];
                if (allowedRoles.includes(user.role)) {
                    const $setting = $("<a>").text("Admin").addClass("log-link").click(function () {
                        window.location.href = "/admin";
                    });
                    $log.append($setting)
                }
            } else {
                const $logIn = $("<a>").text("LogIn").addClass("log-link").click(function () {
                    redirect(event);
                });
                $log.append($logIn)
            }
            
            $navigationMenu.append($log);
            if (categoryPost != null) {
                response.forEach(function (category) {
                    var listItem = $("<li>").appendTo("#categoryList");
                    var link = $("<a>").attr("href", "#").appendTo(listItem).text(category.name);
                    $("<span>").text("(" + category.countPost + ")").appendTo(link);
                    categoryPost.append(listItem);
                });
            }
        },
        error: function (xhr, status, error) {
            // Xử lý lỗi
            console.error(error);
        }
    });
}
function getCategory() {
    $.ajax({
        url: 'http://localhost:5243/api/Category/categoryParent',
        method: 'GET',
        dataType: 'json',
        contentType: 'application/json',
        success: function (response) {
            generatePostsEntrySection(response[0]);
            generatePostsEntrySectionSmall(response[1]);
            generatePostsEntrySectionSecond(response[2]);
            generateSectionPostsEntryList(response[3]);
            generateSectionTravel(response[4]);
        },
        error: function (xhr, status, error) {
            // Xử lý lỗi
            console.error(error);
        }
    });
}
function getCategoryById(id) {
    var tagA = $("<a>")
    $.ajax({
        url: 'http://localhost:5243/api/Category/' + id,
        method: 'GET',
        dataType: 'json',
        contentType: 'application/json',
        success: function (response) {
            tagA.text(response.name)
        },
        error: function (xhr, status, error) {
            // Xử lý lỗi
            console.error(error);
        }
    });
    return tagA;
}

function buildMenu(data, $parent) {
    const $ul = $("<ul>").addClass("dropdown");
    data.forEach(item => {
        const $li = $("<li>");
        const $a = $("<a>").text(item.name).click(function () {
            rediectToCategory(item.id, item.name);
        });
        $li.append($a);

        if (item.childrens && item.childrens.length > 0) {
            $li.addClass("has-children");
            buildMenu(item.childrens, $li);
        }

        $ul.append($li);
    });

    $parent.append($ul);
}

function getcategoryForadd() {
    $.ajax({
        url: "http://localhost:5243/api/Category/category",
        type: "GET",
        dataType: "json",
        success: function (data) {
            // Clear the current options from the category select element
            $("#category").empty();

            // Append the new options to the category select element
            $.each(data, function (index, category) {
                if (index == 0) {
                    getFirstOptionValue(category.id);
                }
                $("#category").append('<option value="' + category.id + '">' + category.name + '</option>');
            });
        },
        error: function (xhr, status, error) {
            console.error("Failed to load categories. Error:", error);
        },
    });
}
function getsubcategory() {
    $("#category").on("change", function () {
        // Get the selected category value
        const selectedCategory = $(this).val();
        // Make the AJAX GET request to fetch subcategories for the selected category
        $.ajax({
            url: "http://localhost:5243/api/Category/subcategory/" + selectedCategory, // Replace "Category" with the appropriate API endpoint to fetch subcategories for a specific category
            type: "GET",
            dataType: "json",
            success: function (data) {
                // Clear the current options from the "subcategory" select element
                $("#subCategory").empty();

                // Append the new options to the "subcategory" select element
                $.each(data, function (index, subCategory) {
                    $("#subCategory").append('<option value="' + subCategory.id + '">' + subCategory.name + '</option>');
                });

                console.log("Subcategories loaded successfully.");
            },
            error: function (xhr, status, error) {
                console.error("Failed to load subcategories. Error:", error);
            },
        });
        
    });
}
function getFirstOptionValue(id) {
    $.ajax({
        url: "http://localhost:5243/api/Category/subcategory/" + id, // Replace "Category" with the appropriate API endpoint to fetch subcategories for a specific category
        type: "GET",
        dataType: "json",
        success: function (data) {
            // Clear the current options from the "subcategory" select element
            $("#subCategory").empty();

            // Append the new options to the "subcategory" select element
            $.each(data, function (index, subCategory) {
                $("#subCategory").append('<option value="' + subCategory.id + '">' + subCategory.name + '</option>');
            });
        },
        error: function (xhr, status, error) {
            console.error("Failed to load subcategories. Error:", error);
        },
    });
}
