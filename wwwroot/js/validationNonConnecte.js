const SEP = "\n"

function validate(conditions, errs, errSeparator = '') {// [if true then error], [error msg]   (same length)
    return {m:errs.reduce((a, b, i) => (a[i] &&= errSeparator + b, a), conditions.map(x => x || '')).join('').slice(errSeparator.length), c:conditions.reduce((a, b)=>a+b)}
}

function validateConnectionForm() {
    let pseudo = connPseudo.value, password = connMdp.value,
        mPseudo = validate(//pseudo
            [pseudo.length < 5, pseudo.match(/[^a-z0-9]/gi), pseudo.length > 50],
            ["Le pseudo doit être au moins 5 charactères", "Le pseudo ne doit contenir que des charatères alphanumériques", "Le pseudo doit conetenir moins de 50 charactères"],
            SEP),
        mPw = validate(//pw
            [password.length < 5, password.length > 100, !password.match(/[a-z]/g), !password.match(/[A-Z]/g), !password.match(/[0-9]/g), !password.match(/[^0-9a-z]/gi)],
            ["Le mot de passe doit être au moins 5 charactères", "Le mot de passe doit conetenir moins de 50 charactères", "Le mot de passe doit contenir au moins une lettre minuscule", "Le mot de passe doit contenir au moins une lettre majuscule", "Le mot de passe doit contenir au moins un chiffre", "Le mot de passe doit contenir au moins un charatère spécial"],
            SEP)

    return { message: mPseudo.m+SEP+mPw.m, count:{ pseudo: mPseudo.c, password:mPw.c, TOTAL:mPseudo.c+mPw.c}}
}
         

function validateInscriptionForm() {
    let pseudo = insPseudo.value, password = insMdp.value, nom = insNomFamille.value, prenom = insPrenom.value,
        mPrenom = validate(//prénom
            [prenom.length < 2, prenom.match(/[^a-z -]/gi), prenom.length > 50],
            ["Le prénom doit être au moins 1 charactère", "Le prénom ne doit contenir que des charatères lettres, espaces ou '-'", "Le prénom doit conetenir moins de 50 charactères"],
            SEP),
        mNom = validate(//nom famille
            [nom.length < 2, nom.match(/[^a-z -]/gi), nom.length > 50],
            ["Le nom de famille doit être au moins 1 charactère", "Le nom de famille ne doit contenir que des charatères lettres, espaces ou '-'", "Le nom de famille doit conetenir moins de 50 charactères"],
            SEP),
        mPseudo = validate(//pseudo
            [pseudo.length < 5, pseudo.match(/[^a-z0-9]/gi), pseudo.length > 50],
            ["Le pseudo doit être au moins 5 charactères", "Le pseudo ne doit contenir que des charatères alphanumériques", "Le pseudo doit conetenir moins de 50 charactères"],
            SEP),
        mPw = validate(//pw
            [password.length < 5, password.length > 100, !password.match(/[a-z]/g), !password.match(/[A-Z]/g), !password.match(/[0-9]/g), !password.match(/[^0-9a-z]/gi)],
            ["Le mot de passe doit être au moins 5 charactères", "Le mot de passe doit conetenir moins de 50 charactères", "Le mot de passe doit contenir au moins une lettre minuscule", "Le mot de passe doit contenir au moins une lettre majuscule", "Le mot de passe doit contenir au moins un chiffre", "Le mot de passe doit contenir au moins un charatère spécial"],
            SEP)

    return {message: mPrenom.m+SEP+mNom.m+SEP+mPseudo.m+SEP+mPw.m, count: { pseudo: mPseudo.c, password: mPw.c, prenom:mPrenom.c, nom:mNom.c , TOTAL:mPseudo.c+mPw.c+mPrenom.c+mNom.c}}

}

// CONNECTION

function displayError(v, isConnection=0) {
    errorHeader.textContent = v.count.TOTAL ? isConnection ? `Erreurs présentes dans: ${v.count.pseudo?`Pseudo (${v.count.pseudo}), `:""} ${v.count.password?`Mot de passe (${v.count.password})`:""}` : `Erreurs présentes dans: ${v.count.prenom?`Prenom (${v.count.prenom}), `:""} ${v.count.nom?`Nom de famille (${v.count.nom}), `:""} ${v.count.pseudo?`Pseudo (${v.count.pseudo}), `:""} ${v.count.password?`Mot de passe (${v.count.password})`:""}` : ""
    errors.textContent = v.message.trimLeft()
}

connForm.onsubmit = (e) => {
    let v = validateConnectionForm()
    if (v.message.trim()) {
        e.preventDefault()
        displayError(v, 1)
    }
    connForm.querySelectorAll(".conn-control").forEach(el=>el.oninput=()=>{displayError(validateConnectionForm(), 1)})
}

// INSCRIPTION
insForm.onsubmit = (e) => {
    let v = validateInscriptionForm()
        if (v.message.trim()) {
        e.preventDefault()
        displayError(v)
    }
    insForm.querySelectorAll(".ins-control").forEach(el=>el.oninput=()=>{displayError(validateInscriptionForm())})
}

