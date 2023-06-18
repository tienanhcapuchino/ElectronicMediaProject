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

import clsx from 'clsx';
import style from './header.module.scss';
import { Icon } from '@fluentui/react/lib/Icon';
import LoginIcon from '@mui/icons-material/Login';

function Header() {
    return (
        <div className={style.header_top}>
            <div className={style.header_container}>
                <div className={style.header__top_flex}>
                    <div className={style.header__tf_left}>
                        <div className={style.header__tf_menu}>
                            <Icon iconName="CollapseMenu" />
                        </div>
                        <div className={style.header__tf_search}>
                            <div className={style.box_search}>
                                <input className={style.btn_search} placeholder="Tìm kiếm"></input>
                                <a href="/" className={style.submit_search}>
                                    <Icon iconName="Search" />
                                </a>
                            </div>
                        </div>
                    </div>
                    <h1>
                        <a href="/">
                            <img src="https://static.thanhnien.com.vn/thanhnien.vn/image/logo.svg"></img>
                        </a>
                    </h1>
                    <div className="login">
                        <LoginIcon />
                    </div>
                </div>
            </div>
        </div>
    );
}

export default Header;
