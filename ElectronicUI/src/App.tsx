import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { publishRoute } from './routes';
import { DefaultLayout } from './components/Layout';
import { initializeIcons } from '@fluentui/font-icons-mdl2';
import { Suspense } from 'react';

initializeIcons();
function App() {
    return (
        <Router>
            <div className="App">
                <Suspense fallback={<div>Loading...</div>}>
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
                </Suspense>
            </div>
        </Router>
    );
}

export default App;
