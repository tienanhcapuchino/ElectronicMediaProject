import Home from '~/pages/Home';
import { Fragment } from 'react';

// don't need login
const publishRoute = [{ path: '/', component: Home }];
// need login
const privateRoute = [];

export { privateRoute, publishRoute };
