import ListFocus from '~/components/ListFocus';
import style from './home.module.scss';

function Home() {
    return (
        <div className={style.main}>
            <div className={style.content}>
                <ListFocus />
            </div>
        </div>
    );
}

export default Home;
