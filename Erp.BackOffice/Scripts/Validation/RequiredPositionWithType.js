

var CheckRequired = function () {

    var CheckRequired_type = $('#Type').val();

    var CheckRequired_position = $('#PositionId').val();

    if (CheckRequired_type == 2) {
        if (CheckRequired_position == "" || CheckRequired_position == null) {
            return false;
        }
    }
    return true;
}

jQuery.validator.addMethod('requiredpositionwithtype', function (value, element, params) {
    return CheckRequired();
}, '');

jQuery.validator.unobtrusive.adapters.add('requiredpositionwithtype', {}, function (options) {
    options.rules['requiredpositionwithtype'] = true;
    options.messages['requiredpositionwithtype'] = options.message;
});