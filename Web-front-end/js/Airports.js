document.addEventListener('DOMContentLoaded', async () => {
    async function handleFormSubmit(event) {
        event.preventDefault();

        const name = document.getElementById("name").value;
        const country = document.getElementById("country").value;
        const city = document.getElementById("city").value;
        const code = document.getElementById("code").value;
        const runways = document.getElementById("runways").value;
        const founded = document.getElementById("founded").value;

        const airportData = {
            name,
            country,
            city,
            code,
            runways,
            founded
        };

        try {
            let response;
            if (editingAirportId) {
                response = await updateAsync('airports', editingAirportId, airportData);
                editingAirportId = null;
            } else {
                response = await createAsync('airports', airportData);
            }

            if (response) {
                alert("Airport saved successfully!");
                loadAirports();
            } else {
                alert("Failed to save airport.");
            }
        } catch (error) {
            console.error("Error saving airport:", error);
        }

        form.reset();
    }

    const form = document.querySelector("form");
    form.addEventListener("submit", handleFormSubmit);

    async function loadAirports() {
        try {
            const airports = await getAllAsync('airports');
            const tbody = document.getElementById("airports-tbody");
            tbody.innerHTML = '';

            if (airports) {
                airports.forEach(airport => {
                    const row = document.createElement("tr");
                    row.innerHTML = `
                        <td>${airport.name}</td>
                        <td>${airport.country}</td>
                        <td>${airport.city}</td>
                        <td>${airport.code}</td>
                        <td>${airport.runways}</td>
                        <td>${new Date(airport.founded).toLocaleDateString()}</td>
                        <td>
                            <button class="delete-btn" data-id="${airport.id}">Delete</button>
                            <button class="edit-btn" data-id="${airport.id}">Edit</button>
                        </td>
                    `;
                    tbody.appendChild(row);
                });

                const deleteButtons = document.querySelectorAll('.delete-btn');
                deleteButtons.forEach(button => {
                    button.addEventListener('click', async () => {
                        const id = button.getAttribute('data-id');
                        const success = await deleteAirport(id);
                        if (success) {
                            const row = button.parentElement.parentElement;
                            row.remove();
                        } else {
                            console.error("Failed to delete the airport.");
                        }
                    });
                });

                const editButtons = document.querySelectorAll('.edit-btn');
                editButtons.forEach(button => {
                    button.addEventListener('click', async () => {
                        const id = button.getAttribute('data-id');
                        await editAirport(id);
                    });
                });
            } else {
                tbody.innerHTML = '<tr><td colspan="7">No airports found.</td></tr>';
            }
        } catch (error) {
            console.error("Error loading airports:", error);
        }
    }

    loadAirports();

    async function deleteAirport(id) {
        try {
            const success = await deleteAsync('airports', id);
            if (success) {
                alert("Airport deleted successfully!");
                return true;
            } else {
                alert("Failed to delete airport.");
                return false;
            }
        } catch (error) {
            console.error("Error deleting airport:", error);
            return false;
        }
    }

    async function editAirport(id) {
        try {
            const airport = await getOneAsync('airports', id);
            if (airport) {
                document.getElementById("name").value = airport.name;
                document.getElementById("country").value = airport.country;
                document.getElementById("city").value = airport.city;
                document.getElementById("code").value = airport.code;
                document.getElementById("runways").value = airport.runways;
                document.getElementById("founded").value = airport.founded;

                editingAirportId = id;

                document.querySelector(".show-form-button").click();
            } else {
                alert("Failed to load airport data.");
            }
        } catch (error) {
            console.error("Error loading airport data:", error);
            alert("Failed to load airport data.");
        }
    }
});
