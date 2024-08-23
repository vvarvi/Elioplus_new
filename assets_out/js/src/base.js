(function ($) {
    'use strict';

    $(document).ready(function() {

        const blurItemsMenu = $('main, footer');

        $('#mainNavigation > ul a').on('mouseover', function (e) {
            e.preventDefault();
            $('#mainNavigation ul a.active').removeClass('active');
            $(this).addClass('active');
            $('.megamenu.open').removeClass('open');
            $($(this).attr('href')).addClass('open');
            blurItemsMenu.addClass('blur-md overflow-hidden');
        });

        $('#mainNavigation').on('mouseleave', function (e) {
            e.preventDefault();
            $('#mainNavigation ul a.active').removeClass('active');
            $('.megamenu.open').removeClass('open');
            blurItemsMenu.removeClass('blur-md overflow-hidden');
        });

        $('.modal-trigger').on('click', function(e){
            e.preventDefault();

            overlay.removeClass('hidden');
            blurItemsMenu.addClass('blur-md');
            $('body').addClass('modal-open overflow-hidden');

            let id = $(this).attr('href');

            $(id).removeClass('hidden').addClass('opened');

        });

        $(document).on('overlayClicked', function(){

            if( $('body').hasClass('modal-open') ){
                blurItemsMenu.removeClass('blur-md');
                $('.modal.opened').removeClass('opened').addClass('hidden');
                $('body').removeClass('modal-open overflow-hidden');
            }

        });

        const mobileMenu = $('#mobileMenu');
        const blurItems = $('main, header, footer');
        const overlay = $('#overlay');

        $('#toggleMobileMenu').on('click', function (e) {
            e.preventDefault();
            mobileMenu.addClass('active');
            blurItems.addClass('blur-md overflow-hidden');
            overlay.removeClass('hidden');
        });

        $('#closeMobileMenu, #overlay').on('click', function (e) {
            e.preventDefault();
            mobileMenu.removeClass('active');
            blurItems.removeClass('blur-md overflow-hidden');
            overlay.addClass('hidden');
            $(document).trigger('overlayClicked');
        });

        $('.has-submenu').on('click', function (e) {
            e.preventDefault();

            if (!$(this).hasClass('active')) {

                $('.has-submenu.active').removeClass('active');
                $('.active-submenu').removeClass('active-submenu').addClass('hidden');

                $(this).addClass('text-blue active');
                $(this).closest('li').find('ul').removeClass('hidden').addClass('active-submenu');
            } else {
                $(this).removeClass('text-blue active');
                $(this).closest('li').find('ul').addClass('hidden').removeClass('active-submenu');
            }

        });

        $(".go-to").click(function(e) {
            e.preventDefault();

            let id = $(this).attr('href');

            $('html, body').animate({
                scrollTop: $(id).offset().top - 120
            }, 700);
        });

        $(".go-to-expand").click(function(e) {
            e.preventDefault();

            let id = $(this).attr('href');

            $(id).parent().find('details[open]').attr('open', false);
            $(id).attr('open', true);

            $('html, body').animate({
                scrollTop: $(id).offset().top - 120
            }, 700);

        });

        $(".go-to-trigger").click(function(e) {
            e.preventDefault();

            let id = $(this).attr('href');
            $(id).trigger('click');

            $('html, body').animate({
                scrollTop: $(id).closest('.search-tabs').offset().top - 120
            }, 700);

        });

        let owlCarousel = $('.owl-carousel');

        if (owlCarousel.length > 0) {

            owlCarousel.each(function(){

                let items       = parseInt( $(this).attr('data-items') );
                // let itemsMobile = parseInt( $(this).attr('data-items-mobile') );

                $(this).owlCarousel({
                    autoplayHoverPause: true,
                    margin: 0,
                    nav: true,
                    dots: true,
                    lazyLoad: true,
                    navText: [
                        '<svg width="30" height="31" viewBox="0 0 30 31" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_1255_27256)"><path d="M6.66606 14.1175L20.0164 0.489249C20.3252 0.173789 20.7374 0 21.1769 0C21.6164 0 22.0286 0.173789 22.3374 0.489249L23.3206 1.49265C23.9603 2.14647 23.9603 3.20913 23.3206 3.86196L12.1099 15.3062L23.333 26.763C23.6418 27.0785 23.8123 27.499 23.8123 27.9475C23.8123 28.3964 23.6418 28.8169 23.333 29.1326L22.3498 30.1358C22.0408 30.4512 21.6289 30.625 21.1894 30.625C20.7498 30.625 20.3377 30.4512 20.0289 30.1358L6.66606 16.495C6.35655 16.1786 6.18655 15.7561 6.18752 15.3069C6.18655 14.856 6.35655 14.4337 6.66606 14.1175Z" fill="#004BE0"></path></g><defs><clipPath id="clip0_1255_27256"><rect width="30" height="30.625" fill="white" transform="matrix(-1 0 0 1 30 0)"></rect></clipPath></defs></svg>',
                        '<svg width="30" height="31" viewBox="0 0 30 31" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_1255_27248)"><path d="M23.3339 14.1175L9.98357 0.489249C9.67479 0.173789 9.2626 0 8.82309 0C8.38358 0 7.97139 0.173789 7.66261 0.489249L6.67945 1.49265C6.0397 2.14647 6.0397 3.20913 6.67945 3.86196L17.8901 15.3062L6.66701 26.763C6.35823 27.0785 6.18774 27.499 6.18774 27.9475C6.18774 28.3964 6.35823 28.8169 6.66701 29.1326L7.65017 30.1358C7.95919 30.4512 8.37114 30.625 8.81065 30.625C9.25016 30.625 9.66235 30.4512 9.97113 30.1358L23.3339 16.495C23.6435 16.1786 23.8135 15.7561 23.8125 15.3069C23.8135 14.856 23.6435 14.4337 23.3339 14.1175Z" fill="#004BE0"></path></g><defs><clipPath id="clip0_1255_27248"><rect width="30" height="30.625" fill="white"></rect></clipPath></defs></svg>',
                    ],
                    responsive: {
                        0: {
                            items: 1
                        },
                        600: {
                            items: 2
                        },
                        1000: {
                            items: items
                        }
                    }
                });

            });


        }

        let mCarousel = $('.m-carousel');
        if (mCarousel.length > 0) {

            if ($(window).width() < 641) {

                mCarousel.each(function () {

                    $(this).find('.hidden').remove();
                    $(this).addClass('owl-carousel owl-theme')
                    let items = parseInt($(this).attr('data-items'));
                    // let itemsMobile = parseInt( $(this).attr('data-items-mobile') );

                    $(this).owlCarousel({
                        autoplayHoverPause: true,
                        margin: 0,
                        nav: true,
                        dots: true,
                        lazyLoad: true,
                        navText: [
                            '<svg width="30" height="31" viewBox="0 0 30 31" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_1255_27256)"><path d="M6.66606 14.1175L20.0164 0.489249C20.3252 0.173789 20.7374 0 21.1769 0C21.6164 0 22.0286 0.173789 22.3374 0.489249L23.3206 1.49265C23.9603 2.14647 23.9603 3.20913 23.3206 3.86196L12.1099 15.3062L23.333 26.763C23.6418 27.0785 23.8123 27.499 23.8123 27.9475C23.8123 28.3964 23.6418 28.8169 23.333 29.1326L22.3498 30.1358C22.0408 30.4512 21.6289 30.625 21.1894 30.625C20.7498 30.625 20.3377 30.4512 20.0289 30.1358L6.66606 16.495C6.35655 16.1786 6.18655 15.7561 6.18752 15.3069C6.18655 14.856 6.35655 14.4337 6.66606 14.1175Z" fill="#004BE0"></path></g><defs><clipPath id="clip0_1255_27256"><rect width="30" height="30.625" fill="white" transform="matrix(-1 0 0 1 30 0)"></rect></clipPath></defs></svg>',
                            '<svg width="30" height="31" viewBox="0 0 30 31" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_1255_27248)"><path d="M23.3339 14.1175L9.98357 0.489249C9.67479 0.173789 9.2626 0 8.82309 0C8.38358 0 7.97139 0.173789 7.66261 0.489249L6.67945 1.49265C6.0397 2.14647 6.0397 3.20913 6.67945 3.86196L17.8901 15.3062L6.66701 26.763C6.35823 27.0785 6.18774 27.499 6.18774 27.9475C6.18774 28.3964 6.35823 28.8169 6.66701 29.1326L7.65017 30.1358C7.95919 30.4512 8.37114 30.625 8.81065 30.625C9.25016 30.625 9.66235 30.4512 9.97113 30.1358L23.3339 16.495C23.6435 16.1786 23.8135 15.7561 23.8125 15.3069C23.8135 14.856 23.6435 14.4337 23.3339 14.1175Z" fill="#004BE0"></path></g><defs><clipPath id="clip0_1255_27248"><rect width="30" height="30.625" fill="white"></rect></clipPath></defs></svg>',
                        ],
                        responsive: {
                            0: {
                                items: items
                            },
                            600: {
                                items: 2
                            },
                            1000: {
                                items: items
                            }
                        }
                    });

                });
            }


        }

        $('.tab-item').on('click', function(e){
            e.preventDefault();
            let id = $(this).attr('href')
            let el = $(id);

            $('.tab-content').addClass('hidden');
            $('.tab-item.active').removeClass('active');
            $(this).addClass('active');
            el.removeClass('hidden');
        })


        $('.stepTab').on('click', function(e){
            e.preventDefault();
            let id = $(this).attr('href')
            let el = $(id);

            $('.step-content').addClass('hidden');
            $('.stepTab.active').removeClass('active');
            $(this).addClass('active');
            el.removeClass('hidden');
        })

        $('.step-nav-btn').on('click', function(e){
            e.preventDefault();
            let id = $(this).attr('href')
            let el = $(id);
            el.trigger('click');
        })


        $('.moreTrigger').on('click', function(e){
            e.preventDefault();

            if( $(this).hasClass('toggled') ){
                $(this).closest('.items-group').find('.extra').addClass('hidden');
                $(this).find('.number').removeClass('hidden');
                $(this).find('.labelText').text('more');
            }
            else {
                $(this).closest('.items-group').find('.extra.hidden').removeClass('hidden');
                $(this).find('.number').addClass('hidden');
                $(this).find('.labelText').text('show less');
            }

            $(this).toggleClass('toggled');

        });

        $('.table-load').on('click', function(e){
            e.preventDefault();

            $(this).parent().find('table .extra.hidden').removeClass('hidden');

        });


        $('.toast-demo').on('click', function(e){
            e.preventDefault();
            let type = $(this).attr('data-type')
            let options = {};

            switch(type){

                case 'info':
                    options = {
                        heading: 'Information',
                        text: 'Some notification message here that describes the result of the user action.',
                        icon: 'info',
                        loader: true,        // Change it to false to disable loader
                        loaderBg: '#fff',  // To change the background
                        position: {
                            top: "80px",
                            right:"20px",
                        }
                    };
                    break;

                case 'success':
                    options = {
                        heading: 'Success',
                        text: 'Some notification message here that describes the result of the user action.',
                        icon: 'success',
                        loader: true,        // Change it to false to disable loader
                        loaderBg: '#fff',  // To change the background
                        position: {
                            top: "80px",
                            right:"20px",
                        }
                    };
                    break;

                case 'warning':
                    options = {
                        heading: 'Warning',
                        text: 'Some notification message here that describes the result of the user action.',
                        icon: 'warning',
                        loader: true,        // Change it to false to disable loader
                        loaderBg: '#fff',  // To change the background
                        position: {
                            top: "80px",
                            right:"20px",
                        }
                    };
                    break;

                case 'error':
                    options = {
                        heading: 'Error',
                        text: 'Some notification message here that describes the result of the user action.',
                        icon: 'error',
                        loader: true,        // Change it to false to disable loader
                        loaderBg: '#fff',  // To change the background
                        position: {
                            top: "80px",
                            right:"20px",
                        }
                    };
                    break;

                default:
                    options = {
                        heading: 'Simple',
                        text: 'Some notification message here that describes the result of the user action.',
                        loader: true,        // Change it to false to disable loader
                        loaderBg: '#E2EDFF',  // To change the background
                        position: {
                            top: "80px",
                            right:"20px",
                        }
                    };
                    break;

            }

            $.toast( options );

        });


        $('.open-menu').on('click', function(e){
            e.preventDefault();
            let id = $(this).attr('href')
            let el = $(id);
            el.toggleClass('hidden');
        });

        $(document).on('click', function(e){

            if($(e.target).closest('.menu-group, .open-menu').length)
                return;

            $('.menu-group').addClass('hidden');


        });


    });

})( jQuery );
