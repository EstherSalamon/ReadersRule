import React from 'react';
import { Route, Routes } from 'react-router-dom';
import Layout from './components/Layout';
import Home from './Pages/Home';
import { AuthComponent } from './AuthContext';
const App = () => {
    return (
        <AuthComponent>
            <Layout>
                <Routes>
                    <Route path='/' element={<Home />} />
                </Routes>
            </Layout>
        </AuthComponent>
    );
}

export default App;