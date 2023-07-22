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