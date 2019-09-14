import React from 'react';
import { connect } from 'react-redux';
import { loginAsync, logoutAsync} from '../../store/Auth/AuthActions';
import { IAuthState } from '../../store/Auth/AuthTypes';
import { ThunkDispatch } from '../../store/AppStore';
import { Redirect } from 'react-router-dom';

export interface IAuthorizeProps
{
}

const Authorize: React.FC<IAuthorizeProps & IAuthorizeDispatchProps> = (props) =>
{
    if (!props.isAuthenticated)
    {
        return (
            <React.Fragment>
                <Redirect to="/auth" />
            </React.Fragment>
        );
    }

    return (
        <React.Fragment>
            {props.children}
        </React.Fragment>
    );
}

const mapStateToProps = (state: IAuthState) =>
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