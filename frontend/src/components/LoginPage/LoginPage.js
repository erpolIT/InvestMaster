// LoginPage.jsx
import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './LoginPage.css';

import loginImage from '../../assets/img/pexels-photo-7947629.jpeg';  // Import obrazka

const LoginPage = () => {
    const navigate = useNavigate();
    const [formData, setFormData] = useState({
        email: '',
        password: '',
        rememberMe: false
    });
    const [error, setError] = useState('');

    const handleChange = (e) => {
        const { name, value, type, checked } = e.target;
        setFormData(prev => ({
            ...prev,
            [name]: type === 'checkbox' ? checked : value
        }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const response = await fetch('https://localhost:5000/login', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(formData)
            });

            if (response.ok) {
                const data = await response.json();
                localStorage.setItem('token', data.token);
                navigate('/dashboard');
            } else {
                setError('Nieprawidłowe dane logowania');
            }
        } catch (err) {
            setError('Wystąpił błąd podczas logowania');
        }
    };

    return (
        <body className="bg-gradient-primary" style={{ background: 'rgb(136,149,195)' }}>
        <div className="container">
            <div className="row justify-content-center">
                <div className="col-md-9 col-lg-12 col-xl-10">
                    <div className="card shadow-lg o-hidden border-0 my-5">
                        <div className="card-body p-0">
                            <div className="row">
                                <div className="col-lg-6 d-none d-lg-flex">
                                    <div
                                        className="flex-grow-1 bg-login-image"
                                        style={{ backgroundImage: `url(${loginImage})` }}
                                    />
                                </div>
                                <div className="col-lg-6">
                                    <div className="p-5">
                                        <div className="text-center">
                                            <h4 className="text-dark mb-4">Welcome Back!</h4>
                                        </div>
                                        {error && (
                                            <div className="alert alert-danger" role="alert">
                                                {error}
                                            </div>
                                        )}
                                        <form className="user" onSubmit={handleSubmit}>
                                            <div className="mb-3">
                                                <input
                                                    className="form-control form-control-user"
                                                    type="email"
                                                    id="exampleInputEmail"
                                                    name="email"
                                                    value={formData.email}
                                                    onChange={handleChange}
                                                    placeholder="Enter Email Address..."
                                                />
                                            </div>
                                            <div className="mb-3">
                                                <input
                                                    className="form-control form-control-user"
                                                    type="password"
                                                    id="exampleInputPassword"
                                                    name="password"
                                                    value={formData.password}
                                                    onChange={handleChange}
                                                    placeholder="Password"
                                                />
                                            </div>
                                            <div className="mb-3">
                                                <div className="custom-checkbox small">
                                                    <div className="form-check">
                                                        <input
                                                            className="form-check-input"
                                                            type="checkbox"
                                                            id="formCheck-1"
                                                            name="rememberMe"
                                                            checked={formData.rememberMe}
                                                            onChange={handleChange}
                                                        />
                                                        <label className="form-check-label" htmlFor="formCheck-1">
                                                            Remember Me
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>
                                            <button
                                                className="btn btn-primary d-block btn-user w-100"
                                                type="submit"
                                            >
                                                Login
                                            </button>
                                            <hr />
                                            <button
                                                className="btn btn-primary d-block btn-google btn-user w-100 mb-2"
                                                type="button"
                                                onClick={() => {/* Dodaj obsługę logowania przez Google */}}
                                            >
                                                <i className="fab fa-google"></i>&nbsp; Login with Google
                                            </button>
                                            <button
                                                className="btn btn-primary d-block btn-facebook btn-user w-100"
                                                type="button"
                                                onClick={() => {/* Dodaj obsługę logowania przez Facebook */}}
                                            >
                                                <i className="fab fa-facebook-f"></i>&nbsp; Login with Facebook
                                            </button>
                                            <hr />
                                        </form>
                                        <div className="text-center">
                                            <a className="small" href="forgot-password.html">Forgot Password?</a>
                                        </div>
                                        <div className="text-center">
                                            <a className="small" href="register.html">Create an Account!</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        </body>
    );
};

export default LoginPage;