import styled from './heading.module.scss';

const Heading = ({ title }: any) => {
    return (
        <>
            <div className={styled.heading}>
                <h6>{title}</h6>
            </div>
        </>
    );
};

export default Heading;
