import React, {Fragment, useState } from 'react';

function Contact() {
  // contact = state, saveContact = function to save the state
  const [contact, saveContact] = useState({
    Name: '',
    phoneNumber: '',
    email:'',
    message: '',
  });


  // Read the form data
  const updateState = e => {
    // Store what the user writes in the state
    saveContact({
      // get a copy of the current state
      ...contact,
      [e.target.name]: e.target.value
    })

    console.log(contact);
  }

  // Validate the form
  const validateContact = () => {
    // Destructuring
    const { Name, email, message, phoneNumber,  } = contact;

    // check that the state properties have content
    let validate = !Name.length || !email.length || !phoneNumber.length || !message.length;

    // return true or false
    return validate;
  }
  return (
    <Fragment>
      <h2>Create Contact</h2>
      <form
      >
        <legend>If you have any question/problem write here..</legend>
        <div className="field">
          <label>Name:</label>
          <input type="text"
            placeholder="Name"
            name="Name"
            onChange={updateState}
          />
        </div>

        <div className="field">
          <label>Email:</label>
          <input type="email"
            placeholder="Email"
            name="email"
            onChange={updateState}
          />
        </div>

        <div className="field">
          <label>Telephone:</label>
          <input type="Tel"
            placeholder="Tel.Number"
            name="phoneNumber"
            onChange={updateState}
          />
        </div>
        <div className="field">
          <label>Message:</label>
          <textarea type="textarea"
            placeholder="Message"
            name="message"
            onChange={updateState}
          />
        </div>
        <div className="send">
          <input
            type="submit"
            className="btn btn-blue"
            value="Add Client"
            disabled={ validateContact() }
          />
        </div>
      </form>
    </Fragment>

  );

}

export default Contact;