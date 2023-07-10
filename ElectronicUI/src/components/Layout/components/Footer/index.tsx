import style from './footer.module.scss';
import { Icon } from '@fluentui/react/lib/Icon';

const Footer = () => {
    return (
        <>
            <footer>
                <div className={style.container}>
                    <div className={style.logo}>
                        <img src="../images/tech-logo-footer.png" alt="" />
                        <p>Busan is an amazing magazine Blogger theme that is easy to customize for your needs</p>
                        <i>
                            <Icon iconName="IncomingCall" />
                        </i>
                        <span> hello@beautiful.com </span> <br />
                        <i className="fa fa-headphones"></i>
                        <span> +91 60521488</span>
                    </div>
                    <div className={style.box}>
                        <h3>SPORT</h3>
                        <div className={style.item}>
                            <img src="../images/hero/hero1.jpg" alt="" />
                            <p>Google To Boost Android Security In Few Days</p>
                        </div>
                        <div className={style.item}>
                            <img src="../images/hero/hero2.jpg" alt="" />
                            <p>Cespedes play the winning Baseball Game</p>
                        </div>
                    </div>
                    <div className={style.box}>
                        <h3>CRICKET</h3>
                        <div className={style.item}>
                            <img src="../images/hero/hero3.jpg" alt="" />
                            <p>US Promises to give Intel aid to locate the soldiers</p>
                        </div>
                        <div className={style.item}>
                            <img src="../images/hero/hero1.jpg" alt="" />
                            <p>Renewable energy dead as industry waits for Policy</p>
                        </div>
                    </div>
                    <div className={style.box}>
                        <h3>LABELS</h3>
                        {/*<i className='fa fa-chevron-right'></i>*/}
                        <ul>
                            <li>
                                <span>Boxing</span> <label>(5)</label>
                            </li>
                            <li>
                                <span>Fashion</span> <label>(6)</label>
                            </li>
                            <li>
                                <span>Health</span> <label>(7)</label>
                            </li>
                            <li>
                                <span>Nature</span> <label>(9)</label>
                            </li>
                        </ul>
                    </div>
                </div>
            </footer>
            <div className={style.legal}>
                <div className="container flexSB">
                    <p>© all rights reserved</p>
                    <p>
                        made with <i className="fa fa-heart"></i> by gorkhcoder
                    </p>
                </div>
            </div>
        </>
    );
};

export default Footer;
