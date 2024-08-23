/*
* ----------------------------------------------------------------------------------------
    Template Name: softbay app landing html template
    Template URI: https://spellbit.com/
    Description: 
    Author: mahedi amin
    Author URI: https://mahediamin.com
    Version: 1.0.0

* ----------------------------------------------------------------------------------------
*/

(function ($) {
    'use strict';

    jQuery(document).on("ready", function () {





        /*
         * smoth scroll activation
         *
         */

        $('a.smooth-scroll').on("click", function (e) {
            var anchor = $(this);
            $('html, body').stop().animate({
                scrollTop: $(anchor.attr('href')).offset().top - 60
            }, 1200, 'easeInOutExpo');
            e.preventDefault();
        });
        $('body').scrollspy({
            target: '.navbar-collapse',
            offset: 195
        });



        /*
         *  owl carosle activation
         *
         */




        //testimonials active

        $('.testimonilas-active').owlCarousel({
            loop: true,
            margin: 15,
            center: true,
            mouseDrag: true,
            autoplay: true,
            responsive: {
                210: {
                    items: 1
                },
                320: {
                    items: 1
                },
                479: {
                    items: 2,
                    center: false,
                },
                768: {
                    items: 2,
                    center: false,
                },
                980: {
                    items: 2,
                    center: false,
                },
                1199: {
                    items: 3
                }
            }
        });

        // brand activation
        $('.brand-product-active').owlCarousel({
            loop: true,
            margin: 10,
            mouseDrag: true,
            autoplay: true,
            responsive: {
                210: {
                    items: 1
                },
                320: {
                    items: 2
                },
                479: {
                    items: 2,
                },
                768: {
                    items: 3,
                },
                980: {
                    items: 4
                },
                1199: {
                    items: 5
                }
            }
        });






        /*
         * Sticky Menu activation
         *
         */

        $(window).on('scroll', function () {
            var scroll = $(window).scrollTop();
            if (scroll < 70) {
                $(".site-header").removeClass("sticky");
            } else {
                $(".site-header").addClass("sticky");
            }
        });



        /*
         * animation active
         *
         */


        $(function () {

            function ckScrollInit(items, trigger) {
                items.each(function () {
                    var ckElement = $(this),
                        AnimationClass = ckElement.attr('data-animation'),
                        AnimationDelay = ckElement.attr('data-animation-delay');

                    ckElement.css({
                        '-webkit-animation-delay': AnimationDelay,
                        '-moz-animation-delay': AnimationDelay,
                        'animation-delay': AnimationDelay
                    });

                    var ckTrigger = (trigger) ? trigger : ckElement;

                    ckTrigger.waypoint(function () {
                        ckElement.addClass("animated").css("visibility", "visible");
                        ckElement.addClass('animated').addClass(AnimationClass);
                    }, {
                        triggerOnce: true,
                        offset: '90%'
                    });
                });
            }

            ckScrollInit($('.animation'));
            ckScrollInit($('.staggered-animation'), $('.staggered-animation-wrap'));

        });




        /*
         *
         * Scroll up activation
         */


        $.scrollUp({
            scrollText: '<i class="icofont icofont-swoosh-up"></i>'

        });


        /*
         * Counter up plugin activation
         *
         */


        $('.counter').counterUp({
            delay: 10,
            time: 1000
        });



        /*
         * bootstarp navigation  mmobile nav active
         *
         */

        $(".navbar-toggler").on('click', function () {

            $(".navbar-toggler").toggleClass("cg");
        });




        $(".main-menu ul > li.nav-item > a.nav-link").on('click',
            function () {

                $(".navbar-collapse").removeClass("show");
                $(".navbar-toggler").removeClass("cg");
            });




        /*      
         * tooltip activation
         *   
         */


        $('[data-toggle="tooltip"]').tooltip();


        /*
         * Youtube background video
         *
         */


        $('.player').mb_YTPlayer();


        /*
         *
         *  video popup active
         */

        $('.video-pop').magnificPopup({
            type: 'iframe',
            removalDelay: 300,
            mainClass: 'mfp-fade'
        });



        /*
         * Preloder Activation
         *
         */

        jQuery(window).load(function () {
            jQuery(".softbay-preloder").fadeOut(300);
        });







    });

})(jQuery);