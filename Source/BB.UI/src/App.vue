<template>
  <div id="app">
    <BibleMenu @navigate="navigate"></BibleMenu>
    <div id="bibleContent">
      <template v-for="verse in verses">
        <sup>{{verse.Verse}}</sup>
        <template v-for="word in verse.ComparedWords">
          <div class="Word"
               v-bind:class="{ HighlightStart: word.IsBeginning, HighlightEnd: word.IsEnd, Highlight: word.Difference !== null }">
            {{word.MainWord}}
          </div>
        </template>
      </template>
    </div>
  </div>
</template>

<script lang="ts">
  import { Component, Vue } from 'vue-property-decorator';
  import { AxiosResponse, AxiosError } from 'axios';
  import BibleMenu from './components/bibleMenu.vue';

  @Component({
    components: {
      BibleMenu
    }
  })
  export default class App extends Vue {
    verses: ComparedBibleVerse[] = [];

    created() {
      this.navigate();
    }

    navigate() {
      this.axios
        .get(this.$store.getters.getBibleCompareEndpoint)
        .then((response: AxiosResponse<ComparedBibleVerse[]>) => {
          try {
            this.verses = response.data;
          } catch (e) {
            alert(e);
          }
        }).catch((err: AxiosError) => {
          alert(JSON.stringify(err));
        });
    }
  }
</script>

<style>
  body {
    text-align: center;
    margin: 0;
    padding: 0;
    width: 100%;
  }

  sup { 
    margin-left: 5px;
  }

  .Word {
    display: inline-block;
    padding-left: 2px;
    padding-right: 2px;
    line-height: 23px;
    margin-bottom: 5px;
  }

  .Highlight {
    border-top: 1px solid blue;
    border-bottom: 1px solid blue;
    background-color: powderblue;
  }

  .HighlightStart {
    border-left: 1px solid blue;
  }

  .HighlightEnd {
    border-right: 1px solid blue;
  }

  #bibleContent {
    margin: 75px;
    padding: 50px;
    box-shadow: 0px 0px 7px #AEAEAE;
    text-align: left;
  }
</style>
