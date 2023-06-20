import { GET_POST_BY_ID } from '../actions/PostActions';

const initialState = {
    productList: [],
    cartList: [],
};

const PostReducer = function (state = initialState, action: any) {
    switch (action.type) {
        case GET_POST_BY_ID: {
            return {
                ...state,
                product: action.payload,
            };
        }
        default: {
            return {
                ...state,
            };
        }
    }
};

export default PostReducer;
