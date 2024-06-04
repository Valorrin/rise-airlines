const baseUrl = "https://localhost:7110/api/";

async function getAllAsync(type) {
  try {
    const url = `${baseUrl}${type}`;
    const response = await fetch(url);
    if (!response.ok) {
      throw new Error(`Could not fetch resource: ${response.statusText}`);
    }
    const data = await response.json();
    return data;
  } catch (error) {
    console.error(`Error fetching ${type} data:`, error);
    return null;
  }
}

async function getOneAsync(type, id) {
  try {
    const url = `${baseUrl}${type}/${id}`;
    const response = await fetch(url);
    if (!response.ok) {
      throw new Error(`Could not fetch resource: ${response.statusText}`);
    }
    const data = await response.json();
    return data;
  } catch (error) {
    console.error(`Error fetching ${type} with ID ${id}:`, error);
    return null;
  }
}

async function deleteAsync(type, id) {
  try {
    const url = `${baseUrl}${type}/${id}`;
    const response = await fetch(url, {
      method: 'DELETE',
    });
    if (!response.ok) {
      throw new Error(`Could not delete ${type} with ID ${id}: ${response.statusText}`);
    }
    const data = await response.json();
    return data;
  } catch (error) {
    console.error(`Error deleting ${type} with ID ${id}:`, error);
    return null;
  }
}

async function createAsync(type, data) {
  try {
    const url = `${baseUrl}${type}`;
    const response = await fetch(url, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(data)
    });
    if (!response.ok) {
      throw new Error(`Could not create ${type}: ${response.statusText}`);
    }
    const responseData = await response.json();
    return responseData;
  } catch (error) {
    console.error(`Error creating ${type}:`, error);
    return null;
  }
}

async function updateAsync(type, id, data) {
  try {
    const url = `${baseUrl}${type}/${id}`;
    const response = await fetch(url, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(data)
    });
    if (!response.ok) {
      throw new Error(`Could not update ${type} with ID ${id}: ${response.statusText}`);
    }
    const responseData = await response.json();
    return responseData;
  } catch (error) {
    console.error(`Error updating ${type} with ID ${id}:`, error);
    return null;
  }
}
