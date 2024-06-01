document.addEventListener('DOMContentLoaded', async () => {
    const type = "airports";
    const airportData = await getAllAsync(type);

    if (airportData) {
        const tableBody = document.getElementById('airports-tbody');
        tableBody.innerHTML = '';

        airportData.forEach(airport => {
            const row = document.createElement('tr');
            row.setAttribute('data-id', airport.airportId);

            row.innerHTML = `
                <td>${airport.name}</td>
                <td>${airport.country}</td>
                <td>${airport.city}</td>
                <td>${airport.code}</td>
                <td>${airport.runways}</td>
                <td>${new Date(airport.founded).toLocaleDateString()}</td>
                <td>
                    <a><button class="edit-btn">Edit</button></a>
                    <a><button class="delete-btn">Delete</button></a>
                </td>
            `;

            row.querySelector('.delete-btn').addEventListener('click', async () => {
                const success = await deleteAsync(type, airport.airportId);
                if (success) {
                    tableBody.removeChild(row);
                } else {
                    console.error("Failed to delete the airport.");
                }
            });

            tableBody.appendChild(row);
        });
    } else {
        console.error("Failed to fetch airport data.");
    }
});