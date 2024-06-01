document.addEventListener('DOMContentLoaded', async () => {
  const type = "flights";
  const flightData = await getAllAsync(type);

  if (flightData) {
      const tableBody = document.getElementById('flights-tbody');
      tableBody.innerHTML = '';

      flightData.forEach(flight => {
          const row = document.createElement('tr');
          row.setAttribute('data-id', flight.airportId);

          row.innerHTML = `
              <td>${flight.number}</td>
              <td>${flight.departureAirport.name}</td>
              <td>${flight.arrivalAirport.name}</td>
              <td>${new Date(flight.departureDateTime).toLocaleString()}</td>
              <td>${new Date(flight.arrivalDateTime).toLocaleString()}</td>
              <td>
                  <a><button class="edit-btn">Edit</button></a>
                  <a><button class="delete-btn">Delete</button></a>
              </td>
          `;

          row.querySelector('.delete-btn').addEventListener('click', async () => {
              const success = await deleteAsync(type, flight.airlineId);
              if (success) {
                  tableBody.removeChild(row);
              } else {
                  console.error("Failed to delete the flight.");
              }
          });

          tableBody.appendChild(row);
      });
  } else {
      console.error("Failed to fetch flight data.");
  }
});