$(document).ready(function () {
    $('#solution-pagepiling').pagepiling({
        anchors: ['page-1', 'page-2', 'page-3', 'page-4', 'page-5', 'page-6'],
        navigation: false,
        scrollingSpeed: 100,
        afterLoad: function (anchorLink, index) {
            var $block = $('#block-' + index),
                    height = $block.height();

            $block.parent().animate({
                'height': height + 10
            });
        },
        afterRender: function () {
            var $block = $('#block-1'),
                    height = $block.height();

            $block.parent().animate({
                'height': height + 10
            });
        }
    });

    $('.solution-page.content-nav ul li a').on('click', function () {
        $('html, body').animate({
            scrollTop: $("#solution-container").offset().top
        }, 500);
    });

    $('body').on({
        'mousewheel': function (e) {
            if (e.target.offsetParent.offsetParent.id != 'solution-pagepiling')
                return;
            e.preventDefault();
            e.stopPropagation();
        }
    });
});