import Header from '../components/Header';
import SideBar from '../components/SideBar';

function DefaultLayout({ children }) {
    return (
        <div>
            <Header />
            <SideBar />
            <div>{children}</div>
        </div>
    );
}

export default DefaultLayout;
