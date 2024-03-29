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
 * Copy right 2023 � PRN231 - SU23 - Group 10 �. All Rights Reserved
 * 
 * Unpublished - All rights reserved under the copyright laws 
 * of the Government of Viet Nam
*********************************************************************/

function viewDetail() {
    var queryParams = new URLSearchParams(window.location.search);
    var postId = queryParams.get("id");
    $.ajax({
        url: "http://localhost:5243/api/Post/" + postId,
        method: "GET",
        dataType: "json",
        contentType: 'application/json',
        success: function (data) {
            generateHeader(data);
            var postHtmlElements = $.parseHTML(data.content);
            var postEntry = $("<p>").append(postHtmlElements);
            var postCate = $("<p>").text("Category:");
            $("#contentPost").append(postEntry);
            var a1 = getCategoryById(data.categoryId);
            postCate.append(a1);
            if (data.subCategoryId != null) {
                var a2 = getCategoryById(data.subCategoryId);
                postCate.append(",",a2);
            }
            $("#category").append(postCate);
        },
        error: function (xhr, status, error) {
            // Handle error here
            console.log("Error:", error);
        }
    });
}
function generateHeader(data) {
    // Create the main div with class "post-entry text-center"
    var postEntry = $("<div>").addClass("post-entry text-center");

    // Create the <h1> element with the title
    var title = $("<h1>").addClass("mb-4").text(data.title);

    // Create the div with class "post-meta align-items-center text-center"
    var postMeta = $("<div>").addClass("post-meta align-items-center text-center");

    // Create the <figure> element with the image
    var figure = $("<figure>").addClass("author-figure mb-0 me-3 d-inline-block");
    var image = $("<img>").attr("src", data.userImage).attr("alt", "imageUser").addClass("img-fluid");
    figure.append(image);

    // Create the first <span> element with the author name
    var authorSpan = $("<span>").addClass("d-inline-block mt-1").text(data.authorName);

    // Create the second <span> element with the date
    var dateSpan = $("<span>").text(" - " + new Date(data.publishedDate).toDateString());

    // Append the elements to the appropriate parent elements
    postMeta.append(figure);
    postMeta.append(authorSpan);
    postMeta.append(dateSpan);

    // Append the title and postMeta div to the main postEntry div
    postEntry.append(title);
    postEntry.append(postMeta);

    // Append the entire postEntry div to the container
    $('#headerTitle').append(postEntry);
}