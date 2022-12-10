// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const defaultModalAction = document.getElementById('defaultModalAction');
const deliveryCorrespondenceModal = document.getElementById('deliveryCorrespondenceModal');

if(defaultModalAction){
    defaultModalAction.addEventListener('show.bs.modal', event => {
    
        var awareId = defaultModalAction.querySelector('#awareId');
        var defaultButton = defaultModalAction.querySelector('#defaultButton');
    
        defaultButton.disabled = true;
        awareId.checked = false;
    
        awareId.addEventListener( 'change', function() {
    
            var button = event.relatedTarget
            var recipient = button.getAttribute('data-bs-whatever')
            
            if(this.checked) {
                defaultButton.disabled = false;
                var defaultInputID = defaultModalAction.querySelector('#defaultInputID')
                defaultInputID.value = recipient
    
            } else {
                defaultButton.disabled = true;
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

$('#alertMessage').ready(function(){
    setTimeout(function() {
        $('#alertMessage').fadeOut('fast');
    }, 5000);
})     