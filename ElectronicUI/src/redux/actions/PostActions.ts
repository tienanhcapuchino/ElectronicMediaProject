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

import { Endpoint as api } from '~/api/helpers';
import axios from 'axios';
export const GET_POST_BY_ID = 'GET_POST_BY_ID';

export const getPostByID = (object: any) => async (dispatch: any) => {
    const query: string = `?id=${object.id}`;
    const res = await axios.get(`${api.getpost}${query}`).then((res) => {
        dispatch({
            type: GET_POST_BY_ID,
            payload: res.data,
        });
    });
};

export const getPostPagding = (object: any) => async (dispatch: any) => {
    return axios.post(`${api.getPostPagding}`, object).then((result) => result.data);
};
