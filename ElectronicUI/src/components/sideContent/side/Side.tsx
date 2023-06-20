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
