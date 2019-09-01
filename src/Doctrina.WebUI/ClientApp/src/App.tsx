import React from 'react';
import { Stack, Text, Link, FontWeights } from 'office-ui-fabric-react';
import Login from './containers/Login/Login';
import { withRouter } from "react-router-dom";

const AsyncDashboard = React.lazy(() => import('./containers/Dashboard/Dashboard'));

import { RouteComponentProps } from 'react-router-dom';

const boldStyle = {
  root: { fontWeight: FontWeights.semibold }
};

export interface IAppProps {

}

export interface IAppParams {

}

const App: React.FunctionComponent<RouteComponentProps<IAppParams> & IAppProps> = (props) =>
{
  const [isAuthenticated, authenticate] = React.useState(false);

  const onAuthenticated = () =>
  {

  };

  return (
    <React.Fragment>
      {isAuthenticated ? { AsyncDashboard } : <Login onSuccess={onAuthenticated} />}
    </React.Fragment>
  );
};

export default withRouter(App);
