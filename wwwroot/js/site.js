// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const unitsModal = document.getElementById('unitsModal');

unitsModal.addEventListener('show.bs.modal', event => {

    const awareUnits = unitsModal.querySelector('#awareUnits');
    const buttonUnitID = unitsModal.querySelector('#unitButtonId');

    buttonUnitID.disabled = true;
    awareUnits.checked = false;

    awareUnits.addEventListener( 'change', function() {

        const button = event.relatedTarget
        const recipient = button.getAttribute('data-bs-whatever')
        
        if(this.checked) {
            buttonUnitID.disabled = false;
            const inputDeleteUnitID = unitsModal.querySelector('#unitInputID')
            inputDeleteUnitID.value = recipient

        } else {
            buttonUnitID.disabled = true;
        }
    });
})