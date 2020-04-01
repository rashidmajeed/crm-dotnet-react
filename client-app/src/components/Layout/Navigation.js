import React, { Fragment } from 'react';
import { Link } from 'react-router-dom';

const Navigation = () => {
  return (
    <Fragment>
      <aside className="sidebar col-3">
        <h2>Administration</h2>

        <nav className="navigation">
                <Link to={"/"} className="clients">Clients</Link>
                <Link to={"/products"} className="products">Products</Link>
                <Link to={"/orders"} className="orders">Orders</Link>
                <Link to={"/contact"} className="contact">Contact</Link>

            </nav>
      </aside>
    </Fragment>
  );
}

export default Navigation;