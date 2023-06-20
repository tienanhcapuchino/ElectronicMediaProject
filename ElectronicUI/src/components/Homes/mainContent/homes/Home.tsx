import Side from '../../../sideContent/side/Side';
import Popular from '../popular/Popular';
import style from './style.module.scss';
import { menuItem } from '~/helper/data';

const Homes = () => {
    return (
        <>
            <main>
                <div className={style.container}>
                    <section className={style.mainContent}>
                        {menuItem.map((item, index) => (
                            <Popular key={index} header={item.name} />
                        ))}
                    </section>
                    <section className={style.sideContent}>
                        <Side />
                    </section>
                </div>
            </main>
        </>
    );
};

export default Homes;
