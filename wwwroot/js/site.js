// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const defaultModalAction = document.getElementById('defaultModalAction');
const deliveryCorrespondenceModal = document.getElementById('deliveryCorrespondenceModal');

if(defaultModalAction) {
    defaultModalAction.addEventListener('show.bs.modal', event => {

        const awareUnits = defaultModalAction.querySelector('#awareId');
        const buttonUnitID = defaultModalAction.querySelector('#defaultButton');

        buttonUnitID.disabled = true;
        awareUnits.checked = false;

        if(awareUnits){
            awareUnits.addEventListener( 'change', function() {
        
                const button = event.relatedTarget
                const recipient = button.getAttribute('data-bs-whatever')
                
                if(this.checked) {
                    buttonUnitID.disabled = false;
                    const inputDeleteUnitID = defaultModalAction.querySelector('#defaultInputID')
                    inputDeleteUnitID.value = recipient
        
                } else {
                    buttonUnitID.disabled = true;
                }
            });
        }
    })
}

// functions for fomrs

function maskCpf(i){

    let v = i.value;
    
    if(isNaN(v[v.length-1])){ 
        i.value = v.substring(0, v.length-1);
        return;
    }
    
    i.setAttribute("maxlength", "14");
    if (v.length == 3 || v.length == 7) i.value += ".";
    if (v.length == 11) i.value += "-";
}



var inputCpf = document.getElementById('Cpf');

if(inputCpf){
    var alertCpf = document.getElementById('alertCpf');
    inputCpf.addEventListener('blur', function(value) {
        var cpf = value.target.value;
            cpf = cpf.replace(/[^\d]+/g, '');
            if (cpf == '') {
                alertCpf.textContent = 'CPF não pode ser vazio';
                alertCpf.classList.remove('d-none');
                return false;
            }
            // Elimina CPFs invalidos conhecidos    
            if (cpf.length != 11 ||
                cpf == "00000000000" ||
                cpf == "11111111111" ||
                cpf == "22222222222" ||
                cpf == "33333333333" ||
                cpf == "44444444444" ||
                cpf == "55555555555" ||
                cpf == "66666666666" ||
                cpf == "77777777777" ||
                cpf == "88888888888" ||
                cpf == "99999999999"){
                    alertCpf.textContent = '';
                    alertCpf.textContent = 'CPF inválido';
                    alertCpf.classList.remove('d-none');
                    return false;
                }
            // Valida 1o digito 
            add = 0;
                for (i = 0; i < 9; i++){
                    add += parseInt(cpf.charAt(i)) * (10 - i);
                }
            rev = 11 - (add % 11);
            if (rev == 10 || rev == 11){
                rev = 0;
            }
            if (rev != parseInt(cpf.charAt(9))){
                alertCpf.textContent = '';
                alertCpf.textContent = 'CPF inválido';
                return false;
            }
            // Valida 2o digito 
            add = 0;
            for (i = 0; i < 10; i++){
                add += parseInt(cpf.charAt(i)) * (11 - i);
            }
            rev = 11 - (add % 11);
            if (rev == 10 || rev == 11){
                rev = 0;
            }
            if (rev != parseInt(cpf.charAt(10))){
                alertCpf.textContent = '';
                alertCpf.textContent = 'CPF inválido';
                return false;
            }
            alertCpf.classList.add('d-none');
            console.log('CPF válido');
            return true;
    });
}

var inputEmail = document.getElementById('Email');

if(inputEmail){
    var alertEmail = document.getElementById('alertEmail');
    inputEmail.addEventListener('blur', function(value) {
        var email = value.target.value;
        if(email == ''){
            alertEmail.textContent = 'Email não pode ser vazio';
            alertEmail.classList.remove('d-none');
            return false;
        }
        if(email.indexOf('@') == -1 || email.indexOf('.') == -1){
            alertEmail.textContent = 'Email inválido';
            alertEmail.classList.remove('d-none');
            return false;
        }
        alertEmail.classList.add('d-none');
        console.log('Email válido');
        return true;
    });
}   