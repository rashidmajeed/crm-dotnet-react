import React, { useEffect, useState, Fragment } from 'react';
import { Link } from 'react-router-dom';

// importing backend API 
import clientAxios from '../../config/axios';
import Client from './Client';

function ClientList() {

  // Work with the state
  // clients = state, saveClients = function to save the state
  const [clients, saveClients] = useState([]);

  // Query to the API
  const ConsumeAPI = async () => {
    const fetchClients = await clientAxios.get('/api/Clients');
    saveClients(fetchClients.data);
  };
  // use effect is similar to componentdidmount 
  useEffect(() => {
    ConsumeAPI();
  }, []);
  return (
    <Fragment>
      <h2>Clients</h2>
      <Link to={"/clients/create"} className="btn btn-green new-client">
        <i className="fas fa-plus-circle"></i>
                Create Client
            </Link>
      <ul className="list-clients">
        {clients.map(client =>
          <Client key={client.id} Client={client}
          />
        )}
      </ul>
    </Fragment>
  );
}

export default ClientList