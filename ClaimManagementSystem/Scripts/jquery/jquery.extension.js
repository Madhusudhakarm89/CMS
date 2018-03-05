(function ($) {
    $.fn.extend({
        serializeFormToJSON: function () {
            var o = {};
            var a = this.serializeArray();
            $.each(a, function () {
                if (o[this.name] !== undefined) {
                    if (!o[this.name].push) {
                        o[this.name] = [o[this.name]];
                    }
                    o[this.name].push(this.value || '');
                } else {
                    o[this.name] = this.value || '';
                }
            });
            return o;
        },

        clearValidationErrors: function () {
            $(this).each(function () {
                $(this).trigger('reset.unobtrusiveValidation');
            });
        },

        renderHtmlToImage: function (options) {
            // Set defaults for the options
            var settings = $.extend({
                width: this.width,
                height: this.height
            }, options);

            return this.canvas({
                width: settings.width,
                height: settings.height
            });
        },

        showRemainingCharacter: function (options) {
            $(this).each(function (index, elem) {
                $(elem).after("<span id=spnCharacterCount" + $(elem)[0].id + " style='display:none;'></span>");
                $(elem).on("change keyup input paste focus", function () {
                    countCharacters(elem);
                });
            });

            //display characters left for max length elements - start
            function countCharacters(control) {
                $(control).next("span[id=spnCharacterCount" + control.id + "]").each(function () {
                    var maxLength = control.maxLength;
                    var txtLength = control.value.length;
                    if (txtLength > 0) {
                        $(this).attr("style", "display:block;");
                        $(this)[0].innerText = "Character(s) left: " + (parseInt(maxLength) - txtLength);
                    }
                    else {
                        $(this).attr("style", "display:none;");
                        $(this)[0].innerText = "";
                    }
                });
            }
            //display characters left for max length elements - end
        }
    });

    $.fn.serializeFormToJSON = function () {
        var o = {};
        var a = this.serializeArray();
        $.each(a, function () {
            if (o[this.name] !== undefined) {
                if (!o[this.name].push) {
                    o[this.name] = [o[this.name]];
                }
                o[this.name].push(this.value || '');
            } else {
                o[this.name] = this.value || '';
            }
        });
        return o;
    };

    $.fn.clearValidationErrors = function () {
        $(this).each(function () {
            $(this).trigger('reset.unobtrusiveValidation');
        });
    };

    $.fn.renderHtmlToImage = function (options) {
        // Set defaults for the options
        var settings = $.extend({
            width: this.width,
            height: this.height
        }, options);

        return this.canvas({
            width: settings.width,
            height: settings.height
        });
    };

    $.fn.showRemainingCharacter = function (options) {
        $(this).each(function (index, elem) {
            $(elem).after("<span id=spnCharacterCount" + $(elem)[0].id + " style='display:none;'></span>");
            $(elem).on("change keyup input paste focus", function () {
                countCharacters(elem);
            });
        });

        //display characters left for max length elements - start
        function countCharacters(control) {
            $(control).next("span[id=spnCharacterCount" + control.id + "]").each(function () {
                var maxLength = control.maxLength;
                var txtLength = control.value.length;
                if (txtLength > 0) {
                    $(this).attr("style", "display:block;");
                    $(this)[0].innerText = "Character(s) left: " + (parseInt(maxLength) - txtLength);
                }
                else {
                    $(this).attr("style", "display:none;");
                    $(this)[0].innerText = "";
                }
            });
        }
        //display characters left for max length elements - end
    }
})(jQuery);