// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const defaultModalAction = document.getElementById('defaultModalAction');
const deliveryCorrespondenceModal = document.getElementById('deliveryCorrespondenceModal');

if(defaultModalAction){
    defaultModalAction.addEventListener('show.bs.modal', event => {
    
        var awareId = defaultModalAction.querySelector('#awareId');
        var defaultButton = defaultModalAction.querySelector('#defaultButton');
        var defaultInputID = defaultModalAction.querySelector('#defaultInputID')
    
        defaultButton.disabled = true;
        awareId.checked = false;
    
        awareId.addEventListener( 'change', function() {
    
            var button = event.relatedTarget
            var recipient = button.getAttribute('data-bs-whatever')
            
            if(this.checked) {
                defaultButton.disabled = false;
                defaultInputID.value = recipient
    
            } else {
                defaultButton.disabled = true;
                defaultInputID.value = null;
            }
        });
    })
}

if(deliveryCorrespondenceModal){
    deliveryCorrespondenceModal.addEventListener('show.bs.modal', event => {
    
            var buttonDelivery = event.relatedTarget
            var recipient = buttonDelivery.getAttribute('data-bs-whatever')
            
            var inputDeliveryCorrespondenceId = deliveryCorrespondenceModal.querySelector('#deliveryCorrespondenceId')
            inputDeliveryCorrespondenceId.value = recipient
    })
}

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
                alertCpf.textContent = 'Campo obrigatório!';
                alertCpf.classList.remove('d-none');
                return false;
            }

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
            return true;
    });
}

var inputEmail = document.getElementById('Email');

if(inputEmail){
    var alertEmail = document.getElementById('alertEmail');
    inputEmail.addEventListener('blur', function(value) {
        var email = value.target.value;
        if(email == ''){
            alertEmail.textContent = 'Campo obrigatório!';
            alertEmail.classList.remove('d-none');
            return false;
        }
        if(email.indexOf('@') == -1 || email.indexOf('.') == -1){
            alertEmail.textContent = 'Email inválido';
            alertEmail.classList.remove('d-none');
            return false;
        }
        alertEmail.classList.add('d-none');
        return true;
    });
}   


// Jquery
$(document).ready(function(){
    $('#alertMessage').ready(function(){
        setTimeout(function() {
            $('#alertMessage').fadeOut('fast');
        }, 5000);
    })     
})