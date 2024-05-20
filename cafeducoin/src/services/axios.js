// Import the axios library for making HTTP requests
import axios from 'axios';

// Create an axios instance with default configuration
const instance = axios.create({
    // Base URL for all requests made with this instance
    baseURL: 'http://localhost:5000',
    // Default headers for all requests
    headers: {
        'Content-Type': 'application/json',
    },
});

// Add a request interceptor to the axios instance
instance.interceptors.request.use(config => {
    // Retrieve the token from localStorage
    const token = localStorage.getItem('token');
    // If the token exists, set the Authorization header
    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
});

export default instance;