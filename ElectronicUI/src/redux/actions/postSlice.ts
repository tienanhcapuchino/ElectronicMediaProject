import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import { PostService } from '~/api';

const initialState = { data: [], isloading: true, isError: false };

const postSlice = createSlice({
    name: 'Post',
    initialState,
    reducers: {
        post: (state, action) => {
            state.data = action.payload;
        },
    },
    extraReducers: (builder) => {
        builder.addCase(getPostByPaging.pending, (state) => {
            state.isloading = false;
        });
        builder.addCase(getPostByPaging.fulfilled, (state, action) => {
            (state.isloading = false), (state.data = action.payload);
        });
        builder.addCase(getPostByPaging.rejected, (state) => {
            state.isError = false;
        });
    },
});

export const { post } = postSlice.actions;

export default postSlice.reducer;

export const getPostByPaging = createAsyncThunk('getPostPaging', async (object: any) => {
    const data = await PostService.getPostPagding(object);
    return data;
});
