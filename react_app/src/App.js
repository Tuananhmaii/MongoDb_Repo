import React, { useState, useEffect } from 'react';

function UserComponent() {
  const [userData, setUserData] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    // Define the function to fetch data
    const fetchData = async () => {
      try {
        const response = await fetch('https://localhost:7171/api/user');
        if (!response.ok) {
          throw new Error(`HTTP error! status: ${response.status}`);
        }
        const data = await response.json();
        setUserData(data);
      } catch (error) {
        setError(error);
      } finally {
        setLoading(false);
      }
    };

    // Call the fetch function
    fetchData();
  }, []);

  // Render the component
  return (
    <div>
      {loading && <p>Loading...</p>}
      {error && <p>Error: {error.message}</p>}
      {userData && (
        <div>
          <h2>User Data</h2>
          <pre>{JSON.stringify(userData, null, 2)}</pre>
        </div>
      )}
    </div>
  );
}

export default UserComponent;
