$(document).ready(function () {
    $("#category").change(function () {
        var selectedCategoryId = $(this).val();
        $.ajax({
            type: "GET",
            url: "http://localhost:5243/api/Category/subcategory/" + selectedCategoryId,
            success: function (data) {
                
                $("#subCategory").empty();
                $.each(data, function (index, item) {
                    $("#subCategory").append($('<option></option>').val(item.id).text(item.name));
                });
            },
            error: function (error) {
                console.log(error);
            }
        });
    });
});