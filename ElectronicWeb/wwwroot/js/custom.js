/*********************************************************************
 * 
 * PROPRIETARY and CONFIDENTIAL
 * 
 * This is licensed from, and is trade secret of:
 * 
 *          Group 10 - PRN231 - SU23
 *          FPT University, Education and Training zone
 *          Hoa Lac Hi-tech Park, Km29, Thang Long Highway
 *          Ha Noi, Viet Nam
 *          
 * Refer to your License Agreement for restrictions on use,
 * duplication, or disclosure
 * 
 * RESTRICTED RIGHTS LEGEND
 * 
 * Use, duplication or disclosure is the
 * subject to restriction in Articles 736 and 738 of the 
 * 2005 Civil Code, the Intellectual Property Law and Decree 
 * No. 85/2011/ND-CP amending and supplementing a number of 
 * articles of Decree 100/ND-CP/2006 of the Government of Viet Nam
 * 
 * 
 * Copy right 2023 © PRN231 - SU23 - Group 10 ®. All Rights Reserved
 * 
 * Unpublished - All rights reserved under the copyright laws 
 * of the Government of Viet Nam
*********************************************************************/

(function () {

	'use strict'


	AOS.init({
		duration: 800,
		easing: 'slide',
		once: true
	});

	var preloader = function() {

		var loader = document.querySelector('.loader');
		var overlay = document.getElementById('overlayer');

		function fadeOut(el) {
			el.style.opacity = 1;
			(function fade() {
				if ((el.style.opacity -= .1) < 0) {
					el.style.display = "none";
				} else {
					requestAnimationFrame(fade);
				}
			})();
		};

		setTimeout(function() {
			fadeOut(loader);
			fadeOut(overlay);
		}, 200);
	};
	preloader();

	var tinyslider = function() {

		var slider = document.querySelectorAll('.features-slider');
		var postSlider = document.querySelectorAll('.post-slider');
		var testimonialSlider = document.querySelectorAll('.testimonial-slider');
		
		if ( slider.length> 0 ) {
			var tnsSlider = tns({
				container: '.features-slider',
				mode: 'carousel',
				speed: 700,
				items: 3,
				// center: true,
				gutter: 30,
				loop: false,
				edgePadding: 80,
				controlsPosition: 'bottom',
				// navPosition: 'bottom',
				nav: false,
				// autoplay: true,
				// autoplayButtonOutput: false,
				controlsContainer: '#features-slider-nav',
				responsive: {
					0: {
						items: 1
					},
					700: {
						items: 2
					},
					900: {
						items: 3
					}
				}
			});
		}

		if ( postSlider.length> 0 ) {
			var tnsPostSlider = tns({
				container: '.post-slider',
				mode: 'carousel',
				speed: 700,
				items: 3,
				// center: true,
				gutter: 30,
				loop: true,
				edgePadding: 10,
				controlsPosition: 'bottom',
				navPosition: 'bottom',
				nav: true,
				autoplay: true,
				autoplayButtonOutput: false,
				controlsContainer: '#post-slider-nav',
				responsive: {
					0: {
						items: 1
					},
					700: {
						items: 2
					},
					900: {
						items: 3
					}
				}
			});
		}

		if ( testimonialSlider.length> 0 ) {
			var tnsTestimonialSlider = tns({
				container: '.testimonial-slider',
				mode: 'carousel',
				speed: 700,
				items: 1,
				// center: true,
				gutter: 30,
				loop: true,
				edgePadding: 10,
				controlsPosition: 'bottom',
				navPosition: 'bottom',
				nav: true,
				autoplay: true,
				autoplayButtonOutput: false,
				controlsContainer: '#testimonial-slider-nav',
				controls: false,
				responsive: {
					0: {
						items: 1
					},
					700: {
						items: 1
					},
					900: {
						items: 1
					}
				}
			});
		}

		
	}
	tinyslider();

	var lightboxVideo = GLightbox({
		selector: '.glightbox'
	});


})()