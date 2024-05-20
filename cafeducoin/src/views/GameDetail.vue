<template>
    <div class="game-detail">
        <h1>{{ game.name }}</h1>
        <p>{{ game.description }}</p>
        <h2>Borrowing History</h2>
        <button @click="borrowGame" v-if="!borrowed">Borrow</button>
        <button @click="returnGame" v-if="borrowed && isCurrentUserBorrower">Return</button>
        <div class="table-container">
            <table>
                <thead>
                    <tr>
                        <th>Borrower</th>
                        <th>Date Borrowed</th>
                        <th>Date Returned</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="loan in loans" :key="loan.id">
                        <td>{{ loan.user.login }}</td>
                        <td>{{ new Date(loan.loanDate).toLocaleString() }}</td>
                        <td v-if="loan.loanReturnDate">{{ new Date(loan.loanReturnDate).toLocaleString() }}</td>
                        <td v-else>Not returned yet</td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</template>

<script>
// Import the axios instance configured with base URL and interceptors
import axios from '../services/axios';

export default {
    props: ['id'],
    data() {
        return {
            game: {},
            loans: [],
            borrowed: false,
            owner: null
        };
    },
    async created() {
        try {
            // Fetch game details from the server using the game ID prop
            const response = await axios.get(`/games/${this.id}`);
            this.game = response.data;
            // Fetch loan history of the game
            const loanResponse = await axios.get(`/games/${this.id}/history`);
            this.loans = loanResponse.data;
            // Determine if the game is currently borrowed by checking if any loan has no return date
            this.borrowed = this.loans.some(loan => !loan.loanReturnDate);
        } catch (error) {
            console.error(error);
        }
    },
    methods: {
        // Method to borrow the game
        async borrowGame() {
            try {
                const gameName = this.game.name;

                const token = localStorage.getItem('token'); // Retrieve the token from localStorage
                console.log(token);
                const config = {
                    headers: {
                        'Authorization': `Bearer ${token}` // Set the Authorization header with the token
                    }
                };

                // Send a PUT request to borrow the game
                await axios.put(`/loans/manage/${gameName}`, null, config);
                this.$router.go(); // Refresh the page
            } catch (error) {
                console.error(error);
            }
        },
        async returnGame() {
            try {
                const gameName = this.game.name;

                const token = localStorage.getItem('token'); // Retrieve the token from localStorage
                const config = {
                    headers: {
                        'Authorization': `Bearer ${token}` // Set the Authorization header with the token
                    }
                };

                // Send a PUT request to return the game
                await axios.put(`/loans/manage/${gameName}`, null, config);
                this.$router.go(); // Refresh the page
            } catch (error) {
                console.error(error);
            }
        }
    },
    computed: {
        // Computed property to check if the current user is the borrower
        isCurrentUserBorrower() {
            // Get the username of the current user from localStorage
            const currentUser = localStorage.getItem('userName');

            // Check if the current user is the borrower of the last loan
            const lastLoan = this.loans[0];
            return lastLoan && lastLoan.user.login === currentUser;
        }
    }
};
</script>

<style scoped>
/* Styles for game detail component */
.table-container {
  max-width: 750px;
  margin: 20px auto 0; /* Center the table horizontally and add margin-top */
}

table {
  width: 100%;
  border-collapse: collapse;
}

th, td {
  border: 1px solid #ddd;
  padding: 8px;
  text-align: left;
}

th {
  background-color: #f2f2f2;
}
</style>