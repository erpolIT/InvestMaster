// App.jsx
import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import LoginPage from './components/LoginPage/LoginPage';
import RegisterPage from './components/RegisterPage/RegisterPage';
import Layout from './components/Layout/Layout';
import Dashboard from './components/Dashboard/Dashboard';
import RequireAuth from './components/RequireAuth/RequireAuth';
import MarketData from './components/MarketData/MarketData';

import ProtectedRoute from './utils/ProtectedRoute';


const App = () => {
    return (
            <Routes>
                <Route path="/login" element={<LoginPage />} />
                <Route path="/register" element={<RegisterPage />} />
                <Route element={<RequireAuth />}>
                    <Route path="/" element={<Layout/>}>
                        <Route path="dashboard" element={<Dashboard />} />
                        <Route path="marketdata" element={<MarketData />} />
                    </Route>
                </Route>
            </Routes>
    );
};

export default App;

// index.jsx
