import React from 'react';
import { Stack, Text, Link, FontWeights } from 'office-ui-fabric-react';
import Login from './Account/login';
import Dashboard from './Dashboard/Dashboard';
import { BrowserRouter } from 'react-router-dom';

const boldStyle = {
  root: { fontWeight: FontWeights.semibold }
};

export const App: React.FunctionComponent = () =>
{
    const [isAuthenticated, authenticate] = React.useState(false);

    return (
        <BrowserRouter>
            {isAuthenticated ? <Dashboard /> : <Login /> }
        </BrowserRouter>
    //<Stack
    //  horizontalAlign="center"
    //  verticalAlign="center"
    //  verticalFill
    //  styles={{
    //    root: {
    //      width: '960px',
    //      margin: '0 auto',
    //      textAlign: 'center',
    //      color: '#605e5c'
    //    }
    //  }}
    //  gap={15}
    //>
    //  <img src={logo} alt="logo" />
    //  <Text variant="xxLarge" styles={boldStyle}>
    //    Welcome to Your UI Fabric App
    //  </Text>
    //  <Text variant="large">For a guide on how to customize this project, check out the UI Fabric documentation.</Text>
    //  <Text variant="large" styles={boldStyle}>
    //    Essential Links
    //  </Text>
    //  <Stack horizontal gap={15} horizontalAlign="center">
    //    <Link href="https://developer.microsoft.com/en-us/fabric">Docs</Link>
    //    <Link href="https://stackoverflow.com/questions/tagged/office-ui-fabric">Stack Overflow</Link>
    //    <Link href="https://github.com/officeDev/office-ui-fabric-react/">Github</Link>
    //    <Link href="https://twitter.com/officeuifabric">Twitter</Link>
    //  </Stack>
    //  <Text variant="large" styles={boldStyle}>
    //    Design System
    //  </Text>
    //  <Stack horizontal gap={15} horizontalAlign="center">
    //    <Link href="https://developer.microsoft.com/en-us/fabric#/styles/icons">Icons</Link>
    //    <Link href="https://developer.microsoft.com/en-us/fabric#/styles/typography">Typography</Link>
    //    <Link href="https://developer.microsoft.com/en-us/fabric#/styles/themegenerator">Theme</Link>
    //  </Stack>
    //</Stack>
  );
};