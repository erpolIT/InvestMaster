// import './App.css';
// import React, { useEffect, useState } from 'react';
//
// function App() {
//     const [users, setUsers] = useState([]);
//     const [message, setMessage] = useState('');
//     const [newUser, setNewUser] = useState('');
//
//     // Funkcja do pobierania użytkowników
//     useEffect(() => {
//         fetch('http://localhost:5000/user') // Upewnij się, że endpoint jest poprawny
//             .then(response => response.json())
//             .then(data => setUsers(data))  // Ustawienie pobranych użytkowników w stanie
//             .catch(error => console.error('Error fetching users:', error));
//     }, []);
//
//     // Funkcja do dodawania nowego użytkownika
//     const addUser = () => {
//         fetch('http://localhost:5000/user', {
//             method: 'POST',
//             headers: {
//                 'Content-Type': 'application/json',
//             },
//             body: JSON.stringify({ name: newUser }), // Przesyłanie danych w formacie JSON
//         })
//             .then(response => response.json())
//             .then(data => {
//                 setMessage(data);  // Wyświetlanie komunikatu z backendu
//                 setNewUser('');     // Czyszczenie pola wejściowego
//                 // Po dodaniu użytkownika, odśwież dane
//                 fetchUsers();
//             })
//             .catch(error => console.error('Error adding user:', error));
//     };
//
//     // Funkcja do ponownego pobierania użytkowników
//     const fetchUsers = () => {
//         fetch('http://localhost:5000/user') // Adres API backendu
//             .then(response => response.json())
//             .then(data => setUsers(data))  // Ustawienie pobranych użytkowników
//             .catch(error => console.error('Error fetching users:', error));
//     };
//
//     return (
//         <div className="App">
//             <h1>Hi, It's Users' List</h1>
//             <ul>
//                 {users.map((user, index) => (
//                     <li key={index}>{user}</li>  // Wyświetlanie nazwy użytkownika
//                 ))}
//             </ul>
//             <div>
//                 <input
//                     type="text"
//                     value={newUser}
//                     onChange={(e) => setNewUser(e.target.value)}  // Aktualizacja wartości w stanie
//                     placeholder="Enter new user"
//                 />
//                 <button onClick={addUser}>Add User</button>
//             </div>
//             {message && <p>{message}</p>}  // Wyświetlanie komunikatu
//         </div>
//     );
// }
//
// export default App;


// App.jsx
import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import LoginPage from './components/LoginPage/LoginPage';
import Layout from './components/Layout/Layout';
import Dashboard from './components/Dashboard/Dashboard';

// import Profile from './components/Profile/Profile';
// import Settings from './components/Settings/Settings';
// import { AuthProvider } from './context/AuthContext';

// import Dashboard from './components/Dashboard';

// Komponent do sprawdzania czy użytkownik jest zalogowany

// const ProtectedRoute = ({ children }) => {
//     const token = localStorage.getItem('token');
//     if (!token) {
//         return <Navigate to="/login" replace />;
//     }
//     return children;
// };

const App = () => {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/login" element={<LoginPage />} />
                <Route path="/" element={<Layout />}>
                    <Route path="dashboard" element={<Dashboard />} />
                </Route>
            </Routes>
        </BrowserRouter>
    );
};

export default App;

// index.jsx
