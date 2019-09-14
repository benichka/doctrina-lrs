import { createStore, compose, Middleware, applyMiddleware, combineReducers } from "redux";
import authReducer from "./Auth/AuthReducer";
import thunk, { ThunkAction, ThunkDispatch } from 'redux-thunk';
import { AuthActionTypes } from "./Auth/AuthTypes";

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
    auth: authReducer
});

export type AppStoreState = ReturnType<typeof rootReducer>

export type AppStoreActions = AuthActionTypes

export type ThunkResult<R> = ThunkAction<R, AppStoreState, null, AppStoreActions>

export type ThunkDispatch = ThunkDispatch<AppStoreState, null, AppStoreActions>

const composeEnhancers = window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose;

const AppStore = createStore(rootReducer, composeEnhancers(applyMiddleware(logger, thunk)));

export default AppStore;

