(function () {
    'use strict';

    $(document).ready(function () {
        $('.game').click(function (evt) {
            location.href = "/User/LoadGame/" + $(this).data('saved-id');
        });
    });

})();