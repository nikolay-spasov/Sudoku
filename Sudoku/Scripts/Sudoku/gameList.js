(function () {
    'use strict';

    var games
      , difficulty;

    $(document).ready(function () {
        games = $('#games');
        difficulty = $('#DifficultyList');
        console.log(difficulty);

        games.on('click', '.game', function (evt) {
            var id = $(this).data('id');
            location.href = "/Home/PlaySudoku/" + id;
        });

        difficulty.change(function (evt) {
            window.location.href = "/Home/Index?difficulty=" + $(this).val();
        });

        $('#load-more-btn').click(function () {
            var skip = games.find('.game').length;

            $.ajax({
                url: '/Home/GetMoreGames?difficulty=' + difficulty.val(),
                type: 'post',
                data: { skip: skip, take: 10 },
                success: function (data) {
                    games.append(data);
                }
            });
        });
    });

})();