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

import axios from 'axios';

export const GET_PRODUCT_LIST = 'GET_PRODUCT_LIST';
export const GET_CART_LIST = 'GET_CART_LIST';
export const GET_CATEGORY_LIST = 'GET_CATEGORY_LIST';
export const GET_RATING_LIST = 'GET_RATING_LIST';
export const GET_BRAND_LIST = 'GET_BRAND_LIST';

export const ADD_PRODUCT_TO_CART = 'ADD_PRODUCT_TO_CART';
export const DELETE_PRODUCT_FROM_CART = 'DELETE_PRODUCT_FROM_CART';

export const UPDATE_CART_AMOUNT = 'UPDATE_CART_AMOUNT';

export const getProductList = () => (dispatch) => {
  axios.get('/api/ecommerce/get-product-list').then((res) => {
    dispatch({
      type: GET_PRODUCT_LIST,
      payload: res.data,
    });
  });
};

export const getCategoryList = () => (dispatch) => {
  axios.get('/api/ecommerce/get-category-list').then((res) => {
    dispatch({
      type: GET_CATEGORY_LIST,
      payload: res.data,
    });
  });
};

export const getRatingList = () => (dispatch) => {
  axios.get('/api/ecommerce/get-rating-list').then((res) => {
    dispatch({
      type: GET_RATING_LIST,
      payload: res.data,
    });
  });
};

export const getBrandList = () => (dispatch) => {
  axios.get('/api/ecommerce/get-brand-list').then((res) => {
    dispatch({
      type: GET_BRAND_LIST,
      payload: res.data,
    });
  });
};

export const getCartList = (uid) => (dispatch) => {
  axios.get('/api/ecommerce/get-cart-list', { data: uid }).then((res) => {
    dispatch({
      type: GET_CART_LIST,
      payload: res.data,
    });
  });
};

export const addProductToCart = (uid, productId) => (dispatch) => {
  axios.post('/api/ecommerce/add-to-cart', { uid, productId }).then((res) => {
    console.log(res.data);
    dispatch({
      type: ADD_PRODUCT_TO_CART,
      payload: res.data,
    });
  });
};

export const deleteProductFromCart = (uid, productId) => (dispatch) => {
  axios.post('/api/ecommerce/delete-from-cart', { uid, productId }).then((res) => {
    dispatch({
      type: DELETE_PRODUCT_FROM_CART,
      payload: res.data,
    });
  });
};

export const updateCartAmount = (uid, productId, amount) => (dispatch) => {
  console.log(uid, productId, amount);
  axios.post('/api/ecommerce/update-cart-amount', { uid, productId, amount }).then((res) => {
    dispatch({
      type: UPDATE_CART_AMOUNT,
      payload: res.data,
    });
  });
};
