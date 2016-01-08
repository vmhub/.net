
//var placeholder = [];
//function saveUSDvals() {
//    var currArr = [];
//    $(document).ready(function () {
//        var curr = $("#dropDownId option:selected").val();
//        $('#mytable tr').each(function () {
//            var price = this.cells[1].innerHTML;
//            currArr.push((parseFloat(price.replace(",", ".")) / parseFloat(curr)).toFixed(2)); //????
          
//        });
//    });
//    placeholder = currArr;
//}
//function populate() {
//    var curr = $("#dropDownId option:selected").val();
//    $('#mytable tr').each(function (index) {
//        var newprice = (parseFloat(placeholder[index]) * parseFloat(curr)).toFixed(2);  
//        this.cells[1].innerHTML = newprice.toString();
//        alert(placeholder[index]);
//    });
      
//    }
function populate() {
    $(document).ready(function () {
        var curr = $("#dropDownId option:selected").val();
            $.ajax({
                url: "/Home/SelectedValue",
                type: "GET",
                data: {curr: curr}
               
            })
            .done(function (partialViewResult) {
              //  alert('aaa');
                $("#mytable").html(partialViewResult);
            });
        });

    };

