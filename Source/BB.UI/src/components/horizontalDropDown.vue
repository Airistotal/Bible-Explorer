<template>
    <div :id="HorizontalDropDownId" class="HorizontalDropDown">
        <div class="HorizontalDropDown-SelectButton"></div>
        <div class="HorizontalDropDown-SelectOptions DisplayNone"></div>
        <select class="HorizontalDropDown-HiddenData">
            <option v-for="option in optionData" :value="option.Value">{{option.Text}}</option>
        </select>
    </div>
</template>

<script lang="ts">
    import { Component, Vue, Prop, Watch } from 'vue-property-decorator';
    import { AxiosResponse, AxiosError } from 'axios';
    import $ from 'jquery';
    import '../assets/bibleMenu.scss';

    @Component({})
    export default class HorizontalDropDown extends Vue {
        @Prop(String) ApiEndpoint!: string;
        @Prop(String) ApiQueryString!: string;
        @Prop(String) HorizontalDropDownId!: string;
        
        optionData: DropDownData[] = [];

        hiddenSelectSelector!: string;
        selectButtonSelector!: string;
        selectOptionsSelector!: string;
        isInitialized: boolean = false;

        created() {
            this.hiddenSelectSelector = "#" + this.HorizontalDropDownId + " .HorizontalDropDown-HiddenData";
            this.selectButtonSelector = "#" + this.HorizontalDropDownId + " .HorizontalDropDown-SelectButton";
            this.selectOptionsSelector = "#" + this.HorizontalDropDownId + " .HorizontalDropDown-SelectOptions";

            if (this.ApiQueryString === undefined) this.ApiQueryString = "";

            this.axios
                .get(this.ApiEndpoint + this.ApiQueryString)
                .then((response: AxiosResponse<DropDownData[]>) => {
                    try {
                        this.optionData = response.data;
                        this.initSelectOptions();
                        this.initSelectButton();
                    } catch (e) {
                        alert(e);
                    }
                }).catch((err: AxiosError) => {
                    alert(JSON.stringify(err));
                });
        }
        
        updated() {
            if (!this.isInitialized) {
                this.initSelectOptions();
                this.initSelectButton();

                this.isInitialized = true;
            }
        }
        
        private initSelectButton() {
            $(this.selectButtonSelector).html($(this.hiddenSelectSelector).first().html());
            ((toggleOptions: any) => {
                $(this.selectButtonSelector).click(function (e) {
                    toggleOptions(e);
                })
            })(this.toggleOptions);
        }

        private initSelectOptions() {
            ((enableHorizontalScolling: any) => {
                $($(this.selectOptionsSelector)).mouseenter(function () {
                    enableHorizontalScolling();
                })
            })(this.enableHorizontalScolling);

            ((disableHoriztonalScrolling: any) => {
                $(this.selectOptionsSelector).mouseenter(function () {
                    disableHoriztonalScrolling();
                })
            })(this.disableHoriztonalScrolling);

            var options = $(this.hiddenSelectSelector + " option");
            for (var i = 0; i < options.length; i++) {
                let option = options[i];

                var optionDiv = document.createElement("div");
                optionDiv.innerHTML = option.innerHTML;
                optionDiv.onclick = this.SelectItem;
                $(this.selectOptionsSelector).append(optionDiv);
            }
        };

        private SelectItem = (e: MouseEvent) => {
            /* When an item is clicked, update the original select box,
            and the selected item: */
            var target = e.target as HTMLElement;
            var origOptions = $(this.hiddenSelectSelector + " option");

            for (var i = 0; i < origOptions.length; i++) {
                if (origOptions[i].innerHTML == target.innerHTML) {
                    //origOptions.selectedIndex = i;
                    $(this.selectButtonSelector).html(target.innerHTML);

                    var prevSelected = $(".same-as-selected", $(this.selectOptionsSelector));
                    for (var k = 0; k < prevSelected.length; k++) {
                        prevSelected[k].removeAttribute("class");
                    }

                    $(target).addClass("same-as-selected");
                    break;
                }
            }

            $(this.selectOptionsSelector).hide();
        }

        private closeOptions(): any {
            $(this.selectOptionsSelector).animate({
                opacity: 0
            }, 300, function () {
                $(this).css("display", "none");
            });
        }

        private openOptions() {
            let bodyWidth = $("body").width();
            let offset = $($(this.selectOptionsSelector)).offset();

            if (bodyWidth === undefined || offset === undefined) return;

            offset.left = 0;
            $(this.selectOptionsSelector).css("opacity", 0);
            $(this.selectOptionsSelector).css("display", "block");
            $(this.selectOptionsSelector).css("width", bodyWidth);
            $(this.selectOptionsSelector).offset(offset);
            $(this.selectOptionsSelector).animate({
                opacity: 1
            }, 300);
        }

        private toggleOptions = (e: MouseEvent): any => {
            e.stopPropagation();

            if ($(this.selectOptionsSelector).css("display") === "none") {
                this.openOptions();
            } else {
                this.closeOptions();
            }
        }

        private enableHorizontalScolling = () => {
            if (window.addEventListener) {
                // older FF
                window.addEventListener('DOMMouseScroll', this.preventDefault, false);
            }

            window.onwheel = this.preventDefault; // modern standard
            window.onmousewheel = this.preventDefault; // older browsers, IE
            window.ontouchmove = this.preventDefault; // mobile
            document.onkeydown = this.preventDefaultForScrollKeys;

            $(this.selectOptionsSelector).on('DOMMouseScroll mousewheel', (e: any) => this.scrollHorizontally(e));
        }

        private disableHoriztonalScrolling = () => {
            if (window.addEventListener) { // older FF
                window.removeEventListener('DOMMouseScroll', this.preventDefault);
            }

            window.onwheel = null; // modern standard
            window.onmousewheel = null; // older browsers, IE
            window.ontouchmove = null; // mobile
            document.onkeydown = null;

            $(this.selectOptionsSelector).on('DOMMouseScroll mousewheel', () => null);
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
                $(this.selectOptionsSelector).scrollLeft(30);
            } else {
                //scroll down
                $(this.selectOptionsSelector).scrollLeft(-30);
            }

            return false;
        }

        private preventDefault = (e: any) => {
            if (e.preventDefault)
                e.preventDefault();
            e.returnValue = false;
        }

        private preventDefaultForScrollKeys = (e: any) => {
            var keys: any = { 37: 1, 38: 1, 39: 1, 40: 1 };

            if (keys[e.keyCode]) {
                this.preventDefault(e);
                return false;
            }
        }
    }
</script>

<style>
    .HorizontalDropDown {
        position: relative;
        display: inline-block;
        font-family: Arial;
        width: 65px;
        height: 65px;
        margin: 5px;
        cursor: pointer;
    }

        .HorizontalDropDown select {
            display: none;
            /*hide original SELECT element: */
        }

    /* Hide the items when the select box is closed: */
    .DisplayNone {
        display: none;
    }

    .DisplayInlineBolck > div {
        display: inline-block;
        opacity: 1;
    }

    /* Styles the selected item*/
    .HorizontalDropDown-SelectButton {
        background-color: DodgerBlue;
        height: 65px;
        width: 65px;
        line-height: 65px;
        border-radius: 35px;
        text-align: center;
        color: #ffffff;
    }

    /* Style items (options): */
    .HorizontalDropDown-SelectOptions {
        position: absolute;
        background-color: DodgerBlue;
        margin-top: 10px;
        top: 100%;
        z-index: 99;
        overflow-x: scroll;
        overflow-y: hidden;
        white-space: nowrap;
        /* style the items (options): */
    }

        .HorizontalDropDown-SelectOptions div {
            color: #ffffff;
            padding: 15px 20px;
            border: 1px solid transparent;
            border-color: transparent rgba(0, 0, 0, 0.1) transparent transparent;
            cursor: pointer;
            display: inline-block;
        }

            .HorizontalDropDown-SelectOptions div:hover, .BEDropDown-select-items .same-as-selected {
                background-color: rgba(0, 0, 0, 0.1);
                border: none;
            }
</style>
