import clsx from 'clsx';
import style from './sideBar.module.scss';
import Nav from '../Nav';

const cx = clsx.bind(style);
function SideBar() {
    return (
        <div className={cx('wrapcontainer')}>
            <div className={cx('sidebar')}>
                <Nav />
            </div>
        </div>
    );
}

export default SideBar;
