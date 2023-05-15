import { menuItem } from '~/helpers/data';
import clsx from 'clsx';
import style from './nav.module.scss';
import { Icon } from '@fluentui/react/lib/Icon';

const cx = clsx.bind(style);
function NavMenu() {
    return (
        <ul className={cx(style.header_top_nav)}>
            <li className={cx(style['header-top_nav-item'])}>
                <a href="/">
                    <Icon iconName="Home" />
                </a>
            </li>
            {menuItem.map((item, index) => {
                return (
                    <li className={cx(style['header-top_nav-item'])} key={index}>
                        <a href="/">{item.name}</a>
                    </li>
                );
            })}
        </ul>
    );
}

export default NavMenu;
