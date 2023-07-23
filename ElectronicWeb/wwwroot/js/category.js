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
                const $a = $("<a>").text(item.name);
                $li.append($a);
                if (item.childrens && item.childrens.length > 0) {
                    $li.addClass("has-children");
                    buildMenu(item.childrens, $li);
                }
                $navigationMenu.append($li);
            });
            const $log = $("<li>");
            if (token != null) {
                const $logOut = $("<a>").text("LogOut").addClass("log-link").click(function () {
                    logout(event);
                });
                $log.append($logOut)
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
        const $a = $("<a>").text(item.name);
        $li.append($a);

        if (item.childrens && item.childrens.length > 0) {
            $li.addClass("has-children");
            buildMenu(item.childrens, $li);
        }

        $ul.append($li);
    });

    $parent.append($ul);
}
