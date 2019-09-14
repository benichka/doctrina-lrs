import { Action } from "redux";

export const AUTH_LOGIN = 'AUTH_LOGIN';
export const AUTH_LOGOUT = 'AUTH_LOGOUT';
export const AUTH_CHECK_SIGN_IN = 'AUTH_CHECK_SIGN_IN';

export interface IAuthLoginAction extends Action
{
    type: typeof AUTH_LOGIN
}

export interface IAuthLogoutAction extends Action
{
    type: typeof AUTH_LOGOUT
}

export interface IAuthTimeoutAction extends Action
{
    type: typeof AUTH_CHECK_SIGN_IN
}


export type AuthActionTypes = IAuthLoginAction
    | IAuthLogoutAction;

/**
 * Client State
 */
export interface IAuthState
{
    authenticated: boolean;
}