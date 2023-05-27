import style from './sideBar.module.scss';
import Nav from '../Nav';

function SideBar() {
    return (
        <div className={style.wrapcontainer}>
            <div className={style.sidebar}>
                <Nav />
            </div>
        </div>
    );
}

export default SideBar;
