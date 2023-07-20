function getNewPost() {
    $.ajax({
        url: 'http://localhost:5243/api/Post/newPost',
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
function getPostByCategory() {

}
function createArticleElemt(item, index) {
    var link = $("<a>").attr("href", item.link).addClass("h-entry v-height gradient").click(function () {
        viewDetail(item.id)
    });
    if (index == 0 | index == 3) {
        link = $("<a>").attr("href", item.link).addClass("h-entry v-height gradient mb-30").click(function () {
            viewDetail(item.id)
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
        viewDetail(item.id)
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
function viewDetail(id) {
    console.log(id)
    alert("chua co trang detail")
}

function generatePostsEntrySection() {
    var section = $("<section>").addClass("section posts-entry");

    // Create container div with class "container"
    var container = $("<div>").addClass("container");

    var headerRow = generateHeaderRow("BUSSINESS");
    var blogEntriesRow = $("<div>").addClass("row g-3");
    var clogEntriesRow9 = generatePostInEntryRow9("");
    var sidebarColumn = generatePostInEntrySider();
    blogEntriesRow.append(clogEntriesRow9, sidebarColumn);
    // Append the header row, blog entries row, and sidebar column to the container
    container.append(headerRow, blogEntriesRow);

    // Append the container to the section
    section.append(container);
    return section;
}
function generateHeaderRow(header) {

    // Create div row for the header with class "row mb-4"
    var headerRow = $("<div>").addClass("row mb-4");

    // Create div for the title column with class "col-sm-6"
    var titleColumn = $("<div>").addClass("col-sm-6");

    // Create h2 element for the title with class "posts-entry-title"
    var titleH2 = $("<h2>").addClass("posts-entry-title").text(header);

    // Create div for the "View All" link column with class "col-sm-6 text-sm-end"
    var viewAllColumn = $("<div>").addClass("col-sm-6 text-sm-end");

    // Create "View All" link with class "read-more" and href "category.html"
    var viewAllLink = $("<a>").attr("href", "category.html").addClass("read-more").text("View All");

    // Append the title and "View All" link to their respective columns
    titleColumn.append(titleH2);
    viewAllColumn.append(viewAllLink);

    // Append the title column and "View All" column to the header row
    headerRow.append(titleColumn, viewAllColumn);
    return headerRow;
}
function generatePostInEntryRow9(order) {
    var clogEntriesRow9 = $("<div>").addClass("col-md-9 " + order);
    var blogEntriesRowContent = $("<div>").addClass("row g-3");
    // Create the first blog entry column
    var blogEntry1Column = $("<div>").addClass("col-md-6");

    // Create the first blog entry content
    var blogEntry1Content = $("<div>").addClass("blog-entry");
    var blogEntry1ImgLink = $("<a>").attr("href", "single.html").addClass("img-link");
    var blogEntry1Img = $("<img>").attr("src", "images/img_1_sq.jpg").addClass("img-fluid");
    var blogEntry1Date = $("<span>").addClass("date").text("Apr. 14th, 2022");
    var blogEntry1Title = $("<h2>").append($("<a>").attr("href", "single.html").text("Thought you loved Python? Wait until you meet Rust"));
    var blogEntry1Description = $("<p>").text("Lorem ipsum dolor sit amet consectetur adipisicing elit. Unde, nobis ea quis inventore vel voluptas.");
    var blogEntry1ReadMore = $("<p>").append($("<a>").attr("href", "single.html").addClass("btn btn-sm btn-outline-primary").text("Read More"));

    // Append the elements to form the first blog entry
    blogEntry1ImgLink.append(blogEntry1Img);
    blogEntry1Content.append(blogEntry1ImgLink, blogEntry1Date, blogEntry1Title, blogEntry1Description, blogEntry1ReadMore);
    blogEntry1Column.append(blogEntry1Content);

    // Create the second blog entry column (similar to the first one)
    var blogEntry2Column = $("<div>").addClass("col-md-6");

    // Create the second blog entry content
    var blogEntry2Content = $("<div>").addClass("blog-entry");
    var blogEntry2ImgLink = $("<a>").attr("href", "single.html").addClass("img-link");
    var blogEntry2Img = $("<img>").attr("src", "images/img_2_sq.jpg").addClass("img-fluid");
    var blogEntry2Date = $("<span>").addClass("date").text("Apr. 14th, 2022");
    var blogEntry2Title = $("<h2>").append($("<a>").attr("href", "single.html").text("Startup vs corporate: What job suits you best?"));
    var blogEntry2Description = $("<p>").text("Lorem ipsum dolor sit amet consectetur adipisicing elit. Unde, nobis ea quis inventore vel voluptas.");
    var blogEntry2ReadMore = $("<p>").append($("<a>").attr("href", "single.html").addClass("btn btn-sm btn-outline-primary").text("Read More"));

    // Append the elements to form the second blog entry
    blogEntry2ImgLink.append(blogEntry2Img);
    blogEntry2Content.append(blogEntry2ImgLink, blogEntry2Date, blogEntry2Title, blogEntry2Description, blogEntry2ReadMore);
    blogEntry2Column.append(blogEntry2Content);

    // Append the blog entry columns to the blog entries row
    blogEntriesRowContent.append(blogEntry1Column, blogEntry2Column);
    clogEntriesRow9.append(blogEntriesRowContent);
    return clogEntriesRow9;
}
function generatePostInEntrySider() {
    // Create the sidebar column with class "col-md-3"
    var sidebarColumn = $("<div>").addClass("col-md-3");

    // Create the ul list for the blog entries on the sidebar with class "list-unstyled blog-entry-sm"
    var sidebarList = $("<ul>").addClass("list-unstyled blog-entry-sm");

    // Create the li items for the blog entries on the sidebar
    var sidebarLi1 = $("<li>");
    var sidebarLi2 = $("<li>");
    var sidebarLi3 = $("<li>");

    // Create the content for the sidebar li items
    var sidebarDate1 = $("<span>").addClass("date").text("Apr. 14th, 2022");
    var sidebarTitle1 = $("<h3>").append($("<a>").attr("href", "single.html").text("Don’t assume your user data in the cloud is safe"));
    var sidebarDescription1 = $("<p>").text("Lorem ipsum dolor sit amet consectetur adipisicing elit. Unde, nobis ea quis inventore vel voluptas.");
    var sidebarReadMore1 = $("<p>").append($("<a>").attr("href", "#").addClass("read-more").text("Continue Reading"));

    var sidebarDate2 = $("<span>").addClass("date").text("Apr. 14th, 2022");
    var sidebarTitle2 = $("<h3>").append($("<a>").attr("href", "single.html").text("Meta unveils fees on metaverse sales"));
    var sidebarDescription2 = $("<p>").text("Lorem ipsum dolor sit amet consectetur adipisicing elit. Unde, nobis ea quis inventore vel voluptas.");
    var sidebarReadMore2 = $("<p>").append($("<a>").attr("href", "#").addClass("read-more").text("Continue Reading"));

    var sidebarDate3 = $("<span>").addClass("date").text("Apr. 14th, 2022");
    var sidebarTitle3 = $("<h3>").append($("<a>").attr("href", "single.html").text("UK sees highest inflation in 30 years"));
    var sidebarDescription3 = $("<p>").text("Lorem ipsum dolor sit amet consectetur adipisicing elit. Unde, nobis ea quis inventore vel voluptas.");
    var sidebarReadMore3 = $("<p>").append($("<a>").attr("href", "#").addClass("read-more").text("Continue Reading"));

    // Append the content to the sidebar li items
    sidebarLi1.append(sidebarDate1, sidebarTitle1, sidebarDescription1, sidebarReadMore1);
    sidebarLi2.append(sidebarDate2, sidebarTitle2, sidebarDescription2, sidebarReadMore2);
    sidebarLi3.append(sidebarDate3, sidebarTitle3, sidebarDescription3, sidebarReadMore3);

    // Append the sidebar li items to the sidebar ul list
    sidebarList.append(sidebarLi1, sidebarLi2, sidebarLi3);

    // Append the sidebar ul list to the sidebar column
    sidebarColumn.append(sidebarList);
    return sidebarColumn;
}

function generatePostsEntrySectionSmall() {
    // Create section element with classes "section posts-entry posts-entry-sm bg-light"
    var section = $("<section>").addClass("section posts-entry posts-entry-sm bg-light");

    // Create container div with class "container"
    var container = $("<div>").addClass("container");

    // Create div row for the blog entries
    var blogEntriesRow = $("<div>").addClass("row");

    // Array of blog entry data
    var blogEntries = [
        {
            imgSrc: "images/img_1_horizontal.jpg",
            date: "Apr. 14th, 2022",
            title: "Thought you loved Python? Wait until you meet Rust",
            description: "Lorem ipsum dolor sit amet consectetur adipisicing elit."
        },
        {
            imgSrc: "images/img_2_horizontal.jpg",
            date: "Apr. 14th, 2022",
            title: "Startup vs corporate: What job suits you best?",
            description: "Lorem ipsum dolor sit amet consectetur adipisicing elit."
        },
        {
            imgSrc: "images/img_3_horizontal.jpg",
            date: "Apr. 14th, 2022",
            title: "UK sees highest inflation in 30 years",
            description: "Lorem ipsum dolor sit amet consectetur adipisicing elit."
        },
        {
            imgSrc: "images/img_4_horizontal.jpg",
            date: "Apr. 14th, 2022",
            title: "Don’t assume your user data in the cloud is safe",
            description: "Lorem ipsum dolor sit amet consectetur adipisicing elit."
        }
    ];

    // Loop through the blogEntries array to create blog entry columns
    for (var i = 0; i < blogEntries.length; i++) {
        var blogEntryData = blogEntries[i];

        // Create the blog entry column with classes "col-md-6 col-lg-3"
        var blogEntryColumn = $("<div>").addClass("col-md-6 col-lg-3");

        // Create the blog entry content
        var blogEntryContent = $("<div>").addClass("blog-entry");
        var blogEntryImgLink = $("<a>").attr("href", "single.html").addClass("img-link");
        var blogEntryImg = $("<img>").attr("src", blogEntryData.imgSrc).addClass("img-fluid");
        var blogEntryDate = $("<span>").addClass("date").text(blogEntryData.date);
        var blogEntryTitle = $("<h2>").append($("<a>").attr("href", "single.html").text(blogEntryData.title));
        var blogEntryDescription = $("<p>").text(blogEntryData.description);
        var blogEntryReadMore = $("<p>").append($("<a>").attr("href", "#").addClass("read-more").text("Continue Reading"));

        // Append the elements to form the blog entry
        blogEntryImgLink.append(blogEntryImg);
        blogEntryContent.append(blogEntryImgLink, blogEntryDate, blogEntryTitle, blogEntryDescription, blogEntryReadMore);
        blogEntryColumn.append(blogEntryContent);

        // Append the blog entry column to the blog entries row
        blogEntriesRow.append(blogEntryColumn);
    }

    // Append the blog entries row to the container
    container.append(blogEntriesRow);

    // Append the container to the section
    section.append(container);

    return section;
}

function generatePostsEntrySectionSecond() {
    var section = $("<section>").addClass("section posts-entry");

    // Create container div with class "container"
    var container = $("<div>").addClass("container");

    var headerRow = generateHeaderRow("CULTURE");
    var blogEntriesRow = $("<div>").addClass("row g-3");
    var clogEntriesRow9 = generatePostInEntryRow9("order-md-2");
    var sidebarColumn = generatePostInEntrySider();
    blogEntriesRow.append(clogEntriesRow9, sidebarColumn);
    // Append the header row, blog entries row, and sidebar column to the container
    container.append(headerRow, blogEntriesRow);

    // Append the container to the section
    section.append(container);
    return section;
}

function generateSectionPostsEntryList() {
    // Create section element with class "section"
    var section = $("<section>").addClass("section");

    // Create container div with class "container"
    var container = $("<div>").addClass("container");



    // Append the title column and "View All" column to the header row
    var headerRow = generateHeaderRow("POLITICS")

    // Append the header row to the container
    container.append(headerRow);

    // Create div row for the blog entries with class "row"
    var blogEntriesRow = $("<div>").addClass("row");

    // Array of blog entry data
    var blogEntries = [
        {
            imgSrc: "images/img_7_horizontal.jpg",
            authorImgSrc: "images/person_1.jpg",
            authorName: "David Anderson",
            date: "July 19, 2019",
            title: "Startup vs corporate: What job suits you best?",
            description: "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Quo sunt tempora dolor laudantium sed optio, explicabo ad deleniti impedit facilis fugit recusandae! Illo, aliquid, dicta beatae quia porro id est."
        },
        {
            imgSrc: "images/img_7_horizontal.jpg",
            authorImgSrc: "images/person_1.jpg",
            authorName: "David Anderson",
            date: "July 19, 2019",
            title: "Startup vs corporate: What job suits you best?",
            description: "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Quo sunt tempora dolor laudantium sed optio, explicabo ad deleniti impedit facilis fugit recusandae! Illo, aliquid, dicta beatae quia porro id est."
        },
        {
            imgSrc: "images/img_7_horizontal.jpg",
            authorImgSrc: "images/person_1.jpg",
            authorName: "David Anderson",
            date: "July 19, 2019",
            title: "Startup vs corporate: What job suits you best?",
            description: "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Quo sunt tempora dolor laudantium sed optio, explicabo ad deleniti impedit facilis fugit recusandae! Illo, aliquid, dicta beatae quia porro id est."
        },
        {
            imgSrc: "images/img_7_horizontal.jpg",
            authorImgSrc: "images/person_1.jpg",
            authorName: "David Anderson",
            date: "July 19, 2019",
            title: "Startup vs corporate: What job suits you best?",
            description: "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Quo sunt tempora dolor laudantium sed optio, explicabo ad deleniti impedit facilis fugit recusandae! Illo, aliquid, dicta beatae quia porro id est."
        },
        // Add more blog entries as needed...
    ];

    // Loop through the blogEntries array to create blog entry columns
    for (var i = 0; i < blogEntries.length; i++) {
        var blogEntryData = blogEntries[i];

        // Create the blog entry column with class "col-lg-4 mb-4"
        var blogEntryColumn = $("<div>").addClass("col-lg-4 mb-4");

        // Create the blog entry content
        var blogEntryContent = $("<div>").addClass("post-entry-alt");
        var blogEntryImgLink = $("<a>").attr("href", "single.html").addClass("img-link");
        var blogEntryImg = $("<img>").attr("src", blogEntryData.imgSrc).addClass("img-fluid");
        var blogEntryExcerpt = $("<div>").addClass("excerpt");
        var blogEntryTitle = $("<h2>").append($("<a>").attr("href", "single.html").text(blogEntryData.title));
        var blogEntryMeta = $("<div>").addClass("post-meta align-items-center text-left clearfix");
        var authorFigure = $("<figure>").addClass("author-figure mb-0 me-3 float-start");
        var authorImg = $("<img>").attr("src", blogEntryData.authorImgSrc).addClass("img-fluid");
        var authorNameLink = $("<a>").attr("href", "#").text(blogEntryData.authorName);
        var blogEntryDate = $("<span>").text("&nbsp;-&nbsp; " + blogEntryData.date);
        var blogEntryDescription = $("<p>").text(blogEntryData.description);
        var blogEntryReadMore = $("<p>").append($("<a>").attr("href", "#").addClass("read-more").text("Continue Reading"));

        // Append the elements to form the blog entry
        authorFigure.append(authorImg);
        blogEntryImgLink.append(blogEntryImg);
        blogEntryContent.append(blogEntryImgLink, blogEntryExcerpt);
        blogEntryExcerpt.append(blogEntryTitle, blogEntryMeta, blogEntryDescription, blogEntryReadMore);
        blogEntryMeta.append(authorFigure, $("<span>").addClass("d-inline-block mt-1").append(authorNameLink), blogEntryDate);

        // Append the blog entry column to the blog entries row
        blogEntryColumn.append(blogEntryContent);
        blogEntriesRow.append(blogEntryColumn);
    }

    // Append the blog entries row to the container
    container.append(blogEntriesRow);

    // Append the container to the section
    section.append(container);

    return section;
}

function generateSectionTravel() {
    // Create section element with class "section" and "bg-light"
    var section = $("<div>").addClass("section bg-light");

    // Create container div with class "container"
    var container = $("<div>").addClass("container");

    var headerRow = generateHeaderRow("Travel")

    // Append the header row to the container
    container.append(headerRow);

    // Create div row for the blog entries with class "row align-items-stretch retro-layout-alt"
    var blogEntriesRow = $("<div>").addClass("row align-items-stretch retro-layout-alt");

    // Array of blog entry data
    var blogEntries = [
        {
            imgSrc: "images/img_2_vertical.jpg",
            date: "February 12, 2019",
            title: "Meta unveils fees on metaverse sales"
        },
        {
            imgSrc: "images/img_1_horizontal.jpg",
            date: "February 12, 2019",
            title: "AI can now kill those annoying cookie pop-ups"
        },
        {
            imgSrc: "images/img_2_sq.jpg",
            date: "February 12, 2019",
            title: "Don’t assume your user data in the cloud is safe"
        },
        {
            imgSrc: "images/img_3_sq.jpg",
            date: "February 12, 2019",
            title: "Startup vs corporate: What job suits you best?"
        }
        // Add more blog entries as needed...
    ];
    var row7 = generateBlogEntries();

    // Loop through the blogEntries array to create blog entries
    var blogEntryData = blogEntries[0];

    var blogEntryColumn = $("<div>").addClass("col-md-5 order-md-2");
    // Create the blog entry link with class "hentry", "img-1" or "img-2" depending on the layout,
    // "h-100" for height (only for the first entry), and "gradient" for styling
    var blogEntryLink = $("<a>").attr("href", "single.html").addClass("hentry img-1 h-100 gradient");

    // Create the blog entry image with class "featured-img" and style for the background image
    var blogEntryImg = $("<div>").addClass("featured-img").css("background-image", "url('images/img_1_horizontal.jpg')");

    // Create the blog entry text div
    var blogEntryText = $("<div>").addClass("text text-sm");

    // Create the date span element with the date and class "text"
    var blogEntryDate = $("<span>").text(blogEntryData.date);

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

    return section;
}
function generateBlogEntries() {
    // Create a div element with class "col-md-7"
    var colMd7Div = $("<div>").addClass("col-md-7");

    // Create the first blog entry link with class "hentry img-2 v-height mb30 gradient" and href "single.html"
    var firstBlogEntryLink = $("<a>").attr("href", "single.html").addClass("hentry img-2 v-height mb30 gradient");

    // Create the first blog entry image with class "featured-img" and style for the background image
    var firstBlogEntryImg = $("<div>").addClass("featured-img").css("background-image", "url('images/img_1_horizontal.jpg')");

    // Create the first blog entry text div with class "text text-sm"
    var firstBlogEntryText = $("<div>").addClass("text text-sm");

    // Create the date span element for the first blog entry with class "text" and text "February 12, 2019"
    var firstBlogEntryDate = $("<span>").text("February 12, 2019");

    // Create the h2 element for the title of the first blog entry with class "text" and text "AI can now kill those annoying cookie pop-ups"
    var firstBlogEntryTitle = $("<h2>").text("AI can now kill those annoying cookie pop-ups");

    // Append the date and title elements to the first blog entry text div
    firstBlogEntryText.append(firstBlogEntryDate, firstBlogEntryTitle);

    // Append the image and text div to the first blog entry link
    firstBlogEntryLink.append(firstBlogEntryImg, firstBlogEntryText);

    // Create the div for the two-column blog entries with class "two-col d-block d-md-flex justify-content-between"
    var twoColDiv = $("<div>").addClass("two-col d-block d-md-flex justify-content-between");

    // Array of blog entry data for the two-column blog entries
    var blogEntriesTwoCol = [
        {
            imgSrc: "images/img_2_sq.jpg",
            date: "February 12, 2019",
            title: "Don’t assume your user data in the cloud is safe"
        },
        {
            imgSrc: "images/img_3_sq.jpg",
            date: "February 12, 2019",
            title: "Startup vs corporate: What job suits you best?"
        }
        // Add more blog entries for the two-column layout as needed...
    ];

    // Loop through the blogEntriesTwoCol array to create the two-column blog entries
    for (var i = 0; i < blogEntriesTwoCol.length; i++) {
        var blogEntryData = blogEntriesTwoCol[i];

        // Create the blog entry link with class "hentry v-height img-2 gradient" and href "single.html"
        var blogEntryLink = $("<a>").attr("href", "single.html").addClass("hentry v-height img-2 gradient");

        // Create the blog entry image with class "featured-img" and style for the background image
        var blogEntryImg = $("<div>").addClass("featured-img").css("background-image", "url('" + blogEntryData.imgSrc + "')");

        // Create the blog entry text div with class "text text-sm"
        var blogEntryText = $("<div>").addClass("text text-sm");

        // Create the date span element for the blog entry with class "text" and the date text
        var blogEntryDate = $("<span>").text(blogEntryData.date);

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

function appendData() {
    var entry = generatePostsEntrySection();
    var entrySmall = generatePostsEntrySectionSmall();
    var entrySecond = generatePostsEntrySectionSecond();
    var entryThird = generateSectionPostsEntryList();
    var finalEntry = generateSectionTravel();
    $("#container_post").append(entry, entrySmall, entrySecond, entryThird, finalEntry);
}
