(function (window, base, $) {
    $.s3UploadController = function (element, options) {
        var defaultOptions = {};
        // Establish our default settings
        var settings = $.extend(defaultOptions, options);
        $.extend(playListController, settings);
        return this.each(function () {
            //code here
        });

    };
    window.s3UploadController = {
        ui: {
            formId: "#frm"
        },
        s3: {
            s3Upload: null,
        },
        init: function (options) {
            var defaultOptions = {
            };
            var settings = $.extend(defaultOptions, options);
            //s3UploadController.onSearch(settings);
            $('#file').change(function (evt) {
                $('#upload-cancel').hide();
                $('#div-file-name').hide();
                if (s3UploadController.checkExtension({ file: this })) {
                    
                    $(this).attr('disabled', 'disabled');
                    $('#upload-cancel').show();
                    $('#file-name').text($(this)[0].files[0].name);
                    $('#div-file-name').show();
                }
            });
        },
        uploadS3: function (options) {
            var defaultOptions = {
                user: "user",
                pass: "pass",
                S3AmazonUrl: ""
            };
            var settings = $.extend(defaultOptions, options);
            $('.col-xs-9 .field-validation-error').hide();
            if (!(window.File && window.FileReader && window.FileList && window.Blob && window.Blob.prototype.slice)) {
                alert(window.setaJs.JsMessage.updateBrowser);
                return;
            }

            if (s3UploadController.s3.s3Upload != null) {
                //alert(window.setaJs.JsMessage.multilFile);
                s3UploadController.s3.s3Upload = null;
                s3UploadController.uploadS3(options);
                return;
            }

            var file = $('#file')[0].files[0];
            s3UploadController.s3.s3Upload = new S3MultiUpload(file, { user: settings.user, pass: settings.pass });

            s3UploadController.s3.s3Upload.onServerError = function (command, jqXHR, textStatus, errorThrown) {
                if (jqXHR.status === 403) {
                    alert(window.setaJs.JsMessage.notAllowUpload);
                } else {
                    console.log("Our server is not responding correctly ");
                }
            };

            s3UploadController.s3.s3Upload.onS3UploadError = function (xhr) {
                s3UploadController.s3.s3Upload.waitRetry();
                alert(window.setajs.JsMessage.uploadFail + s3UploadController.s3.s3Upload.RETRY_WAIT_SEC + " seconds");
            };

            s3UploadController.s3.s3Upload.onProgressChanged = function (uploadingSize, uploadedSize, totalSize) {
                //$('#uploading_progress').attr('value', uploadingSize);
                //$('#uploading_progress').attr('max', totalSize);
                //console.log(" uploadedSize= " + uploadedSize + " =totalSize = " + totalSize + " uploadingSize = " + uploadingSize)
                $('#uploaded_progress').attr('value', uploadedSize);
                $('#uploaded_progress').attr('max', totalSize);
                var currentPercen = (uploadedSize * 100) / totalSize;
                $('#percen-process').html(currentPercen.toFixed(0) + '%');
                $('#percen-process').attr('style', 'width:' + currentPercen.toFixed(0) + '%');
                //$('#summed_progress').attr('value', uploadedSize + uploadingSize);
                //$('#summed_progress').attr('max', totalSize);
            };

            s3UploadController.s3.s3Upload.onUploadCompleted = function (data, fileName) {
                $('#file').attr('disabled', 'disabled');
                $('#pause-resume-cancel').hide();
                s3UploadController.uploadCancel({ isHide: true });
                $('#URL').val(fileName);
                var vid = document.getElementById("my_video");
                vid.src = settings.S3AmazonUrl + "/" + fileName;
                //vid.pause();
                var source = document.getElementById("video-source");
                $(source).attr("src", settings.S3AmazonUrl + "/" + fileName);
                // Add event listeners
                
                vid.addEventListener("timeupdate", function seektimeupdate() {
                    vid.pause();
                }, false);

                vid.addEventListener("durationchange", function () {
                    console.log(vid.duration.toFixed(0));
                    $("#Duration").val(vid.duration.toFixed(0));
                    $('#btn-submit').removeAttr("disabled");
                    
                });

                vid.addEventListener("error", function errorMes(e) {
                    $('#btn-submit').removeAttr("disabled");
                    console.log("errror" + e);
                }, false);
                vid.load();
                vid.play();
                alert("Upload successfully cmnr.");
                $('#btn-submit').removeAttr("disabled");
            };
            $('#pause-resume-cancel').show();
            $('#file').attr('disabled', 'disabled');
            $('#upload-cancel').hide();
            $('#btn-submit').attr("disabled", "disabled");
            $('#btn-resume').hide();
            s3UploadController.s3.s3Upload.start();
        },
        pause: function (options) {
            var defaultOptions = {
            };
            var settings = $.extend(defaultOptions, options);
            s3UploadController.s3.s3Upload.pause();
            $('#btn-resume').show();
        },
        resume: function (options) {
            var defaultOptions = {
            };
            var settings = $.extend(defaultOptions, options);
            s3UploadController.s3.s3Upload.resume();
            $('#btn-resume').hide();
        },
        cancel: function (options) {
            var defaultOptions = {
            };
            var settings = $.extend(defaultOptions, options);
            s3UploadController.s3.s3Upload.cancel();
            s3UploadController.s3.s3Upload = null;
        },
        uploadCancel: function (options) {
            var defaultOptions = {
                isHide: false
            };
            var settings = $.extend(defaultOptions, options);
            $('#file').val("");
            $('#file').removeAttr('disabled');
            $('#upload-cancel').hide();
            $('#percen-process').html('0%');
            $('#percen-process').attr('style', 'width:0%');

            if (settings.isHide != true) {
                $('#div-file-name').hide();
                $('#pause-resume-cancel').hide();
            }
        },
        intializePlayer: function () {
            var vid = document.getElementById("my_video");
            vid.addEventListener("timeupdate", function seektimeupdate() {
                console.log(vid.duration.toFixed(0));
                vid.pause();
            }, false);
            vid.play();
        },
        checkExtension: function (options) {
            var defaultOptions = {
                file: null,
                validFilesTypes: ["mp3", "mp4" , "mp4v", "m4v", "mkv","mov"]
            };
            var settings = $.extend(defaultOptions, options);

            var filePath = settings.file.value;
            var ext = filePath.substring(filePath.lastIndexOf('.') + 1).toLowerCase();
            var isValidFile = false;
            for (var i = 0; i < settings.validFilesTypes.length; i++) {
                if (ext == settings.validFilesTypes[i]) {
                    isValidFile = true;
                    var vMediaTypeID = $("#MediaTypeID");
                    if (ext.toLowerCase() == "mp4" || ext.toLowerCase() == "mp4v" || ext.toLowerCase() == "m4v" || ext.toLowerCase() == "mkv" || ext.toLowerCase() == "mov") {
                        vMediaTypeID.val("2");
                    } else if (ext.toLowerCase() == "mp3") {
                        vMediaTypeID.val("1");
                    } else {
                        vMediaTypeID.val("0");
                    }
                    break;
                }
            }

            if (!isValidFile) {
                settings.file.value = null;
                alert(window.setaJs.JsMessage.extension + settings.validFilesTypes.join(", "));
            }
            if (isValidFile) {
                if ($(settings.file)[0].files[0].size > 10737418240)
                {
                    alert(window.setaJs.JsMessage.size);
                    return false;
                }
            }
            return isValidFile;
        },
        getExtension: function (fileName) {
            try {
                var ext = fileName.substring(fileName.lastIndexOf('.') + 1).toLowerCase();
                if (ext.toLowerCase() == "mp4") {
                    return ".mp4";
                }
                if ( ext.toLowerCase() == "mp4v" ) {
                    return ".mp4v";
                }
                if (ext.toLowerCase() == "m4v") {
                    return ".m4v";
                }
                if (ext.toLowerCase() == "mkv") {
                    return ".mkv";
                }
                if (ext.toLowerCase() == "mov") {
                    return ".mov";
                }
                return ".mp3";
            } catch (e) {
                return ".unknow";
            }
        }
    };
    window.s3FineUploader = {
        init: function (options) {
            var defaultOptions = {
                access_key: "",
                bucket: ""
            };
            var settings = $.extend(defaultOptions, options);
            var uploader = new qq.s3.FineUploader({
                debug: true,
                element: document.getElementById('fine-uploader'),
                request: {
                    endpoint: '' + settings.bucket + '.s3.amazonaws.com',
                    accessKey: settings.access_key,
                },
                signature: //JSON.stringify({ policy: policy, signature: signature }),//signature,
                    {
                        endpoint: '/Mix/PolicySignature'
                    },
                uploadSuccess: {
                    endpoint: '/Mix/S3Success'
                },
                iframeSupport: {
                    localBlankPagePath: '/success.html'
                },
                //retry: {
                //    enableAuto: false // defaults to false
                //},
                //deleteFile: {
                //    enabled: true,
                //    endpoint: '/s3handler'
                //}
                multiple: false,
                chunking: {
                    enabled: true,
                    partSize: 5 * 1024 * 1024,
                },
                resume: {
                    enabled: true
                },
                //deleteFile: {
                //    enabled: true,
                //    method: "POST",
                //    endpoint: "/s3-upload"
                //},
                validation: {
                    itemLimit: 1,
                    allowedExtensions: ["mp4", "mp3", "mp4v", "m4v", "mkv","mov"]
                    //sizeLimit: 15000000000000
                }
            });
        },
    };
})(window, window.base, jQuery);