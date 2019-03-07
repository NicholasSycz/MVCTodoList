<template>
  <div>
    <h1 class="title">Todo List</h1>
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
        <div class="container" v-for="(todo, index) in todos" :key="index">
          <table>
            <tr>
              <td class="complete-box">
                <input
                  type="checkbox"
                  :checked="todo.IsComplete"
                  v-on:change="completeTodo"
                  unchecked
                >
              </td>
              <td class="todos">{{ todo.name }}</td>
              <td class="btn">
                <button class="btn edit" type="button" v-on:click="editable(todo)">Edit</button>
                <button class="btn delete" type="button" v-on:click="deleteTodo(todo.id)">Delete</button>
              </td>
            </tr>
          </table>
        </div>
      </div>
      <button type="button" v-on:click="clearTodos(todo.id)">Clear Todos</button>
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
  // methods for adding, editing, removing todos.
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
    editable(todo) {
      li.setAttribute("isEditable", true);
      li.setAttribute("id", todo.id);
      li.addEventListener("blur", editTodo);
      li.append(li);
    },
    /*
     ******** BROKEN ********
     */
    editTodo(todoId) {
      fetch(`http://localhost:3000/api/todo/${todoId}`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json"
        },
        body: JSON.stringify({ id: todoId })
          .then(resp => resp.json())
          .then(editTodo => {
            if (editing) {
              this.todo === editedTodo;
            }
          })
      });
    },
    completeTodo(todoId) {
      fetch(`http://localhost:3000/api/todo/${todoId}`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json"
        },
        body: JSON.stringify({
          id: todoId
        })
      })
        .then(resp => resp.json())
        .then(completedTodo => {
          if (todoId.IsComplete) {
            todo.isComplete === true;
          }
        })
        .catch(error => {
          console.log(error);
        });
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
      for(let i = 0; i < this.todos.length; i++) {
        fetch(`http://localhost:3000/api/todo/${this.todos[i].id}`, {
          method: "DELETE",
          headers: {
            "Content-Type": "application/json"
          },
          body: JSON.stringify({ id: this.todos[i].id })
        })
        .then(resp => resp.text())
        .then(deleteAll => {
          let newTodosState = this.todos.filter(todo => {
            return this.todo.id === this.todos[i].id;
          });
          this.todos = newTodosState;
        })
      }
      //this.todos = [];
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

.title {
  color: black;
}

.todos {
  text-align: left;
  width: 75%;
}

.container {
  text-align: center;
  display: inline-block;
}
.complete-box {
  padding: 0px 5px 0px 5px;
}
:checked {
  text-decoration: line-through;
}

.btn {
  float: right;
}
</style>
