import * as React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import Counter from './components/Counter';
import FetchData from './components/FetchData';
import Login from './components/Login'

import './custom.css'

export default () => (
    <Layout>

        <Route exact path="/" component={Login} />
        <Route exact path='/home' component={Home} />
        <Route exact path='/counter' component={Counter} />
        <Route exact path='/fetch-data/:startDateIndex?' component={FetchData} />
    </Layout>
);
