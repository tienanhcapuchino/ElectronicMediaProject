import SideBar from '../components/SideBar';
import style from './sideBarOnly.module.scss';
import clsx from 'clsx';
const cx = clsx.bind(style);
function SideBarOnly({ children }) {
    return (
        <div>
            <div className={cx(style.logo)}>
                <a href="https://thanhnien.vn/" title="Báo thanh niên">
                    <img alt="Báo thanh niên" src="https://static.mediacdn.vn/thanhnien.vn/image/logo.svg" />
                </a>
            </div>
            <SideBar />
            {children}
        </div>
    );
}

export default SideBarOnly;
