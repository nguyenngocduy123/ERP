//Fetch Department of University
var urlLabourContract = '/api/BackOfficeServiceAPI/FetchLabourContract';
var idCategory = $('#CategoryId'); // cache it
var urlContract = '/api/BackOfficeServiceAPI/FetchContract';
var urlCustomer = '/api/BackOfficeServiceAPI/FetchCustomer';
var urlNotifications = '/api/BackOfficeServiceAPI/FetchInternalNotifications';
$("#Category").change(function () {
    idCategory.empty(); // remove any existing options
    $(document.createElement('option'))
                .attr('value', '')
                .text('- Rỗng -')
                .appendTo(idCategory).trigger('chosen:updated');
    var id = $(this).val(); // Use $(this) so you don't traverse the DOM again
    if (id == "Contract") {
        $.getJSON(urlContract, function (response) {
            idCategory.empty(); // remove any existing options

            $(response).each(function () {
                $(document.createElement('option'))
                    .attr('value', this.Id)
                    .text(this.Code)
                    .appendTo(idCategory).trigger('chosen:updated');
            });
        });
    }
    else if (id == "LabourContract") {
        $.getJSON(urlLabourContract, function (response) {
            idCategory.empty(); // remove any existing options

            $(response).each(function () {
                $(document.createElement('option'))
                    .attr('value', this.Id)
                    .text(this.Name)
                    .appendTo(idCategory).trigger('chosen:updated');
            });
        });
    }
    else if (id == "InternalNotifications") {
        $.getJSON(urlNotifications, function (response) {
            idCategory.empty(); // remove any existing options

            $(response).each(function () {
                $(document.createElement('option'))
                    .attr('value', this.Id)
                    .text(this.Titles)
                    .appendTo(idCategory).trigger('chosen:updated');
            });
        });
    }
});