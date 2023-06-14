import { useContext } from 'react';
import NotificationContext from '~/api/contexts/NotificationContext';

const useNotification = () => useContext(NotificationContext);

export default useNotification;
