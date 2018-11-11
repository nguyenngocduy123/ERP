        
var CheckUrl = function () {
            var CheckUrl_type = $('#Type').val();
            var CheckUrl_Ischecked = $('#IsUrl').is(':checked');
            var CheckUrl_Url = $('#Url').val();
            if (CheckUrl_type == 1 || CheckUrl_type == 2) {
                if (CheckUrl_Ischecked) {
                    if (CheckUrl_Url == "") {
                        return false;
                    }
                }
            }
            return true;
        }


        jQuery.validator.addMethod('requiredurlwithtypeandischecked', function (value, element, params) {
            return CheckUrl();
        }, '');

        jQuery.validator.unobtrusive.adapters.add('requiredurlwithtypeandischecked', {}, function (options) {
        options.rules['requiredurlwithtypeandischecked'] = true;
        options.messages['requiredurlwithtypeandischecked'] = options.message;
});