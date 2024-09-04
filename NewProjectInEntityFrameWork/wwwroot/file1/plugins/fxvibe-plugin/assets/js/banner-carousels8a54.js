(function($) {
	
	"use strict";
	var banner_carousels_script_js = function($scope, $) {
		
		// banner-carousel
		if ($('.banner-carousel').length) {
			var slider_attr = $('.banner-carousel').data('slider-banner');
			$('.banner-carousel').owlCarousel({
				loop:slider_attr.loop,
				margin:slider_attr.spacebetween,
				nav:true,
				animateOut: 'fadeOut',
				animateIn: 'fadeIn',
				active: true,
				smartSpeed: 1000,
				autoplay: 6000,
				navText: [ '<span class="flaticon-left-align"></span>', '<span class="flaticon-right"></span>' ],
				responsive:{
					0:{
						items:1
					},
					600:{
						items:1
					},
					800:{
						items:1
					},
					1024:{
						items:slider_attr.slidesperview,
					}
				}
			});
		}
		
		if($('.curved-circle').length) {
        	$('.curved-circle').circleType({position: 'absolute', dir: 0.77, radius: 55, forceHeight: true, forceWidth: true});
		}
	
		if($('.curved-circle-2').length) {
			$('.curved-circle-2').circleType({position: 'absolute', dir: 0.89, radius: 65, forceHeight: true, forceWidth: true});
		}
		
	};
	$(window).on('elementor/frontend/init', function () {
            elementorFrontend.hooks.addAction('frontend/element_ready/fxvibe_banner_carousel.default', banner_carousels_script_js);
    });	

})(window.jQuery);