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

import style from './popular..module.scss';

import Slider from 'react-slick';
import 'slick-carousel/slick/slick.css';
import 'slick-carousel/slick/slick-theme.css';
import Heading from '~/components/heading/Heading';

const Popular = ({ header }: any) => {
    const settings = {
        className: 'center',
        centerMode: false,
        infinite: true,
        centerPadding: '0',
        slidesToShow: 2,
        speed: 500,
        rows: 4,
        slidesPerRow: 1,
        responsive: [
            {
                breakpoint: 800,
                settings: {
                    slidesToShow: 1,
                    slidesToScroll: 1,
                    rows: 4,
                },
            },
        ],
    };
    return (
        <>
            <section className={style.popular}>
                <Heading title={header} />
                <div className={style.content}>
                    <Slider {...settings}>
                        {/* {popular.map((val: any) => {
                            return (
                                <div className="items">
                                    <div className="box shadow">
                                        <div className="images row">
                                            <div className="img">
                                                <img src={val.cover} alt="" />
                                            </div>
                                            <div className="category category1">
                                                <span>{val.catgeory}</span>
                                            </div>
                                        </div>
                                        <div className="text row">
                                            <h1 className="title">{val.title.slice(0, 40)}...</h1>
                                            <div className="date">
                                                <i className="fas fa-calendar-days"></i>
                                                <label>{val.date}</label>
                                            </div>
                                            <div className="comment">
                                                <i className="fas fa-comments"></i>
                                                <label>{val.comments}</label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            );
                        })} */}
                    </Slider>
                </div>
            </section>
        </>
    );
};

export default Popular;
