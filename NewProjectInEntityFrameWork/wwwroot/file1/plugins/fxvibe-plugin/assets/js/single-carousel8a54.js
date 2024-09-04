(function($) {
	
	"use strict";
	var single_carousels_script_js = function($scope, $) {
		
		// three-item-carousel
		if ($('.three-item-carousel').length) {
			var slider_attr = $('.three-item-carousel').data('feature-service');
			$('.three-item-carousel').owlCarousel({
				loop:slider_attr.loop,
				margin:slider_attr.spacebetween,
				nav:true,
				smartSpeed: 500,
				autoplay: 1000,
				navText: [ '<span class="flaticon-left"></span>', '<span class="flaticon-right-arrow-1"></span>' ],
				responsive:{
					0:{
						items:1
					},
					480:{
						items:1
					},
					600:{
						items:2
					},
					800:{
						items:2
					},			
					1200:{
						items:slider_attr.slidesperview
					}
	
				}
			});    		
		}	
		
	};
	$(window).on('elementor/frontend/init', function () {
		elementorFrontend.hooks.addAction('frontend/element_ready/fxvibe_why_choose_us.default', single_carousels_script_js);
		elementorFrontend.hooks.addAction('frontend/element_ready/fxvibe_our_awards.default', single_carousels_script_js);
		elementorFrontend.hooks.addAction('frontend/element_ready/fxvibe_pricing_carousel.default', single_carousels_script_js);
    });	

})(window.jQuery);