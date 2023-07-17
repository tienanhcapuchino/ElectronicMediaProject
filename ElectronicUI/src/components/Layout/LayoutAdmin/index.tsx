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

import navLinkGroups from "~/helper/layoutAdmin";
import { Nav, INavLink, INavStyles, INavLinkGroup, initializeIcons } from '@fluentui/react';
import './layoutAdmin.scss'

initializeIcons();
const navStyles: Partial<INavStyles> = {
    root: {
        width: 200,
        height: '100vh',
        boxSizing: 'border-box',
        border: '1px solid #eee',
        overflowY: 'auto',
    },
};
function LayoutAdmin({ children }: any) {
    return (
        <div className="nav-admin">
            <div>
                <Nav
                    groups={navLinkGroups}
                    styles={navStyles}
                />
            </div>
            <div>
                {children}
            </div>
        </div>
    )
};

export default LayoutAdmin;