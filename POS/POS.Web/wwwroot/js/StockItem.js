function CheckItem() {
    var Code = $("#txtCode").val();
    var Grp = $("#stkgrpId").val();

    if ((Grp != "x") && (Code != "" && Code != null)) {
        var url = "/StockItem/CheckItem";

        var data = {
            stkGrp: Grp,
            stkItem: Code
        }

        $.get(url, data, function (response) {
            if (response != null) {
                alert("'" + Code + "' already exist under 'Stock Group - " + $('#stkgrpId option:selected').text() + "'.");
                $("#txtCode").val("");
                $('#txtCode').focus();
            }
            toogleEntryButton();
        });
    }
    else { toogleEntryButton(); }
};

function toogleEntryButton() {
    if ($('#txtCode').val().length != 0 && $('#stkgrpId').val() != 'x') {
        $('#btnEntry').attr('disabled', false);

        $('#imageUpload').attr('disabled', false);
        $('#customUploadBtn').attr('disabled', false);
        $('#customUploadBtn').removeClass('custom-disable');
    }
    else {
        $('#btnEntry').attr('disabled', true);

        $('#imageUpload').attr('disabled', true);
        $('#customUploadBtn').attr('disabled', true);
        $('#customUploadBtn').addClass('custom-disable');
    }
};
function imageUpload(originalFile) {

    const fileType = originalFile.type;
    const fileName = originalFile.name.toLowerCase();
    const fileExtension = fileName.substring(fileName.lastIndexOf('.'));

    const validMimeTypes = ["image/jpeg", "image/png", "image/gif", "image/webp"];
    const validExtensions = [".jpg", ".jpeg", ".png", ".gif", ".webp"];

    if (!validMimeTypes.includes(fileType) || !validExtensions.includes(fileExtension)) {
        alert('Invalid image type. Please select a valid image.');        
        return;
    }

    // Create a new file with a custom name (e.g., based on timestamp)
    const newFileName = $('#stkgrpId option:selected').text() + '-' + $('#txtCode').val() + originalFile.name.slice(originalFile.name.lastIndexOf('.'));
    const renamedFile = new File([originalFile], newFileName, { type: originalFile.type });

    $('#fileNameDisplay').text(newFileName);

    var formData = new FormData();
    formData.append('image', renamedFile); // Send renamed file

    $.ajax({
        url: '/StockItem/UploadImage', // Backend endpoint
        type: 'POST',
        data: formData,
        contentType: false,
        processData: false,
        success: function (response) {
            // Replace image with the newly uploaded one
            $('#uploadedImage').attr('src', response.imagePath + '?t=' + new Date().getTime());            
            $('#txtimgPath').val(response.imagePath);
        },
        error: function () {
            alert('Image upload failed.');
        }
    });
};
