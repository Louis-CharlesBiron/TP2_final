function confirmation(pseudo) {
    supprimer.style.display = "flex";
    supprimer.style.justifyContent = "space-between";
    console.log(pseudo);

    textConfirmation.textContent = "Voulez-vous bel et bien supprimer l'utilisateur : " + pseudo;
}

function annule() {
    supprimer.style.display = "none";
}