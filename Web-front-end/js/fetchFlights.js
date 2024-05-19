document.addEventListener('DOMContentLoaded', async () => {
    const flightsData = await getAllAsync("flights");
  
    if (flightsData) {
      populateTable(flightsData, 'flights-tbody');
    } else {
      console.error("Failed to fetch flight data.");
    }
  });