function TriggerLogout() {
    $.ajax({
        type: "POST",
        url: "/Auth/Logout",
        success: function (response) {
            location.reload();
            setTimeout(function () { }, 200);
        },
        failure: function (response) {
            console.log(response.Message);
        },
        error: function (response) {
            console.log(response.Message);
        }
    });
}