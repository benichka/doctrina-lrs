import { ActionCreator } from 'redux';
import { ThunkAction } from 'redux-thunk';
import
{
    IAuthState,
    IAuthLoginAction,
    IAuthLogoutAction,
    IAuthTimeoutAction
} from './AuthTypes';
import
{
    AUTH_LOGIN,
    AUTH_LOGOUT,
    AUTH_CHECK_SIGN_IN
} from './AuthTypes';

import { AppStoreState } from '../AppStore';



/**
 * Logout action creator
 */
const logoutAction: ActionCreator<IAuthLogoutAction> = () =>
{
    return {
        type: AUTH_LOGOUT
    };
};

/**
 * Logout async
 */
export const logoutAsync: ActionCreator<
    ThunkAction<Promise<void>, IAuthState, null, IAuthLogoutAction>
> = () =>
{
    return async (dispatch, getState) =>
    {
        if (getState().authenticated)
        {
            await fetch(`/api/auth/logout`, { method: 'POST' });
            dispatch(logoutAction());
        } else
        {
            dispatch(logoutAction());
        }
    }
}

/**
 * Login action creator
 */
export const loginAction = (): IAuthLoginAction =>
{
    return {
        type: AUTH_LOGIN
    }
}

/**
 * Login async
 */
export const loginAsync: ActionCreator<
    ThunkAction<Promise<void>, AppStoreState, null, IAuthLoginAction>
> = (username: string, password: string) =>
{
    return async (dispatch) =>
    {
        const formData = new FormData();
        formData.set('username', username);
        formData.set('password', password);

        const response = await fetch(`/api/auth/login`, {
            method: 'POST',
            body: JSON.stringify({ username, password }),
            headers: {
                "Accept": "application/json",
                "Content-Type": "application/json"
            }
        });

        if (response.ok)
        {
            dispatch(loginAction());
        }
    }
}

/**
 * Login action creator
 */
export const checkAuthTimeoutAction = (): IAuthTimeoutAction =>
{
    return {
        type: AUTH_CHECK_SIGN_IN
    }
}

/**
 * Check auth async
 */
export const checkAuthTimeout: ActionCreator<
    ThunkAction<Promise<void>, IAuthState, null, IAuthTimeoutAction | IAuthLogoutAction>
> = () =>
    {
        return async (dispatch, getState) =>
        {
            const result = await fetch(`/api/auth/check`, { method: 'GET' });
            switch (result.status)
            {
                case 200:
                    dispatch(checkAuthTimeoutAction());
                case 401:
                    dispatch(logoutAction());
            }
        }
    }
