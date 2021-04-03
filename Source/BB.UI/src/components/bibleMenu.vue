<template>
  <div id="bibleMenu">
    <BB_DropDown DropDownId="selectMainBible" 
                  :ApiEndpoint="biblesUrl"
                  :InitialValue="$store.getters.getMainBible"
                  @changed="changeMainBible"/>

    <BB_DropDown DropDownId="selectCompareBible"
                  :ApiEndpoint="biblesUrl"
                  :InitialValue="$store.getters.getCompareBible"
                  @changed="changeCompareBible"/>

    <BB_DropDown DropDownId="selectBook" 
                  :ApiEndpoint="bookUrl"
                  :InitialValue="$store.getters.getBook"
                  @changed="changeBibleBook"/>

    <BB_DropDown DropDownId="selectChapter"
                  :ApiEndpoint="chaptersUrl"
                  :InitialValue="$store.getters.getChapter"
                  @changed="changeBibleChapter"/>
  </div>
</template>

<script lang="ts">
  import { Component, Vue } from 'vue-property-decorator';
  import '../assets/bibleMenu.scss';
  import BB_DropDown from './BB_DropDown.vue';

  @Component({
    components: {
      BB_DropDown
    }
  })
  export default class BibleMenu extends Vue {
    biblesUrl: string = "";
    bookUrl: string = "";
    chaptersUrl: string = "";

    created() {
      this.biblesUrl = process.env.VUE_APP_API_BASE_URL + "api/BibleMeta/GetBibles";
      this.bookUrl = process.env.VUE_APP_API_BASE_URL + "api/BibleMeta/GetBibleBooks";
      this.chaptersUrl = this.$store.getters.getBibleBookChaptersEndpoint;
    }

    changeMainBible(bibleId: any) {
      this.$store.commit('setMainBible', bibleId);
      this.$emit("navigate");
    }

    changeCompareBible(bibleId: any) {
      this.$store.commit('setCompareBible', bibleId);
      this.$emit("navigate");
    }

    changeBibleBook(book: number) {
      this.$store.commit('setBibleBook', book);
      this.chaptersUrl = this.$store.getters.getBibleBookChaptersEndpoint;
      this.$emit("navigate");
    }

    changeBibleChapter(chapter: number) {
      this.$store.commit('setBibleChapter', chapter);
      this.$emit("navigate");
    }
  }
</script>

<style>
  #bibleMenu {
    margin-top: 75px;
    margin-left: 75px;
    margin-right: 75px;
  }
</style>