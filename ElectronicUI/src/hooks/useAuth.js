import { useContext } from 'react';
import AuthContext from '~/api/contexts/JWTAuthContext';

const useAuth = () => useContext(AuthContext);

export default useAuth;
