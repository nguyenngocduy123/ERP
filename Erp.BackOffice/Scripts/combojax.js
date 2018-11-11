/***
* Combojax
*/
window.pageCombojaxs = window.pageCombojaxs || {};
$.fn.extend({
    combojax: function (params) {
        var aObj = [];
        $(this).each(function () {
            if (!$(this).data("combojax")) {
                var combojax = new ComboJax(this, params);
                var name = $(this).attr("id");
                if (name.length > 0)
                    window.pageCombojaxs[name] = combojax;

                aObj.push(combojax);
                $(this).data("combojax", combojax);
            } else {
                aObj.push($(this).data("combojax"));
            }
        });
        if (aObj.length == 1)
            return aObj[0];
        return aObj;
    }
});

ComboJax = (function ($) {
    function comboJax(container, options) {
        this.jqContainer = $(container);
        this.id = this.jqContainer.attr("id");
        options = options || {};
        this.options = $.extend({}, this.defaults(), options);
        this.init();
    }

    comboJax.prototype.init = function () {
        var $this = this;
        var combojaxContainer = this.jqContainer;
        combojaxContainer.addClass("chosen-container chosen-container-single");
        combojaxContainer.append("<input type=\"hidden\" name=\"" + this.id + "_value\" id=\"" + this.id + "_value\" />");
        combojaxContainer.append("<a class=\"chosen-single\"><span>" + this.options.noneSelectedText + "</span><div><b></b></div></a>");
        combojaxContainer.append("<div class='chosen-drop'><div class=\"chosen-search\"><input type=\"text\" autocomplete=\"off\" /></div><ul class=\"chosen-results\"><li class=\"active-result\" value=\"\">" + this.options.noneSelectedText + "</li></ul></div>");

        this.list = combojaxContainer.find("ul");
        this.container = combojaxContainer.find(".chosen-drop");
        this.button = combojaxContainer.find(".chosen-single");
        this.hiddenDataId = combojaxContainer.find("#" + this.id + "_value");
        this.key = combojaxContainer.find(".chosen-search input");

        this.loadData(this.options.skip, this);

        $(this.list).scroll(function () {
            if ($(this).scrollTop() + $(this).innerHeight() >= $(this)[0].scrollHeight) {
                $this.options.skip += $this.options.take;
                $this.loadData($this.options.skip, $this);
            }
        });
        
        $(this.button).click(function () {
            if ($(combojaxContainer).hasClass("chosen-with-drop"))
                $(combojaxContainer).removeClass("chosen-with-drop").removeClass("chosen-container-active");
            else
                $(combojaxContainer).addClass("chosen-with-drop").addClass("chosen-container-active");
        });

        $(document).mouseup(function (e) {
            if (!$(combojaxContainer).is(e.target) // if the target of the click isn't the container...
                && $(combojaxContainer).has(e.target).length === 0) // ... nor a descendant of the container
            {
                $(combojaxContainer).removeClass("chosen-with-drop").removeClass("chosen-container-active");
            }
        });

        $(this.list).find("li").hover(
            function () {
                $(this).addClass("highlighted");
            }, function () {
                $(this).removeClass("highlighted");
            }
        );

        $(this.list).find("li").click(function () {
            $this.selectIndex(0);
            $(combojaxContainer).removeClass("chosen-with-drop").removeClass("chosen-container-active");
        });

        var timeOutForLoadData = null;
        $(this.key).keydown(function (event) {
            clearTimeout(timeOutForLoadData);
            timeOutForLoadData = setTimeout(function () { $($this.list).find("li:not(:first)").remove(); $this.loadData(0, $this); },500);
            if (event.keyCode === 13) {
                clearTimeout(timeOutForLoadData);
                $($this.list).find("li:not(:first)").remove();
                $this.loadData(0, $this);
            }

            if (event.keyCode === 10 || event.keyCode === 13) {
                event.preventDefault();
            }
        });

        //$(this.key).keydown(function (event) {
        //    if (event.keyCode === 10 || event.keyCode === 13) {
        //        event.preventDefault();
        //    }
        //});

        $(this.key).focus(function () {
            $(this).select();
        });
    };

    comboJax.prototype.loadData = function (skip, $context) {
        $($context.container).block({
            message: '<p>Loading...</p>',
            css: { backgroundColor: '#fff', color: '#000' },
            fadeIn: 100,
        });

        $.getJSON($context.options.source, { skip: skip, take: $context.options.take, key: $context.key.val() }, function (response) {
            $(response).each(function () {                
                var id = this.Value;
                var name = this.Text;
                var newItem = $(document.createElement('li'))
                    .attr('value', id)
                    .text(name)
                    .addClass("active-result")
                    .appendTo($context.list);

                $(newItem).click(function () {
                    $($context.hiddenDataId).val(id);
                    $($context.list).find("li").removeClass("result-selected");
                    $(this).addClass("result-selected");
                    $($context.jqContainer).find(".chosen-single span").text(name);
                    $($context.jqContainer).removeClass("chosen-with-drop").removeClass("chosen-container-active");
                });

                $(newItem).hover(
                  function () {
                      $(this).addClass("highlighted");
                  }, function () {
                      $(this).removeClass("highlighted");
                  }
                );
            });

            $($context.container).unblock({
                fadeOut: 100
            });
        });
    };

    comboJax.prototype.selectIndex = function (index) {
        var itemSelected = $(this.list).find("li").eq(index);
        $(this.hiddenDataId).val(itemSelected.data("value"));
        $(this.list).find("li").removeClass("result-selected");
        $(itemSelected).addClass("result-selected");
        $(this.jqContainer).find(".chosen-single span").text(itemSelected.text());
        $(this).removeClass("chosen-with-drop").removeClass("chosen-container-active");
    };

    comboJax.prototype.getSelectedValue = function () {
        return $(this.hiddenDataId).val();
    };

    comboJax.prototype.getSelectedText = function () {
        return $(this.jqContainer).find(".chosen-single span").text();
    };

    comboJax.prototype.click = function (func) {        
        $(this.list).on("click", "li", function () {
            func.call();
        });
    };

    /***
    * Default Grid.Mvc options
    */
    comboJax.prototype.defaults = function () {
        return {
            source: '',
            skip: 0,
            take: 10,
            noneSelectedText: 'Vui lòng chọn'
        };
    };

    return comboJax;
})(window.jQuery);