(function () {
    'use strict';

    var selectDigitMenu = initMenu()
      , selectedSquare
      , field
      , boardId;

    field = [];
    for (var row = 0; row < 9; row++) {
        field[row] = [];
        for (var col = 0; col < 9; col++) {
            field[row][col] = '.';
        }
    }

    $(document).ready(function () {
        boardId = $("#board-id").val();

        $('.square').not('.modifiable').each(function (index, square) {
            var row = parseInt($(square).data('row'));
            var col = parseInt($(square).data('col'));
            field[row][col] = $(square).find('.square-content').first().html().trim();
        });

        $('#wrapper').append(selectDigitMenu);

        $('#wrapper').on('click', '.modifiable', function (event) {
            selectedSquare = $(this);

            var top = event.pageY + 'px';
            var left = event.pageX + 'px';

            selectDigitMenu.css({
                top: top,
                left: left
            });

            selectDigitMenu.show();
        });

        $('#show-initial-values').change(function (event) {
            if (this.checked) {
                $('.square').not('.modifiable').css({ 'background-color': '#AAA' });
            } else {
                $('.square').not('.modifiable').css({ 'background-color': 'white' });
            }
        });

        $('#send-btn').click(function (event) {
            sendSolved(boardId);
        });

        $('#save-btn').click(function (event) {
            saveGame();
        });

        $('#hide-btn').click(function () {
            $(this).parent().hide();
            $('.invalid-square').removeClass('invalid-square');
        });
    });

    function initMenu() {
        var selectDigitMenu = $("<div id='select-digit-menu' class='hidden'></div>");

        selectDigitMenu.append("<div id='dispose-btn'>x</div>");
        selectDigitMenu.append($("<div><span>1</span><span>2</span><span>3</span></div>"));
        selectDigitMenu.append($("<div><span>4</span><span>5</span><span>6</span></div>"));
        selectDigitMenu.append($("<div><span>7</span><span>8</span><span>9</span></div>"));
        selectDigitMenu.append($("<div id='clear-btn'>Clear</div>"));

        selectDigitMenu.on('click', '#dispose-btn', function (evt) {
            selectDigitMenu.hide('slow');
        });
        selectDigitMenu.on('click', 'span', function (evt) {
            var digit = $(this).html();
            selectedSquare.html("<div class='square-content'>" + digit + "</div>");
            selectDigitMenu.hide('slow');

            updateField(digit);
        });
        selectDigitMenu.on('click', '#clear-btn', function () {
            selectedSquare.html("<div class='square-content'> </div>");
            selectDigitMenu.hide('slow');

            updateField('.');
        });

        return selectDigitMenu;
    }

    function updateField(value) {
        var row = parseInt(selectedSquare.data('row'));
        var col = parseInt(selectedSquare.data('col'));

        field[row][col] = value;
    }

    function sendSolved(id) {
        var boardStr = getCurrentBoardAsString();

        var messagesWrapper = $('#messages');

        $.ajax({
            url: '/Home/SubmitSolution',
            type: 'post',
            data: { Id: id, Solution: boardStr },
            success: function (data) {
                console.log(data);
                if (data.IsValid) {
                    messagesWrapper.find('#message').html('Congratulations! You solved puzzle #' + id);
                    messagesWrapper.show();
                } else {
                    messagesWrapper.find('#message').html('Sorry, your solution is not valid.');
                    messagesWrapper.show();
                    if (data.InvalidIndices.length > 0) {
                        data.InvalidIndices.forEach(function (current) {
                            var row = Math.floor(current / 9);
                            var col = current % 9;
                            $(".square[data-row='" + row + "'][data-col='" + col + "']").addClass('invalid-square');
                        });
                    }
                }
            }
        });
    }

    function saveGame() {
        var boardStr = getCurrentBoardAsString();
        var data = { initialBoardId: boardId, content: boardStr };

        var messagesWrapper = $('#messages');

        $.ajax({
            url: '/Home/SaveGame',
            type: 'post',
            data: data,
            success: function (data) {
                if (data === true) {
                    messagesWrapper.find('#message').html('Your game was saved successfully');
                    messagesWrapper.show();
                } else {
                    messagesWrapper.find('#message').html('Your game was NOT saved.');
                    messagesWrapper.show();
                }
            },
            error: function (err) {
                messagesWrapper.find('#message').html('There was an error with your request.');
                messagesWrapper.show();
            }
        });
    }

    function getCurrentBoardAsString() {
        var val = "";
        for (var row = 0; row < 9; row++)
            for (var col = 0; col < 9; col++)
                if (field[row][col])
                    val += field[row][col];

        return val;
    }

})();