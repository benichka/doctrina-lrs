import React from 'react';
import { withRouter } from "react-router-dom";
import { RouteComponentProps } from 'react-router-dom';
import Authorize from './containers/Authorize/Authorize';

const AsyncDashboard = React.lazy(() => import('./containers/Dashboard/Dashboard'));

export interface IAppProps
{
}

export interface IAppParams
{

}

const App: React.FunctionComponent<RouteComponentProps<IAppParams> & IAppProps> = (props) =>
{
    return (
        <React.Fragment>
            <Authorize>
                <AsyncDashboard />
            </Authorize>
        </React.Fragment>
    );
};

export default withRouter(App);
