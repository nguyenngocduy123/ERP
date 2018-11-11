
var CheckSourceFile_Required = function () {
    var CheckSourceFile_type = $('#Type').val();
    var CheckSourceFile_Ischecked = $('#IsUrl').is(':checked');
    var CheckSourceFile_SourceFile = $('#SourceFile').val();
    if (CheckSourceFile_type == 1 || CheckUrl_type == 2) {
        if (!CheckSourceFile_Ischecked) {
            if (CheckSourceFile_SourceFile == "" || CheckSourceFile_SourceFile == null) {
                return false;
            }
        }
    }
    return true;
}

jQuery.validator.addMethod('requiredsourcefilewithtypeandischecked', function (value, element, params) {
    return CheckSourceFile_Required();
}, '');

jQuery.validator.unobtrusive.adapters.add('requiredsourcefilewithtypeandischecked', {}, function (options) {
    options.rules['requiredsourcefilewithtypeandischecked'] = true;
    options.messages['requiredsourcefilewithtypeandischecked'] = options.message;
});