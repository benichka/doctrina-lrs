import { Reducer } from 'redux';
import { ClientActionTypes, LOGIN, LOGOUT, IClientState } from './types';

const initialState: IClientState = {
    authenticated: false
};

const clientReducer: Reducer<IClientState, ClientActionTypes> = (state = initialState, action) =>
{
    switch (action.type)
    {
        case LOGIN: {
            return {
                ...state,
                authenticated: true
            };
        }
        case LOGOUT: {
            return {
                ...state,
                authenticated: false
            }
        }
    }
    return action;
};

export default clientReducer;