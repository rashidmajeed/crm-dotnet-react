import React, { Fragment } from 'react';

// Routing
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';

/*** Layout */

import Header from './components/Layout/Header';
import Navigation from './components/Layout/Navigation';

/** Components */
import Clients from './components/Clients/ClientList';
import CreateClient from './components/Clients/CreateClient';
import EditClient from './components/Clients/EditClient';
import Products from './components/Products/ProductList';
import Orders from './components/Orders/OrderList';
import ContactForm from './components/Contact/ContactForm';


function App() {
  return (
    <Router>
      <Fragment>
        <Header />
        <div className="grid container content-principal">
          <Navigation />
          <main className="box-content col-9">
            <Switch>
              <Route exact path="/" component={Clients} />
              <Route exact path="/clients/create" component={CreateClient} />
              <Route exact path="/clients/edit/:id" component={EditClient} />
              <Route exact path="/products" component={Products} />
              <Route exact path="/orders" component={Orders} />
              <Route exact path="/contact" component={ContactForm} />
            </Switch>
          </main>
        </div>
      </Fragment>
    </Router>
  )
}

export default App;
