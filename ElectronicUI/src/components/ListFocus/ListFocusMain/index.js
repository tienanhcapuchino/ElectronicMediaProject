import style from './listFocusMain.module.scss';

import clsx from 'clsx';

const cx = clsx.bind(style);
function ListMain() {
    return (
        <div className={style.list__focus_main}>
            <div className="box_category">
                <div className="middle">
                    <div className={style.main_item}>
                        <div className={style.item_first}>
                            <a href="/">
                                <img src="https://images2.thanhnien.vn/zoom/462_289/528068263637045248/2023/5/27/2023-05-27t071614z963452423rc2s61aemgjqrtrmadp3kosovo-serbs-violence-1685197704538304342654-130-0-1380-2000-crop-16851978696921376974063.jpg"></img>
                            </a>
                            <div>
                                <h2>
                                    <a href="/">
                                        Serbia duy trì tình trạng báo động, Nga lên tiếng về vụ căng thẳng tại Kosovo
                                    </a>
                                </h2>
                                <p>
                                    Quân đội Serbia vẫn đang trong tình trạng báo động cao nhất sau vụ đụng độ giữa
                                    người Serb và cảnh sát Kosovo, vụ việc mà Nga tố Kosovo và phương Tây chịu trách
                                    nhiệm.
                                </p>
                            </div>
                        </div>
                        <div className={style.item_reletive}>
                            <div className="box_cate_item">
                                <a>
                                    <img src="https://images2.thanhnien.vn/zoom/254_159/528068263637045248/2023/5/27/24c-16851946677861267713641-0-168-969-1718-crop-16851948748291681483594.jpg"></img>
                                </a>
                                <div>
                                    <h3>
                                        <a href="/">Nhật Bản tập trận bắn đạn thật tái chiếm đảo</a>
                                    </h3>
                                </div>
                            </div>
                            <div className="box_cate_item">
                                <a>
                                    <img src="https://images2.thanhnien.vn/zoom/254_159/528068263637045248/2023/5/27/24c-16851946677861267713641-0-168-969-1718-crop-16851948748291681483594.jpg"></img>
                                </a>
                                <div>
                                    <h3>
                                        <a href="/">Nhật Bản tập trận bắn đạn thật tái chiếm đảo</a>
                                    </h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className="sub_item"></div>
                </div>
            </div>
        </div>
    );
}

export default ListMain;
