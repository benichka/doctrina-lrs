import { ActionCreator } from 'redux';
import { ThunkAction } from 'redux-thunk';
import
{
    IClientState,
    ILoginAction,
    ILogoutAction,
    ICheckSignInAction
} from './types';
import
{
    LOGIN,
    LOGOUT,
    CHECK_SIGN_IN
} from './types';

import { StoreState } from '../store';



/**
 * Logout action creator
 */
const logoutAction: ActionCreator<ILogoutAction> = () =>
{
    return {
        type: LOGOUT
    };
};

/**
 * Logout async
 */
export const logoutAsync: ActionCreator<
    ThunkAction<Promise<void>, IClientState, null, ILogoutAction>
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
export const loginAction = (): ILoginAction =>
{
    return {
        type: LOGIN
    }
}

/**
 * Login async
 */
export const loginAsync: ActionCreator<
    ThunkAction<Promise<void>, StoreState, null, ILoginAction>
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
export const checkSignInAction = (): ICheckSignInAction =>
{
    return {
        type: CHECK_SIGN_IN
    }
}


/**
 * Check auth async
 */
export const checkSignInAsync: ActionCreator<
    ThunkAction<Promise<void>, IClientState, null, ICheckSignInAction>
> = () =>
    {
        return async (dispatch, getState) =>
        {
            const result = await fetch(`/api/auth/check`, { method: 'GET' });
            switch (result.status)
            {
                case 200:
                    dispatch(checkSignInAction())
            }
        }
    }
