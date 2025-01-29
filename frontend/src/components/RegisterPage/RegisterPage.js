import React, {useState} from 'react';
import {useNavigate} from "react-router-dom";
import {Link} from "react-router";
import registerImage from '../../assets/img/pexels-photo-7947629.jpeg';


const RegistrationPage = () => {
    //const { auth, setAuth } = useAuth();
    const navigate = useNavigate();
    const [formData, setFormData] = useState({
        first_name: '',
        last_name: '',
        email: '',
        password: '',
        password_repeat: ''
    });
    const [error, setError] = useState('');

    const handleChange = (e) => {
        const {name, value} = e.target;
        setFormData(prev => ({
            ...prev,
            [name]: value
        }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const response = await fetch('http://localhost:5000/api/auth/register', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(formData)
            });

            if (response.ok) {
                const data = await response.json();
                //console.log('Token-data:', token);
                //setAuth({ token: data.token });
                navigate('/login', { replace: true });
            } else {
                setError('Nieprawidłowe dane logowania');
            }
        } catch (err) {
            setError('Wystąpił błąd podczas logowania');
            if(!err?.response) {
                console.log('Błąd po stronie klienta:', err.message);
            }else if(err.response?.status === 400){
                console.log('Missing user')
            }
        }
    };

    return (
        <div className="container">
            <div className="card shadow-lg o-hidden border-0 my-5">
                <div className="card-body p-0">
                    <div className="row">
                        <div className="col-lg-5 d-none d-lg-flex">
                            <div
                                className="flex-grow-1 bg-register-image"
                                style={{ backgroundImage: `url(${registerImage})` }}
                            />
                        </div>
                        <div className="col-lg-7">
                            <div className="p-5">
                                <div className="text-center">
                                    <h4 className="text-dark mb-4">Create an Account!</h4>
                                </div>
                                <form className="user" onSubmit={handleSubmit}>
                                    <div className="row mb-3">
                                        <div className="col-sm-6 mb-3 mb-sm-0">
                                            <input
                                                className="form-control form-control-user"
                                                type="text"
                                                id="exampleFirstName"
                                                placeholder="First Name"
                                                name="first_name"
                                                value={formData.first_name}
                                                onChange={handleChange}
                                            />
                                        </div>
                                        <div className="col-sm-6">
                                            <input
                                                className="form-control form-control-user"
                                                type="text"
                                                id="exampleLastName"
                                                placeholder="Last Name"
                                                name="last_name"
                                                value={formData.last_name}
                                                onChange={handleChange}
                                            />
                                        </div>
                                    </div>
                                    <div className="mb-3">
                                        <input
                                            className="form-control form-control-user"
                                            type="email"
                                            id="exampleInputEmail"
                                            aria-describedby="emailHelp"
                                            placeholder="Email Address"
                                            name="email"
                                            value={formData.email}
                                            onChange={handleChange}
                                        />
                                    </div>
                                    <div className="row mb-3">
                                        <div className="col-sm-6 mb-3 mb-sm-0">
                                            <input
                                                className="form-control form-control-user"
                                                type="password"
                                                id="examplePasswordInput"
                                                placeholder="Password"
                                                name="password"
                                                value={formData.password}
                                                onChange={handleChange}
                                            />
                                        </div>
                                        <div className="col-sm-6">
                                            <input
                                                className="form-control form-control-user"
                                                type="password"
                                                id="exampleRepeatPasswordInput"
                                                placeholder="Repeat Password"
                                                name="password_repeat"
                                                value={formData.password_repeat}
                                                onChange={handleChange}
                                            />
                                        </div>
                                    </div>
                                    <button
                                        className="btn btn-primary d-block btn-user w-100"
                                        type="submit"
                                    >
                                        Register Account
                                    </button>
                                    <hr />
                                    <a
                                        className="btn btn-primary d-block btn-google btn-user w-100 mb-2"
                                        role="button"
                                    >
                                        <i className="fab fa-google"></i>&nbsp; Register with Google
                                    </a>
                                    <a
                                        className="btn btn-primary d-block btn-facebook btn-user w-100"
                                        role="button"
                                    >
                                        <i className="fab fa-facebook-f"></i>&nbsp; Register with Facebook
                                    </a>
                                    <hr />
                                </form>
                                <div className="text-center">
                                    <a className="small" href="forgot-password.html">
                                        Forgot Password?
                                    </a>
                                </div>
                                <div className="text-center">
                                    <Link className="small" to="/login">
                                        Already have an account? Login!
                                    </Link>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default RegistrationPage;