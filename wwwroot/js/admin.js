function confirmation() {
    supprimer.style.display = "flex";
    supprimer.style.justifyContent = "space-between";
    let pseudo = this.id;

    textConfirmation.textContent = "Voulez-vous bel et bien supprimer l'utilisateur : " + pseudo;
}

function annule() {
    supprimer.style.display = "none";
}