import React, { Fragment } from 'react';
import { Link } from 'react-router-dom';
import Swal from 'sweetalert2';
import clientAxios from '../../config/axios';

function Client({ Client }) {

  const { id, firstName, lastName, company, phoneNumber, location } = Client;

  // Delete client
  const deleteClient = clientID => {
    Swal.fire({
      title: 'Are you sure?',
      text: "A deleted client cannot be recovered",
      type: 'warning',
      showCancelButton: true,
      confirmButtonColor: '# 3085d6',
      cancelButtonColor: '# d33',
      confirmButtonText: 'Yes, delete!',
      cancelButtonText: 'Cancel'
    }).then((result) => {
      if (result.value) {
        // called axios
        clientAxios.delete(`/api/Clients/${clientID}`)
          .then(res => {
            Swal.fire(
              'Removed',
              res.data.message,
              'success'
            );
          });
      }
    });
  }
  return (
    < Fragment>
      <li className="client">
        <div className="info-client">
          <p className="name">{firstName} {lastName} </p>
          <p className="company">{company}</p >
          <p className="location">Location: {location}</p>
          <p className="telephone">Tel: {phoneNumber}</p >
        </div >
        <div className="action" >
          <Link to={`/clients/edit/${id}`} className="btn btn-blue" >
            <i className="fas fa-pen-alt" > </i >
            Edit Client
           </Link>
          <button
            type="button"
            className="btn btn-red btn-delete"
            onClick={() => deleteClient(id)}
          >
            <i className="fas fa-times" />
					Delete Client
				</button>
        </div >

      </li>
    </Fragment>
  );
}

export default Client;