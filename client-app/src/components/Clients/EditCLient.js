import React, { Fragment, useState, useEffect } from 'react';
import { withRouter } from 'react-router-dom';

// importing backend API 
import clientAxios from '../../config/axios';
import Swal from 'sweetalert2';

function EditClient(props) {

  // obtener el ID 
  const { id } = props.match.params;

  // client = state, saveclient = function to save the state
  const [client, clientData] = useState({
    firstName: '',
    lastName: '',
    company: '',
    location: '',
    phoneNumber: ''
  });

  // Query to the API for a Client
  const fetchAPI = async () => {
    const queryClient = await clientAxios.get(`/api/Clients/${id}`);
    // place in the state
    clientData(queryClient.data);
    console.log(queryClient)
  }

  // useEffect, when the component loads
  useEffect(() => {
    fetchAPI();
  }, []);

  // Read the form data
  const updateState = e => {
    // Store what the user writes in the state
    clientData({
      // get a copy of the current state
      ...client,
      [e.target.name]: e.target.value
    })

  }
  // Send a request for axios to update the client
  const updateClient = e => {
    e.preventDefault();

    // send request by axios
    clientAxios.put(`/api/Clients/${client.id}`, client)
      .then(res => {
        // validate if there backend error
        if (res.data.code === 11000) {
          Swal.fire({
            type: 'error',
            title: 'There was an error',
            text: 'That Client is already registered'
          })
        } else {
          Swal.fire(
            'Right',
            'Updated Correctly',
            'success'
          )
        }
        // redirect
        props.history.push('/');
      })
  }
  // Validate the form
  const validateClient = () => {
    // Destructuring
    const { firstName, lastName, company, phoneNumber, location } = client;

    // check that the state properties have content
    let validate = !firstName.length || !lastName.length || !company.length || !phoneNumber.length || !location.length;

    // return true or false
    return validate;
  }

  return (
    <Fragment>
      <h2>Edit Client</h2>
      <form
        onSubmit={updateClient}
      >
        <legend>Fill all the fields</legend>
        <div className="field">
          <label>First Name:</label>
          <input type="text"
            placeholder="First Name"
            name="firstName"
            onChange={updateState}
            value={client.firstName}
          />
        </div>

        <div className="field">
          <label>Last Name:</label>
          <input type="text"
            placeholder="Last Name"
            name="lastName"
            onChange={updateState}
            value={client.lastName}
          />
        </div>

        <div className="field">
          <label>Company:</label>
          <input type="text"
            placeholder="Company"
            name="company"
            onChange={updateState}
            value={client.company}
          />
        </div>

        <div className="field">
          <label>Telephone:</label>
          <input type="Tel"
            placeholder="Telephone"
            name="phoneNumber"
            onChange={updateState}
            value={client.phoneNumber}
          />
        </div>
        <div className="field">
          <label>Location:</label>
          <input type="text"
            placeholder="Location"
            name="location"
            onChange={updateState}
            value={client.location}

          />
        </div>
        <div className="send">
          <input
            type="submit"
            className="btn btn-blue"
            value="Save Client"
            disabled={validateClient()}
          />
        </div>
      </form>
    </Fragment>
  );
}

export default withRouter(EditClient);