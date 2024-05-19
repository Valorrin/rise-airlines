document.addEventListener('DOMContentLoaded', async () => {
    const airlinesData = await getAllAsync("airlines");
    
    if (airlinesData) {
      populateTable(airlinesData, 'airlines-tbody');
    } else {
      console.error("Failed to fetch airline data.");
    }
  });