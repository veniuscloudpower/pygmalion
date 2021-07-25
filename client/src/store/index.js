import Vue from "vue";
import Vuex from "vuex";
import { auth } from "./modules/auth";
// import 'es6-promise/auto';
import layout from './modules/layout'
import menu from './modules/menu'
Vue.use(Vuex);

export const store = new Vuex.Store({
    state: {
    },
    mutations: {
    },
    actions: {
    },
    modules: {
      layout,
      menu,
      auth
    }
});

