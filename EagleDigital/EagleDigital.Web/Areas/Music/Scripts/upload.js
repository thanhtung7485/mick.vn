/**
 * S3MultiUpload Object
 * Create a new instance with new S3MultiUpload(file, otherInfo)
 * To start uploading, call start()
 * You can pause with pause()
 * Resume with resume()
 * Cancel with cancel()
 *
 * You can override the following functions (no event emitter :( , description below on the function definition, at the end of the file)
 * onServerError = function(command, jqXHR, textStatus, errorThrown) {}
 * onS3UploadError = function(xhr) {}
 * onProgressChanged = function(uploadingSize, uploadedSize, totalSize) {}
 * onUploadCompleted = function() {}
 *
 * @param {type} file
 * @param {type} otheInfo
 * @returns {MultiUpload}
 */
function S3MultiUpload(file, otheInfo) {
    d = new Date();
    //alert(d.YYYYMMDDHHMMSS())
    this.PART_SIZE = 5 * 1024 * 1024; //minimum part size defined by aws s3
    this.SERVER_LOC = '/S3Upload/S3MultiUpload'; //location of the server
    this.RETRY_WAIT_SEC = 10; //wait before retrying again on upload failure
    this.maxCurrUploadNumber = 5;
    this.file = file;
    this.fileInfo = {
        name: "uploads/" + d.YYYYMMDDHHMMSS() + this.removeSpecials(this.file.name) + window.s3UploadController.getExtension(this.file.name),
        type: this.file.type,
        size: this.file.size,
        lastModifiedDate: this.file.lastModifiedDate
    };
    this.sendBackData = null;
    this.isPaused = false;
    this.isCompleted = false;
    this.uploadXHR = null;
    this.uploadXHRs = new Array();
    this.otherInfo = otheInfo;
    this.uploadedSize = 0;
    this.uploadingSize = 0;
    this.eTags = new Array();
    this.partNos = new Array();
    this.currPartNos = new Array();
    this.curUploadInfo = {
        blob: null,
        partNum: 0
    };
    this.totalPart = 0;
    this.currUploadNumber = 0;
    this.currUploadIndex = 0;
    this.latestPart = 0;
    this.failParts = new Array();
    this.progress = [];
    this.isOk = false;
    if (console && console.log) {
        this.log = console.log;
    } else {
        this.log = function () {
        };
    }
}

Date.prototype.YYYYMMDDHHMMSS = function () {

    var date = new Date(this);
    var yyyy = date.getFullYear().toString();
    var MM = pad(date.getMonth() + 1, 2);
    var dd = pad(date.getDate(), 2);
    var hh = pad(date.getHours(), 2);
    var mm = pad(date.getMinutes(), 2);
    var ss = pad(date.getSeconds(), 2);
    return yyyy + MM + dd + hh + mm + ss;
};
function pad(number, length) {
    var str = '' + number;
    while (str.length < length) {
        str = '0' + str;
    }
    return str;
}

/** private */

S3MultiUpload.prototype.createMultipartUpload = function () {
    var self = this;
    $.get(self.SERVER_LOC, {
        command: 'CreateMultipartUpload',
        fileInfo: self.fileInfo,
        otherInfo: self.otherInfo
    }).done(function (data) {
        self.sendBackData = data;
        //self.uploadPart(1);
        self.totalPart = Math.ceil(self.file.size / self.PART_SIZE);
        self.uploadXHRs = new Array(self.totalPart);
        for (var i = 1; i <= self.maxCurrUploadNumber && i <= self.totalPart; i++) {
            self.latestPart = i;
            self.uploadSiglePart_NewS(i);
        }

    }).fail(function (jqXHR, textStatus, errorThrown) {
        self.onServerError('CreateMultipartUpload', jqXHR, textStatus, errorThrown);
    });
};

S3MultiUpload.prototype.resetUpload = function () {

}

/**
 * Call this function to start uploading to server
 *
 */
S3MultiUpload.prototype.start = function () {
    //this.uploadPart(0);
    this.isCompleted = false;
    this.uploadSiglePart_NewS(0);
};

/** private */
S3MultiUpload.prototype.uploadSiglePart = function (partNum) {
    var self = this;
    var blobs = this.blobs = [], promises = [];
    var start = 0;
    var end = 0, blob;

    this.curUploadInfo.partNum = partNum;

    if (this.curUploadInfo.partNum === 0) {
        this.createMultipartUpload();
        return;
    }

    start = this.PART_SIZE * (this.curUploadInfo.partNum - 1);
    if (start > this.file.size) {
        this.completeMultipartUpload();
        return;
    }

    if (start < this.file.size) {
        end = Math.min(start + this.PART_SIZE, this.file.size);
        blob = this.file.slice(start, end);
        //start = this.PART_SIZE * this.curUploadInfo.partNum++;
    }

    $.get(this.SERVER_LOC, {
        command: 'SignUploadPart',
        sendBackData: this.sendBackData,
        partNumber: this.curUploadInfo.partNum,
        contentLength: blob.size
    }).done(function (data) {
        self.sendSingleToS3(data, blob, partNum);
    }).fail(this.onServerError);
};

S3MultiUpload.prototype.uploadSiglePart_NewS = function (partNum) {
    var self = this;
    var blobs = this.blobs = [], promises = [];
    var start = 0;
    var end = 0, blob;

    //this.curUploadInfo.partNum = partNum;

    if (partNum === 0) {
        this.createMultipartUpload();
        return;
    }

    start = this.PART_SIZE * (partNum - 1);
    if (start > this.file.size) {
        this.completeMultipartUpload();
        return;
    }

    self.currPartNos.push(partNum);

    if (start < this.file.size) {
        end = Math.min(start + this.PART_SIZE, this.file.size);
        blob = this.file.slice(start, end);
        //start = this.PART_SIZE * this.curUploadInfo.partNum++;
    }

    $.get(this.SERVER_LOC, {
        command: 'SignUploadPart',
        sendBackData: this.sendBackData,
        partNumber: partNum,
        contentLength: blob.size
    }).done(function (data) {
        self.sendSingleToS3_NewS(data, blob, partNum);
    }).fail(this.onServerError);
};

/** private */
S3MultiUpload.prototype.uploadPart = function (partNum) {
    var blobs = this.blobs = [], promises = [];
    var start = 0;
    var end = 0, blob;

    this.curUploadInfo.partNum = partNum;

    if (this.curUploadInfo.partNum === 0) {
        this.createMultipartUpload();
        return;
    }

    if (start > this.file.size) {
        this.completeMultipartUpload();
        return;
    }
    while (start < this.file.size) {
        end = Math.min(start + this.PART_SIZE, this.file.size);
        blobs.push(this.file.slice(start, end));

        start = this.PART_SIZE * this.curUploadInfo.partNum++;
    }

    for (var i = 0; i < blobs.length; i++) {
        blob = blobs[i];
        promises.push($.get(this.SERVER_LOC, {
            command: 'SignUploadPart',
            sendBackData: this.sendBackData,
            partNumber: i + 1,//this.curUploadInfo.partNum,
            contentLength: blob.size
        }));
    }

    // we need to pass $.when an array of arguments
    // so we are using .apply()
    $.when.apply(null, promises)
     .then(this.sendAll.bind(this), this.onServerError);
};

S3MultiUpload.prototype.sendAll = function () {
    var blobs = this.blobs;
    var length = blobs.length;
    if (length == 0)
        return;

    for (var i = 0; i < length && this.currUploadNumber < this.maxCurrUploadNumber; i++) {
        // sendToS3( XHRresponse, blob);
        //this.sendToS3(arguments[i][0], blobs[i], i);
        this.latestPart = i;
        this.sendToS3_2(arguments, blobs, i);
        this.currUploadNumber++;
    }
    //var i = 0;
    //while (i < length) {
    //    if (this.currUploadNumber < this.maxCurrUploadNumber) {
    //        this.sendToS3(arguments[i][0], blobs[i], i);
    //        i++;
    //        this.currUploadNumber++;
    //    }
    //}
};
/** private */
S3MultiUpload.prototype.sendSingleToS3 = function (data, blob, index) {
    var self = this;
    var url = data['url'];
    var size = blob.size;
    var authHeader = data['authHeader'];
    var dateHeader = data['dateHeader'];
    var partNo = parseInt(data['partNo']);
    var request = self.uploadXHR = createCORSRequest();
    request.onreadystatechange = function () {
        if (request.readyState === 4) {
            self.uploadXHR = null;
            self.progress[index] = 100;
            if (request.status !== 200) {
                self.updateProgressBar();
                if (!self.isPaused)
                    self.onS3UploadError(request);
                return;
            }

            if (self.eTags.indexOf(request.getResponseHeader("ETag")) < 0) {
                self.eTags.push(request.getResponseHeader("ETag"));
                self.partNos.push(partNo);
            }
            self.uploadedSize += blob.size;
            self.updateProgressBar();

            //Upload next part when current part completed
            self.latestPart++;
            self.uploadSiglePart(self.latestPart);
        }
    };

    request.upload.onprogress = function (e) {
        if (e.lengthComputable) {
            self.progress[index] = e.loaded / size;
            self.updateProgressBar();
        }
    };
    if (typeof XDomainRequest != "undefined") {
        request.open('PUT', url, true);
    } else {
        request.open('PUT', url);
    }

    //request.setRequestHeader("x-amz-date", dateHeader);
    //request.setRequestHeader("Authorization", authHeader);
    request.setRequestHeader("Accept", "application/json;odata=verbose;charset=utf-8");
    request.send(blob);
};

S3MultiUpload.prototype.sendSingleToS3_NewS = function (data, blob, index) {
    var self = this;
    var url = data['url'];
    var size = blob.size;
    var authHeader = data['authHeader'];
    var dateHeader = data['dateHeader'];
    var partNo = parseInt(data['partNo']);
    var request = self.uploadXHRs[index - 1] = createCORSRequest();
    request.onreadystatechange = function () {
        if (request.readyState === 4) {
            //self.uploadXHR = null;
            self.uploadXHRs[index - 1] = null;
            self.progress[index] = 100;
            if (request.status !== 200) {
                self.failParts.push(partNo);
                self.updateProgressBar();
                if (!self.isPaused)
                    self.onS3UploadError(request);
                return;
            }

            if (self.eTags.indexOf(request.getResponseHeader("ETag")) < 0) {
                self.eTags.push(request.getResponseHeader("ETag"));
                self.partNos.push(partNo);
            }

            var pIdx = self.currPartNos.indexOf(index);
            self.currPartNos.splice(pIdx, 1);
            self.uploadedSize += blob.size;
            self.updateProgressBar();

            //Check Completed
            if (self.eTags.length == self.totalPart && self.isCompleted == false) {
                self.isCompleted == true;
                self.completeMultipartUpload();
                return;
            }

            var nxtPart = 0;
            if (self.failParts.length > 0) {
                nxtPart = self.failParts.pop();
            } else {
                self.latestPart++;
                nxtPart = self.latestPart;
                if (self.latestPart > self.totalPart)
                    return;
            }

            //Upload next part when current part completed
            self.uploadSiglePart_NewS(nxtPart);
            return;
        }
    };

    request.upload.onprogress = function (e) {
        if (e.lengthComputable) {
            self.progress[index] = e.loaded / size;
            self.updateProgressBar();
        }
    };
    if (typeof XDomainRequest != "undefined") {
        request.open('PUT', url, true);
    } else {
        request.open('PUT', url);
    }

    //request.setRequestHeader("x-amz-date", dateHeader);
    //request.setRequestHeader("Authorization", authHeader);
    request.setRequestHeader("Accept", "application/json;odata=verbose;charset=utf-8");
    request.send(blob);
};
/** private */
S3MultiUpload.prototype.sendToS3_2 = function (datas, blobs, index) {
    var data = datas[index][0];
    var blob = blobs[index];
    var self = this;
    var url = data['url'];
    var size = blob.size;
    var authHeader = data['authHeader'];
    var dateHeader = data['dateHeader'];
    var partNo = parseInt(data['partNo']);
    var request = self.uploadXHR = createCORSRequest();
    request.onreadystatechange = function () {
        if (request.readyState === 4) {
            self.uploadXHR = null;
            self.progress[index] = 100;
            if (request.status !== 200) {
                self.updateProgressBar();
                if (!self.isPaused)
                    self.onS3UploadError(request);
                return;
            }
            self.eTags.push(request.getResponseHeader("ETag"));
            self.partNos.push(partNo);
            self.uploadedSize += blob.size;
            self.updateProgressBar();

            if (self.eTags.length == self.curUploadInfo.partNum - 1) {
                self.completeMultipartUpload();
            } else if (self.latestPart < datas.length - 1) {
                self.latestPart++;
                self.sendToS3_2(datas, blobs, self.latestPart);
            }
        }
    };

    request.upload.onprogress = function (e) {
        if (e.lengthComputable) {
            self.progress[index] = e.loaded / size;
            self.updateProgressBar();
        }
    };
    if (typeof XDomainRequest != "undefined") {
        request.open('PUT', url, true);
    } else {
        request.open('PUT', url);
    }
    //request.setRequestHeader("x-amz-date", dateHeader);
    //request.setRequestHeader("Authorization", authHeader);
    request.setRequestHeader("Accept", "application/json;odata=verbose;charset=utf-8");
    request.send(blob);
};
function createCORSRequest() {
    var xhr = new XMLHttpRequest();
    if ("withCredentials" in xhr) {

    } else if (typeof XDomainRequest != "undefined") {

        // Otherwise, check if XDomainRequest.
        // XDomainRequest only exists in IE, and is IE's way of making CORS requests.
        xhr = new XDomainRequest();

    } else {

        // Otherwise, CORS is not supported by the browser.
        xhr = null;

    }
    return xhr;
}
S3MultiUpload.prototype.sendToS3 = function (data, blob, index) {
    var self = this;
    var url = data['url'];
    var size = blob.size;
    var authHeader = data['authHeader'];
    var dateHeader = data['dateHeader'];
    var partNo = parseInt(data['partNo']);
    var request = self.uploadXHR = createCORSRequest();
    request.onreadystatechange = function () {
        if (request.readyState === 4) {
            self.uploadXHR = null;
            self.progress[index] = 100;
            if (request.status !== 200) {
                self.updateProgressBar();
                if (!self.isPaused)
                    self.onS3UploadError(request);
                return;
            }
            self.eTags.push(request.getResponseHeader("ETag"));
            self.partNos.push(partNo);
            self.uploadedSize += blob.size;
            self.updateProgressBar();

            if (self.eTags.length == self.curUploadInfo.partNum - 1)
                self.completeMultipartUpload();

            this.currUploadNumber--;
        }
    };

    request.upload.onprogress = function (e) {
        if (e.lengthComputable) {
            self.progress[index] = e.loaded / size;
            //self.updateProgressBar();
        }
    };
    if (typeof XDomainRequest != "undefined") {
        request.open('PUT', url, true);
    } else {
        request.open('PUT', url);
    }
    //request.setRequestHeader("x-amz-date", dateHeader);
    //request.setRequestHeader("Authorization", authHeader);
    request.send(blob);
};

/**
 * Pause the upload
 * Remember, the current progressing part will fail,
 * that part will start from beginning (< 5MB of uplaod is wasted)
 */
S3MultiUpload.prototype.pause = function () {
    this.isPaused = true;
    //if (this.uploadXHR !== null) {
    //    this.uploadXHR.abort();
    //}

    if (this.currPartNos.length == 0)
        return;

    for (var i = 0; i < this.currPartNos.length; i++)
    {
        if(this.uploadXHRs[this.currPartNos[i] - 1] != null)
            this.uploadXHRs[this.currPartNos[i] -1].abort();

        if (this.failParts.indexOf(this.currPartNos[i]) < 0)
            this.failParts.push(this.currPartNos[i]);
    }
};

/**
 * Resumes the upload
 *
 */
S3MultiUpload.prototype.resume = function () {
    self = this;
    self.isPaused = false;
    var failNum = self.failParts.length;
    for (var i = 0; i < failNum && i < self.maxCurrUploadNumber; i++) {
        self.uploadSiglePart_NewS(self.failParts[0]);
        self.failParts.splice(0, 1);
    }
};

S3MultiUpload.prototype.cancel = function () {
    var self = this;
    self.pause();
    $.get(self.SERVER_LOC, {
        command: 'AbortMultipartUpload',
        sendBackData: self.sendBackData
    }).done(function (data) {
        //alert("canceled");
        s3UploadController.uploadCancel({ isHide: false });
    });
};

S3MultiUpload.prototype.waitRetry = function () {
    var self = this;
    window.setTimeout(function () {
        self.retry();
    }, this.RETRY_WAIT_SEC * 1000);
};

S3MultiUpload.prototype.retry = function () {
    self = this;
    //if (!this.isPaused && self.uploadXHR === null) {
    if (!this.isPaused) {
        //this.currUploadNumber--;
        //this.uploadSiglePart(this.latestPart);

        for (var i = 1; i <= self.maxCurrUploadNumber && i <= self.totalPart - self.currPartNos.length; i++) {
            if (self.failParts.length > 0) {
                self.uploadSiglePart_NewS(self.failParts.pop());
            } else if (self.partNos.indexOf(self.latestPart) < 0 && self.latestPart <= self.totalPart) {
                self.uploadSiglePart_NewS(self.latestPart);
            } else if (self.latestPart < self.totalPart) {
                self.latestPart++;
                self.uploadSiglePart_NewS(self.latestPart);
            }
        }
    }
};

/** private */
S3MultiUpload.prototype.completeMultipartUpload = function () {
    var self = this;
    if (self.isOk == false) {
        self.isOk = true;
        $.post(self.SERVER_LOC, {
            command: 'CompleteMultipartUpload',
            sendBackData: self.sendBackData,
            etags: self.eTags.toString(),
            partNos: self.partNos.toString(),
            fileName: self.fileInfo.name
        }).done(function(data) {
            self.onUploadCompleted(data, self.fileInfo.name);
        }).fail(function(jqXHR, textStatus, errorThrown) {
            self.onServerError('CompleteMultipartUpload', jqXHR, textStatus, errorThrown);
        });
    }
    return false;
};

/** private */
S3MultiUpload.prototype.updateProgressBar = function () {
    //var progress = this.progress;
    //var length = progress.length;
    //var total = 0;
    //for (var i = 0; i < progress.length; i++) {
    //    total = total + progress[i];
    //}
    //total = total / length;

    var total = Math.min(this.partNos.length * this.PART_SIZE, this.file.size);
    this.onProgressChanged(this.uploadingSize, total, this.file.size);



    //var progress = this.progress;
    //var total = 0;
    //total = parseInt((this.file.size / this.PART_SIZE).toFixed(0));
    //// this.onProgressChanged(this.uploadingSize, total, this.file.size);
    //if (progress.length - 1 == total + 1) {

    //}
    //this.onProgressChanged(this.uploadingSize, progress.length - 1, total + 1);
};

/**
 * Overrride this function to catch errors occured when communicating to your server
 * If this occurs, the program stops, you can retry by retry() or wait and retry by waitRetry()
 *
 * @param {type} command Name of the command which failed,one of 'CreateMultipartUpload', 'SignUploadPart','CompleteMultipartUpload'
 * @param {type} jqXHR jQuery XHR
 * @param {type} textStatus resonse text status
 * @param {type} errorThrown the error thrown by the server
 */
S3MultiUpload.prototype.onServerError = function (command, jqXHR, textStatus, errorThrown) {
};

/**
 * Overrride this function to catch errors occured when uploading to S3
 * If this occurs, we retry upload after RETRY_WAIT_SEC seconds
 * Most of the time you don't need to override this, except for informing user that upload of a part failed
 *
 * @param XMLHttpRequest xhr the XMLHttpRequest object
 */
S3MultiUpload.prototype.onS3UploadError = function (xhr) {
    self.waitRetry();
};
S3MultiUpload.prototype.removeSpecials = function (str) {
    return str.replace(/[^a-zA-Z0-9]+/g, "");
};

/**
 * Override this function to show user update progress
 *
 * @param {type} uploadingSize is the current upload part
 * @param {type} uploadedSize is already uploaded part
 * @param {type} totalSize the total size of the uploading file
 */
S3MultiUpload.prototype.onProgressChanged = function (uploadingSize, uploadedSize, totalSize) {
    this.log("uploadedSize = " + uploadedSize);
    this.log("uploadingSize = " + uploadingSize);
    this.log("totalSize = " + totalSize);
};

/**
 * Override this method to execute something when upload finishes
 *
 */
S3MultiUpload.prototype.onUploadCompleted = function (serverData) {

};
