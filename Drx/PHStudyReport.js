$(document).ready(function () {

    OrderID = $('#orderID').val();
    $("#tabs").tabs();
    $("#progressbar").progressbar({ value: 0 });
    setTimeout(updateProgress, 10);
    
});



function updateProgress() {
    var progress;
    progress = $("#progressbar")
      .progressbar("option","value");
    if (progress < 100) {
        $("#progressbar")
          .progressbar("option", "value", progress + 1);
        setTimeout(updateProgress, 10);
    }
}

