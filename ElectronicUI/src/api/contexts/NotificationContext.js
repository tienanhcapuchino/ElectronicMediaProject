/*********************************************************************
 * 
 * PROPRIETARY and CONFIDENTIAL
 * 
 * This is licensed from, and is trade secret of:
 * 
 *          Group 10 - PRN231 - SU23
 *          FPT University, Education and Training zone
 *          Hoa Lac Hi-tech Park, Km29, Thang Long Highway
 *          Ha Noi, Viet Nam
 *          
 * Refer to your License Agreement for restrictions on use,
 * duplication, or disclosure
 * 
 * RESTRICTED RIGHTS LEGEND
 * 
 * Use, duplication or disclosure is the
 * subject to restriction in Articles 736 and 738 of the 
 * 2005 Civil Code, the Intellectual Property Law and Decree 
 * No. 85/2011/ND-CP amending and supplementing a number of 
 * articles of Decree 100/ND-CP/2006 of the Government of Viet Nam
 * 
 * 
 * Copy right 2023 © PRN231 - SU23 - Group 10 ®. All Rights Reserved
 * 
 * Unpublished - All rights reserved under the copyright laws 
 * of the Government of Viet Nam
*********************************************************************/

import { createContext, useEffect, useReducer } from 'react';
import axios from 'axios';

const reducer = (state, action) => {
  switch (action.type) {
    case 'LOAD_NOTIFICATIONS': {
      return { ...state, notifications: action.payload };
    }

    case 'DELETE_NOTIFICATION': {
      return { ...state, notifications: action.payload };
    }

    case 'CLEAR_NOTIFICATIONS': {
      return { ...state, notifications: action.payload };
    }

    default:
      return state;
  }
};

const NotificationContext = createContext({
  notifications: [],
  deleteNotification: () => {},
  clearNotifications: () => {},
  getNotifications: () => {},
  createNotification: () => {}
});

export const NotificationProvider = ({ children }) => {
  const [state, dispatch] = useReducer(reducer, []);

  const deleteNotification = async (notificationID) => {
    try {
      const res = await axios.post('/api/notification/delete', { id: notificationID });
      dispatch({ type: 'DELETE_NOTIFICATION', payload: res.data });
    } catch (e) {
      console.error(e);
    }
  };

  const clearNotifications = async () => {
    try {
      const res = await axios.post('/api/notification/delete-all');
      dispatch({ type: 'CLEAR_NOTIFICATIONS', payload: res.data });
    } catch (e) {
      console.error(e);
    }
  };

  const getNotifications = async () => {
    try {
      const res = await axios.get('/api/notification');
      dispatch({ type: 'LOAD_NOTIFICATIONS', payload: res.data });
    } catch (e) {
      console.error(e);
    }
  };

  const createNotification = async (notification) => {
    try {
      const res = await axios.post('/api/notification/add', { notification });
      dispatch({ type: 'CREATE_NOTIFICATION', payload: res.data });
    } catch (e) {
      console.error(e);
    }
  };

  useEffect(() => {
    getNotifications();
  }, []);

  return (
    <NotificationContext.Provider
      value={{
        getNotifications,
        deleteNotification,
        clearNotifications,
        createNotification,
        notifications: state.notifications
      }}
    >
      {children}
    </NotificationContext.Provider>
  );
};

export default NotificationContext;
