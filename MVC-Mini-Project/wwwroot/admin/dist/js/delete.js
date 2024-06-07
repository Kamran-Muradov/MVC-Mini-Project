$(function () {
    $(document).on("click", "#slider-area .delete-btn", function () {
        let id = parseInt($(this).attr("data-id"));

        $(document).on("click", "#slider-area .yes-btn", function () {
            $.ajax({
                type: "POST",
                url: `/admin/slider/delete?id=${id}`,
                success: function () {
                    window.location.reload();
                }
            });
        })
    })

    $(document).on("click", "#information-area .delete-btn", function () {
        let id = parseInt($(this).attr("data-id"));

        $(document).on("click", "#information-area .yes-btn", function () {
            $.ajax({
                type: "POST",
                url: `/admin/information/delete?id=${id}`,
                success: function () {
                    window.location.reload();
                }
            });
        })
    })

    $(document).on("click", "#about-area .delete-btn", function () {
        let id = parseInt($(this).attr("data-id"));

        $(document).on("click", "#about-area .yes-btn", function () {
            $.ajax({
                type: "POST",
                url: `/admin/about/delete?id=${id}`,
                success: function () {
                    window.location.reload();
                }
            });
        })
    })

})