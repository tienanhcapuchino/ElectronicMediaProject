import style from './hero.module.scss';
import Card from './Card';
import { useState } from 'react';

const Hero = () => {
    const [items, setIems] = useState();
    return (
        <>
            <section className={style.hero}>
                <div className={style.container}>
                    {/* {items.map((item: any) => {
                        return (
                            <>
                                <Card key={item.id} item={item} />
                            </>
                        );
                    })} */}
                </div>
            </section>
        </>
    );
};

export default Hero;
