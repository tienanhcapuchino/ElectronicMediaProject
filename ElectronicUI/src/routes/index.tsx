/*********************************************************************
 * 
 * PROPRIETARY and CONFIDENTIAL
 * 
 * This is licensed from, and is trade secret of:
 * 
 *          Group 10 - PRN231 - SU23
 *          FPT University, Education and Training zone
 *          Hoa Lac Hi-tech Park, Km29, Thang Long Highway
 *          Ha Noi, Viet Nam
 *          
 * Refer to your License Agreement for restrictions on use,
 * duplication, or disclosure
 * 
 * RESTRICTED RIGHTS LEGEND
 * 
 * Use, duplication or disclosure is the
 * subject to restriction in Articles 736 and 738 of the 
 * 2005 Civil Code, the Intellectual Property Law and Decree 
 * No. 85/2011/ND-CP amending and supplementing a number of 
 * articles of Decree 100/ND-CP/2006 of the Government of Viet Nam
 * 
 * 
 * Copy right 2023 © PRN231 - SU23 - Group 10 ®. All Rights Reserved
 * 
 * Unpublished - All rights reserved under the copyright laws 
 * of the Government of Viet Nam
*********************************************************************/

import Home from '~/pages/Home';
import { Fragment } from 'react';
import Login from '~/pages/SignIn';
import Reading from '~/pages/Reading';
import { SideBarOnly } from '~/components/Layout';
import Admin from '~/pages/Admin';
import User from '~/pages/Admin/Users/user';
import LayoutAdmin from '~/components/Layout/LayoutAdmin';

// don't need login
const publishRoute = [
    { path: '/', component: Home },
    { path: '/login', component: Login, layout: SideBarOnly },
    { path: '/reading', component: Reading, layout: SideBarOnly },
];
// need login
const privateRoute = [
{ 
    path: '/admin', component: Admin, layout: LayoutAdmin,
},
{
    path: '/admin/users', component: User, layout: LayoutAdmin
}];

export { privateRoute, publishRoute };
