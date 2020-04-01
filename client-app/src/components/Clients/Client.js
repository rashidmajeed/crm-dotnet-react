import React from 'react';

function Client({Client}) {
  
const { id, firstName, lastName, company, phoneNumber, location} = Client;

  console.log(Client);
  return (
    <li className="client">
      <div className="info-client">
        <p className="name">{firstName} {lastName} </p>
        <p className="company">{company}</p >
        <p className="location">Location: {location}</p>
        <p className="telephone">Tel: {phoneNumber}</p >
      </div >
      <div className="action" >
      <a href="" className="btn btn-blue" >
        <i className="fas fa-pen-alt" > </i >
            Edit Client
            </a >
      <button type="button" className="btn btn-red btn-delete" >
        <i className="fas fa-times" > </i >
            Delete Client
            </button >
            </div >
        </li >
     );
}

export default Client;