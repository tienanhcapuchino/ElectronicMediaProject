function getcomment() {
    var queryParams = new URLSearchParams(window.location.search);
    var postId = queryParams.get("id");
    $("#commentsList").empty();
    $.ajax({
        url: 'http://localhost:5243/api/Comment/' + postId,
        method: 'GET',
        dataType: 'json',
        contentType: 'application/json',
        success: function (response) {
            console.log(response)
            $("#numberComment").text(`${response.countComment} Comments`);
            response.commentModels.forEach(function (comment) {
                var commentItem = $("<li>").addClass("comment").appendTo("#commentsList");
                var vcard = $("<div>").addClass("vcard").appendTo(commentItem);
                $("<img>").attr("src", comment.imageUser).attr("alt", "Image placeholder").appendTo(vcard);
                var commentBody = $("<div>").addClass("comment-body").appendTo(commentItem);
                $("<h3>").text(comment.userName).appendTo(commentBody);
                $("<div>").addClass("meta").text(new Date(comment.createdDate).toDateString()).appendTo(commentBody);
                $("<p>").text(comment.content).appendTo(commentBody);
                var replyLink = $("<a>").addClass("reply rounded").text("Reply").appendTo($("<p>").appendTo(commentBody));
                replyLink.on("click", function (e) {
                    e.preventDefault();
                    if (!commentBody.find(".reply-form").length) {
                        var form = createReplyForm(commentBody);
                        form.find(".reply-btn").on("click", function (e) {
                            e.preventDefault();
                            // Get the reply text from the text area
                            var replyText = form.find("textarea").val();
                            var token = getCookie("token");
                            if (token == null) {
                                window.location.href = "account/login";
                            } else {
                                var user = decodeToken(token);
                                
                                var data = {
                                    "parentId": comment.id,
                                    "content": replyText,
                                    "userId": user.UserId,
                                };
                                createComment(data);
                                // Optionally, you can display the reply or perform other actions here

                                // Remove the reply form after submitting the reply
                                form.remove();
                            }
                        });
                    }
                });

                if (comment.replyComment && comment.replyComment.length > 0) {
                    var childrenList = $("<ul>").addClass("children").appendTo(commentItem);
                    comment.replyComment.forEach(function (reply) {
                        var replyItem = $("<li>").addClass("comment").appendTo(childrenList);
                        var vcard = $("<div>").addClass("vcard").appendTo(replyItem);
                        $("<img>").attr("src", reply.imageUser).attr("alt", "Image placeholder").appendTo(vcard);
                        var replyBody = $("<div>").addClass("comment-body").appendTo(replyItem);
                        $("<h3>").text(reply.userName).appendTo(replyBody);
                        $("<div>").addClass("meta").text(new Date(reply.createdDate).toDateString()).appendTo(replyBody);
                        $("<p>").text(reply.content).appendTo(replyBody);
                        var post = $("<p>").appendTo(replyBody);
                        var reply = $("<a>").addClass("reply rounded").text("Reply").appendTo(post);
                        reply.on("click", function (e) {
                            e.preventDefault();
                            if (!replyBody.find(".reply-form").length) {
                                var form = createReplyForm(replyBody);
                                form.find(".reply-btn").on("click", function (e) {
                                    e.preventDefault();
                                    // Get the reply text from the text area
                                    var replyText = form.find("textarea").val();
                                    var token = getCookie("token");
                                    if (token == null) {
                                        window.location.href = "account/login";
                                    } else {
                                        var user = decodeToken(token);

                                        var data = {
                                            "parentId": comment.id,
                                            "content": replyText,
                                            "userId": user.UserId,
                                        };
                                        createComment(data,false);
                                        // Optionally, you can display the reply or perform other actions here

                                        // Remove the reply form after submitting the reply
                                        form.remove();
                                    }
                                });
                            }
                        });
                    });
                }
            });
        },
        error: function (xhr, status, error) {
            // Xử lý lỗi
            console.error(error);
        }
    });
}

function createReplyForm(commentBody) {
    var form = $("<form>").addClass("reply-form").appendTo(commentBody);
    var textarea = $("<textarea>").addClass("form-control mb-3").attr("placeholder", "Type your reply here...").appendTo(form);
    $("<button>").text("Send").addClass("btn btn-primary reply-btn").appendTo(form);
    return form;
}
function createComment(data, check) {
    if (check) {
        $.ajax({
            url: "http://localhost:5243/api/Comment/create",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(data),
            success: function (response) {
                // Handle the success response from the API, if needed
                getcomment();
            },
            error: function (xhr, status, error) {
                // Handle the error response from the API, if needed
                console.error("Error sending the reply:", error);
            }
        });
    } else {
        $.ajax({
            url: "http://localhost:5243/api/ReplyComment/create",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(data),
            success: function (response) {
                // Handle the success response from the API, if needed
                getcomment();
            },
            error: function (xhr, status, error) {
                // Handle the error response from the API, if needed
                console.error("Error sending the reply:", error);
            }
        });
    }
    
}

function postComment(event) {
    event.preventDefault();
    var queryParams = new URLSearchParams(window.location.search);
    var postId = queryParams.get("id");
    var replyText = $("#message").val();
    var token = getCookie("token");
    if (token == null) {
        window.location.href = "account/login";
    } else {
        var user = decodeToken(token);

        var data = {
            "postId": postId,
            "content": replyText,
            "userId": user.UserId,
        };
        createComment(data,true);
        $("#message").val("");
    }
}