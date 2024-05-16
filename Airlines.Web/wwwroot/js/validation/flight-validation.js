const formElement = document.querySelector(".form-wrapper form");
const numberInputElement = document.querySelector("input[name='Number']");
const arrivalAirportIdInputElement = document.querySelector("select[name='ArrivalAirportId']");
const departureAirportInputElement = document.querySelector("select[name='DepartureAirportId']");
const arrivalAirportDateTimeInputElement = document.querySelector("input[name='DepartureAirportDateTime']");
const departureAirportDateTimeInputElement = document.querySelector("input[name='ArrivalAirportDateTime']");;
const submitButtonElement = document.querySelector(".submit-form-button");

formElement.addEventListener("change", validateForm);
numberInputElement.addEventListener("blur", validateNumber);
arrivalAirportIdInputElement.addEventListener("blur", validateArrivalAirport);
departureAirportInputElement.addEventListener("blur", validateDepartureAirport);
arrivalAirportDateTimeInputElement.addEventListener("blur", validateArrivalAirportDateTime);
departureAirportDateTimeInputElement.addEventListener("blur", validateDepartureAirportDateTime);
foundedInputElement.addEventListener("blur", validateFounded);


function validateForm() {
    const isFormValid = validateNumber() &&
        validateArrivalAirpot() &&
        validateDepartureAirport() &&
        validateArrivalAirportDateTime() &&
        validateDepartureAirportDateTime();

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

function validateArrivalAirport() {
    let arrivalAirportInput = document.getElementById('from');
    let arrivalAirportError = document.getElementById('from-error');
    let arrivalAirport = arrivalAirportInput.value.trim();

    if (arrivalAirport === '') {
        arrivalAirportError.textContent = 'Arrival airport is required';
        return false;
    } else {
        arrivalAirportError.textContent = '';
        return true;
    }
}

function validateDepartureAirport() {
    let departureAirportInput = document.getElementById('to');
    let departureAirportError = document.getElementById('to-error');
    let departureAirport = departureAirportInput.value.trim();

    if (departureAirport === '') {
        departureAirportError.textContent = 'Departure airport is required';
        return false;
    } else {
        departureAirportError.textContent = '';
        return true;
    }
}

function validateArrivalAirportDateTime() {
    let arrivalDateInput = document.getElementById('arrivalTime');
    let arrivalDateError = document.getElementById('arrivalTime-error');
    let arrivalDate = new Date(arrivalDateInput.value.trim());
    let currentDate = new Date();

    if (isNaN(arrivalDate)) {
        arrivalDateError.textContent = 'Please enter a valid date';
        return false;
    } else if (arrivalDate < currentDate) {
        foundedError.textContent = 'Founded date cannot be in the past';
        return false;
    } else {
        foundedDateError.textContent = '';
        return true;
    }
}

function validateDepartureAirportDateTime() {
    let departurelDateInput = document.getElementById('departureTime');
    let departureDateError = document.getElementById('departureTime-error');
    let departureDate = new Date(departurelDateInput.value.trim());
    let currentDate = new Date();

    if (isNaN(departureDate)) {
        departureDateError.textContent = 'Please enter a valid date';
        return false;
    } else if (departureDate < currentDate) {
        foundedError.textContent = 'Founded date cannot be in the past';
        return false;
    } else {
        departureDateError.textContent = '';
        return true;
    }
}