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

import React, { useState } from 'react';
import { Formik } from 'formik';
import * as Yup from 'yup';
import { Container, Box, Typography, TextField, FormControlLabel, Checkbox, Button, Grid } from '@mui/material';
import useAuth from '~/hooks/useAuth';
import { useNavigate } from 'react-router-dom';
import Loading from '~/components/Loading';
import AlertMessage from '~/components/Alert';
import { createTheme, ThemeProvider } from '@mui/material/styles';

const validationSchema = Yup.object().shape({
    username: Yup.string().email('Invalid Email address').required('Email is required!'),
    password: Yup.string().min(6, 'Password must be 6 characters long').required('Password is required!'),
});

function Login() {
    const { login, user } = useAuth();
    const [open, setOpen] = useState(false);
    const [openAlert, setOpenAlert] = useState(false);
    const [message, setMessage] = useState('');
    const initialValues = {
        username: '',
        password: '',
    };
    const defaultTheme = createTheme();

    const navigate = useNavigate();

    const handleLogin = async (values) => {
        setOpen(true);
        try {
            await login(values);
            navigate('/');
        } catch (error) {
            setOpen(false);
            setOpenAlert(true);
            setMessage('Login False, please try again!');
            console.log(error);
        }
        if (!user.isSucceed) {
            setOpen(false);
            setOpenAlert(true);
            setMessage(user.message);
        }
    };

    return (
        <ThemeProvider theme={defaultTheme}>
            <Container component="main" maxWidth="xs">
                <Box sx={{ marginTop: 8, display: 'flex', flexDirection: 'column', alignItems: 'center' }}>
                    <Typography component="h1" variant="h5">
                        Sign in
                    </Typography>
                    <Formik initialValues={initialValues} validationSchema={validationSchema} onSubmit={handleLogin}>
                        {({ values, errors, touched, handleChange, handleBlur, handleSubmit }) => (
                            <Box component="form" sx={{ mt: 1 }} onSubmit={handleSubmit}>
                                <TextField
                                    fullWidth
                                    size="small"
                                    type="email"
                                    name="username"
                                    label="Email"
                                    variant="outlined"
                                    onBlur={handleBlur}
                                    value={values.username}
                                    onChange={handleChange}
                                    helperText={touched.username && errors.username}
                                    error={Boolean(errors.username && touched.username)}
                                    sx={{ mb: 3 }}
                                />

                                <TextField
                                    fullWidth
                                    size="small"
                                    name="password"
                                    type="password"
                                    label="Password"
                                    variant="outlined"
                                    onBlur={handleBlur}
                                    value={values.password}
                                    onChange={handleChange}
                                    helperText={touched.password && errors.password}
                                    error={Boolean(errors.password && touched.password)}
                                    sx={{ mb: 1.5 }}
                                />
                                <FormControlLabel
                                    control={<Checkbox value="remember" color="primary" />}
                                    label="Remember me"
                                />
                                <Button type="submit" fullWidth variant="contained" sx={{ mt: 3, mb: 2 }}>
                                    Sign In
                                </Button>
                                <Grid container>
                                    <Grid item xs></Grid>
                                    <Grid item></Grid>
                                </Grid>
                            </Box>
                        )}
                    </Formik>
                </Box>
            </Container>
            <Loading open={open} />
            <AlertMessage open={openAlert} type={'error'} message={message} />
        </ThemeProvider>
    );
}

export default Login;
