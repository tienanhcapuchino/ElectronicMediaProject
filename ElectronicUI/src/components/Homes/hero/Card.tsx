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

import { Link } from 'react-router-dom';
import style from './hero.module.scss';

const Card = ({ item: { id, cover, catgeory, title, authorName, time } }: any) => {
    return (
        <>
            <div className={style.box}>
                <div className={style.img}>
                    <img src={cover} alt="" />
                </div>
                <div className={style.text}>
                    <span className={style.category}>{catgeory}</span>
                    {/*<h1 className='titleBg'>{title}</h1>*/}
                    <Link to={`/SinglePage/${id}`}>
                        <h1 className={style.titleBg}>{title}</h1>
                    </Link>
                    <div className="author flex">
                        <span>by {authorName}</span>
                        <span>{time}</span>
                    </div>
                </div>
            </div>
        </>
    );
};

export default Card;
