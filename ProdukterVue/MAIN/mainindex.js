// Funktionen til index.html
function indexFunction() {
    console.log("Dette er index-siden");
}

// Funktionen til login.html
function loginFunction() {
    console.log("Dette er login-siden");
}

// Funktionen til signup.html
function signupFunction() {
    console.log("Dette er signup-siden");
}

// Tjek hvilken side, der kører koden
if (window.location.pathname.includes("index.html")) {
    indexFunction();
} else if (window.location.pathname.includes("login.html")) {
    loginFunction();
} else if (window.location.pathname.includes("signup.html")) {
    signupFunction();
}
