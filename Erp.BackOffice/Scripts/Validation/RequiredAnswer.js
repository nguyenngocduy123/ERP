

var CheckAnswer = function () {
    var CheckAnswer_type = $('#Type').val();

    var Count = 0;
    var correct = 0;
    for (var i = 0; i <= $("#answerCollection").children().length; i++) {
        if ($("ul#answerCollection li:nth-child(" + i + ") [name=AnswerTitles]").val() != "" && $("ul#answerCollection li:nth-child(" + i + ") [name=AnswerTitles]").val() != undefined) {
            Count++;
        }
        if ($("ul#answerCollection li:nth-child(" + (i + 1) + ") [name=AnswerCorrects]:checked").length == 1) {
            if ($("ul#answerCollection li:nth-child(" + (i + 1) + ") [name=AnswerTitles]").val() != "") {
                correct++;
            }
        }
    }

    var quizCount = 0;
    for (var i = 0; i <= $("#quizAnswerCollection").children().length; i++) {
        if ($("ul#quizAnswerCollection li:nth-child(" + i + ") [name=QuizAnswerTitles]").val() != "" && $("ul#quizAnswerCollection li:nth-child(" + i + ") [name=QuizAnswerTitles]").val() != undefined) {
            quizCount ++;
        }

    }
    if (CheckAnswer_type != 3) {
        if (Count <= 1) {
            return false;
        }
        if (correct<1) {
            return false;
        }
    }
    else {
        if (quizCount <= 1) {
            return false;
        }
    }
    return true;
}

jQuery.validator.addMethod('requiredanswer', function (value, element, params) {
    return CheckAnswer();
}, '');

jQuery.validator.unobtrusive.adapters.add('requiredanswer', {}, function (options) {
    options.rules['requiredanswer'] = true;
    options.messages['requiredanswer'] = options.message;
});