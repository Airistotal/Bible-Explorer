<template>
  <div :id="HorizontalDropDownId" class="HorizontalDropDown">
    <div class="HorizontalDropDown-SelectButton" @click="toggleOptions"></div>

    <div class="HorizontalDropDown-SelectOptions DisplayNone"
         @mouseover="enableHorizontalScolling()"
         @mouseleave="disableHoriztonalScrolling()">
      <div v-for="option in optionData" :value="option.Value" @click="selectItem(option.Text)">
        {{option.Text}}
      </div>
    </div>

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
    scrollPosition: number = 0;

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
            try {
              $(this.selectButtonSelector).html(this.optionData[0].Text);
            }
            catch (e)
            { }
          } catch (e) {
            alert(e);
          }
        }).catch((err: AxiosError) => {
          alert(JSON.stringify(err));
        });
    }

    private selectItem(selectText: string) {
      var origOptions = $(this.hiddenSelectSelector + " option");

      var prevSelected = $(".same-as-selected", $(this.selectOptionsSelector));
      for (var i = 0; i < prevSelected.length; i++) {
        prevSelected[i].removeAttribute("class");
      }

      for (var i = 0; i < origOptions.length; i++) {
        if (origOptions[i].innerHTML == selectText) {
          $(this.selectButtonSelector).html(selectText);
          $(origOptions[i]).addClass("same-as-selected");
          $(this.selectOptionsSelector).hide();

          break;
        }
      }
    }

    private closeOptions() {
      $(".HorizontalDropDown-SelectOptions").css("opacity", 0);
      $(".HorizontalDropDown-SelectOptions").addClass("DisplayNone");
    }

    private openOptions() {
      this.closeOptions();
      $(this.selectOptionsSelector).removeClass("DisplayNone");

      let parent = $($(this.selectOptionsSelector)).parent();
      let parentOffset = parent.offset();
      let parentHeight = parent.height();

      if (parentOffset !== undefined && parentHeight !== undefined) {
        $(this.selectOptionsSelector).css("top", parentHeight + "px");
        $(this.selectOptionsSelector).css("left", -parentOffset.left + "px");
      }
      
      let bodyWidth = $("body").width();
      if (bodyWidth !== undefined) {
        $(this.selectOptionsSelector).css("max-width", bodyWidth + "px");
      }

      $(this.selectOptionsSelector).animate({
        opacity: 1
      }, 300);
    }

    private toggleOptions() {
      if ($(this.selectOptionsSelector).hasClass("DisplayNone")) {
        this.openOptions();
      } else {
        this.closeOptions();
      }
    }

    private enableHorizontalScolling() {
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

    private disableHoriztonalScrolling() {
      if (window.addEventListener) { // older FF
        window.removeEventListener('DOMMouseScroll', this.preventDefault);
      }

      window.onwheel = null; // modern standard
      window.onmousewheel = null; // older browsers, IE
      window.ontouchmove = null; // mobile
      document.onkeydown = null;

      $(this.selectOptionsSelector).on('DOMMouseScroll mousewheel', () => null);
    }

    private scrollHorizontally(e: JQuery.ScrollEvent) {
      var origEvent: any = e.originalEvent;
      var wheelDelta = 0;
      try {
        wheelDelta = origEvent.wheelDelta;
      } catch (e) {
        wheelDelta = origEvent.detail;
      }

      if (wheelDelta > 0) {
        this.scrollPosition = this.scrollPosition + 30;
      } else {
        if (this.scrollPosition > 0) {
          this.scrollPosition = this.scrollPosition - 30;
        }
      }

      $(this.selectOptionsSelector)[0].scrollLeft = this.scrollPosition;

      return false;
    }

    private preventDefault(e: any) {
      if (e.preventDefault)
        e.preventDefault();
      e.returnValue = false;
    }

    private preventDefaultForScrollKeys(e: any) {
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
    overflow-x: auto;
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
