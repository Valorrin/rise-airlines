const formElement = document.querySelector(".form-wrapper form");
const nameInputElement = document.querySelector("input[name='Name']");
const foundedInputElement = document.querySelector("input[name='Founded']");
const fleetSizeInputElement = document.querySelector("input[name='FleetSize']");
const descriptionTextAreaElement = document.querySelector("textarea[name='Description']");
const submitButtonElement = document.querySelector(".submit-form-button");

formElement.addEventListener("change", validateForm);
nameInputElement.addEventListener("blur", validateName);
foundedInputElement.addEventListener("blur", validateFounded);
fleetSizeInputElement.addEventListener("blur", validateFleetSize);
descriptionTextAreaElement.addEventListener("blur", validateDescription);

function validateForm() {
    const isFormValid = validateName(false) &&
        validateFounded(false) &&
        validateFleetSize(false) &&
        validateDescription(false);

    if (isFormValid) {
        submitButtonElement.removeAttribute("disabled");
    } else {
        submitButtonElement.setAttribute("disabled", true);
    }
}

function validateName() {
    let nameInput = document.getElementById('name');
    let nameError = document.getElementById('name-error');
    let name = nameInput.value.trim();

    if (name === '') {
        nameError.textContent = 'Name is required';
        return false;
    } else if (!/^[A-Za-z\s]+$/.test(name)) {
        nameError.textContent = 'Name must only contain letters and spaces';
        return false;
    } else {
        nameError.textContent = '';
        return true;
    }
}

function validateFounded() {
    let foundedInput = document.getElementById('founded');
    let foundedError = document.getElementById('founded-error');
    let foundedDate = new Date(foundedInput.value.trim());
    let currentDate = new Date();

    if (isNaN(foundedDate)) {
        foundedError.textContent = 'Please enter a valid date';
        return false;
    } else if (foundedDate > currentDate) {
        foundedError.textContent = 'Founded date cannot be in the future';
        return false;
    } else {
        foundedError.textContent = '';
        return true;
    }
}
function validateFleetSize() {
    let fleetSizeInput = document.getElementById('fleetSize');
    let fleetSizeError = document.getElementById('fleetSize-error');
    let fleetSize = parseInt(fleetSizeInput.value.trim(), 10);

    if (isNaN(fleetSize)) {
        fleetSizeError.textContent = 'Fleet size is required';
        return false;
    } else if (fleetSize <= 0) {
        fleetSizeError.textContent = 'Fleet size must be a positive number';
        return false;
    } else {
        fleetSizeError.textContent = '';
        return true;
    }
}

function validateDescription() {
    let descriptionInput = document.getElementById('description');
    let descriptionError = document.getElementById('description-error');
    let description = descriptionInput.value.trim();

    if (description === '') {
        descriptionError.textContent = 'Description is required';
        return false;
    } else if (!/^[A-Za-z\s]+$/.test(description)) {
        descriptionError.textContent = 'Description must contain letters and spaces only';
        return false;
    } else {
        descriptionError.textContent = '';
        return true;
    }
}