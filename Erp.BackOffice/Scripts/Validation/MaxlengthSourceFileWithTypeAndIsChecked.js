
var SourceSize = 0;
$('#SourceFile').change(function () {
    SourceSize = this.files[0].size;
});

var CheckSourceFile_Maxlength = function () {
    var CheckSourceFile_type = $('#Type').val();
    var CheckSourceFile_Ischecked = $('#IsUrl').is(':checked');
    var CheckSourceFile_SourceFile = $('#SourceFile').val();
    if (CheckSourceFile_type == 1 || CheckUrl_type == 2) {
        if (!CheckSourceFile_Ischecked) {
            if (CheckSourceFile_SourceFile != "" || CheckSourceFile_SourceFile != null) {
                var maxLength = 52428800;
                if (SourceSize.valueOf() > maxLength) {
                    return false;
                }
                
            }
        }
    }
    return true;
}

jQuery.validator.addMethod('maxlengthsourcefilewithtypeandischecked', function (value, element, params) {
    return CheckSourceFile_Maxlength();
}, '');

jQuery.validator.unobtrusive.adapters.add('maxlengthsourcefilewithtypeandischecked', {}, function (options) {
    options.rules['maxlengthsourcefilewithtypeandischecked'] = true;
    options.messages['maxlengthsourcefilewithtypeandischecked'] = options.message;
});