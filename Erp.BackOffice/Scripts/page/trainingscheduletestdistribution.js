var TrainingScheduleTestDistribution = {
    
    init: function () {
        if ($("#CategoryId").val() != '' && $("#TrainingScheduleId").val() != '') {
            TrainingScheduleTestDistribution.getTrainingScheduleTest($("#CategoryId").val(), $("#TrainingScheduleId").val());
        }
        $("#CategoryId").change(function () {
            TrainingScheduleTestDistribution.getTrainingSchedulesOfTest($(this).val());
            TrainingScheduleTestDistribution.clearForm();
        });
        $("#TrainingScheduleId").change(function () {
            if ($(this).val() != '')
            {
            TrainingScheduleTestDistribution.getTrainingScheduleTest($("#CategoryId").val(), $(this).val());
            TrainingScheduleTestDistribution.clearForm();
            }
        });
        $("#TestIds").change(function () {
            $('[data-name="trDistribution"]').hide();
            var lessonIds = $(this).val();
            if (lessonIds != null) {
                for (var i = 0; i < lessonIds.length; i++) {
                    $('#tr-' + lessonIds[i]).show();
                }
            }
        });
        $("#tbSaveDistribution").click(function () {
            TrainingScheduleTestDistribution.saveTrainingScheduleTestDistribution();
        });
    },
    clearForm: function () {
        for (var i = $('#TestIds').children().length; i >= 1; i--) {
            $("select#TestIds option:nth-child(" + i + ")").remove();
        }
        $(".chzn-select").trigger('liszt:updated');

        $("#dvTrainingScheduleDistribution").html('');
    },
    getTrainingSchedulesOfTest: function (categoryId) {
        $.ajax({
            url: '/TrainingSchedule/TrainingScheduleDistribution/GetTrainingSchedulesOfTest',
            //url: '/TrainingScheduleDistribution/GetTrainingSchedulesOfTest',
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
    getTrainingScheduleTest: function (categoryId, trainingScheduleId) {
        $.ajax({
            url: '/TrainingSchedule/TrainingScheduleDistribution/GetTrainingScheduleTest',
            //url: '/TrainingScheduleDistribution/GetTrainingScheduleTest',
            type: "POST",
            data: { categoryId: categoryId, trainingScheduleId: trainingScheduleId },
            success: function (data) {
                for (var i = $('#TestIds').children().length; i >= 1; i--) {
                    if (i != 1) {
                        $("select#TestIds option:nth-child(" + i + ")").remove();
                    }
                }
                $('#TestIds').html($('#TestIds').html() + data);
                $(".chzn-select").trigger('liszt:updated');

                TrainingScheduleTestDistribution.loadTestDistributionDetail($('#TestIds').val(), $('#TrainingScheduleId').val());
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(thrownError);
            }
        });
    },
    loadTestDistributionDetail: function (lessonIds, trainingScheduleId) {
        $.ajax({
            url: '/TrainingSchedule/TrainingScheduleDistribution/TestDistributionDetail',
            //url: '/TrainingScheduleDistribution/TestDistributionDetail',
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
    saveTrainingScheduleTestDistribution: function () {
        if ($("#CategoryId").val() != '' && $("#TrainingScheduleId").val() != '') {
            var distribution = new Array();
            var i = 0;
            $("input:checked[name='chkDistribution']").each(function () {
                distribution[i++] = '{"PositionId":' + $(this).attr("data-position-id") + ',"LevelId":' + $(this).attr("data-level-id") + ',"TestId":' + $(this).attr("data-test-id") + '}';
            });

            var json = '[' + distribution.join(",") + ']';
            var testIds = $("#TestIds").val() == null ? '' : $("#TestIds").val().toString();
            $.ajax({
                url: '/TrainingSchedule/TrainingScheduleDistribution/UpdateTestDistribution',
                //url: '/TrainingScheduleDistribution/UpdateTestDistribution',
                type: "POST",
                data: { jsonData: json, testIds: testIds, trainingScheduleId: $("#TrainingScheduleId").val() },
                success: function (data) {
                    if (data) {
                        alert("Thành công");
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(thrownError);
                }
            });
        }
    }
};

$(document).ready(function () {
    TrainingScheduleTestDistribution.init();
});