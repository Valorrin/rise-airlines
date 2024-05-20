document.addEventListener('DOMContentLoaded', async () => {
  const type = "airlines";
  const airlineData = await getAllAsync(type);

  if (airlineData) {
      console.log("Airline data is available.");
      const tableBody = document.getElementById('airlines-tbody');
      tableBody.innerHTML = ''; // Clear any existing rows

      // Append airport data to table
      airlineData.forEach(airline => {
          const row = document.createElement('tr');
          row.setAttribute('data-id', airline.airportId);

          row.innerHTML = `
              <td>${airline.name}</td>
              <td>${new Date(airline.founded).toLocaleDateString()}</td>
              <td>${airline.fleetSize}</td>
              <td>${airline.description}</td>
              <td>
                  <a><button class="edit-btn">Edit</button></a>
                  <a><button class="delete-btn">Delete</button></a>
              </td>
          `;

          row.querySelector('.delete-btn').addEventListener('click', async () => {
              const success = await deleteAsync(type, airline.airlineId);
              if (success) {
                  tableBody.removeChild(row);
              } else {
                  console.error("Failed to delete airline data.");
              }
          });

          tableBody.appendChild(row);
      });
  } else {
      console.error("Failed to fetch airline data.");
  }
});