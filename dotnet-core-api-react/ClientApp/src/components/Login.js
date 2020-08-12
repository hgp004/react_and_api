"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var React = require("react");
var Login = function () {
    return React.createElement("div", null,
        React.createElement("button", { onClick: function () {
                window.location.href = "/api/Customer/login";
            } }, "Login"));
};
exports.default = Login;
//# sourceMappingURL=Login.js.map