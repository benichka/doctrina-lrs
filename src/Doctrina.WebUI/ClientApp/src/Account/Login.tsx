import React from 'react';
import { Stack, Image, ImageFit, Label, TextField, PrimaryButton } from 'office-ui-fabric-react';

const Login: React.FunctionComponent = () =>
{
    return (
        <Stack horizontal styles={{ root: { height: '100%', alignItems: 'stretch' } }}>
            <Stack grow={3}>
                <Image
                    src="http://placehold.it/2000x2000"
                    alt=""
                    imageFit={ImageFit.cover}
                    width={'100%'}
                    height={'100%'}
                />
            </Stack>
            <Stack grow tokens={{ padding: 30 }} styles={{ root: { verticalAlign: "center" } }}>
                <Stack>
                    <Label required={true}>Username</Label>
                    <TextField />
                    <Label required={true}>Password</Label>
                    <TextField type="password" />
                    <PrimaryButton text={'Login'} />
                </Stack>
            </Stack>
        </Stack>
    );
}

export default Login;
