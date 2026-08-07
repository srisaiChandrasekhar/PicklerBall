$.validator.addMethod("fullname", function (value, element) {
    if (!value) return true;

    var namePart = /^[A-Za-z]+(['\-][A-Za-z]+)*$/;
    var parts = value.trim().split(/\s+/);

    for (var i = 0; i < parts.length; i++) {
        if (!namePart.test(parts[i])) return false;
    }

    return parts.length >= 2;
});

$.validator.unobtrusive.adapters.addBool("fullname");