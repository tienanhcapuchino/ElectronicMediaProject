import Home from '~/pages/Home';
import { Fragment } from 'react';
import Login from '~/pages/Login';
import { SideBarOnly } from '~/components/Layout';

// don't need login
const publishRoute = [
    { path: '/', component: Home },
    { path: '/login', component: Login, layout: SideBarOnly },
];
// need login
const privateRoute = [];

export { privateRoute, publishRoute };
