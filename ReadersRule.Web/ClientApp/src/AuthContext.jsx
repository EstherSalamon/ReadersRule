import { createContext, useContext, useEffect, useState } from 'react';
import axios from 'axios';

const AuthContext = createContext();

const AuthComponent = ({ children }) => {
    const [isLoading, setIsLoading] = useState(true);
    const [user, setUser] = useState(null);

    useEffect(() => {

        const loadData = async () => {
            const { data } = await axios.get("/api/get/currentuser");
            setUser(data);
            setIsLoading(false);
        };
        loadData();

    }, []);

    return isLoading
        ?
        <h1>Loading...</h1>
        :
        (<AuthContext.Provider value={{ user, setUser }}>
            {children}
        </AuthContext.Provider>);
}

const useAuthentication = () => useContext(AuthContext);

export { AuthComponent, useAuthentication };