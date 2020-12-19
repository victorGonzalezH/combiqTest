import { createApp } from 'vue'
import VueCompositionAPI from '@vue/composition-api'
import App from './App.vue'
import router from './router'
import store from './store'



createApp(App).use(store).use(router).mount('#app')
