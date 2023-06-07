import Header from '../components/Header';
import SideBar from '../components/SideBar';

function DefaultLayout({ children }: any) {
    return (
        <div>
            <Header />
            <SideBar />
            {children}
        </div>
    );
}

export default DefaultLayout;
