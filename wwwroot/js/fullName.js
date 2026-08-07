$.validator.addMethod("fullname", function (value, element) {
    if (value == "") {
        return true;
    }

    var parts = value.trim().split(" ");
    var count = 0;

    for (var i = 0; i < parts.length; i++) {
        if (parts[i] != "") {
            count++;
        }
    }

    return count >= 2;
});

$.validator.unobtrusive.adapters.add("fullname", function (options) {
    options.rules["fullname"] = true;
    options.messages["fullname"] = options.message;
});