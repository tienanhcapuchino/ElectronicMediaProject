import React from 'react';
import Heading from '~/components/heading/Heading';
import './tpost.module.scss';

const Tpost = () => {
    return (
        <>
            <section className="tpost">
                <Heading title="Tiktok post" />
                {/* {tpost.map((val : any) => {
                    return (
                        <div className="box flexSB">
                            <div className="img">
                                <img src={val.cover} alt="" />
                            </div>
                            <div className="text">
                                <h1 className="title">{val.title.slice(0, 35)}...</h1>
                                <span>a year ago</span>
                            </div>
                        </div>
                    );
                })} */}
            </section>
        </>
    );
};

export default Tpost;
