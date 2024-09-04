(function($) {
	
	"use strict";
	var testimonials_carousels_script_js = function($scope, $) {
		
		// two-item-carousel
		if ($('.two-item-carousel').length) {
			var slider_attr = $('.two-item-carousel').data('slider-testi');
			$('.two-item-carousel').owlCarousel({
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
		
		// three-item-carousel
		if ($('.three-item-carousel').length) {
			var slider_attr_two = $('.three-item-carousel').data('slider-testi-two');
			$('.three-item-carousel').owlCarousel({
				loop:slider_attr_two.loop,
				margin:slider_attr_two.spacebetween,
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
						items:slider_attr_two.slidesperview
					}	
				}
			});    		
		}
		
		// three-item-carousel
		if ($('.single-item-carousel').length) {
			var slider_attr_three = $('.single-item-carousel').data('slider-testi-three');
			$('.single-item-carousel').owlCarousel({
				loop:slider_attr_three.loop,
				margin:slider_attr_three.spacebetween,
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
					1200:{
						items:slider_attr_three.slidesperview
					}
	
				}
			});    		
		}	
		
	};
	$(window).on('elementor/frontend/init', function () {
		elementorFrontend.hooks.addAction('frontend/element_ready/fxvibe_testimonial_carousel.default', testimonials_carousels_script_js);
		elementorFrontend.hooks.addAction('frontend/element_ready/fxvibe_blog_carousel.default', testimonials_carousels_script_js);
		elementorFrontend.hooks.addAction('frontend/element_ready/fxvibe_courses_carousel.default', testimonials_carousels_script_js);
    });	

})(window.jQuery);