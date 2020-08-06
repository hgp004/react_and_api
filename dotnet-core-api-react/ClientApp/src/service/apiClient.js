"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var getClient;
getClient = function (url) {
    return {};
};
var Handler = /** @class */ (function () {
    function Handler() {
        var _this = this;
        this.info = "init";
        this.onClickGood = function (e) { _this.info = e.message; };
    }
    return Handler;
}());
var handler1 = new Handler();
handler1.info = "123";
var handler2 = new Handler();
handler2.info = "234";
var a = {
    message: "aaa"
};
console.log("ff", handler1.onClickGood(a));
console.log("handler1.info", handler1.info, "handler2.info", handler2.info);
var Person = /** @class */ (function () {
    function Person() {
        this.name = "";
    }
    return Person;
}());
var animal = /** @class */ (function () {
    function animal() {
        this.type = "";
    }
    return animal;
}());
var p;
// OK, because of structural typing
p = new Person();
//p = new animal();//error: 类型和名字都需要匹配
exports.default = getClient;
//# sourceMappingURL=apiClient.js.map