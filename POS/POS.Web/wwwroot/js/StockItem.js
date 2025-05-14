function EnableField(selectElement) {

    const selectedValue = selectElement.value;    

    $("#DiscAmt").val("");
    $("#DiscPer").val("");

    if (selectedValue === "A") {
        $("#DiscAmt").prop('disabled', false);
        $("#DiscPer").prop('disabled', true);
    } else if (selectedValue === "P") {
        $("#DiscAmt").prop('disabled', true);
        $("#DiscPer").prop('disabled', false);
    } else {
        $("#DiscAmt").prop('disabled', true);
        $("#DiscPer").prop('disabled', true);
    }

    //if ($('#radioAmt').is(':checked')) {
    //    $("#DiscAmt").prop('disabled', false); 
    //    $("#DiscPer").prop('disabled', true); 

    //} else {
    //    $("#DiscAmt").prop('disabled', true);
    //    $("#DiscPer").prop('disabled', false); 
    //}
}