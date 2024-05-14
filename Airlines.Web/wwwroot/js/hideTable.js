window.addEventListener('load', function () {
    let formElement = document.querySelector('.form-wrapper');
    let toggleTableButton = document.querySelector('.toggle-button');
    let tableElement = document.querySelector('.table-container');

    formElement.style.display = 'none';
    toggleTableButton.onclick = function () {
        tableElement.style.display = (tableElement.style.display === 'none') ? 'flex' : 'none';
        toggleTableButton.textContent = (tableElement.style.display === 'none') ? 'Show Table' : 'Hide Table';
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
        };
    }
});