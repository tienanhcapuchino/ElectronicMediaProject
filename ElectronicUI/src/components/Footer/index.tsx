/*********************************************************************
 * 
 * PROPRIETARY and CONFIDENTIAL
 * 
 * This is licensed from, and is trade secret of:
 * 
 *          Group 10 - PRN231 - SU23
 *          FPT University, Education and Training zone
 *          Hoa Lac Hi-tech Park, Km29, Thang Long Highway
 *          Ha Noi, Viet Nam
 *          
 * Refer to your License Agreement for restrictions on use,
 * duplication, or disclosure
 * 
 * RESTRICTED RIGHTS LEGEND
 * 
 * Use, duplication or disclosure is the
 * subject to restriction in Articles 736 and 738 of the 
 * 2005 Civil Code, the Intellectual Property Law and Decree 
 * No. 85/2011/ND-CP amending and supplementing a number of 
 * articles of Decree 100/ND-CP/2006 of the Government of Viet Nam
 * 
 * 
 * Copy right 2023 © PRN231 - SU23 - Group 10 ®. All Rights Reserved
 * 
 * Unpublished - All rights reserved under the copyright laws 
 * of the Government of Viet Nam
*********************************************************************/

const Footer = () => {
    return (
        <>
            <footer>
                <div className="container">
                    <div className="box logo">
                        <img src="../images/tech-logo-footer.png" alt="" />
                        <p>Busan is an amazing magazine Blogger theme that is easy to customize for your needs</p>
                        <i className="fa fa-envelope"></i>
                        <span> hello@beautiful.com </span> <br />
                        <i className="fa fa-headphones"></i>
                        <span> +91 60521488</span>
                    </div>
                    <div className="box">
                        <h3>SPORT</h3>
                        <div className="item">
                            <img src="../images/hero/hero1.jpg" alt="" />
                            <p>Google To Boost Android Security In Few Days</p>
                        </div>
                        <div className="item">
                            <img src="../images/hero/hero2.jpg" alt="" />
                            <p>Cespedes play the winning Baseball Game</p>
                        </div>
                    </div>
                    <div className="box">
                        <h3>CRICKET</h3>
                        <div className="item">
                            <img src="../images/hero/hero3.jpg" alt="" />
                            <p>US Promises to give Intel aid to locate the soldiers</p>
                        </div>
                        <div className="item">
                            <img src="../images/hero/hero1.jpg" alt="" />
                            <p>Renewable energy dead as industry waits for Policy</p>
                        </div>
                    </div>
                    <div className="box">
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
            <div className="legal  ">
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
