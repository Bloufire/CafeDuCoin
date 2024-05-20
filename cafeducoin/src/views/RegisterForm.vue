<template>
  <div class="login-page">
    <h1 class="title">Register</h1>
    <form @submit.prevent="register" class="login-form">
        <div class="form-group">
            <label for="username">Username:</label>
            <input id="username" type="text" v-model="username" required>
        </div>
        <div class="form-group">
            <label for="email">Email:</label>
            <input id="email" type="email" v-model="email" required>
        </div>
        <div class="form-group">
            <label for="password">Password:</label>
            <input id="password" type="password" v-model="password" required>
        </div>
        <div class="form-group">
            <label for="confirmPassword">Confirm Password:</label>
            <input id="confirmPassword" type="password" v-model="confirmPassword" required>
        </div>
        <button type="submit">Register</button>
        <router-link to="/login" class="register-button">Back</router-link>
    </form>
  </div>
</template>

<script>
import axios from '../services/axios';

export default {
  data() {
    return {
      username: '',
      email: '',
      password: '',
      confirmPassword: ''
    };
  },
  methods: {
    async register() {
        try {
          const response = await axios.post('/users/register', {
              username: this.username,
              email: this.email,
              password: this.password,
              confirmPassword: this.confirmPassword
          });
          // Handle successful login (e.g., store token, redirect user)
          console.log(response.data);

          this.$router.push('/login');
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

.login-button {
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
