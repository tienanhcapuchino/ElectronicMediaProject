import { combineReducers } from 'redux';
import EcommerceReducer from './EcommerceReducer';
import NotificationReducer from './NotificationReducer';

const RootReducer = combineReducers({
    notifications: NotificationReducer,
    ecommerce: EcommerceReducer,
});

export default RootReducer;
