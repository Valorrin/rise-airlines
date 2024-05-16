window.addEventListener('load', function () {
    let toggleTableButton = document.querySelector('.toggle-button');
    let tableContainer = document.querySelector('.table-container');
    let tableElement = document.querySelector("table");

    let toggleFormButton = document.querySelector('.show-form-button');
    let formElement = document.querySelector('.form-wrapper');

    let goToTopButton = document.getElementById("go-to-top-button");

    formElement.style.display = 'none';
    toggleTableButton.onclick = function () {
        tableContainer.style.display = (tableContainer.style.display === 'none') ? 'flex' : 'none';
        toggleTableButton.textContent = (tableContainer.style.display === 'none') ? 'Show Table' : 'Hide Table';
    };

    toggleFormButton.onclick = function () {
        formElement.style.display = (formElement.style.display === 'none') ? 'flex' : 'none';
        toggleFormButton.textContent = (formElement.style.display === 'none') ? 'Show Form' : 'Hide Form';
    };

    let table = document.querySelector('.table-container table');
    let rows = table.getElementsByTagName('tr');

    if (rows.length > 3) {
        let showMoreButton = document.createElement('button');
        showMoreButton.id = 'show-more-button';
        showMoreButton.textContent = 'Show more';
        showMoreButton.style.display = 'block';
        showMoreButton.classList.add("toggle-button");
        table.parentNode.insertBefore(showMoreButton, table.nextSibling);

        for (let i = 4; i < rows.length; i++) {
            rows[i].style.display = 'none';
        }

        showMoreButton.onclick = function () {
            for (let i = 4; i < rows.length; i++) {
                rows[i].style.display = (rows[i].style.display === 'none') ? '' : 'none';
            }
            showMoreButton.textContent = (showMoreButton.textContent === 'Show more') ? 'Hide' : 'Show more';

            if (tableElement.offsetHeight > 500) {
                goToTopButton.style.display = "block";
            } else {
                goToTopButton.style.display = "none";
            }
        };
    }

    goToTopButton.addEventListener("click", function () {
        tableContainer.scrollTo({
            top: 0,
            behavior: "smooth",
        });
    });
});
