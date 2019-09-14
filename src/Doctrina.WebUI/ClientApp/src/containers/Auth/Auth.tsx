import React from 'react';
import Login from './Login';
import Logout from './Logout';
import { AppStoreState } from '../../store/AppStore';
import { connect } from 'react-redux';


const Auth: React.FC<AuthProps> = (props) => {

    return (
        <React.Fragment>
            {!props.isAuthenticated
            ? <Login />
            : <Logout />}
        </React.Fragment>
    );
}

const mapStateToProps = (state: AppStoreState) =>
{
    return {
        isAuthenticated: state.auth.authenticated
    }
}

type AuthProps = ReturnType<typeof mapStateToProps>;

export default connect(mapStateToProps)(Auth);