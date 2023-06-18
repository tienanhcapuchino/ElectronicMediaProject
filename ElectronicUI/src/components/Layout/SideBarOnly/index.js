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

import SideBar from '../components/SideBar';
import style from './sideBarOnly.module.scss';
import clsx from 'clsx';
const cx = clsx.bind(style);
function SideBarOnly({ children }) {
    return (
        <div>
            <div className={cx(style.logo)}>
                <a href="https://thanhnien.vn/" title="Báo thanh niên">
                    <img alt="Báo thanh niên" src="https://static.mediacdn.vn/thanhnien.vn/image/logo.svg" />
                </a>
            </div>
            <SideBar />
            {children}
        </div>
    );
}

export default SideBarOnly;
