$(function() {
	$('.chzn-choices').sortable({
		placeholder: 'chzn-choices-placeholder',
		tolerance: 'pointer',
		forcePlaceholderSize: true,
		items: 'li:not(.search-field)',
		start: function() {
			$('.chzn-choices-placeholder').css({
				height: '20px',
				display: 'block',
				width: '20px',
				border: 'dashed 1px #CCCCCC',
				margin: '3px 0px 3px 5px'
			});
		}
	}).disableSelection();

	$($('.chzn-select').parents('form')[0]).submit(function() {
		var valid = $(this).valid();
		//var valid =  true;
		if(valid) {
			$('.chzn-choices').each(function() {
				var select = $(this).parent().prev();
				var li = $(this).children('.search-choice');
				var html = $("<select></select>");
				for(var i = li.length - 1; i >= 0; i--) {
					var idx = $(li[i]).children('a').attr('rel');
					console.log(idx);
					var option = select.children().eq(idx);
					html.prepend(option.attr('selected', 'selected').clone());
				}
				select.html(html.html());
			});
		}
	});
});