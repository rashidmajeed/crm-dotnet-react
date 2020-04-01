import React from 'react';

const Header = () => {
  return (
    <header className="bar">
      <div className="container">
        <div className="content-bar">
          <h1>CRM - Clients Administration</h1>
            <button
              type="button"
              className="btn btn-green"
            >
              <i className="fas fa-sign-in-alt"></i>
               Get Started
            </button>
        </div>
      </div>
    </header>
  );
}

export default Header;