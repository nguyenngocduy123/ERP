$(function () {
    $('#SourceFile').ace_file_input({
        no_file: 'File',
        btn_choose: 'Select',
        btn_change: 'Other File',
        droppable: false,
        onchange: null,
        thumbnail: false,

        before_change: function (files, dropped) {
            var fileType = $("#Type").val();
            var fileUpload = $(this).val();
            var extension = fileUpload.substring(fileUpload.lastIndexOf('.')).toLowerCase();
            var validFileTypeImg = ".jpg, .jpeg, .gif";
            var reg = null;
            var regTypeFile = null;
            if (fileUpload.length > 0) {
                if (validFileTypeImg.toLowerCase().indexOf(extension) < 0) {
                    alert("Bạn đã chọn File (" + extension + ") không đúng định dạng đối với hình ảnh!\nChú ý đối với hình ảnh, phần mở rộng cho phép là:\n .jpg, .jpeg, .gif");
                    return false;
                } else {
                    reg = /\.(jpg|jpe?g|gif)$/i;
                    regTypeFile = /^(image)\/(jpg|jpe?g|gif)$/i;
                }

                var allowed_files = [];
                for (var i = 0; i < files.length; i++) {
                    var file = files[i];
                    if (typeof file === "string") {
                        //IE8 and browsers that don't support File Object
                        if (!reg.test(file)) return false; //|png|gif|bmp
                    } else {
                        var type = $.trim(file.type);
                        if ((type.length > 0 && !regTypeFile.test(type)) || (type.length == 0 && !reg.test(file.name)))
                            continue; //not an image so don't keep this file
                        //for android's default browser which gives an empty string for file.type
                    }
                    allowed_files.push(file);
                }
                if (allowed_files.length == 0) return false;
                return allowed_files;
            }
            else {
                return false;
            }
        }
    });
});

$(function () {
    $('#SourceFileThumbnailImage').ace_file_input({
        no_file: 'File',
        btn_choose: 'Select',
        btn_change: 'Other File',
        droppable: false,
        onchange: null,
        thumbnail: false,

        before_change: function (files, dropped) {
            var fileType = $("#Type").val();
            var fileUpload = $(this).val();
            var extension = fileUpload.substring(fileUpload.lastIndexOf('.')).toLowerCase();
            var validFileTypeImg = ".jpg, .jpeg, .gif";
            var reg = null;
            var regTypeFile = null;
            if (fileUpload.length > 0) {
                if (validFileTypeImg.toLowerCase().indexOf(extension) < 0) {
                    alert("Bạn đã chọn File (" + extension + ") không đúng định dạng đối với hình ảnh!\nChú ý đối với hình ảnh, phần mở rộng cho phép là:\n .jpg, .jpeg, .gif");
                    return false;
                } else {
                    reg = /\.(jpg|jpe?g|gif)$/i;
                    regTypeFile = /^(image)\/(jpg|jpe?g|gif)$/i;
                }

                var allowed_files = [];
                for (var i = 0; i < files.length; i++) {
                    var file = files[i];
                    if (typeof file === "string") {
                        //IE8 and browsers that don't support File Object
                        if (!reg.test(file)) return false; //|png|gif|bmp
                    } else {
                        var type = $.trim(file.type);
                        if ((type.length > 0 && !regTypeFile.test(type)) || (type.length == 0 && !reg.test(file.name)))
                            continue; //not an image so don't keep this file
                        //for android's default browser which gives an empty string for file.type
                    }
                    allowed_files.push(file);
                }
                if (allowed_files.length == 0) return false;
                return allowed_files;
            }
            else {
                return false;
            }
        }
    });
});

$(function () {
    $('#SourceFileImage').ace_file_input({
        no_file: 'File',
        btn_choose: 'Select',
        btn_change: 'Other File',
        droppable: false,
        onchange: null,
        thumbnail: false,

        before_change: function (files, dropped) {
            var fileType = $("#Type").val();
            var fileUpload = $(this).val();
            var extension = fileUpload.substring(fileUpload.lastIndexOf('.')).toLowerCase();
            var validFileTypeImg = ".jpg, .jpeg, .gif";
            var reg = null;
            var regTypeFile = null;
            if (fileUpload.length > 0) {
                if (validFileTypeImg.toLowerCase().indexOf(extension) < 0) {
                    alert("Bạn đã chọn File (" + extension + ") không đúng định dạng đối với hình ảnh!\nChú ý đối với hình ảnh, phần mở rộng cho phép là:\n .jpg, .jpeg, .gif");
                    return false;
                } else {
                    reg = /\.(jpg|jpe?g|gif)$/i;
                    regTypeFile = /^(image)\/(jpg|jpe?g|gif)$/i;
                }

                var allowed_files = [];
                for (var i = 0; i < files.length; i++) {
                    var file = files[i];
                    if (typeof file === "string") {
                        //IE8 and browsers that don't support File Object
                        if (!reg.test(file)) return false; //|png|gif|bmp
                    } else {
                        var type = $.trim(file.type);
                        if ((type.length > 0 && !regTypeFile.test(type)) || (type.length == 0 && !reg.test(file.name)))
                            continue; //not an image so don't keep this file
                        //for android's default browser which gives an empty string for file.type
                    }
                    allowed_files.push(file);
                }
                if (allowed_files.length == 0) return false;
                return allowed_files;
            }
            else {
                return false;
            }
        }
    });
});