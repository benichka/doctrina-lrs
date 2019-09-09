import { Action } from "redux";

export const LOGIN = 'LOGIN';
export const LOGOUT = 'LOGOUT';
export const CHECK_SIGN_IN = 'CHECK_SIGN_IN';

export interface ILoginAction extends Action
{
    type: typeof LOGIN
}

export interface ILogoutAction extends Action
{
    type: typeof LOGOUT
}

export interface ICheckSignInAction extends Action
{
    type: typeof CHECK_SIGN_IN
}


export type ClientActionTypes = ILoginAction
    | ILogoutAction;

/**
 * Client State
 */
export interface IClientState
{
    authenticated: boolean;
}