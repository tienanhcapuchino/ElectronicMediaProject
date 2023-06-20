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
