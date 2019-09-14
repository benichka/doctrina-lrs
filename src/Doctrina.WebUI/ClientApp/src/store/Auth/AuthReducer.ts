import { Reducer } from 'redux';
import { AuthActionTypes, AUTH_LOGIN, AUTH_LOGOUT, IAuthState } from './AuthTypes';

const initialState: IAuthState = {
    authenticated: false
};

const authReducer: Reducer<IAuthState, AuthActionTypes> = (state = initialState, action) =>
{
    switch (action.type)
    {
        case AUTH_LOGIN: {
            return {
                ...state,
                authenticated: true
            };
        }
        case AUTH_LOGOUT: {
            return {
                ...state,
                authenticated: false
            }
        }
    }
    return action;
};

export default authReducer;