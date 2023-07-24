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

function getNewPost() {
    $.ajax({
        url: 'http://localhost:5243/api/Post/newPost/5',
        method: 'GET',
        dataType: 'json',
        contentType: 'application/json',
        success: function (response) {
            response.forEach(function (item, index) {
                // Sử dụng hàm createArticleElement để tạo phần tử

                if (index < 2) {
                    var articleElement = createArticleElemt(item, index);
                    $('#row_1').append(articleElement);
                } else if (index == 2) {
                    var articleElement = createArticleElementRow2(item);
                    $('#row_2').append(articleElement);
                } else {
                    var articleElement = createArticleElemt(item, index);
                    $('#row_3').append(articleElement);
                }
            });
        },
        error: function (xhr, status, error) {
            // Xử lý lỗi
            console.error(error);
        }
    });
}
function getPopularPost(number) {
    $.ajax({
        url: 'http://localhost:5243/api/Post/newPost/' + number,
        method: 'GET',
        dataType: 'json',
        contentType: 'application/json',
        success: function (response) {
            response.forEach(function (post) {
                var listItem = $("<li>").appendTo("#postList");
                var link = $("<a>").appendTo(listItem).click(function () {
                    rediectToReading(post.id);
                });
                $("<img>").attr("src", post.image).attr("alt", "Image placeholder").addClass("me-4 rounded").appendTo(link);
                var textDiv = $("<div>").addClass("text").appendTo(link);
                $("<h4>").text(post.title).appendTo(textDiv);
                var postMetaDiv = $("<div>").addClass("post-meta").appendTo(textDiv);
                $("<span>").text(new Date(post.createdDate).toDateString()).addClass("mr-2").appendTo(postMetaDiv);
            });
        },
        error: function (xhr, status, error) {
            // Xử lý lỗi
            console.error(error);
        }
    });
}
function getPostByCategory(page) {
    var queryParams = new URLSearchParams(window.location.search);
    var category = queryParams.get("id");
    var categotyName = localStorage.getItem("categoryName");
    var data = {
        "categoryId": category,
        "pageNumber": page,
        "pageSize": 10
    }
    $("#blogEntriesContainer").empty();
    $("#heading_c").append(`Category: ${categotyName}`);
    $.ajax({
        url: 'http://localhost:5243/api/Post/category',
        method: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(data),
        success: function (response) {
            response.values.forEach(function (entry) {
                var blogEntryDiv = $("<div>").addClass("blog-entry d-flex blog-entry-search-item").appendTo("#blogEntriesContainer");
                var imgLink = $("<a>").addClass("img-link me-4").appendTo(blogEntryDiv);
                $("<img>").attr("src", entry.image).attr("alt", "Image").addClass("img-fluid").appendTo(imgLink);
                var entryDiv = $("<div>").appendTo(blogEntryDiv);
                $("<span>").addClass("date").text(new Date(entry.createdDate).toDateString()).appendTo(entryDiv);
                $("<h2>").appendTo(entryDiv).append($("<a>").text(entry.title)).click(function () {
                    rediectToReading(entry.id);
                });
                $("<p>").text(entry.description).appendTo(entryDiv);
                $("<p>").appendTo(entryDiv).append($("<a>").addClass("btn btn-sm btn-outline-primary").text("Read More")).click(function () {
                    rediectToReading(entry.id);
                });
            });
            var totalPages = response.totalPages;
            var pagesToShow = 5;
            var currentPage = response.currentPage;
            
            var parent = $("<div>").addClass("row text-start pt-5 border-top").appendTo("#blogEntriesContainer");
            var divcontain = $("<div>").addClass("col-md-12").appendTo(parent);
            var paginationDiv = $("<div>").addClass("custom-pagination").appendTo(divcontain);

            function calculateStartEndPages() {
                var halfPagesToShow = Math.floor(pagesToShow / 2);
                var startPage = Math.max(currentPage - halfPagesToShow, 1);
                var endPage = Math.min(startPage + pagesToShow - 1, totalPages);
                if (endPage - startPage + 1 < pagesToShow) {
                    startPage = Math.max(endPage - pagesToShow + 1, 1);
                }
                return { start: startPage, end: endPage };
            }
            if (currentPage > pagesToShow / 2) {
                createPaginationLink("First", paginationDiv);
                createPaginationLink("<<", paginationDiv);
            }
            var { start, end } = calculateStartEndPages();
            for (var i = start; i <= end; i++) {
                createPaginationLink(i, paginationDiv);
            }

            // Add ">>" for next page if not on the last set of pages
            if (end < totalPages) {
                createPaginationLink(">>", paginationDiv);
                createPaginationLink("Last", paginationDiv);

            }

            paginationDiv.on('click', 'a', function (event) {
                event.preventDefault();
                var clickedPage = $(this).attr("data-page");

                if (clickedPage === "First") {
                    currentPage = 1;
                } else if (clickedPage === "<<") {
                    currentPage = Math.max(currentPage - 1, 1);
                } else if (clickedPage === ">>") {
                    currentPage = Math.min(currentPage + 1, totalPages);
                } else if (clickedPage === "Last") {
                    currentPage = totalPages;
                } else {
                    currentPage = parseInt(clickedPage, 10);
                }
                paginationDiv.empty();

                getPostByCategory(currentPage);
                updatePagination(currentPage);
            });
            updatePagination(currentPage);
        },
        error: function (xhr, status, error) {
            // Xử lý lỗi
            console.error(error);
        }
    });
}

function updatePagination(activePage) {
    $('.custom-pagination a').removeClass('active'); // Remove 'active' class from all links
    $('.custom-pagination a[data-page="' + activePage + '"]').addClass('active'); // Add 'active' class to the clicked link
}
function createPaginationLink(pageNumber, paginationDiv) {
    return $("<a>").attr("data-page", pageNumber).text(pageNumber).appendTo(paginationDiv);
}
function createArticleElemt(item, index) {
    var link = $("<a>").addClass("h-entry v-height gradient").click(function () {
        rediectToReading(item.id)
    });
    if (index == 0 | index == 3) {
        link = $("<a>").addClass("h-entry v-height gradient mb-30").click(function () {
            rediectToReading(item.id)
        });
    }
    var featuredImg = $("<div>").addClass("featured-img").css("background-image", "url('" + item.image + "')");
    var textDiv = $("<div>").addClass("text");
    var dateSpan = $("<span>").addClass("date").text(new Date(item.createdDate).toDateString());
    var titleH2 = $("<h2>").text(item.title);

    // Append elements together
    textDiv.append(dateSpan, titleH2);
    link.append(featuredImg, textDiv);
    return link;
}
function createArticleElementRow2(item) {
    // Create elements
    var link = $("<a>").addClass("h-entry img-5 h-100 gradient").click(function () {
        rediectToReading(item.id)
    });
    var featuredImg = $("<div>").addClass("featured-img").css("background-image", "url('" + item.image + "')");
    var textDiv = $("<div>").addClass("text");
    var dateSpan = $("<span>").addClass("date").text(new Date(item.createdDate).toDateString());
    var titleH2 = $("<h2>").text(item.title);

    // Append elements together
    textDiv.append(dateSpan, titleH2);
    link.append(featuredImg, textDiv);

    return link;
}
function rediectToReading(id) {
    window.location.href = "/reading?id=" + id;
}

function generatePostsEntrySection(category) {
    var section = $("<section>").addClass("section posts-entry");
    // Create container div with class "container"
    var container = $("<div>").addClass("container");

    var headerRow = generateHeaderRow(category.name, category.id);
    var blogEntriesRow = $("<div>").addClass("row g-3");
    $.ajax({
        url: "http://localhost:5243/api/Post/category",
        method: "POST",
        dataType: "json",
        contentType: 'application/json',
        data: JSON.stringify({
            categoryId: category.id,
            pageSize: 5,
            pageNumber: 1
        }),
        success: function (data) {
            // Handle the response data here
            var listFirstEntry = []
            var listSecondEntry = []
            data.values.forEach(function (item, index) {
                if (index < 2) {
                    listFirstEntry.push(item)
                } else {
                    listSecondEntry.push(item)
                }
            });
            var clogEntriesRow9 = generatePostInEntryRow9(listFirstEntry, "");
            var sidebarColumn = generatePostInEntrySider(listSecondEntry);
            blogEntriesRow.append(clogEntriesRow9,sidebarColumn);
        },
        error: function (xhr, status, error) {
            // Handle error here
            console.log("Error:", error);
        }
    });
    
    
    // Append the header row, blog entries row, and sidebar column to the container
    container.append(headerRow, blogEntriesRow);

    // Append the container to the section
    section.append(container);
    $("#container_post").append(section);
}
function generateHeaderRow(header,id) {

    // Create div row for the header with class "row mb-4"
    var headerRow = $("<div>").addClass("row mb-4");

    // Create div for the title column with class "col-sm-6"
    var titleColumn = $("<div>").addClass("col-sm-6");

    // Create h2 element for the title with class "posts-entry-title"
    var titleH2 = $("<h2>").addClass("posts-entry-title").text(header);

    // Create div for the "View All" link column with class "col-sm-6 text-sm-end"
    var viewAllColumn = $("<div>").addClass("col-sm-6 text-sm-end");

    // Create "View All" link with class "read-more" and href "category.html"
    var viewAllLink = $("<a>").addClass("read-more").text("View All").click(function () {
        rediectToCategory(id, header);
    });

    // Append the title and "View All" link to their respective columns
    titleColumn.append(titleH2);
    viewAllColumn.append(viewAllLink);

    // Append the title column and "View All" column to the header row
    headerRow.append(titleColumn, viewAllColumn);
    return headerRow;
}
function generatePostInEntryRow9(post, order) {
    var clogEntriesRow9 = $("<div>").addClass("col-md-9 " + order);
    var blogEntriesRowContent = $("<div>").addClass("row g-3");
    // Create the first blog entry column
    if (post != null) {
        post.forEach(x => {
            var blogEntry1Column = $("<div>").addClass("col-md-6");
            // Create the first blog entry content
            var blogEntry1Content = $("<div>").addClass("blog-entry");
            var blogEntry1ImgLink = $("<a>").addClass("img-link").click(function () {
                rediectToReading(x.id)
            });
            var blogEntry1Img = $("<img>").attr("src", x.image).addClass("img-fluid");
            var blogEntry1Date = $("<span>").addClass("date").text(new Date(x.createdDate).toDateString());
            var blogEntry1Title = $("<h2>").append($("<a>").text(x.title).click(function () {
                rediectToReading(x.id)
            }));
            var blogEntry1Description = $("<p>").text(x.description);
            var blogEntry1ReadMore = $("<p>").append($("<a>").addClass("btn btn-sm btn-outline-primary").text("Read More")).click(function () {
                rediectToReading(x.id)
            });
            // Append the elements to form the first blog entry
            blogEntry1ImgLink.append(blogEntry1Img);
            blogEntry1Content.append(blogEntry1ImgLink, blogEntry1Date, blogEntry1Title, blogEntry1Description, blogEntry1ReadMore);
            blogEntry1Column.append(blogEntry1Content);

            blogEntriesRowContent.append(blogEntry1Column);

        })
    }
    // Append the blog entry columns to the blog entries row
    clogEntriesRow9.append(blogEntriesRowContent);
    return clogEntriesRow9;
}
function generatePostInEntrySider(listSecondEntry) {
    // Create the sidebar column with class "col-md-3"
    var sidebarColumn = $("<div>").addClass("col-md-3");

    // Create the ul list for the blog entries on the sidebar with class "list-unstyled blog-entry-sm"
    var sidebarList = $("<ul>").addClass("list-unstyled blog-entry-sm");
    if (listSecondEntry != null) {
        listSecondEntry.forEach(x => {
            var sidebarLi = $("<li>");
            var sidebarDate3 = $("<span>").addClass("date").text(new Date(x.createdDate).toDateString());
            var sidebarTitle3 = $("<h3>").append($("<a>").text(x.title).click(function () {
                rediectToReading(x.id)
            }));
            var sidebarDescription3 = $("<p>").text(x.description);
            var sidebarReadMore3 = $("<p>").append($("<a>").addClass("read-more").text("Reading").click(function () {
                rediectToReading(x.id)
            }));

            // Append the content to the sidebar li items
            sidebarLi.append(sidebarDate3, sidebarTitle3, sidebarDescription3, sidebarReadMore3);

            // Append the sidebar li items to the sidebar ul list
            sidebarList.append(sidebarLi);
        })
    }
    sidebarColumn.append(sidebarList);
    return sidebarColumn;
}

function generatePostsEntrySectionSmall(category) {
    // Create section element with classes "section posts-entry posts-entry-sm bg-light"
    var section = $("<section>").addClass("section posts-entry posts-entry-sm bg-light");

    // Create container div with class "container"
    var container = $("<div>").addClass("container");

    // Create div row for the blog entries
    var blogEntriesRow = $("<div>").addClass("row");
    $.ajax({
        url: "http://localhost:5243/api/Post/category",
        method: "POST",
        dataType: "json",
        contentType: 'application/json',
        data: JSON.stringify({
            categoryId: category.id,
            pageSize: 4,
            pageNumber: 1
        }),
        success: function (data) {
            // Handle the response data here
            data.values.forEach(function (item) {
                var blogEntryColumn = $("<div>").addClass("col-md-6 col-lg-3");
                var blogEntryContent = $("<div>").addClass("blog-entry");
                var blogEntryImgLink = $("<a>").addClass("img-link").click(function () {
                    rediectToReading(item.id)
                });
                var blogEntryImg = $("<img>").attr("src", item.image).attr("alt","Image").addClass("img-fluid");
                var blogEntryDate = $("<span>").addClass("date").text(new Date(item.createdDate).toDateString());
                var blogEntryTitle = $("<h2>").append($("<a>").text(item.title).click(function () {
                    rediectToReading(item.id)
                }));
                var blogEntryDescription = $("<p>").text(item.description);
                var blogEntryReadMore = $("<p>").append($("<a>").addClass("read-more").text("Reading")).click(function () {
                    rediectToReading(item.id)
                });

                // Append the elements to form the blog entry
                blogEntryImgLink.append(blogEntryImg);
                blogEntryContent.append(blogEntryImgLink, blogEntryDate, blogEntryTitle, blogEntryDescription, blogEntryReadMore);
                blogEntryColumn.append(blogEntryContent);

                blogEntriesRow.append(blogEntryColumn);
            });
        },
        error: function (xhr, status, error) {
            // Handle error here
            console.log("Error:", error);
        }
    });
    container.append(blogEntriesRow);
    section.append(container);
    $("#container_post").append(section);
}

function generatePostsEntrySectionSecond(category) {
    var section = $("<section>").addClass("section posts-entry");

    // Create container div with class "container"
    var container = $("<div>").addClass("container");

    var headerRow = generateHeaderRow(category.name, category.id);
    var blogEntriesRow = $("<div>").addClass("row g-3");
    $.ajax({
        url: "http://localhost:5243/api/Post/category",
        method: "POST",
        dataType: "json",
        contentType: 'application/json',
        data: JSON.stringify({
            categoryId: category.id,
            pageSize: 5,
            pageNumber: 1
        }),
        success: function (data) {
            // Handle the response data here
            var listFirstEntry = []
            var listSecondEntry = []
            data.values.forEach(function (item, index) {
                if (index < 2) {
                    listFirstEntry.push(item)
                } else {
                    listSecondEntry.push(item)
                }
            });
            var clogEntriesRow9 = generatePostInEntryRow9(listFirstEntry, "order-md-2");
            var sidebarColumn = generatePostInEntrySider(listSecondEntry);
            blogEntriesRow.append(clogEntriesRow9, sidebarColumn);
        },
        error: function (xhr, status, error) {
            // Handle error here
            console.log("Error:", error);
        }
    });
    // Append the header row, blog entries row, and sidebar column to the container
    container.append(headerRow, blogEntriesRow);
    section.append(container);
    $("#container_post").append(section);
}

function generateSectionPostsEntryList(category) {
    // Create section element with class "section"
    var section = $("<section>").addClass("section");

    // Create container div with class "container"
    var container = $("<div>").addClass("container");

    // Append the title column and "View All" column to the header row
    var headerRow = generateHeaderRow(category.name, category.id);

    // Append the header row to the container
    container.append(headerRow);

    // Create div row for the blog entries with class "row"
    var blogEntriesRow = $("<div>").addClass("row");
    $.ajax({
        url: "http://localhost:5243/api/Post/category",
        method: "POST",
        dataType: "json",
        contentType: 'application/json',
        data: JSON.stringify({
            categoryId: category.id,
            pageSize: 6,
            pageNumber: 1
        }),
        success: function (data) {
            // Handle the response data here
            data.values.forEach(function (item) {
                var blogEntryColumn = $("<div>").addClass("col-lg-4 mb-4");

                // Create the blog entry content
                var blogEntryContent = $("<div>").addClass("post-entry-alt");
                var blogEntryImgLink = $("<a>").addClass("img-link").click(function () {
                    rediectToReading(item.id)
                });
                var blogEntryImg = $("<img>").attr("src", item.image).addClass("img-fluid");
                var blogEntryExcerpt = $("<div>").addClass("excerpt");
                var blogEntryTitle = $("<h2>").append($("<a>").text(item.title)).click(function () {
                    rediectToReading(item.id)
                });
                var blogEntryMeta = $("<div>").addClass("post-meta align-items-center text-left clearfix");
                var authorFigure = $("<figure>").addClass("author-figure mb-0 me-3 float-start");
                var authorImg = $("<img>").attr("src", item.imageUser).attr("alt", "Avatar").addClass("img-fluid");
                var authorNameLink = $("<a>").text(item.authorName).click(function () {
                    rediectToReading(item.id)
                });
                var blogEntryDate = $("<span>").text(" - " + item.createdDate);
                var blogEntryDescription = $("<p>").text(item.description);
                var blogEntryReadMore = $("<p>").append($("<a>").addClass("read-more").text("Reading")).click(function () {
                    rediectToReading(item.id)
                });

                // Append the elements to form the blog entry
                authorFigure.append(authorImg);
                blogEntryImgLink.append(blogEntryImg);
                blogEntryContent.append(blogEntryImgLink, blogEntryExcerpt);
                blogEntryExcerpt.append(blogEntryTitle, blogEntryMeta, blogEntryDescription, blogEntryReadMore);
                blogEntryMeta.append(authorFigure, $("<span>").addClass("d-inline-block mt-1").append(authorNameLink), blogEntryDate);

                // Append the blog entry column to the blog entries row
                blogEntryColumn.append(blogEntryContent);
                blogEntriesRow.append(blogEntryColumn);
            });
            // Append the blog entries row to the container
            container.append(blogEntriesRow);

            // Append the container to the section
            section.append(container);
        },
        error: function (xhr, status, error) {
            // Handle error here
            console.log("Error:", error);
        }
    });
    
    $("#container_post").append(section);
}

function generateSectionTravel(category) {
    
    // Create section element with class "section" and "bg-light"
    var section = $("<div>").addClass("section bg-light");

    // Create container div with class "container"
    var container = $("<div>").addClass("container");

    var headerRow = generateHeaderRow(category.name, category.id);

    // Append the header row to the container
    container.append(headerRow);

    // Create div row for the blog entries with class "row align-items-stretch retro-layout-alt"
    var blogEntriesRow = $("<div>").addClass("row align-items-stretch retro-layout-alt");

    // Array of blog entry data
    var blogEntries = [];
    var blogEntriesTwo = [];
    $.ajax({
        url: "http://localhost:5243/api/Post/category",
        method: "POST",
        dataType: "json",
        contentType: 'application/json',
        data: JSON.stringify({
            categoryId: category.id,
            pageSize: 4,
            pageNumber: 1
        }),
        success: function (data) {
            // Handle the response data here
            data.values.forEach(function (item, index) {
                if (index < 1) {
                    blogEntries.push(item);
                } else {
                    blogEntriesTwo.push(item);
                }
            });
            var row7 = generateBlogEntries(blogEntriesTwo);

            // Loop through the blogEntries array to create blog entries
            var blogEntryData = blogEntries[0];

            var blogEntryColumn = $("<div>").addClass("col-md-5 order-md-2");
            // Create the blog entry link with class "hentry", "img-1" or "img-2" depending on the layout,
            // "h-100" for height (only for the first entry), and "gradient" for styling
            var blogEntryLink = $("<a>").addClass("hentry img-1 h-100 gradient").click(function () {
                rediectToReading(blogEntryData.id)
            });

            // Create the blog entry image with class "featured-img" and style for the background image
            var blogEntryImg = $("<div>").addClass("featured-img").css("background-image", "url('" + blogEntryData.image + "')");

            // Create the blog entry text div
            var blogEntryText = $("<div>").addClass("text text-sm");

            // Create the date span element with the date and class "text"
            var blogEntryDate = $("<span>").text(new Date(blogEntryData.createdDate).toDateString());

            // Create the h2 element for the blog entry title with class "text" and the title text
            var blogEntryTitle = $("<h2>").text(blogEntryData.title);

            // Append the date and title elements to the blog entry text div
            blogEntryText.append(blogEntryDate, blogEntryTitle);

            // Append the image and text to the blog entry link
            blogEntryLink.append(blogEntryImg, blogEntryText);

            // Append the blog entry link to the blog entry column
            blogEntryColumn.append(blogEntryLink);
            blogEntriesRow.append(blogEntryColumn);
            blogEntriesRow.append(row7);
            // Append the blog entries row to the container
            container.append(blogEntriesRow);
            // Append the container to the section
            section.append(container);
        },
        error: function (xhr, status, error) {
            // Handle error here
            console.log("Error:", error);
        }
    });
    $("#container_post").append(section);
}
function generateBlogEntries(listFirstEntry) {
    var entryFirst = listFirstEntry[0];
    var entry = [];
    entry.push(listFirstEntry[1]);
    entry.push(listFirstEntry[2]);
    // Create a div element with class "col-md-7"
    var colMd7Div = $("<div>").addClass("col-md-7");

    // Create the first blog entry link with class "hentry img-2 v-height mb30 gradient" and href "single.html"
    var firstBlogEntryLink = $("<a>").addClass("hentry img-2 v-height mb30 gradient").click(function () {
        rediectToReading(entryFirst.id)
    });

    // Create the first blog entry image with class "featured-img" and style for the background image
    var firstBlogEntryImg = $("<div>").addClass("featured-img").css("background-image", "url('" + entryFirst.image + "')");

    // Create the first blog entry text div with class "text text-sm"
    var firstBlogEntryText = $("<div>").addClass("text text-sm");

    // Create the date span element for the first blog entry with class "text" and text "February 12, 2019"
    var firstBlogEntryDate = $("<span>").text(new Date(entryFirst.createdDate).toDateString());

    // Create the h2 element for the title of the first blog entry with class "text" and text "AI can now kill those annoying cookie pop-ups"
    var firstBlogEntryTitle = $("<h2>").text(entryFirst.title);

    // Append the date and title elements to the first blog entry text div
    firstBlogEntryText.append(firstBlogEntryDate, firstBlogEntryTitle);

    // Append the image and text div to the first blog entry link
    firstBlogEntryLink.append(firstBlogEntryImg, firstBlogEntryText);

    // Create the div for the two-column blog entries with class "two-col d-block d-md-flex justify-content-between"
    var twoColDiv = $("<div>").addClass("two-col d-block d-md-flex justify-content-between");

    // Loop through the blogEntriesTwoCol array to create the two-column blog entries
    for (var i = 0; i < entry.length; i++) {
        var blogEntryData = entry[i];

        // Create the blog entry link with class "hentry v-height img-2 gradient" and href "single.html"
        var blogEntryLink = $("<a>").addClass("hentry v-height img-2 gradient").click(function () {
            rediectToReading(blogEntryData.id)
        });

        // Create the blog entry image with class "featured-img" and style for the background image
        var blogEntryImg = $("<div>").addClass("featured-img").css("background-image", "url('" + blogEntryData.image + "')");

        // Create the blog entry text div with class "text text-sm"
        var blogEntryText = $("<div>").addClass("text text-sm");

        // Create the date span element for the blog entry with class "text" and the date text
        var blogEntryDate = $("<span>").text(new Date(blogEntryData.createdDate).toDateString());

        // Create the h2 element for the title of the blog entry with class "text" and the title text
        var blogEntryTitle = $("<h2>").text(blogEntryData.title);

        // Append the date and title elements to the blog entry text div
        blogEntryText.append(blogEntryDate, blogEntryTitle);

        // Append the image and text div to the blog entry link
        blogEntryLink.append(blogEntryImg, blogEntryText);

        // Append the blog entry link to the two-column div
        twoColDiv.append(blogEntryLink);
    }

    // Append the first blog entry link and the two-column div to the col-md-7 div
    colMd7Div.append(firstBlogEntryLink, twoColDiv);

    return colMd7Div;
}
