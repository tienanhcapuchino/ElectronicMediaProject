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

import {
  CREATE_NOTIFICATION,
  DELETE_ALL_NOTIFICATION,
  DELETE_NOTIFICATION,
  GET_NOTIFICATION,
} from '../actions/NotificationActions';

const initialState = [];

const NotificationReducer = function (state = initialState, action) {
  switch (action.type) {
    case GET_NOTIFICATION: {
      return [...action.payload];
    }
    case CREATE_NOTIFICATION: {
      return [...action.payload];
    }
    case DELETE_NOTIFICATION: {
      return [...action.payload];
    }
    case DELETE_ALL_NOTIFICATION: {
      return [...action.payload];
    }
    default: {
      return [...state];
    }
  }
};

export default NotificationReducer;
