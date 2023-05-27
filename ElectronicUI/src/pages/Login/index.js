import style from './login.module.scss';
import clsx from 'clsx';

const cx = clsx.bind(style);
function Login() {
    return (
        <div className={cx('main')}>
            <div className={cx('container')}>
                <form action="" className={cx(style['form-login'])} data-ng-controller={cx('loginController')}>
                    <ul className={cx(style['tabs-login'])}>
                        <li className="tabs-login-item active">
                            <a href="#menu_1" className="tabs-link" data-ng-click="message = ''">
                                Đăng nhập
                            </a>
                        </li>
                        <li className="tabs-login-item">
                            <a href="#menu_2" className="tabs-link" data-ng-click="message = ''">
                                Đăng ký
                            </a>
                        </li>
                    </ul>

                    <div className="login-network">
                        <div className="login-by-account">
                            <p className="login-by-subtext">Bằng tài khoản mạng xã hội</p>
                        </div>
                        <div className="login-social-network">
                            <a
                                href="javascript:void(0)"
                                className="link-network"
                                data-ng-click="authExternalProvider('Google')"
                            >
                                Google
                            </a>
                            <a
                                href="javascript:void(0)"
                                className="link-network"
                                data-ng-click="authExternalProvider('Facebook')"
                            >
                                Facebook
                            </a>
                            <a
                                href="javascript:void(0)"
                                className="link-network"
                                data-ng-click="authExternalProvider('Zalo')"
                            >
                                <img src="#" alt="" className="icon-network" />
                                Zalo
                            </a>
                        </div>
                        <div className="login-or">
                            <p className="test-or">Hoặc</p>
                        </div>
                    </div>

                    <div className="login-cnt">
                        <div id="menu_1" className="login-form-1">
                            <div className="login-email">
                                <label for="" className="email-name">
                                    Email
                                </label>
                                <input
                                    type="text"
                                    className="input-email ng-pristine ng-valid"
                                    data-ng-model="loginData.email"
                                    placeholder="Nhập email"
                                />
                            </div>
                            <div className="login-password">
                                <label for="" className="email-name">
                                    Mật khẩu
                                </label>
                                <input
                                    type="password"
                                    className="input-password ng-pristine ng-valid"
                                    data-ng-model="loginData.password"
                                    placeholder="Nhập mật khẩu"
                                />
                                <a
                                    href="javascript:void(0)"
                                    tabindex="-1"
                                    className="view"
                                    onclick="viewPass(this)"
                                ></a>
                            </div>
                            <a href="/quen-mat-khau" className="forget-password">
                                Quên mật khẩu?
                            </a>
                            <div className="btn-login" data-ng-click="login()">
                                <a href="javascript:void(0)" className="link-btn">
                                    Đăng nhập
                                </a>
                            </div>
                        </div>

                        <div id="menu_2" className="login-form-2">
                            <div className="login-email">
                                <label for="" className="email-name">
                                    Email
                                </label>
                                <input
                                    type="text"
                                    className="input-email ng-pristine ng-valid"
                                    data-ng-model="registration.email"
                                    placeholder="Nhập email"
                                />
                            </div>
                            <div className="login-name">
                                <label for="" className="email-name">
                                    Tên hiển thị
                                </label>
                                <input
                                    type="text"
                                    className="input-name ng-pristine ng-valid"
                                    data-ng-model="registration.displayName"
                                    placeholder="Nhập tên"
                                />
                            </div>
                            <div className="login-password">
                                <label for="" className="email-name">
                                    Mật khẩu
                                </label>
                                <input
                                    type="password"
                                    className="input-password ng-pristine ng-valid"
                                    data-ng-model="registration.password"
                                    placeholder="Nhập mật khẩu"
                                />
                                <a
                                    href="javascript:void(0)"
                                    className="view"
                                    onclick="viewPass(this)"
                                    tabindex="-1"
                                ></a>
                            </div>
                            <div className="login-password">
                                <label for="" className="email-name">
                                    Xác nhận mật khẩu
                                </label>
                                <input
                                    type="password"
                                    className="input-password ng-pristine ng-valid"
                                    data-ng-model="registration.confirmPassword"
                                    placeholder="Nhập mật khẩu"
                                />
                                <a href="javascript:void(0)" className="view" onclick="viewPass(this)"></a>
                            </div>
                            <p className="regula">
                                Khi bấm đăng ký tài khoản bạn đã đồng ý với
                                <a
                                    href="https://thanhnien.vn/stories/chinh-sach-bao-mat"
                                    target="_blank"
                                    className="forget-password"
                                >
                                    quy định
                                </a>
                                của tòa soạn
                            </p>
                            <div className="btn-login">
                                <a href="javascript:void(0)" className="link-btn" data-ng-click="signUp()">
                                    Đăng ký tài khoản
                                </a>
                            </div>
                        </div>
                    </div>
                </form>
            </div>
            <div className="ms-Grid" dir="ltr">
                <div className="ms-Grid-row">
                    <div className="ms-Grid-col ms-sm6 ms-md4 ms-lg2">A</div>
                    <div className="ms-Grid-col ms-sm6 ms-md8 ms-lg10">B</div>
                </div>
            </div>
        </div>
    );
}

export default Login;
