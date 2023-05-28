import clsx from 'clsx';
import style from './header.module.scss';
import { Icon } from '@fluentui/react/lib/Icon';

function Header() {
    return (
        <div className={style.header_top}>
            <div className={style.header_container}>
                <div className={style.header__top_flex}>
                    <div className={style.header__tf_left}>
                        <div className={style.header__tf_menu}>
                            <Icon iconName="CollapseMenu" />
                        </div>
                        <div className={style.header__tf_search}>
                            <div className={style.box_search}>
                                <input className={style.btn_search} placeholder="Tìm kiếm"></input>
                                <a href="/" className={style.submit_search}>
                                    <Icon iconName="Search" />
                                </a>
                            </div>
                        </div>
                    </div>
                    <h1>
                        <a href="/">
                            <img src="https://static.thanhnien.com.vn/thanhnien.vn/image/logo.svg"></img>
                        </a>
                    </h1>
                </div>
            </div>
        </div>
    );
}

export default Header;
