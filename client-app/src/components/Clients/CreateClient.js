import React, { Fragment, useState } from 'react';
import { withRouter } from 'react-router-dom'; 

// importing backend API 
import clientAxios from '../../config/axios';
import Swal from 'sweetalert2';

function CreateClient({history}) {

  // client = state, saveclient = function to save the state
  const [client, saveClient] = useState({
    firstName: '',
    lastName: '',
    company: '',
    location: '',
    phoneNumber: ''
  });

  // Read the form data
  const updateState = e => {
    // Store what the user writes in the state
    saveClient({
      // get a copy of the current state
      ...client,
      [e.target.name]: e.target.value
    })
  }

   // Add a new client in the REST API
   const addClient = e => {
    e.preventDefault();

    // Send request
    clientAxios.post('/api/Clients', client)
      .then(res => {
    // validate if there are backend error
         if(res.data.code === 11000) {
          Swal.fire({
              type: 'error',
              title: 'There was an error',
              text: 'Client is already registered'
          })
      } else {
          Swal.fire(
              'Client is successfully Added',
              res.data.message,
              'success'
          )
      }
      // Redirect
      history.push('/');
      });
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
      <h2>Create Client</h2>
      <form
      onSubmit={addClient}
      >
        <legend>Fill all the fields</legend>
        <div className="field">
          <label>First Name:</label>
          <input type="text"
            placeholder="First Name"
            name="firstName"
            onChange={updateState}
          />
        </div>

        <div className="field">
          <label>Last Name:</label>
          <input type="text"
            placeholder="Last Name"
            name="lastName"
            onChange={updateState}
          />
        </div>

        <div className="field">
          <label>Company:</label>
          <input type="text"
            placeholder="Company"
            name="company"
            onChange={updateState}
          />
        </div>

        <div className="field">
          <label>Telephone:</label>
          <input type="Tel"
            placeholder="Telephone"
            name="phoneNumber"
            onChange={updateState}
          />
        </div>
        <div className="field">
          <label>Location:</label>
          <input type="text"
            placeholder="Location"
            name="location"
            onChange={updateState}
          />
        </div>
        <div className="send">
          <input
            type="submit"
            className="btn btn-blue"
            value="Add Client"
            disabled={validateClient()}
          />
        </div>
      </form>
    </Fragment>
  );
}

export default withRouter(CreateClient);