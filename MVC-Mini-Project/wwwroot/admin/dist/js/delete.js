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

})