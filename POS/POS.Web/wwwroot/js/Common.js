//Call from Item, Member Discount
function DiscTypeOnChange(selectElement) {

    const selectedValue = selectElement.value;    

    $("#DiscAmt").val("0");
    $("#DiscPer").val("0");
    $("#txtOffPrice").val("0");

    $("#txtFrom_Date").prop('readonly', false);
    $("#txtTo_Date").prop('readonly', false);

    if (selectedValue === "A") {
        $("#DiscAmt").prop('readonly', false);
        $("#DiscPer").prop('readonly', true);
    } else if (selectedValue === "P") {
        $("#DiscAmt").prop('readonly', true);
        $("#DiscPer").prop('readonly', false);
    } else {
        $("#DiscAmt").prop('readonly', true);
        $("#DiscPer").prop('readonly', true);
        $("#txtFrom_Date").prop('readonly', true);
        $("#txtTo_Date").prop('readonly', true);

        $("#txtFrom_Date").val("0");
        $("#txtTo_Date").val("0");
    }    
}

//Call from Item, Member Discount
function calculateOfferPrice() {

    var ORIGIN_PRICE = 0;
    if (document.querySelector("#sellPrice"))
    {
        ORIGIN_PRICE = $("#sellPrice").val();
    } else {
        ORIGIN_PRICE = $("#txtOrgPrice").val();
    }
    
    var Disc_Type = $("#DiscType").val();
    var Disc_Amount = $("#DiscAmt").val();
    var Disc_Percent = $("#DiscPer").val();
    var OFF_PRICE = 0;

    if (ORIGIN_PRICE > 0) {
        if (Disc_Type === "A") {
            OFF_PRICE = ORIGIN_PRICE - Disc_Amount;
        } else if (Disc_Type === "P") {
            OFF_PRICE = ORIGIN_PRICE - (ORIGIN_PRICE * (Disc_Percent * 0.01));
        }
    }

    $("#txtOffPrice").val(OFF_PRICE);
};

//Stock Group and stock Item Partial View Action
//Call from Stock Income, Member Discount
function ItemPartialLoad(selectedstkGrp, stkItm) {
    const apiUrl = $('#configGrp').data('api-url');

    $.ajax({
        url: apiUrl, //'@Url.Action("FilterByStkGrp", "_Share")',
        type: 'GET',
        data: { stkGrp: selectedstkGrp },
        success: function (result) {
            $('#ItemList').html(result);
            if (stkItm !== null && stkItm !== "") $("#stkItemId").val(stkItm);
        },
        error: function () {
            alert("Failed to load Stock Item.");
        }
    });
};

$(document).on('change', '#stkgrpId', function () {
    var selectedstkGrp = $(this).val();
    ItemPartialLoad(selectedstkGrp, "");

    //Member Discount
    if (document.querySelector("#txtOrgPrice") && document.querySelector("#txtOffPrice")) {
        $("#txtOrgPrice").val("0"); //clear Selling Price
        $("#txtOffPrice").val("0"); //clear Offer Price
    } 

});

$(document).on('change', '#stkItemId', function () {

    //Member Discount
    if (document.querySelector("#txtOffPrice")) $("#txtOffPrice").val("0");

    var selecteditem = $(this).val();
    const apiUrl = $('#configItem').data('api-url');

    if (selecteditem != "x") {
        $.ajax({
            url: apiUrl, //'@Url.Action("FilterByStkItem", "_Share")',
            type: 'GET',
            data: { stkItem: selecteditem },
            success: function (result) {
                if (result != null) {
                    $("#stkgrpId").val(result.stkGrpId);

                    //Member Discount
                    if (document.querySelector("#txtOrgPrice")) {
                        $("#txtOrgPrice").val(result.price);
                        calculateOfferPrice();
                    }
                }
            },
            error: function () {
                alert("Failed to load Stock Group.");
            }
        });
    }
});

