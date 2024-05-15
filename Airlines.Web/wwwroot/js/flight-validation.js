const formElement = document.querySelector(".form-wrapper form");
const numberInputElement = document.querySelector("input[name='Number']");
const arrivalAirportIdInputElement = document.querySelector("input[name='ArrivalAirportId']");
const departureAirportInputElement = document.querySelector("input[name='DepartureAirportId']");
const arrivalAirportDateTimeInputElement = document.querySelector("input[name='DepartureAirportDateTime']");
const departureAirportDateTimeInputElement = document.querySelector("input[name='ArrivalAirportDateTime']");;
const submitButtonElement = document.querySelector(".submit-form-button");

formElement.addEventListener("change", validateForm);
numberInputElement.addEventListener("blur", validateNumber);
arrivalAirportIdInputElement.addEventListener("blur", validateArrivalAirportId);
departureAirportInputElement.addEventListener("blur", validateDepartureAirportId);
arrivalAirportDateTimeInputElement.addEventListener("blur", validateArrivalAirportDateTime);
departureAirportDateTimeInputElement.addEventListener("blur", validateDepartureAirportDateTime);
foundedInputElement.addEventListener("blur", validateFounded);


function validateForm() {
    const isFormValid = validateNumber() &&
        validateCountry() &&
        validateCity() &&
        validateCode() &&
        validateRunways() &&
        validateFounded();

    if (isFormValid) {
        submitButtonElement.removeAttribute("disabled");
    } else {
        submitButtonElement.setAttribute("disabled", true);
    }
}

function validateNumber() {
    let numberInput = document.getElementById('number');
    let numberError = document.getElementById('number-error');
    let number = numberInput.value.trim();

    if (number === '') {
        numberError.textContent = 'Number is required';
        return false;
    } else if (number.length > 5) {
        codeError.textContent = 'Number must be no more than 5 characters long';
        return false;
    } else {
        codeError.textContent = '';
        return true;
    }
}

function validateArrivalAirpotId() {

}

function validateDepartureAirportId() {

}

function validateArrivalAirportDateTime() {
    let arrivalDateInput = document.getElementById('arrivalTime');
    let arrivalDateError = document.getElementById('arrivalTime-error');
    let arrivalDate = new Date(arrivalDateInput.value.trim());

    if (isNaN(arrivalDate)) {
        arrivalDateError.textContent = 'Please enter a valid date';
        return false;
    } else {
        foundedError.textContent = '';
        return true;
    }
}

function validateDepartureAirportDateTime() {

}