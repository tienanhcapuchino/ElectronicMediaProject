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
  ADD_PRODUCT_TO_CART,
  DELETE_PRODUCT_FROM_CART,
  GET_BRAND_LIST,
  GET_CART_LIST,
  GET_CATEGORY_LIST,
  GET_PRODUCT_LIST,
  GET_RATING_LIST,
  UPDATE_CART_AMOUNT,
} from '../actions/EcommerceActions';

const initialState = {
  productList: [],
  cartList: [],
};

const EcommerceReducer = function (state = initialState, action) {
  switch (action.type) {
    case GET_PRODUCT_LIST: {
      return {
        ...state,
        productList: [...action.payload],
      };
    }
    case GET_CATEGORY_LIST: {
      return {
        ...state,
        categoryList: [...action.payload],
      };
    }
    case GET_RATING_LIST: {
      return {
        ...state,
        ratingList: [...action.payload],
      };
    }
    case GET_BRAND_LIST: {
      return {
        ...state,
        brandList: [...action.payload],
      };
    }
    case GET_CART_LIST: {
      return {
        ...state,
        cartList: [...action.payload],
      };
    }
    case ADD_PRODUCT_TO_CART: {
      return {
        ...state,
        cartList: [...action.payload],
      };
    }
    case DELETE_PRODUCT_FROM_CART: {
      return {
        ...state,
        cartList: [...action.payload],
      };
    }
    case UPDATE_CART_AMOUNT: {
      return {
        ...state,
        cartList: [...action.payload],
      };
    }
    default: {
      return {
        ...state,
      };
    }
  }
};

export default EcommerceReducer;
