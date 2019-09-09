import { createStore, compose, Middleware, applyMiddleware, combineReducers } from "redux";
import clientReducer from "./client/reducers";
import thunk, { ThunkAction, ThunkDispatch } from 'redux-thunk';
import { ClientActionTypes } from "./client/types";

declare global
{
    interface Window
    {
        __REDUX_DEVTOOLS_EXTENSION_COMPOSE__: any;
    }
}

const logger: Middleware = (state) => (next) => (action) =>
{
    const result = next(action);
    return result;
}

const rootReducer = combineReducers({
    client: clientReducer
});

export type StoreState = ReturnType<typeof rootReducer>

export type StoreActions = ClientActionTypes

export type ThunkResult<R> = ThunkAction<R, StoreState, null, StoreActions>

export type ThunkDispatch = ThunkDispatch<StoreState, null, StoreActions>

const composeEnhancers = window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose;

const store = createStore(rootReducer, composeEnhancers(applyMiddleware(logger, thunk)));

export default store;

