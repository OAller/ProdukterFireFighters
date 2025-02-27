// mainsignup.js

new Vue({
    el: '#signup-app',
    data: {
        email: '',
        password: '',
        confirmPassword: ''
    },
    methods: {
        signupFunction() {
            // Tjek om password og confirm password matcher
            if (this.password !== this.confirmPassword) {
                alert('Passwords do not match');
                return;
            }

            // Opret et objekt med signup dataene
            const signupData = {
                email: this.email,
                password: this.password
            };

            // Send signup dataene til backend med Axios
            axios.post('http://localhost:5209/api/Produkter/register', signupData)
                .then(response => {
                    // Hvis signup er succesfuldt
                    if (response.status === 200) {
                        alert("Signup succesfuld!");
                        // Redirect til forsiden
                        window.location.href = "index.html";
                    }
                })
                .catch(error => {
                    // Hvis der er fejl, vis en fejlmeddelelse
                    console.error("Der opstod en fejl ved signup:", error);
                    alert("Signup mislykkedes. Prøv igen.");
                });
        }
    }
});