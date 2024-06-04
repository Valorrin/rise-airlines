document.addEventListener('DOMContentLoaded', async () => {
    async function handleFormSubmit(event) {
        event.preventDefault();

        const number = document.getElementById("number").value;
        const departureAirportId = document.getElementById("departureAirport").value;
        const arrivalAirportId = document.getElementById("arrivalAirport").value;
        const departureDateTime = document.getElementById("departureDateTime").value;
        const arrivalDateTime = document.getElementById("arrivalDateTime").value;

        const flightData = {
            number,
            departureAirportId,
            arrivalAirportId,
            departureDateTime,
            arrivalDateTime
        };

        try {
            let response;
            if (editingFlightId) {
                response = await updateAsync('flights', editingFlightId, flightData);
                editingFlightId = null;
            } else {
                response = await createAsync('flights', flightData);
            }

            if (response) {
                alert("Flight saved successfully!");
                loadFlights();
            } else {
                alert("Failed to save flight.");
            }
        } catch (error) {
            console.error("Error saving flight:", error);
        }

        form.reset();
    }

    const form = document.querySelector("form");
    form.addEventListener("submit", handleFormSubmit);

    async function loadFlights() {
        try {
            const flights = await getAllAsync('flights');
            const tbody = document.getElementById("flights-tbody");
            tbody.innerHTML = '';

            if (flights) {
                flights.forEach(flight => {
                    const row = document.createElement("tr");
                    row.innerHTML = `
                        <td>${flight.number}</td>
                        <td>${flight.departureAirport.name}</td>
                        <td>${flight.arrivalAirport.name}</td>
                        <td>${new Date(flight.departureDateTime).toLocaleString()}</td>
                        <td>${new Date(flight.arrivalDateTime).toLocaleString()}</td>
                        <td>
                            <button class="delete-btn" data-id="${flight.id}">Delete</button>
                            <button class="edit-btn" data-id="${flight.id}">Edit</button>
                        </td>
                    `;
                    tbody.appendChild(row);
                });

                const deleteButtons = document.querySelectorAll('.delete-btn');
                deleteButtons.forEach(button => {
                    button.addEventListener('click', async () => {
                        const id = button.getAttribute('data-id');
                        const success = await deleteFlight(id);
                        if (success) {
                            const row = button.parentElement.parentElement;
                            row.remove();
                        } else {
                            console.error("Failed to delete the flight.");
                        }
                    });
                });

                const editButtons = document.querySelectorAll('.edit-btn');
                editButtons.forEach(button => {
                    button.addEventListener('click', async () => {
                        const id = button.getAttribute('data-id');
                        await editFlight(id);
                    });
                });
            } else {
                tbody.innerHTML = '<tr><td colspan="6">No flights found.</td></tr>';
            }
        } catch (error) {
            console.error("Error loading flights:", error);
        }
    }

    loadFlights();

    async function deleteFlight(id) {
        try {
            const success = await deleteAsync('flights', id);
            if (success) {
                alert("Flight deleted successfully!");
                return true;
            } else {
                alert("Failed to delete flight.");
                return false;
            }
        } catch (error) {
            console.error("Error deleting flight:", error);
            return false;
        }
    }

    async function editFlight(id) {
        try {
            const flight = await getOneAsync('flights', id);
            if (flight) {
                document.getElementById("number").value = flight.number;
                document.getElementById("departureAirport").value = flight.departureAirportId;
                document.getElementById("arrivalAirport").value = flight.arrivalAirportId;
                document.getElementById("departureDateTime").value = flight.departureDateTime;
                document.getElementById("arrivalDateTime").value = flight.arrivalDateTime;

                editingFlightId = id;

                document.querySelector(".show-form-button").click();
            } else {
                alert("Failed to load flight data.");
            }
        } catch (error) {
            console.error("Error loading flight data:", error);
            alert("Failed to load flight data.");
        }
    }
});
