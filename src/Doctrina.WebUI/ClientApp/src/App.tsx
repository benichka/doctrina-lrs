import React, { Suspense } from 'react';
import { withRouter, Switch, Route, Redirect } from "react-router-dom";
import { RouteComponentProps } from 'react-router-dom';
import { AppStoreState } from './store/AppStore';
import { connect } from 'react-redux';
import Login from './containers/Auth/Login';
import { Spinner } from 'office-ui-fabric-react/lib/Spinner';

const AsyncDashboard = React.lazy(() => import('./containers/Dashboard/Dashboard'));

export interface IAppParams
{

}

const App: React.FunctionComponent<RouteComponentProps<IAppParams> & AppStateProps> = (props) =>
{
    return (
        <React.Fragment>
            {props.isAuthenticated
            ? <Suspense fallback={Spinner}>
                <AsyncDashboard />
            </Suspense>
            : <Login /> }
        </React.Fragment>
    );
};

const mapStateToProps = (state: AppStoreState) => {
    return {
        isAuthenticated: state.auth.authenticated
    };
}

type AppStateProps = ReturnType<RouteComponentProps<IAppParams> & typeof mapStateToProps>;

export default withRouter(connect(mapStateToProps)(App));
