// login.js

// Funktion der håndterer login
function loginFunction(event) {
    event.preventDefault();  // Forhindre at formularen sendes på traditionel måde

    // Hent data fra formularen
    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;

    // Opret et objekt med login dataene
    const loginData = {
        email: email,
        password: password
    };

    // Send login dataene til backend med Axios
    axios.post('http://localhost:5209/api/Produkter/login', loginData)
        .then(function (response) {
            // Hvis login er succesfuldt
            if (response.status === 200) {
                alert("Login succesfuld!");
                // Gem brugeren som login-session, eller redirect til en anden side
                window.location.href = "index.html";  // Omvej til forsiden
            }
        })
        .catch(function (error) {
            // Hvis der er fejl, vis en fejlmeddelelse
            console.error("Der opstod en fejl ved login:", error);
            alert("Login mislykkedes. Prøv igen.");
        });
}

// Event Listener til at fange formularens submit event
document.getElementById('login-form').addEventListener('submit', loginFunction);
