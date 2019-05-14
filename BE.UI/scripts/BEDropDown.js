/* Look for any elements with the class "BEDropDown": */
var BEDropDown = /** @class */ (function () {
    function BEDropDown(context) {
        var _this = this;
        this.toggleOptions = function (e) {
            e.stopPropagation();
            if ($(_this.optionContainer).css("display") === "none") {
                _this.openOptions();
            }
            else {
                _this.closeOptions();
            }
        };
        this.SelectItem = function (e) {
            /* When an item is clicked, update the original select box,
            and the selected item: */
            var target = e.target;
            var originalOptions = _this.originalSelect.options;
            for (var i = 0; i < originalOptions.length; i++) {
                if (originalOptions[i].innerHTML == target.innerHTML) {
                    originalOptions.selectedIndex = i;
                    _this.selected.innerHTML = target.innerHTML;
                    var prevSelected = $(".same-as-selected", _this.optionContainer);
                    for (var k = 0; k < prevSelected.length; k++) {
                        prevSelected[k].removeAttribute("class");
                    }
                    $(target).addClass("same-as-selected");
                    break;
                }
            }
            $(_this.optionContainer).hide();
        };
        if (!context.classList.contains("BEDropDown")) {
            throw new Error("This element isn't a BEDropDown!");
        }
        this.rootElement = context;
        this.originalSelect = $(this.rootElement).children("select")[0];
        this.initSelected();
        this.initOptions();
    }
    BEDropDown.prototype.closeOptions = function () {
        $(this.optionContainer).animate({
            opacity: 0
        }, 300, function () {
            $(this).css("display", "none");
        });
    };
    BEDropDown.prototype.initSelected = function () {
        this.selected = document.createElement("div");
        $(this.selected).addClass("select-selected");
        this.selected.innerHTML = this.originalSelect.selectedOptions[0].innerHTML;
        this.selected.onclick = this.toggleOptions;
        this.rootElement.append(this.selected);
    };
    BEDropDown.prototype.initOptions = function () {
        this.optionContainer = document.createElement("div");
        $(this.optionContainer).addClass("BEDropDown-select-items select-hide");
        this.rootElement.append(this.optionContainer);
        var options = this.originalSelect.options;
        for (var i = 0; i < options.length; i++) {
            var currOption = document.createElement("div");
            currOption.innerHTML = options[i].innerHTML;
            currOption.onclick = this.SelectItem;
            this.optionContainer.append(currOption);
        }
    };
    BEDropDown.prototype.openOptions = function () {
        closeAllSelect(this);
        $(this.optionContainer).css("opacity", 0);
        $(this.optionContainer).css("display", "block");
        $(this.optionContainer).css("width", $("body").width());
        var offset = $(this.optionContainer).offset();
        offset.left = 0;
        $(this.optionContainer).offset(offset);
        $(this.optionContainer).animate({
            opacity: 1
        }, 300);
    };
    return BEDropDown;
}());
var dropdowns = new Array();
$(document).ready(function () {
    // Initialize custom dropdowns
    var selectContainers = $(".BEDropDown");
    for (var i = 0; i < selectContainers.length; i++) {
        dropdowns.push(new BEDropDown(selectContainers[i]));
    }
    ;
    $(window).on("resize", function () {
        closeAllSelect(null);
    });
    $(document).on("click", function () { closeAllSelect(null); });
    $(".BEDropDown-select-items").mouseenter(function () {
        if (window.addEventListener) // older FF
            window.addEventListener('DOMMouseScroll', preventDefault, false);
        window.onwheel = preventDefault; // modern standard
        window.onmousewheel = preventDefault; // older browsers, IE
        window.ontouchmove = preventDefault; // mobile
        document.onkeydown = preventDefaultForScrollKeys;
    });
    window.onscroll = function (e) {
        e.stopPropagation();
    };
});
function closeAllSelect(elmnt) {
    /* A function that will close all select boxes in the document,
    except the current select box: */
    dropdowns.forEach(function (val) {
        if (val !== elmnt) {
            val.closeOptions();
        }
    });
}
var keys = { 37: 1, 38: 1, 39: 1, 40: 1 };
function preventDefault(e) {
    if (e.preventDefault)
        e.preventDefault();
    e.returnValue = false;
}
function preventDefaultForScrollKeys(e) {
    if (keys[e.keyCode]) {
        preventDefault(e);
        return false;
    }
}
