'use strict'
const SEP = "\n", MAXLENGTH = 100, MINLENGTH = 3

/**
 * @param conditions: un array de boolean ex: [1==1, 2==3]
 * @param errs: un array de message d'erreur ex: ["condition 1 invalide", "condition 2 invalide"]
 * @param errSeparator: string qui sépare les messages d'erreur ex: "condition 1 invalide" + errSeparator
 * @returns un Object contenant {m:les messages concatené par errSeparator et c:le nombre d'erreurs}
 */
function validate(conditions, errs, errSeparator = '') {// [if true then error], [error msg]
    return {m:errs.reduce((a, b, i) => (a[i] &&= errSeparator + b, a), conditions.map(x => x || '')).join('').slice(errSeparator.length), c:conditions.reduce((a, b)=>a+Boolean(b))}
};

/**
 * effectue la validation sur les champs du pseudo et du mot de passe lors de la connection
 * @return un object contenant {message: le total des messages d'erreur du pseudo et du mot de passe, count:le compte d'erreur de chaque champs et leur total}
 */
function validateConnectionForm() {
    let pseudo = connPseudo.value, password = connMdp.value,
        mPseudo = validate(//pseudo
            [pseudo.length < MINLENGTH, pseudo.match(/[^a-z0-9]/gi), pseudo.length > MAXLENGTH],
            ["Le pseudo doit être au moins " + MINLENGTH +" charactères", "Le pseudo ne doit contenir que des charatères alphanumériques", "Le pseudo doit conetenir moins de " + MAXLENGTH +" charactères"],
            SEP),
        mPw = validate(//pw
            [password.length < MINLENGTH, password.length > MAXLENGTH, !password.match(/[a-z]/g), !password.match(/[A-Z]/g), !password.match(/[0-9]/g), !password.match(/[^0-9a-z]/gi)],
            ["Le mot de passe doit être au moins " + MINLENGTH +" charactères", "Le mot de passe doit conetenir moins de " + MAXLENGTH +" charactères", "Le mot de passe doit contenir au moins une lettre minuscule", "Le mot de passe doit contenir au moins une lettre majuscule", "Le mot de passe doit contenir au moins un chiffre", "Le mot de passe doit contenir au moins un charatère spécial"],
            SEP)

    return { message: mPseudo.m+SEP+mPw.m, count:{ pseudo: mPseudo.c, password:mPw.c, TOTAL:mPseudo.c+mPw.c}}
}
         
/**
 * effectue la validation sur les champs du prénom, du nom de famille, du pseudo et du mot de passe lors de l'inscription
 * @return un object contenant {message: le total des messages d'erreur du prénom, du nom de famille, du pseudo et du mot de passe, count:le compte d'erreur de chaque champs et leur total}
 */
function validateInscriptionForm() {
    let pseudo = insPseudo.value, password = insMdp.value, nom = insNomFamille.value, prenom = insPrenom.value,
        mPrenom = validate(//prénom
            [prenom.length < MINLENGTH, prenom.match(/[^a-z -]/gi), prenom.length > MAXLENGTH],
            ["Le prénom doit être au moins " + MINLENGTH +" charactère", "Le prénom ne doit contenir que des charatères lettres, espaces ou '-'", "Le prénom doit conetenir moins de " + MAXLENGTH +" charactères"],
            SEP),
        mNom = validate(//nom famille
            [nom.length < MINLENGTH, nom.match(/[^a-z -]/gi), nom.length > MAXLENGTH],
            ["Le nom de famille doit être au moins " + MINLENGTH +" charactère", "Le nom de famille ne doit contenir que des charatères lettres, espaces ou '-'", "Le nom de famille doit conetenir moins de "+MAXLENGTH+" charactères"],
            SEP),
        mPseudo = validate(//pseudo
            [pseudo.length < MINLENGTH, pseudo.match(/[^a-z0-9]/gi), pseudo.length > MAXLENGTH],
            ["Le pseudo doit être au moins " + MINLENGTH +" charactères", "Le pseudo ne doit contenir que des charatères alphanumériques", "Le pseudo doit conetenir moins de " + MAXLENGTH +" charactères"],
            SEP),
        mPw = validate(//pw
            [password.length < MINLENGTH, password.length > MAXLENGTH, !password.match(/[a-z]/g), !password.match(/[A-Z]/g), !password.match(/[0-9]/g), !password.match(/[^0-9a-z]/gi)],
            ["Le mot de passe doit être au moins " + MINLENGTH +" charactères", "Le mot de passe doit conetenir moins de " + MAXLENGTH +" charactères", "Le mot de passe doit contenir au moins une lettre minuscule", "Le mot de passe doit contenir au moins une lettre majuscule", "Le mot de passe doit contenir au moins un chiffre", "Le mot de passe doit contenir au moins un charatère spécial"],
            SEP)

    return {message: mPrenom.m+SEP+mNom.m+SEP+mPseudo.m+SEP+mPw.m, count: { pseudo: mPseudo.c, password: mPw.c, prenom:mPrenom.c, nom:mNom.c , TOTAL:mPseudo.c+mPw.c+mPrenom.c+mNom.c}}

}


/**
 * affiche les erreurs sur la page
 * @param v: l'object d'erreur fournit par validateConnectionForm ou validateInscriptionForm
 * @param isConnection si l'object d'erreur fournit provient d'une tentative de connexion ou d'inscription
 */
function displayError(v, isConnection=0) {
    errorHeader.textContent = v.count.TOTAL ? 
    isConnection ? `Erreurs présentes dans: ${v.count.pseudo?`Pseudo (${v.count.pseudo}), `:""} ${v.count.password?`Mot de passe (${v.count.password})`:""}`
    : `Erreurs présentes dans: ${v.count.prenom?`Prenom (${v.count.prenom}), `:""} ${v.count.nom?`Nom de famille (${v.count.nom}), `:""} ${v.count.pseudo?`Pseudo (${v.count.pseudo}), `:""} ${v.count.password?`Mot de passe (${v.count.password})`:""}`
    : ""
    errors.textContent = v.message.trimLeft()
}

/**
 * exécute la validation des champs pour la connexion lors d'une tentative de connexion et empêche le submit si des erreurs sont présentes, ensuite fait la vérification lors de chaque keypress
 */
// CONNECTION
let connControls = connForm.querySelectorAll(".conn-control")
connForm.onsubmit = (e) => {
    let v = validateConnectionForm()
    if (v.message.trim()) {
        e.preventDefault()
        displayError(v, 1)
    }
    //vérificaiton des champs à chaque keypress
    connControls.forEach(el=>el.oninput=()=>{displayError(validateConnectionForm(), 1)})
}

/**
 * exécute la validation des champs pour l'inscription lors d'une tentative d'inscription et empêche le submit si des erreurs sont présentes, ensuite fait la vérification lors de chaque keypress
 */
// INSCRIPTION
let insControl = insForm.querySelectorAll(".ins-control")
insForm.onsubmit = (e) => {
    let v = validateInscriptionForm()
        if (v.message.trim()) {
        e.preventDefault()
        displayError(v)
    }
    //vérificaiton des champs à chaque keypress
    insControl.forEach(el=>el.oninput=()=>{displayError(validateInscriptionForm())})
}

