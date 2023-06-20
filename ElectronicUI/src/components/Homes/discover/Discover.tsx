import Heading from '../../heading/Heading';
import style from './discover.module.scss';

const Discover = () => {
    return (
        <>
            <section className={style.discover}>
                <div className={style.container}>
                    <Heading title="Discover" />
                    <div className={style.content}>
                        {/* {discover.map((val) => {
                            return (
                                <div className="box">
                                    <div className="img">
                                        <img src={val.cover} alt="" />
                                    </div>
                                    <h1 className="title">{val.title}</h1>
                                </div>
                            );
                        })} */}
                    </div>
                </div>
            </section>
        </>
    );
};

export default Discover;
