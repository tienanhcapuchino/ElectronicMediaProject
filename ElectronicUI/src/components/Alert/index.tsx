import { Snackbar } from '@mui/material';
import MuiAlert, { AlertProps } from '@mui/material/Alert';
import * as React from 'react';

const Alert = React.forwardRef<HTMLDivElement, AlertProps>(function Alert(props, ref) {
    return <MuiAlert elevation={6} ref={ref} variant="filled" {...props} />;
});

function AlertMessage({ open, type, message }: any) {
    return (
        <Snackbar
            autoHideDuration={6000}
            anchorOrigin={{ vertical: 'bottom', horizontal: 'right' }}
            open={open}
            key={'bottom' + 'right'}
        >
            <Alert severity={type} sx={{ width: '100%' }}>
                {message}
            </Alert>
        </Snackbar>
    );
}

export default AlertMessage;
