(function () {
    'use strict';

    var games;

    $(document).ready(function () {
        games = $('#games');

        games.on('click', '.game', function (evt) {
            var id = $(this).data('id');
            location.href = "/Home/PlaySudoku/" + id;
        });

        $('#load-more-btn').click(function () {
            var skip = games.find('.game').length;

            $.ajax({
                url: '/Home/GetMoreGames',
                type: 'post',
                data: { skip: skip, take: 10 },
                success: function (data) {
                    games.append(data);
                }
            });
        });
    });

})();