<template>
  <div class="login-page">
    <h1 class="title">Login</h1>
    <form @submit.prevent="login" class="login-form">
        <div class="form-group">
            <label for="username">Username:</label>
            <input id="username" type="text" v-model="username" required>
        </div>
        <div class="form-group">
            <label for="password">Password:</label>
            <input id="password" type="password" v-model="password" required>
        </div>
        <button type="submit">Login</button>
        <router-link to="/register" class="register-button">Register</router-link>
    </form>
  </div>
</template>

<script>
import axios from '../services/axios';

export default {
  data() {
    return {
      username: '',
      password: ''
    };
  },
  methods: {
    async login() {
        try {
            const response = await axios.post('/users/login', {
                username: this.username,
                password: this.password
            });
            // Handle successful login (e.g., store token, redirect user)
            console.log(response.data);

            const token = response.data.token;
            localStorage.setItem('token', token);
            localStorage.setItem('userName', this.username);

            this.$router.push('/games');
        } catch (error) {
            // Handle login error (e.g., display error message)
            console.error(error);
        }
    }
  }
};
</script>

<style>
.login-page {
  text-align: center;
}

.title {
  font-size: 24px; /* Adjust the font size as needed */
  margin-bottom: 20px;
}

.login-form {
  display: inline-block;
  text-align: left;
}

.form-group {
  margin-bottom: 15px;
}

.register-button {
    margin-left: 10px;
}

label {
  display: block;
}

input {
  width: 100%;
  padding: 8px;
  box-sizing: border-box;
  margin-top: 5px;
  margin-bottom: 5px;
}

button {
  padding: 10px 20px;
  background-color: #007bff;
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
}

button:hover {
  background-color: #0056b3;
}
</style>
