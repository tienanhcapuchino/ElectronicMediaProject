import ListMain from './ListFocusMain';
import ListSub from './ListFocusSub';
import style from './listFocus.module.scss';

function ListFocus() {
    return (
        <div className={style.list_f}>
            <div className={style.container}>
                <div className={style.list_f_flex}>
                    <ListMain />
                    <ListSub />
                </div>
            </div>
        </div>
    );
}

export default ListFocus;
