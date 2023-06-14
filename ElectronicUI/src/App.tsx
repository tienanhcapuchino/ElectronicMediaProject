import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { privateRoute, publishRoute } from './routes';
import { DefaultLayout } from './components/Layout';
import { initializeIcons } from '@fluentui/font-icons-mdl2';
import { AuthProvider } from './api/contexts/JWTAuthContext';

initializeIcons();
function App() {
    return (
        <Router>
            <AuthProvider>
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
                        {privateRoute.map((route, index) => {
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
            </AuthProvider>
        </Router>
    );
}

export default App;
