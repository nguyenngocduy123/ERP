

var CheckAnswerCorrect = function () {

    var CheckAnswer_type = $('#Type').val();

    var Count = 0;
    for (var i = 1; i <= $("#answerCollection").children().length; i++) {
        if ($("ul#answerCollection li:nth-child(" + i + ") [name=AnswerCorrects]:checked").length == 1) {
            Count ++;
        }
    }

    if (CheckAnswer_type != 3) {
        if (Count < 1) {
            return false;
        }
    }
    return true;
}

jQuery.validator.addMethod('requiredanswercorrect', function (value, element, params) {
    return CheckAnswerCorrect();
}, '');

jQuery.validator.unobtrusive.adapters.add('requiredanswercorrect', {}, function (options) {
    options.rules['requiredanswercorrect'] = true;
    options.messages['requiredanswercorrect'] = options.message;
});