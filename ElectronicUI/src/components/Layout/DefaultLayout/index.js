import Header from './Header';
import SideBar from './SideBar';

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
