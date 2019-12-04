<template>
    <div id="bibleMenu">
        <HorizontalDropDown ApiEndpoint="./api/BibleMeta/getBibles" 
                            HorizontalDropDownId="selectMainBible" />

        <HorizontalDropDown ApiEndpoint="./api/BibleMeta/getBibles"
                            HorizontalDropDownId="selectCompareBible" />

        <HorizontalDropDown ApiEndpoint="./api/BibleMeta/getBibleBooks" 
                            HorizontalDropDownId="selectBook" />

        <HorizontalDropDown ApiEndpoint="./api/BibleMeta/GetChaptersForBookInBible" 
                            ApiQueryString="?bible=2&book=1"
                            HorizontalDropDownId="selectChapter" />
    </div>
</template>

<script lang="ts">
    import { Component, Vue } from 'vue-property-decorator';
    import '../assets/bibleMenu.scss';
    import HorizontalDropDown from './horizontalDropDown.vue';

    @Component({
        components: {
            HorizontalDropDown
        }
    })
    export default class BibleMenu extends Vue {}
</script>

<style>
  #bibleMenu {
    margin-top: 75px;
    margin-left:75px;
    margin-right:75px;
  }
</style>