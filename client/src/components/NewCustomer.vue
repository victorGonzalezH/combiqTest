<template>
  <div class="newcustomer">
    <h2>{{message}}</h2>
    <br>
        <div>
            <table>
                <tr>
                    <td class="columnLabel" > <b> Name: </b></td>
                    <td  >

                        <input 
                        type="text" 
                        required
                        v-model="newCustomer.name" />

                    </td>
                </tr>
                <tr>
                    <td > <b> Address: </b> </td>
                    <td ><input type="text" 
            required
            v-model="newCustomer.address"
            /></td>
                </tr>

            <tr>
                <td><b>Phone: </b></td>
                <td><input type="text"
                required
                v-model="newCustomer.phone"
                /></td>
            </tr>
            <tr>
                <td><b>Email: </b></td>
                <td><input type="email" 
            required
            v-model="newCustomer.email"
            /></td>
            </tr>
            <tr>
                <td><b>Age:</b></td>
                <td><input type="number" 
            required
            v-model="newCustomer.age"
            /></td>
            </tr>
            

            </table>
            
        </div>
        <br>
        
        <div>
            <button  @click="saveCustomer" class="submit" >Save customer</button>
        </div>
    
  </div>
</template>

<script lang="ts">
import { Vue } from 'vue-class-component';
import { Customer } from '../shared/models/customer-model';
import  DataService  from '../shared/services/data-service';

export default class NewCustomer extends Vue {

 message = "New customer form"

 private newCustomer: Customer = { name:'', address:'', phone:'', email:'', age: ''  };
 name = '';

/**
 * Funcion que guarda un nuevo cliente
  */ 
saveCustomer() {

    // Validaciones
   DataService.create('saver', this.newCustomer)
   .then((response) => {

        console.log(response.data);
        
      })
      .catch((e) => {
        console.log(e);
      });
}

}

</script>

<style scoped>
.submit {
  margin-top: 10px;
  padding: 10px;
  border-radius: 0px;
  border: 0px;
  background: green;
  color: white;
  font-weight: bold;
}

table {
  width: 100%;
  
}

td .columnLabel {
width: 10%;
  
}
</style>