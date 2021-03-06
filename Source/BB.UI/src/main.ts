import Vue from 'vue';
import App from './App.vue';
import Axios from 'axios';
import VueAxios from 'vue-axios';
import { store } from './store';

//Vue.config.productionTip = true;
Vue.config.devtools = true;

Vue.use(VueAxios, Axios);

new Vue({
  store,
  render: h => h(App)
}).$mount('#app');
