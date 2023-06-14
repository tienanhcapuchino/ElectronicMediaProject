import Home from '~/pages/Home';
import { Fragment } from 'react';
import Login from '~/pages/SignIn';
import Reading from '~/pages/Reading';
import { SideBarOnly } from '~/components/Layout';
import Admin from '~/pages/Admin';

// don't need login
const publishRoute = [
    { path: '/', component: Home },
    { path: '/login', component: Login, layout: SideBarOnly },
    { path: '/reading', component: Reading, layout: SideBarOnly },
];
// need login
const privateRoute = [{ path: '/admin', component: Admin, layout: Fragment }];

export { privateRoute, publishRoute };
