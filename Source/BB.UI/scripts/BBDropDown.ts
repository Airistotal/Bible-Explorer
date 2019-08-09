/* Look for any elements with the class "BBDropDown": */
class BBDropDown {
    private rootElement: HTMLElement;
    private originalSelect: HTMLSelectElement;
    private selected: HTMLDivElement;
    private optionContainer: HTMLDivElement;
    public horizontalScroll: boolean;

    constructor(context: HTMLElement) {
        if (!context.classList.contains("BBDropDown")) {
            throw new Error("This element isn't a BBDropDown!");
        }

        this.horizontalScroll = false;
        this.rootElement = context;
        this.originalSelect = <HTMLSelectElement>$(this.rootElement).children("select")[0];

        this.initSelected();
        this.initOptions();
    }
    
    public closeOptions(): any {
        $(this.optionContainer).animate({
            opacity: 0
        }, 300, function () {
            $(this).css("display", "none");
        });
    }

    private initSelected() {
        this.selected = document.createElement("div");
        $(this.selected).addClass("select-selected");
        this.selected.innerHTML = this.originalSelect.selectedOptions[0].innerHTML;
        this.selected.onclick = this.toggleOptions;
        this.rootElement.append(this.selected);
    }

    private initOptions() {
        this.optionContainer = document.createElement("div");
        this.optionContainer.onmouseenter = this.enableHorizontalScolling;
        this.optionContainer.onmouseleave = this.disableHoriztonalScrolling;
        $(this.optionContainer).addClass("BBDropDown-select-items select-hide");
        this.rootElement.append(this.optionContainer);

        var options = this.originalSelect.options;

        for (var i = 0; i < options.length; i++) {
            var currOption = document.createElement("div");
            currOption.innerHTML = options[i].innerHTML;
            currOption.onclick = this.SelectItem;
            this.optionContainer.append(currOption);
        }
    }

    private toggleOptions = (e: MouseEvent): any => {
        e.stopPropagation();

        if ($(this.optionContainer).css("display") === "none") {
            this.openOptions();
        } else {
            this.closeOptions();
        }
    }

    private SelectItem = (e: MouseEvent) => {
        /* When an item is clicked, update the original select box,
        and the selected item: */
        var target = <HTMLElement>e.target;
        var originalOptions = this.originalSelect.options;

        for (var i = 0; i < originalOptions.length; i++) {
            if (originalOptions[i].innerHTML == target.innerHTML) {
                originalOptions.selectedIndex = i;
                this.selected.innerHTML = target.innerHTML;

                var prevSelected = $(".same-as-selected", this.optionContainer);
                for (var k = 0; k < prevSelected.length; k++) {
                    prevSelected[k].removeAttribute("class");
                }

                $(target).addClass("same-as-selected");
                break;
            }
        }

        $(this.optionContainer).hide();
    }

    private openOptions() {
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
    }

    private enableHorizontalScolling = () => {
        this.horizontalScroll = true;

        if (window.addEventListener) // older FF
            window.addEventListener('DOMMouseScroll', preventDefault, false);
        window.onwheel = preventDefault; // modern standard
        window.onmousewheel = preventDefault; // older browsers, IE
        window.ontouchmove = preventDefault; // mobile
        document.onkeydown = preventDefaultForScrollKeys;

        $(this.optionContainer).on("DOMMouseScroll mousewheel", this.scrollHorizontally);
    }

    private disableHoriztonalScrolling = () => {
        this.horizontalScroll = false;

        if (window.addEventListener) // older FF
            window.addEventListener('DOMMouseScroll', null, false);
        window.onwheel = null; // modern standard
        window.onmousewheel = null; // older browsers, IE
        window.ontouchmove = null; // mobile
        document.onkeydown = null;

        $(this.optionContainer).on("DOMMouseScroll mousewheel", null);
    }

    private scrollHorizontally = (e: JQuery.ScrollEvent) => {
        var origEvent: any = e.originalEvent;
        var wheelDelta = 0;

        try {
            wheelDelta = origEvent.wheelDelta;
        } catch (e) {
            wheelDelta = origEvent.detail;
        }
        
        if (wheelDelta > 0) {
            //scroll up
            this.optionContainer.scrollLeft += 30;
        } else {
            //scroll down
            this.optionContainer.scrollLeft += -30;
        }

        return false;
    }
}

var dropdowns = new Array<BBDropDown>();

$(document).ready(function () {
    // Initialize custom dropdowns
    var selectContainers = $(".BBDropDown");
    var horizontalScroll = false;

    for (var i = 0; i < selectContainers.length; i++) {
        dropdowns.push(new BBDropDown(selectContainers[i]));
    };

    $(window).on("resize", function () {
        closeAllSelect(null);
    });

    $(document).on("click", function () { closeAllSelect(null) });

    $(".BBDropDown-select-items").mouseenter(function () {
    });

    $(".BBDropDown-select-items").mouseleave(function () {
    });
});

function closeAllSelect(elmnt: BBDropDown) {
    /* A function that will close all select boxes in the document,
    except the current select box: */
    dropdowns.forEach(function (val) {
        if (val !== elmnt) {
            val.closeOptions();
        }
    });
}

function preventDefault(e: any) {
    if (e.preventDefault)
        e.preventDefault();
    e.returnValue = false;
}

function preventDefaultForScrollKeys(e: any) {
    var keys: any = { 37: 1, 38: 1, 39: 1, 40: 1 };

    if (keys[e.keyCode]) {
        preventDefault(e);
        return false;
    }
}
