(function () {
    'use strict';

    $(document).ready(function () {
        $('#vote').on('click', "input[type='radio']", function (evt) {
            var val = $(this).val();

            $.ajax({
                url: "../RateBoard",
                type: 'post',
                data: { boardId: $('#board-id').val(), ratingValue: val },
                success: function (data) {
                    $('#total-rating').html(data.rating);
                }
            });
        });
    });

})();