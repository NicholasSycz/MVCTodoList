<template>
  <div>
    <h1>Lets Yeet this Wheat</h1>
    <div>
      <input type="text" v-model="todo" placeholder="Enter a Todo" @keypress.enter="addTodo(todo)">
      
      <button type="button" v-on:click="addTodo(todo)">Add</button>
    </div>
    <template v-if="todos.length === 0">
      <h3>Try adding a new todo!</h3>
    </template>
    <template v-else>
      <div>
        <h3>Things I must do...</h3>
        <div v-for="(todo, index) in todos" :key="index">
          <h5>
            {{ index+1 }}. {{ todo.name }}
            <button type="button" v-on:click="editTodo()">Edit</button>
            <button type="button" v-on:click="deleteTodo(todo.id)">Delete</button>
          </h5>
        </div>
      </div>
      <button type="button" v-on:click="clearTodos()">Clear Todos</button>
    </template>
  </div>
</template>

<script>
export default {
  name: "TodoList",
  data() {
    return {
      todo: "",
      todos: []
    };
  },
  mounted() {
    fetch("http://localhost:3000/api/todo")
      .then(resp => resp.json())
      .then(data => {
        this.todos = data;
      })
      .catch(error => {
        alert("error");
      });
  },
  methods: {
    addTodo(todo) {
      fetch("http://localhost:3000/api/todo", {
        method: "POST",
        headers: {
          "Content-Type": "application/json"
        },
        body: JSON.stringify({ Name: todo, IsComplete: false })
      })
        .then(resp => resp.json())
        .then(data => {
          this.todos.push(data);
          this.todo = "";
        })
        .catch(error => {
          console.log(error);
        });
    },
    editTodo() {
      // TODO -- Create an editable todo
    },
    completeTodo(todo) {
      // TODO -- Implement a Completed Todo section
    },
    deleteTodo(todoId) {
      fetch(`http://localhost:3000/api/todo/${todoId}`, {
        method: "DELETE",
        headers: {
          "Content-Type": "application/json"
        },
        body: JSON.stringify({ id: todoId })
      })
        .then(resp => resp.text())
        .then(deletedTodo => {
          let newTodosState = this.todos.filter(todo => {
            return todo.id !== todoId;
          });
    
          this.todos = newTodosState;
        })
        .catch(error => {
          console.log(error);
        });
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
