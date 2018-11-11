var TrainingScheduleLessonDistribution = {
    init: function() {
        if ($("#CategoryId").val() != '' && $("#TrainingScheduleId").val() != '') {
            TrainingScheduleLessonDistribution.getTrainingScheduleLesson($("#CategoryId").val(), $("#TrainingScheduleId").val());
        }
        $("#CategoryId").change(function () {
            TrainingScheduleLessonDistribution.getTrainingSchedulesOfLesson($(this).val());
            TrainingScheduleLessonDistribution.clearForm();
        });
        $("#TrainingScheduleId").change(function () {
            if ($(this).val() != '') {
                TrainingScheduleLessonDistribution.getTrainingScheduleLesson($("#CategoryId").val(), $(this).val());
                TrainingScheduleLessonDistribution.clearForm();
            }
        });
        $("#LessonIds").change(function () {
            $('[data-name="trDistribution"]').hide();
            var lessonIds = $(this).val();
            if (lessonIds != null) {
                for (var i = 0; i < lessonIds.length; i++) {
                    $('#tr-' + lessonIds[i]).show();
                }
            }
        });
        $("#tbSaveDistribution").click(function () {
            TrainingScheduleLessonDistribution.saveTrainingScheduleLessonDistribution();
        });
    },
    clearForm: function() {
        for (var i = $('#LessonIds').children().length; i >= 1; i--) {
            $("select#LessonIds option:nth-child(" + i + ")").remove();
        }
        $(".chzn-select").trigger('liszt:updated');
        
        $("#dvTrainingScheduleDistribution").html('');
    },
    getTrainingSchedulesOfLesson: function (categoryId) {
        $.ajax({
            url: '/TrainingSchedule/TrainingScheduleDistribution/GetTrainingSchedulesOfLesson',
            //url: '/TrainingScheduleDistribution/GetTrainingSchedulesOfLesson',
            type: "POST",
            data: { categoryId: categoryId },
            success: function (data) {
                for (var i = $('#TrainingScheduleId').children().length; i > 1; i--) {
                    if (i != 1) {
                        $("select#TrainingScheduleId option:nth-child(" + i + ")").remove();
                    }
                }
                $('#TrainingScheduleId').html($('#TrainingScheduleId').html() + data);
                $(".chzn-select").trigger('liszt:updated');
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(thrownError);
            }
        });
    },
    getTrainingScheduleLesson: function (categoryId, trainingScheduleId) {
        $.ajax({
            url: '/TrainingSchedule/TrainingScheduleDistribution/GetTrainingScheduleLesson',
            //url: '/TrainingScheduleDistribution/GetTrainingScheduleLesson',
            type: "POST",
            data: { categoryId: categoryId, trainingScheduleId: trainingScheduleId },
            success: function (data) {
                for (var i = $('#LessonIds').children().length; i >= 1; i--) {
                    if (i != 1) {
                        $("select#LessonIds option:nth-child(" + i + ")").remove();
                    }
                }
                $('#LessonIds').html($('#LessonIds').html() + data);
                $(".chzn-select").trigger('liszt:updated');

                TrainingScheduleLessonDistribution.loadLessonDistributionDetail($('#LessonIds').val(), $('#TrainingScheduleId').val());
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(thrownError);
            }
        });
    },
    loadLessonDistributionDetail: function (lessonIds, trainingScheduleId) {
        $.ajax({
            url: '/TrainingSchedule/TrainingScheduleDistribution/LessonDistributionDetail',
            //url: '/TrainingScheduleDistribution/LessonDistributionDetail',
            type: "POST",
            data: { categoryId: $('#CategoryId').val(), trainingScheduleId: trainingScheduleId },
            success: function (data) {
                $("#dvTrainingScheduleDistribution").html(data);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(thrownError);
            }
        });
    },
    saveTrainingScheduleLessonDistribution: function() {
        if ($("#CategoryId").val() != '' && $("#TrainingScheduleId").val() != '') {
            var distribution = new Array();
            var i = 0;
            $("input:checked[name='chkDistribution']").each(function() {
                distribution[i++] = '{"PositionId":' + $(this).attr("data-position-id") + ',"LevelId":' + $(this).attr("data-level-id") + ',"LessonId":' + $(this).attr("data-lesson-id") + '}';
            });

            var json = '[' + distribution.join(",") + ']';
            var lessonIds = $("#LessonIds").val() == null ? '' : $("#LessonIds").val().toString();
            $.ajax({
                url: '/TrainingSchedule/TrainingScheduleDistribution/UpdateLessonDistribution',
                //url: '/TrainingScheduleDistribution/UpdateLessonDistribution',
                type: "POST",
                data: { jsonData: json, lessonIds: lessonIds, trainingScheduleId: $("#TrainingScheduleId").val() },
                success: function(data) {
                    if (data) {
                        alert("Thành công");
                    }
                },
                error: function(xhr, ajaxOptions, thrownError) {
                    alert(thrownError);
                }
            });
        }
    }
};

$(document).ready(function() {
    TrainingScheduleLessonDistribution.init();
});