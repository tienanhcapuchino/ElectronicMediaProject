import { Suspense } from 'react';
import Loading from '.';

const Loadable = (Component: any) => (props: any) => {
    return (
        <Suspense fallback={<Loading />}>
            <Component {...props} />
        </Suspense>
    );
};

export default Loadable;
