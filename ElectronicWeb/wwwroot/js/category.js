function getTopCategory() {
    var token = getCookie("token");
    $.ajax({
        url: 'http://localhost:5243/api/Category/categoryParent',
        method: 'GET',
        dataType: 'json',
        contentType: 'application/json',
        success: function (response) {
            console.log(response)
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
            
        },
        error: function (xhr, status, error) {
            // Xử lý lỗi
            console.error(error);
        }
    });
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
