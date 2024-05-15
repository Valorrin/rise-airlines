const formElement = document.querySelector(".form-wrapper form");
const nameInputElement = document.querySelector("input[name='Name']");
const countryInputElement = document.querySelector("input[name='Country']");
const cityInputElement = document.querySelector("input[name='City']");
const codeInputElement = document.querySelector("input[name='Code']");
const runwaysInputElement = document.querySelector("input[name='Runways']");
const foundedInputElement = document.querySelector("input[name='Founded']");
const submitButtonElement = document.querySelector(".submit-form-button");

formElement.addEventListener("change", validateForm);
nameInputElement.addEventListener("blur", validateName);
countryInputElement.addEventListener("blur", validateCountry);
cityInputElement.addEventListener("blur", validateCity);
codeInputElement.addEventListener("blur", validateCode);
runwaysInputElement.addEventListener("blur", validateRunways);
foundedInputElement.addEventListener("blur", validateFounded);


function validateForm() {
    const isFormValid = validateName() &&
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

function validateCountry() {
    let countryInput = document.getElementById('country');
    let countryError = document.getElementById('country-error');
    let country = countryInput.value.trim();

    if (country === '') {
        countryError.textContent = 'Country is required';
        return false;
    } else if (!/^[A-Za-z\s]+$/.test(country)) {
        countryError.textContent = 'Country must only contain letters and spaces';
        return false;
    } else {
        countryError.textContent = '';
        return true;
    }
}

function validateCity() {
    let cityInput = document.getElementById('city');
    let cityError = document.getElementById('city-error');
    let city = cityInput.value.trim();

    if (city === '') {
        cityError.textContent = 'City is required';
        return false;
    } else if (!/^[A-Za-z\s]+$/.test(city)) {
        cityError.textContent = 'City must only contain letters and spaces';
        return false;
    } else {
        cityError.textContent = '';
        return true;
    }
}

function validateCode() {
    let codeInput = document.getElementById('code');
    let codeError = document.getElementById('code-error');
    let code = codeInput.value.trim();

    if (code === '') {
        codeError.textContent = 'Code is required';
        return false;
    } else if (code.length > 3) {
        codeError.textContent = 'Code must be no more than 3 characters long';
        return false;
    } else {
        codeError.textContent = '';
        return true;
    }
}

function validateRunways() {
    let runwaysInput = document.getElementById('runways');
    let runwaysError = document.getElementById('runways-error');
    let runways = parseInt(runwaysInput.value.trim(), 10);

    if (isNaN(runways)) {
        runwaysError.textContent = 'Runways is required';
        return false;
    } else if (runways <= 0) {
        runwaysError.textContent = 'Runways must be a positive number';
        return false;
    } else {
        runwaysError.textContent = '';
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