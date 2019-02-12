<template>
  <div>
    <h1>Lets Yeet this Wheat</h1>
      <div>
        <input
          type="text"
          v-model="todo"
          placeholder="Enter a Todo"
          @keypress.enter="addTodo(todo)"
        >
      
      <button type="button" v-on:click="addTodo(todo)">Add</button>
      </div>
    <template v-if="todos.length === 0">
      <h3>Try adding a new todo!</h3>
    </template>
    <template v-else>
      <div>
        <h3>Things I must do...</h3>
          <div v-for="(todo, index) in todos" :key="index">
            <h5>{{ index+1 }}. {{ todo }}
            <button type="button" v-on:click="editTodo()">Edit</button>
            <button type="button" v-on:click="deleteTodo(index)">Delete</button>
            </h5>
          </div>
      </div>
      <button type="button" v-on:click="clearTodos()">Clear Todos</button>
    </template>
    <!-- <template v-if="completed">
      <h2>TODOS ACCOMPLISHED</h2>
    </template>
    <template v-else>
      <h4> Let's complete some todos! :) </h4>
    </template>-->
  </div>
</template>

<script>
export default {
  name: "TodoList",
  data() {
    return {
      todo: "",
      todos: []
    }
  },
  methods: {
    addTodo(todo) {
      this.todos.push(todo)
      this.todo = ""
    },
    editTodo() {
      // TODO -- Create an editable todo
    },
    completeTodo(todo) {
      // TODO -- Implement a Completed Todo section
    },
    deleteTodo(index) {
      this.todos.splice(index, 1);
    },
    clearTodos() {
      this.todos = [];
    }
  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h1,
h2 {
  font-weight: normal;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
.todos {
  justify-content: left;
}
</style>
