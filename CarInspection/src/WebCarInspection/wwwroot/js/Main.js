$(".form_datetime").datetimepicker({
    format: "yyyy-MM-dd hh:ii",
    autoclose: true,
    todayBtn: true,
    minuteStep: 5,
});

document.addEventListener("click", (event) => {
    let dataContentAttr = event.target.getAttribute("data-content");
    let dataDictionary = {};
    let dataValuAttr = event.target.getAttribute("data-value");

    switch (dataContentAttr) {
        case "create-driver":
            ajaxQuery("GET", "/Driver/CreateDriver");
            break;
    }
});

function ajaxQuery(ajaxType, ajaxUrl, loadElement, dataDictionary, resultId) {
    $.ajax({
        type: ajaxType,
        url: ajaxUrl,
        beforeSend: () => {
            $(loadElement).show();
        },
        complete: () => {
            $(loadElement).hide();
        },
        onBegin: "ajaxBegin",
        data: dataDictionary,
        success: (data) => {
            $(resultId).html(data);
        }
    });
}
