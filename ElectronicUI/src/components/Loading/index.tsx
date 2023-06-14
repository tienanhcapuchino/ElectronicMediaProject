import { CircularProgress, Backdrop } from '@mui/material';

const Loading = ({ open2 }: any) => {
    return (
        <Backdrop sx={{ color: '#fff', zIndex: (theme) => theme.zIndex.drawer + 1 }} open={open2}>
            <CircularProgress color="inherit" />
        </Backdrop>
    );
};

export default Loading;
