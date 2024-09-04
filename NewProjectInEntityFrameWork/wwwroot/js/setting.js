$(document).ready(function(){
	//Animate scroll
	if($(".wow").length){
		new WOW().init();
	}

	//Active nav
	$("#nav a, .nav a").each(function(){
		var location = window.location.href;
		var link = this.href;
		if(location == link){
			$(this).addClass("active");
		}
	});
	
	//Digital Watch
	$.ajax({
		type: "POST",
		async: false,
		url: "/php/date.php",
		success: function(html){
			var res = html.match(/\d+/ig);
			dates = new Date(res[0], res[1]-1, res[2], res[3], res[4], res[5]);
		}
	});
	function digitalWatch() {
		dates.setSeconds(dates.getSeconds() + 1);
		day = dates.getDate();
		month = dates.getMonth() + 1;
		year = dates.getFullYear();
		hours = dates.getHours();
		minutes = dates.getMinutes();
		seconds = dates.getSeconds();
		if (day < 10) day = "0" + day;
		if (month < 10) month = "0" + month;
		if (year < 10) year = "0" + year;
		if (hours < 10) hours = "0" + hours;
		if (minutes < 10) minutes = "0" + minutes;
		if (seconds < 10) seconds = "0" + seconds;
		$("#date").text(day + "/" + month + "/" + year + " " + hours + ":" + minutes + ":" + seconds);
	}
	digitalWatch();
	setInterval(function(){ digitalWatch(); }, 1000);
	
	//Course
	$.ajax({
		type: "GET",
		url: "php/course.php",
		success: function(html) {
			course = 100 / html;
			course = course.toFixed(2);
			$("#bar .course span, #info .course").text(course + " USD");
		}
	});
	
	//Parallax
	$(".scene").parallax({
		limitY: 0
	});
	
	//Animate number
	if($(".counter1").length){
		$(".counter1").counterUp({ time: 300 });
	}
	if($(".counter2").length){
		$(".counter2").counterUp({ time: 2500 });
	}
	if($(".counter3").length){
		$(".counter3").counterUp({ time: 6000 });
	}
	
	
	//Setting calculator
	var percent 	= [7, 		9, 				12];
	var minMoney 	= [0.01, 	50.00000001, 	350.00000001];
	var maxMoney	= [50, 		350, 			999999999999];
	var period		= [1, 		1, 				1];
	$("#money").val(minMoney[0]);
	
	//Calculator
	function calc(){
		money = parseFloat($("#money").val());
		days = parseFloat($("#days").val());
		
		if(days < 1 || isNaN(days) == true){
			days = 1;
		}
		
		id = -1;
		var length = percent.length;
		var i = 0;
		do {
			if(minMoney[i] <= money && money <= maxMoney[i]){
				id = i;
				i = i + length;
			}
			i++
		}
		while(i < length)
		
		if(id != -1){
			profitDaily = money / 100 * percent[id] * period[id];
			profitDaily = profitDaily.toFixed(8);
			summa = profitDaily * days;
			summa = summa.toFixed(8);


			if(money < minMoney[id] || isNaN(money) == true){
				$("#profit").text("Error!");
				$("#profitDaily").text("Error!");
			} else {
				$("#profit").text(summa);
				$("#profitDaily").text(profitDaily);
			}
		} else {
			$("#profit").text("Error!");
			$("#profitDaily").text("Error!");
		}
	}
	if($("#money").length){
		calc();
	}
	$("#money, #days").keyup(function(){
		calc();
	});
	
	//Video
	var youtubeId = ["vToH1u1f5vo", "aFKB12qg4zw", "TkpA2alGgVY", "P9SGMLPl6Ig"];
	$(".video .box").click(function(){
		if(!$(this).hasClass("active")){
			var id = $(this).index();
			$(".video .active").removeClass("active");
			$(this).addClass("active");
			$("#player").attr("src", "https://www.youtube.com/embed/" + youtubeId[id] + "?rel=0");
		}
	});
	
	//Slider
	if($(".slider").length){
		$(".slider").carouFredSel({
			scroll: { duration: 800 },
			auto: 5000,
			prev: { button : ".prev1", key : "left" },
			next: { button : ".next1", key : "right" }
		});
	}
	
	
	//Select
	if($(".select").length){
		$(".select select").each(function (i) {
			var SelectText = $(this).find("option:selected").text();
			$(this).parent().find(".int").val(SelectText);
		});
		$(".select .int").show();
		$(".select select").css({ opacity: 0 });
		$(".select select").change(function(){
			var SelectText = $(this).find("option:selected").text();
			$(this).parent().find(".int").val(SelectText);
			if($(this).val() != ""){
				$(this).parent().find(".int").removeClass("error");
			}
		});
	}

	//Checkbox
	if($(".checkbox").length){
		$(".checkbox input").css({ opacity: 0 });
		$(".checkbox input").change(function(){
			if($(this).prop("checked") == true) {
				$(this).parent(".checkbox").addClass("check_act");
			} else {
				$(this).parent(".checkbox").removeClass("check_act");
			}
		});
		$('.checkbox input[type="checkbox"]:checked').parent(".checkbox").addClass("check_act");
	}

	//Radio
	if($(".radio").length){
		$(".radio input").css({ opacity: 0 });
		$(".radio input").change(function(){
			name = $(this).attr("name");
			$('input[name="' + name + '"]').parent(".radio").removeClass("radio_act");
			$(this).parent(".radio").addClass("radio_act");
		});
		$('.radio input[type="radio"]:checked').parent(".radio").addClass("radio_act");
	}
	
	//Gallery
	if($(".gallery").length){
		$(".gallery a").click(function(){
			$("body").css({ height: "auto" });
		});
		$(".gallery a").fancybox({
			helpers		: {
				title	: { type : 'inside' },
				buttons	: {}
			}
		});
	}
	
	//Slider2
	if($(".slider2").length){
		$(".slider2").carouFredSel({
			scroll: { duration: 800 },
			auto: false,
			prev: { button : ".prev2", key : "left" },
			next: { button : ".next2", key : "right" },
			pagination: {
				container: "#thumbnails",
				anchorBuilder: function(nr) {
					return '<div class="box blocks">\
								<img src="img/video/video' + nr + '.jpg" width="195" height="109" alt="">\
								<div class="play">\
									<span class="blocks">' + $('#txt').text() + '</span>\
								</div>\
							</div>';
				}
			}
		});
	}
	
	//Confirm img
	if($(".btc_form").length){
		$("#confirm .boxs").append($(".btc_form img"));
		$(".btc_form a").attr("id", "address");
	}
	
	//Refresh
	$(".refresh").click(function(){
		location.reload(true);
	});
	
	//Copy
	$(".copy").click(function(){
		new Clipboard(".copy");
	});
	
	//Form repres
	if($(".form_v_repres").length){
		$(".form_v_repres").validate({
			rules:{
				facebook: { url: true },
				twitter: { url: true },
				blog: { url: true },
				country: { required: true },
				language: { required: true },
				phone: { required: true },
				timing: { required: true }
			},
			messages:{
				facebook: { url: '' },
				twitter: { url: '' },
				blog: { url: '' },
				country: { required: function(){ $("#country").parent().find(".int").addClass("error"); } },
				language: { required: '' },
				phone: { required: '' },
				timing: { required: '' }
			},
			submitHandler: function(form) {
				regExp = /https*:\/{2}w*.*[a-zA-Z0-9]*.*[a-zA-Z0-9]+.[a-zA-Z0-9]+\//gim;
				if($("#facebook_r").val() == ""){
					$("#facebook_r").val("NO");
				} else {
					$("#facebook_r").val($("#facebook_r").val().replace(regExp,''));
				}
				if($("#twitter_r").val() == ""){
					$("#twitter_r").val("NO");
				} else {
					$("#twitter_r").val($("#twitter_r").val().replace(regExp,''));
				}
				if($("#skype_r").val() == ""){
					$("#skype_r").val("NO");
				}
				if($("#blog_r").val() == ""){
					$("#blog_r").val("NO");
				}
				$(form).ajaxSubmit();
			}
		});
	}
	
	if($("#info").length){
		(function() {
			var xhr = new XMLHttpRequest();
			  xhr.onreadystatechange = function () {
				if (xhr.readyState === 4) {
				  // document.getElementById('ajax').innerHTML = xhr.responseText;

				  var data = JSON.parse(xhr.responseText);
				  showStats(data);
				}
			  };
			  xhr.open('GET', '/php/get_stats.php');

			  function getRemoteStats() {
				xhr.send( );
				// document.getElementById('load').style.display = 'none';
			  }

			 getRemoteStats();

			function showStats(data){
				console.log("data::",data);
				document.querySelector(".rate_bitfinex").textContent = "$"+data["rates"]["bitfix"];
				document.querySelector(".rate_bitstamp").textContent = "$"+data["rates"]["bitstamp"];
				document.querySelector(".rate_itbit").textContent = "$"+data["rates"]["itbit"];
				document.querySelector(".rate_btce").textContent = "$"+data["rates"]["btce"];

				document.querySelector(".stats_marketcap").textContent = data["stats"]["market_cap"];
				document.querySelector(".stats_hashrate").textContent = data["stats"]["hash_rate"];
				document.querySelector(".stats_difficulty").textContent = data["stats"]["difficulty"];
				document.querySelector(".stats_totalblocks").textContent = data["stats"]["n_blocks_total"];
				document.querySelector(".stats_networkspeed").textContent = data["stats"]["network_speed"];	
			}
		})();
	}
});