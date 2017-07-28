$(document).ready(function () {

    var widthWindow = $(window).width();
    var heightWindow = $(window).height();

    // Menu mobile
    $("#menu-mobile").mmenu();

    // Slider photo
    $('.js-slider-photo').slick({
        dots: false,
        arrows: true,
        infinite: true,
        speed: 500,
        fade: true,
        autoplay: true,
        autoplaySpeed: 6000,
        cssEase: 'linear'
    });


    // Magnific popup
    //Popup Work Detail
    $('.js-popup-photo .item a').magnificPopup({
        mainClass: 'mfp-fade mfp-work-detail',
        type: 'image',
        gallery: {
            enabled: true
        },
        removalDelay: 160
    });

    // Slick
    $('.js-testimonial-slider').slick({
        dots: true,
        arrows: false,
        speed: 500,
        fade: true,
        autoplay: true,
        autoplaySpeed: 6000,
        cssEase: 'linear'
    });
});
