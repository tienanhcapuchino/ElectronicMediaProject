import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { publishRoute } from '~/routes';
import { DefaultLayout } from '~/components/Layout';
import { initializeIcons } from '@fluentui/font-icons-mdl2';

initializeIcons();
function App() {
    return (
        <Router>
            <div className="App">
                <Routes>
                    {publishRoute.map((route, index) => {
                        const Layout = route.layout || DefaultLayout;
                        const Page = route.component;
                        return (
                            <Route
                                key={index}
                                path={route.path}
                                element={
                                    <Layout>
                                        <Page />
                                    </Layout>
                                }
                            />
                        );
                    })}
                </Routes>
            </div>
        </Router>
    );
}

export default App;
