import React from 'react';
import Login from '../Login/Login';
import { connect } from 'react-redux';
import { loginAsync, logoutAsync} from '../../store/client/actions';
import { IClientState } from '../../store/client/types';
import { ThunkDispatch } from '../../store/store';

export interface IAuthorizeProps
{
}

const Authorize: React.FC<IAuthorizeProps & IAuthorizeDispatchProps> = (props) =>
{
    if (!props.isAuthenticated)
    {
        return (
            <React.Fragment>
                <Login />
            </React.Fragment>
        );
    }

    return (
        <React.Fragment>
            {props.children}
        </React.Fragment>
    );
}

const mapStateToProps = (state: IClientState) =>
{
    return {
        isAuthenticated: state.authenticated
    }
}

const mapDispatchToProps = (dispatch: ThunkDispatch) =>
{
    return {
        handleLoginSuccess: (username:string, password: string) =>
            dispatch(loginAsync()),

        handleLogout: () => dispatch(logoutAsync)
    }
}

type IAuthorizeDispatchProps = ReturnType<typeof mapStateToProps> & ReturnType<typeof mapDispatchToProps>;

export default connect(mapStateToProps, mapDispatchToProps)(Authorize);