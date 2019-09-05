//
// - Fox Biz/Data Service REST API 클라이언트 자바 스크립트 라이브러리 ver 4.5.4.0
//
// DataService 클라이언트
var FoxRestDataClient = /** @class */ (function () {
    function FoxRestDataClient(url) {
        this.baseUrl = url;
    }
    // - XMLHttpRequest 객체를 생성하고 초기화 한다.
    FoxRestDataClient.prototype.createXHR = function (operation, onComplete, onError) {
        var url = this.baseUrl + "/" + operation;
        var xhr;
        xhr = new XMLHttpRequest();
        xhr.onreadystatechange = function () {
            if (this.readyState === 4) {
                if (this.status === 200) {
                    var response = JSON.parse(this.responseText);
                    if (onComplete != null) {
                        onComplete(response);
                    }
                }
                else {
                    var contentType = this.getResponseHeader("Content-Type");
                    var errorInfo = null;
                    if (contentType != null && contentType.indexOf("application/json") >= 0) {
                        errorInfo = JSON.parse(this.responseText);
                    }
                    else {
                        errorInfo = new FoxServiceErrorInfo();
                        errorInfo.Message = this.status + " " + this.statusText;
                        errorInfo.MessageDetail = this.responseText;
                    }
                    if (onError != null) {
                        onError(errorInfo);
                    }
                }
            }
        };
        xhr.open("POST", url, true);
        if (this.authString != null && this.authString.length > 0) {
            xhr.setRequestHeader("FoxRest-Authenticate", this.authString);
        }
        xhr.setRequestHeader("Content-Type", "application/json; charset=utf-8");
        xhr.setRequestHeader("Accept", "appliction/json");
        return xhr;
    };
    // - Invoke 메서드 (operation 이름을 명시하는 메서드)
    FoxRestDataClient.prototype.invoke = function (operation, request, onComplete, onError) {
        var json;
        var xhr = this.createXHR(operation, onComplete, onError);
        json = JSON.stringify(request);
        xhr.send(json);
    };
    // - ExecuteDataSet 메서드를 호출한다.
    FoxRestDataClient.prototype.executeDataSet = function (request, onComplete, onError) {
        this.invoke("executedataset", request, onComplete, onError);
    };
    // - ExecuteNonQuery 메서드를 호출한다.
    FoxRestDataClient.prototype.executeNonQuery = function (request, onComplete, onError) {
        this.invoke("executenonquery", request, onComplete, onError);
    };
    // - ExecuteScalar 메서드를 호출한다.
    FoxRestDataClient.prototype.executeScalar = function (request, onComplete, onError) {
        this.invoke("executescalar", request, onComplete, onError);
    };
    // - Execute 메서드를 호출한다.
    FoxRestDataClient.prototype.execute = function (request, onComplete, onError) {
        this.invoke("execute", request, onComplete, onError);
    };
    // - SaveDataTable 메서드를 호출한다.
    FoxRestDataClient.prototype.saveDataTable = function (request, onComplete, onError) {
        this.invoke("saveDataTable", request, onComplete, onError);
    };
    // - ExecuteMultiple 메서드를 호출한다.
    FoxRestDataClient.prototype.executeMultiple = function (requests, onComplete, onError) {
        this.invoke("executeMultiple", requests, onComplete, onError);
    };
    FoxRestDataClient.prototype.getChanges = function (dt) {
        var newTable = new DataTable();
        newTable.name = dt.name;
        newTable.columns = dt.columns;
        newTable.rows = [];
        for (var _i = 0, _a = dt.rows; _i < _a.length; _i++) {
            var row = _a[_i];
            if (row.$rowState !== undefined && row.$rowState != null) {
                var state = row.$rowState;
                if (state === "Added" || state === "Modified" || state === "Deleted") {
                    newTable.rows.push(row);
                }
            }
        }
        return newTable;
    };
    return FoxRestDataClient;
}());
// BizService 클라이언트
var FoxRestBizClient = /** @class */ (function () {
    function FoxRestBizClient(url) {
        this.baseUrl = url;
    }
    // - XMLHttpRequest 객체를 생성하고 초기화 한다.
    FoxRestBizClient.prototype.createXHR = function (operation, onComplete, onError) {
        var url = this.baseUrl + "/" + operation;
        var xhr;
        xhr = new XMLHttpRequest();
        xhr.onreadystatechange = function () {
            if (this.readyState === 4) {
                if (this.status === 200) {
                    var response = JSON.parse(this.responseText);
                    if (onComplete != null) {
                        onComplete(response);
                    }
                }
                else {
                    var contentType = this.getResponseHeader("Content-Type");
                    var errorInfo = null;
                    if (contentType != null && contentType.indexOf("application/json") >= 0) {
                        errorInfo = JSON.parse(this.responseText);
                    }
                    else {
                        errorInfo = new FoxServiceErrorInfo();
                        errorInfo.Message = this.status + " " + this.statusText;
                        errorInfo.MessageDetail = this.responseText;
                    }
                    if (onError != null) {
                        onError(errorInfo);
                    }
                }
            }
        };
        xhr.open("POST", url, true);
        if (this.authString != null && this.authString.length > 0) {
            xhr.setRequestHeader("FoxRest-Authenticate", this.authString);
        }
        xhr.setRequestHeader("Content-Type", "application/json; charset=utf-8");
        xhr.setRequestHeader("Accept", "appliction/json");
        return xhr;
    };
    // - Invoke 메서드 (operation 이름을 명시하는 메서드)
    FoxRestBizClient.prototype.invoke = function (operation, request, onComplete, onError) {
        var json;
        var xhr = this.createXHR(operation, onComplete, onError);
        json = JSON.stringify(request);
        xhr.send(json);
    };
    // - Execute 메서드를 호출한다.
    FoxRestBizClient.prototype.execute = function (request, onComplete, onError) {
        this.invoke("execute", request, onComplete, onError);
    };
    // - ExecuteMultiple 메서드를 호출한다.
    FoxRestBizClient.prototype.executeMultiple = function (requests, onComplete, onError) {
        this.invoke("executeMultiple", requests, onComplete, onError);
    };
    return FoxRestBizClient;
}());
// - FoxDataRequest 클래스
var FoxDataRequest = /** @class */ (function () {
    function FoxDataRequest() {
        this.parameters = new FoxServiceParameterCollection();
    }
    return FoxDataRequest;
}());
// - FoxDataRequestCollection 클래스
var FoxDataRequestCollection = /** @class */ (function () {
    function FoxDataRequestCollection() {
        this.items = [];
    }
    return FoxDataRequestCollection;
}());
// - FoxDataResponse 클래스
var FoxDataResponse = /** @class */ (function () {
    function FoxDataResponse() {
    }
    return FoxDataResponse;
}());
// - FoxBizRequest 클래스
var FoxBizRequest = /** @class */ (function () {
    function FoxBizRequest() {
        this.parameters = new FoxServiceParameterCollection();
    }
    return FoxBizRequest;
}());
// - FoxBizRequestCollection 클래스
var FoxBizRequestCollection = /** @class */ (function () {
    function FoxBizRequestCollection() {
        this.items = [];
    }
    return FoxBizRequestCollection;
}());
// - FoxBizResponse 클래스
var FoxBizResponse = /** @class */ (function () {
    function FoxBizResponse() {
    }
    return FoxBizResponse;
}());
// - FoxServiceParameterCollection 클래스
var FoxServiceParameterCollection = /** @class */ (function () {
    function FoxServiceParameterCollection() {
    }
    return FoxServiceParameterCollection;
}());
// - REST API 오류 정보 클래스
var FoxServiceErrorInfo = /** @class */ (function () {
    function FoxServiceErrorInfo() {
    }
    return FoxServiceErrorInfo;
}());
// - DataTable 클래스
var DataTable = /** @class */ (function () {
    function DataTable() {
    }
    return DataTable;
}());
//# sourceMappingURL=FoxRestClient.js.map