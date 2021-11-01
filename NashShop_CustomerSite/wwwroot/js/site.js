

$('body').on('click', '.btn-add-cart', function (e) {
    e.preventDefault();
    const id = $(this).data('id');
    /*alert("day la " + id);*/
    $.ajax({
        type: "POST",
        url: '/Cart/AddToCart',
        data: {
            id : id
        },
        success: function (res) {
            console.log(res);
        },
        error: function (error) {
            console.log(error);
        }
    })
})