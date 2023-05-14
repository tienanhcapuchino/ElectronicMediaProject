import clsx from 'clsx';
import style from './header.module.scss';
const cx = clsx.bind(style);
function Header() {
    return <div className={cx('container')}>Header</div>;
}

export default Header;
