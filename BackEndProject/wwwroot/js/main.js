(function ($) {
"use strict";  
    
/*------------------------------------
	Sticky Menu 
--------------------------------------*/
 var windows = $(window);
    var stick = $(".header-sticky");
	windows.on('scroll',function() {    
		var scroll = windows.scrollTop();
		if (scroll < 5) {
			stick.removeClass("sticky");
		}else{
			stick.addClass("sticky");
		}
	});  
/*------------------------------------
	jQuery MeanMenu 
--------------------------------------*/
	$('.main-menu nav').meanmenu({
		meanScreenWidth: "767",
		meanMenuContainer: '.mobile-menu'
	});
    
    
    /* last  2 li child add class */
    $('ul.menu>li').slice(-2).addClass('last-elements');
/*------------------------------------
	Owl Carousel
--------------------------------------*/
    $('.slider-owl').owlCarousel({
        loop:true,
        nav:true,
        animateOut: 'fadeOut',
        animateIn: 'fadeIn',
        smartSpeed: 2500,
        navText:['<i class="fa fa-angle-left"></i>','<i class="fa fa-angle-right"></i>'],
        responsive:{
            0:{
                items:1
            },
            768:{
                items:1
            },
            1000:{
                items:1
            }
        }
    });

    $('.partner-owl').owlCarousel({
        loop:true,
        nav:true,
        navText:['<i class="fa fa-angle-left"></i>','<i class="fa fa-angle-right"></i>'],
        responsive:{
            0:{
                items:1
            },
            768:{
                items:3
            },
            1000:{
                items:5
            }
        }
    });  

    $('.testimonial-owl').owlCarousel({
        loop:true,
        nav:true,
        navText:['<i class="fa fa-angle-left"></i>','<i class="fa fa-angle-right"></i>'],
        responsive:{
            0:{
                items:1
            },
            768:{
                items:1
            },
            1000:{
                items:1
            }
        }
    });
/*------------------------------------
	Video Player
--------------------------------------*/
    $('.video-popup').magnificPopup({
        type: 'iframe',
        mainClass: 'mfp-fade',
        removalDelay: 160,
        preloader: false,
        zoom: {
            enabled: true,
        }
    });
    
    $('.image-popup').magnificPopup({
        type: 'image',
        gallery:{
            enabled:true
        }
    }); 
/*----------------------------
    Wow js active
------------------------------ */
    new WOW().init();
/*------------------------------------
	Scrollup
--------------------------------------*/
    $.scrollUp({
        scrollText: '<i class="fa fa-angle-up"></i>',
        easingType: 'linear',
        scrollSpeed: 900,
        animation: 'fade'
    });
/*------------------------------------
	Nicescroll
--------------------------------------*/
     $('body').scrollspy({ 
            target: '.navbar-collapse',
            offset: 95
        });
$(".notice-left").niceScroll({
            cursorcolor: "#EC1C23",
            cursorborder: "0px solid #fff",
            autohidemode: false,
            
        });
/*------------------------------------
	Search
--------------------------------------*/
    $("#search-course-text").keyup(function() {
        let search = $(this).val();
        $("#searchULv2 li").remove();
        if (search.length >0 && search.trim()!="") {
            $.ajax({
                url: "/Course/Search?search=" + search,
                type: "Get",
                success: function (res) {
                    $("#searchULv2").append(res);
                }
            })
        }

    })

    $("#searchBtnv2").click(function (e) {
        e.preventDefault();
        let search = $("#search-course-text").val();
        $("#searchULv2 li").remove();
        if (search.trim() != "") {
            $(".allCourse").remove();
            $.ajax({
                url: "/Course/SearchBtn?search=" + search,
                type: "Get",
                success: function (res) {
                    if (res != "null") {
                        $(".errorText").css("display","none");
                        $(".allCourseParent").append(res);
                    } else {
                        $(".errorText").css("display","block");
                        $(".errorText").text(search + " was not found");
                    }
                }

            })
        }
        
    })

    $("#searchTextDetailv2").keyup(function () {
        let search = $(this).val();
        $("#searchULv2 li").remove();
        if (search.length > 0 && search.trim() != "") {
            $.ajax({
                url: "/Course/Search?search=" + search,
                type: "Get",
                success: function (res) {
                    $("#searchULv2").append(res);
                }
            })
        } 

    })
    $("#searchTextBlogv2").keyup(function () {
        let search = $(this).val();
        $("#searchULv2 li").remove();
        if (search.length > 0 && search.trim() != "") {
            $.ajax({
                url: "/Blog/Search?search=" + search,
                type: "Get",
                success: function (res) {
                    $("#searchULv2").append(res);
                }
            })
        }
    })
    $("#searchTextEventv2").keyup(function () {
        let search = $(this).val();
        $("#searchULv2 li").remove();
        if (search.length > 0 && search.trim() != "") {
            $.ajax({
                url: "/Event/Search?search=" + search,
                type: "Get",
                success: function (res) {
                    $("#searchULv2").append(res);
                }
            })
        }
    })
    $("#search-course").keyup(function () {
        let search = $(this).val();
        $("#searchUL li").remove();
        if (search.length > 0 && search.trim() != "") {
            $.ajax({
                url: "/Course/Search?search=" + search,
                type: "Get",
                success: function (res) {
                    $("#searchUL").append(res);
                }
            })
        }

    })
    $("#search-blog").keyup(function () {
        let search = $(this).val();
        $("#searchUL li").remove();
        if (search.length > 0 && search.trim() != "") {
            $.ajax({
                url: "/Blog/Search?search=" + search,
                type: "Get",
                success: function (res) {
                    $("#searchUL").append(res);
                }
            })
        }

    })

    $("#searchBlogBtn").click(function (e) {
        e.preventDefault();
        let search = $("#searchTextBlogv2").val();
        $("#searchULv2 li").remove();
        if (search.trim() != "") {
            $(".allBlog").remove();
            $.ajax({
                url: "/Blog/SearchBtn?search=" + search,
                type: "Get",
                success: function (res) {
                    if (res != "null") {
                        $(".allPagination").remove();
                        $(".errorText").css("display", "none");
                        $(".allBlogParent").append(res);
                    } else {
                        $(".errorText").css("display", "block");
                        $(".errorText").text(search + " was not found");
                    }
                }

            })
        }

    })

    $("#search-teacher").keyup(function () {
        let search = $(this).val();
        $("#searchUL li").remove();
        if (search.length > 0 && search.trim() != "") {
            $.ajax({
                url: "/Teacher/Search?search=" + search,
                type: "Get",
                success: function (res) {
                    $("#searchUL").append(res);
                }
            })
        }

    })
    $("#search-event").keyup(function () {
        let search = $(this).val();
        $("#searchUL li").remove();
        if (search.length > 0 && search.trim() != "") {
            $.ajax({
                url: "/Event/Search?search=" + search,
                type: "Get",
                success: function (res) {
                    $("#searchUL").append(res);
                }
            })
        }

    })
})(jQuery);	