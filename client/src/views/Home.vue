<template>
  <div class="home">
    <h2>{{homeMessage}}</h2>
    <NewCustomer/>
  </div>
</template>

<script lang="ts">
import { Options, Vue } from 'vue-class-component';
import NewCustomer from '@/components/NewCustomer.vue'; // @ is an alias to /src


@Options({
  components: {
    NewCustomer,
  },
})
export default class Home extends Vue {
  //
  homeMessage = 'Welcome!!'


}
</script>
