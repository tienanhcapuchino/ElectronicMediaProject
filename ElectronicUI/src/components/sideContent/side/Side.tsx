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
import style from './side.module.scss';
import Slider from 'react-slick';
import Heading from '~/components/heading/Heading';
import Tpost from '../Tpost/Tpost';
import SocialMedia from '../social/SocialMedia';

//const allCat = [...new Set(popular.map((curEle) => curEle.catgeory))]
//console.log(allCat)

const Side = () => {
    const settings = {
        dots: false,
        infinite: true,
        speed: 500,
        slidesToShow: 1,
        slidesToScroll: 1,
    };

    const catgeory = ['world', 'travel', 'sport', 'fun', 'health', 'fashion', 'business', 'technology'];
    return (
        <>
            <Heading title="Stay Connected" />
            <SocialMedia />

            <Heading title="Subscribe" />

            <section className={style.subscribe}>
                <h1 className={style.title}>Subscribe to our New Stories</h1>
                <form action="">
                    <input type="email" placeholder="Email Address..." />
                    <button>
                        <i className="fa fa-paper-plane"></i> SUBMIT
                    </button>
                </form>
            </section>

            <section className="banner">
                <img src="./images/sidebar-banner-new.jpg" alt="" />
            </section>

            <Tpost />

            <section className={style.catgorys}>
                <Heading title="Catgeorys" />
                {/*<div className='items'>{allCat}</div>*/}
                {catgeory.map((val) => {
                    return (
                        <div className={style.category1}>
                            <span>{val}</span>
                        </div>
                    );
                })}
            </section>

            <section className={style.gallery}>
                <Heading title="Gallery" />
                <Slider {...settings}>
                    {/* {gallery.map((val: any) => {
                        return (
                            <div className="img">
                                <img src={val.cover} alt="" />
                            </div>
                        );
                    })} */}
                </Slider>
            </section>
        </>
    );
};

export default Side;
