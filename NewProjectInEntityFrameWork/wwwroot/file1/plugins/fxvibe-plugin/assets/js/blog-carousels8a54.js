(function($) {
	
	"use strict";
	var blog_carousels_script_js = function($scope, $) {
		
		// two-item-carousel
		if ($('.blog-two-item-carousel').length) {
			var slider_attr = $('.blog-two-item-carousel').data('slider-blog');
			$('.blog-two-item-carousel').owlCarousel({
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
						items:1
					},
					800:{
						items:1
					},
					1024:{
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
		elementorFrontend.hooks.addAction('frontend/element_ready/fxvibe_blog_carousel.default', blog_carousels_script_js);
    });	

})(window.jQuery);