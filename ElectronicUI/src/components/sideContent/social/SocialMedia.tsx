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

import React from 'react';
import style from '../side/side.module.scss';
const SocialMedia = () => {
    return (
        <>
            <section className={style.social}>
                <div className={style.socBox}>
                    <i className="fab fa-facebook-f"></i>
                    <span>12,740 Likes</span>
                </div>
                <div className={style.socBox}>
                    <i className="fab fa-pinterest"></i>
                    <span>5,600 Fans</span>
                </div>
                <div className={style.socBox}>
                    <i className="fab fa-twitter"></i>
                    <span>8,700 Followers</span>
                </div>
                <div className={style.socBox}>
                    <i className="fab fa-instagram"></i>
                    <span>22,700 Followers</span>
                </div>
                <div className={style.socBox}>
                    <i className="fab fa-youtube"></i>
                    <span>2,700 Subscriber</span>
                </div>
            </section>
        </>
    );
};

export default SocialMedia;
