import React from 'react';
import { Outlet } from 'react-router-dom';
import { Bell, Search, Envelope, User, Cog, List, LogOut } from 'lucide-react';
// import '../../assets/bootstrap/css/bootstrap.min.css';  
// import '../../assets/fonts/fontawesome-all.min.css';  




// const Layout = ({ children }) => {
//     // const { user } = useContext(AuthContext);
//     //
//     // if (!user) return null;
//
//     return (
//         <div id="wrapper" className="min-h-screen">
//             <Sidebar />
//             <div className="flex flex-col flex-1">
//                 <TopBar />
//                 <main className="p-6 bg-gray-100 flex-1">
//                     {children}
//                 </main>
//                 <Footer />
//             </div>
//         </div>
//     );
// };
//


const Layout = () => {
    // Jeśli masz AuthContext, możesz tutaj sprawdzać, czy użytkownik jest zalogowany
    // const { user } = useContext(AuthContext);
    // if (!user) return <Redirect to="/login" />;

    return (
        <div id="wrapper">
            <Sidebar/>
            <div className="d-flex flex-column" id="content-wrapper">
                <div id="content">
                    <Navbar/>
                    <div className="container-fluid">
                        <Outlet/>
                    </div>
                </div>
                <Footer/>
            </div>
            <a className="border rounded d-inline scroll-to-top" href="#page-top">
                <i className="fas fa-angle-up"></i>
            </a>
        </div>
)
    ;
};


const Sidebar = () => {
    return (
        <nav className="navbar align-items-start sidebar sidebar-dark accordion bg-gradient-primary p-0 navbar-dark" style={{ '--bs-primary': '#9fab15', '--bs-primary-rgb': '159,171,21', background: 'rgb(136,149,195)' }}>
            <div className="container-fluid d-flex flex-column p-0">
                <a className="navbar-brand d-flex justify-content-center align-items-center sidebar-brand m-0" href="#">
                    <div className="sidebar-brand-icon rotate-n-15"><i className="fas fa-laugh-wink"></i></div>
                    <div className="sidebar-brand-text mx-3"><span>Brand</span></div>
                </a>
                <hr className="sidebar-divider my-0" />
                <ul className="navbar-nav text-light" id="accordionSidebar">
                    <li className="nav-item">
                        <a className="nav-link active" href="/">
                            <span>Dashboard</span>
                        </a>
                    </li>
                    <li className="nav-item">
                        <a className="nav-link" href="/profile">
                            <span>Profile</span>
                        </a>
                    </li>
                </ul>
                <div className="text-center d-none d-md-inline">
                    <button className="btn rounded-circle border-0" id="sidebarToggle" type="button"></button>
                </div>
            </div>
        </nav>
    );
};

const Navbar = () => {
    return (
        <nav className="navbar navbar-expand bg-white shadow mb-4 topbar">
            <div className="container-fluid">
                <button className="btn btn-link d-md-none rounded-circle me-3" id="sidebarToggleTop" type="button">
                    <i className="fas fa-bars"></i>
                </button>
                <SearchBar />
                <TopBarNav />
            </div>
        </nav>
    );
};

const SearchBar = () => {
    return (
        <form className="d-none d-sm-inline-block me-auto ms-md-3 my-2 my-md-0 mw-100 navbar-search">
            <div className="input-group">
                <input className="bg-light form-control border-0 small" type="text" placeholder="Search for ..." />
                <button className="btn btn-primary py-0" type="button">
                    <Search className="w-4 h-4" />
                </button>
            </div>
        </form>
    );
};

const TopBarNav = () => {
    return (
        <ul className="navbar-nav flex-nowrap ms-auto">
            <NavNotifications />
            <NavMessages />
            <div className="d-none d-sm-block topbar-divider"></div>
            <NavProfile />
        </ul>
    );
};

const Footer = () => {
    return (
        <footer className="bg-white sticky-footer">
            <div className="container my-auto">
                <div className="text-center my-auto copyright">
                    <span>Copyright © Brand {new Date().getFullYear()}</span>
                </div>
            </div>
        </footer>
    );
};



const NavNotifications = () => {
    return (
        <li className="nav-item dropdown no-arrow mx-1">
            <div className="nav-item dropdown no-arrow">
                <a className="dropdown-toggle nav-link" aria-expanded="false" data-bs-toggle="dropdown" href="#">
                    <span className="badge bg-danger badge-counter">3+</span>
                    <Bell className="w-4 h-4" />
                </a>
                <div className="dropdown-menu dropdown-menu-end dropdown-list animated--grow-in">
                    <h6 className="dropdown-header">Alerts Center</h6>
                    <a className="dropdown-item d-flex align-items-center" href="#">
                        <div className="me-3">
                            <div className="bg-primary icon-circle">
                                <i className="fas fa-file-alt text-white"></i>
                            </div>
                        </div>
                        <div>
                            <span className="small text-gray-500">December 12, 2019</span>
                            <p>A new monthly report is ready to download!</p>
                        </div>
                    </a>
                    <a className="dropdown-item text-center small text-gray-500" href="#">Show All Alerts</a>
                </div>
            </div>
        </li>
    );
};

const NavMessages = () => {
    return (
        <li className="nav-item dropdown no-arrow mx-1">
            <div className="nav-item dropdown no-arrow">
                <a className="dropdown-toggle nav-link" aria-expanded="false" data-bs-toggle="dropdown" href="#">
                    <span className="badge bg-danger badge-counter">7</span>
                    {/*<Envelope className="w-4 h-4" />*/}
                </a>
                <div className="dropdown-menu dropdown-menu-end dropdown-list animated--grow-in">
                    <h6 className="dropdown-header">Messages Center</h6>
                    <a className="dropdown-item d-flex align-items-center" href="#">
                        <div className="dropdown-list-image me-3">
                            <img className="rounded-circle" src="/api/placeholder/32/32" alt="avatar" />
                            <div className="bg-success status-indicator"></div>
                        </div>
                        <div className="fw-bold">
                            <div className="text-truncate"><span>Hi there! I am wondering if you can help me with a problem I've been having.</span></div>
                            <p className="small text-gray-500 mb-0">Emily Fowler - 58m</p>
                        </div>
                    </a>
                    <a className="dropdown-item text-center small text-gray-500" href="#">Show All Messages</a>
                </div>
            </div>
        </li>
    );
};

const NavProfile = () => {
    return (
        <li className="nav-item dropdown no-arrow">
            <div className="nav-item dropdown no-arrow">
                <a className="dropdown-toggle nav-link" aria-expanded="false" data-bs-toggle="dropdown" href="#">
                    <span className="d-none d-lg-inline me-2 text-gray-600 small">Valerie Luna</span>
                    <img className="border rounded-circle img-profile" src="/api/placeholder/32/32" alt="Profile" />
                </a>
                <div className="dropdown-menu shadow dropdown-menu-end animated--grow-in">
                    <a className="dropdown-item" href="#">
                        <User className="w-4 h-4 me-2 text-gray-400" />
                        Profile
                    </a>
                    <a className="dropdown-item" href="#">
                        <Cog className="w-4 h-4 me-2 text-gray-400" />
                        Settings
                    </a>
                    <a className="dropdown-item" href="#">
                        <List className="w-4 h-4 me-2 text-gray-400" />
                        Activity Log
                    </a>
                    <div className="dropdown-divider"></div>
                    <a className="dropdown-item" href="#">
                        <LogOut className="w-4 h-4 me-2 text-gray-400" />
                        Logout
                    </a>
                </div>
            </div>
        </li>
    );
};


export default Layout;