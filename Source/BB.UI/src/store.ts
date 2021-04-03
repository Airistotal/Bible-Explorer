import Vue from 'vue';
import Vuex from 'vuex';

Vue.config.devtools = true;
Vue.use(Vuex);

export const store = new Vuex.Store({
  state: {
    mainBible: 2,
    compareBible: 1,
    book: 1,
    chapter: 1
  },
  mutations: {
    setMainBible(state, bibleID: number) {
      state.mainBible = bibleID;
    },
    setCompareBible(state, bibleID: number) {
      state.compareBible = bibleID;
    },
    setBibleBook(state, book: number) {
      state.book = book;
    },
    setBibleChapter(state, chapter: number) {
      state.chapter = chapter;
    }
  },
  getters: {
    getMainBible: state => {
      return state.mainBible;
    },
    getCompareBible: state => {
      return state.compareBible;
    },
    getBook: state => {
      return state.book;
    },
    getChapter: state => {
      return state.chapter;
    },
    getBibleCompareEndpoint: state => {
      return `./api/BibleComparer?` +
        `mainBible=${state.mainBible}` +
        `&book=${state.book}` +
        `&chapter=${state.chapter}` +
        `&compareBible=${state.compareBible}`;
    },
    getBibleBookChaptersEndpoint: state => {
      return `./api/BibleMeta/GetChaptersForBookInBible?` +
        `bible=${state.mainBible}` +
        `&book=${state.book}`;
    }
  }
});
