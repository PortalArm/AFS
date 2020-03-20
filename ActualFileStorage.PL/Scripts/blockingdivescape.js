$(document).ready(() => {
    $(".blocking-div").on("click", e => {
        if (e.target.className.indexOf("blocking-div") != -1) {
            $(".blocking-div").remove();
            $(document).off("keydown");
        }
    });
    $(document).on("keydown", e => {
        if (e.target.nodeName != "INPUT" && e.key == "Escape") {
            e.preventDefault();
            $(".blocking-div").remove();
            $(document).off("keydown");
        }
    });
    $(".blocking-div .form-control").last().on("keydown", e => {
        if (e.keyCode == 13)
            $(".blocking-div .btn").click();
    });
});