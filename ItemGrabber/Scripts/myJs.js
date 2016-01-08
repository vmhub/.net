
function populate() {
    $(document).ready(function () {
        var curr = $("#dropDownId option:selected").val();
            $.ajax({
                url: "/Home/SelectedValue",
                type: "GET",
                data: {curr: curr}
               
            })
            .done(function (partialViewResult) {
                $("#mytable").html(partialViewResult);
            });
        });

    };

