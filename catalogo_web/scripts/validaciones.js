function campoRequerido(idCampo) {
    const campo = document.getElementById(idCampo);
    if (campo.value == "") {
        campo.classList.add("is-invalid");
        return false;
    }
    campo.classList.remove("is-invalid");
    return true;
}