// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const defaultModalAction = document.getElementById('defaultModalAction');

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