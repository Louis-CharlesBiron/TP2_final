function confirmation(pseudo) {
    supprimer.style.display = "flex";
    supprimer.style.justifyContent = "space-between";

    textConfirmation.textContent = "Voulez-vous bel et bien supprimer l'utilisateur : " + pseudo;
}

function annule() {
    supprimer.style.display = "none";
}