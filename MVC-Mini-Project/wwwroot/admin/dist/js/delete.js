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

    $(document).on("click", "#category-area .delete-btn", function () {
        let id = parseInt($(this).attr("data-id"));

        $(document).on("click", "#category-area .yes-btn", function () {
            $.ajax({
                type: "POST",
                url: `/admin/category/delete?id=${id}`,
                success: function () {
                    window.location.reload();
                }
            });
        })
    })

    $(document).on("click", "#instructor-edit-area .delete-social", function () {
        let instructorId = parseInt($(this).attr("data-instructorId"));
        let socialId = parseInt($(this).attr("data-socialId"));
        let link = $(this).attr("data-link");

        let data = { instructorId, socialId, link };

        $.ajax({
            type: "POST",
            url: `/admin/instructor/deletesocial`,
            data: data,
            success: function () {
                $(`[data-instructorId=${instructorId}]`).closest("ul").remove();
            }
        });
    })

      $(document).on("click", "#instructor-area .delete-btn", function () {
        let id = parseInt($(this).attr("data-id"));

        $(document).on("click", "#instructor-area .yes-btn", function () {
            $.ajax({
                type: "POST",
                url: `/admin/instructor/delete?id=${id}`,
                success: function () {
                    window.location.reload();
                }
            });
        })
    })

})