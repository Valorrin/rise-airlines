document.addEventListener("DOMContentLoaded", () => {
    let editingAirlineId = null;

    async function handleFormSubmit(event) {
        event.preventDefault();

        const name = document.getElementById("name").value;
        const founded = document.getElementById("founded").value;
        const fleetSize = document.getElementById("fleetSize").value;
        const description = document.getElementById("description").value;

        const airlineData = {
            name,
            founded,
            fleetSize,
            description
        };

        try {
            let response;
            if (editingAirlineId) {
                response = await updateAsync('airlines', editingAirlineId, airlineData);
                editingAirlineId = null;
            } else {
                response = await createAsync('airlines', airlineData);
            }

            if (response) {
                alert("Airline saved successfully!");
                loadAirlines();
            } else {
                alert("Failed to save airline.");
            }
        } catch (error) {
            console.error("Error saving airline:", error);
        }

        form.reset();
    }

    const form = document.querySelector("form");
    form.addEventListener("submit", handleFormSubmit);

    async function loadAirlines() {
        try {
            const airlines = await getAllAsync('airlines');
            const tbody = document.getElementById("airlines-tbody");
            tbody.innerHTML = '';

            if (airlines) {
                airlines.forEach(airline => {
                    const row = document.createElement("tr");
                    row.innerHTML = `
                        <td>${airline.name}</td>
                        <td>${airline.founded}</td>
                        <td>${airline.fleetSize}</td>
                        <td>${airline.description}</td>
                        <td>
                            <button class="delete-btn" data-id="${airline.id}">Delete</button>
                            <button class="edit-btn" data-id="${airline.id}">Edit</button>
                        </td>
                    `;
                    tbody.appendChild(row);
                });

                const deleteButtons = document.querySelectorAll('.delete-btn');
                deleteButtons.forEach(button => {
                    button.addEventListener('click', async () => {
                        const id = button.getAttribute('data-id');
                        const success = await deleteAirline(id);
                        if (success) {
                            const row = button.parentElement.parentElement;
                            row.remove();
                        } else {
                            console.error("Failed to delete the airline.");
                        }
                    });
                });

                const editButtons = document.querySelectorAll('.edit-btn');
                editButtons.forEach(button => {
                    button.addEventListener('click', async () => {
                        const id = button.getAttribute('data-id');
                        await editAirline(id);
                    });
                });
            } else {
                tbody.innerHTML = '<tr><td colspan="5">No airlines found.</td></tr>';
            }
        } catch (error) {
            console.error("Error loading airlines:", error);
        }
    }

    loadAirlines();

    async function deleteAirline(id) {
        try {
            const success = await deleteAsync('airlines', id);
            if (success) {
                alert("Airline deleted successfully!");
                return true;
            } else {
                alert("Failed to delete airline.");
                return false;
            }
        } catch (error) {
            console.error("Error deleting airline:", error);
            return false;
        }
    }

    async function editAirline(id) {
        try {
            const airline = await getOneAsync('airlines', id);
            if (airline) {
                document.getElementById("name").value = airline.name;
                document.getElementById("founded").value = airline.founded;
                document.getElementById("fleetSize").value = airline.fleetSize;
                document.getElementById("description").value = airline.description;

                editingAirlineId = id;

                document.querySelector(".show-form-button").click();
            } else {
                alert("Failed to load airline data.");
            }
        } catch (error) {
            console.error("Error loading airline data:", error);
            alert("Failed to load airline data.");
        }
    }
});
