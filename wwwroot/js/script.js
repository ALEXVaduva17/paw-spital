// Simulare utilizator conectat
const loggedUser = {
  name: "Popescu Ana",
  email: "ana.popescu@example.com",
};

document.addEventListener("DOMContentLoaded", () => {
  const nameSpan = document.getElementById("user-name");
  const emailSpan = document.getElementById("user-email");

  if (nameSpan) {
    nameSpan.textContent = loggedUser.name;
  }

  if (emailSpan) {
    emailSpan.textContent = loggedUser.email;
  }
});

