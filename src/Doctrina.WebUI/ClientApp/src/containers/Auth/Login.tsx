import React, { useState } from 'react';
import { Stack, Image, ImageFit, Label, TextField, PrimaryButton } from 'office-ui-fabric-react';
import { RouteComponentProps, withRouter } from 'react-router';
import { loginAsync } from '../../store/Auth/AuthActions';
import { connect } from 'react-redux';
import { AppStoreState, ThunkDispatch } from '../../store/AppStore';

export interface ILoginParams
{
    returnUrl?: string;
}

interface ILoginModel{
    username: string | undefined;
    password: string | undefined;
}

const Login: React.FunctionComponent<RouteComponentProps<ILoginParams> & ILoginDispatchProps> = (props) =>
{
    const [loginModel, setLoginModel] = useState<ILoginModel>({
        username: "",
        password: ""
    });

    const handleSubmit = (event: React.FormEvent<HTMLFormElement>) =>
    {
        event.preventDefault();
        if(loginModel.username && loginModel.password){
            props.handleLogin(loginModel.username, loginModel.password);
        }
    }

    const handleUsernameChange = (event: React.FormEvent<HTMLInputElement | HTMLTextAreaElement>, newValue: string | undefined) =>
    {
        setLoginModel({...loginModel, username: newValue});
    }

    const handlePasswordChange = (event: React.FormEvent<HTMLInputElement | HTMLTextAreaElement>, newValue: string | undefined) =>
    {
        setLoginModel({...loginModel, password: newValue});
    }

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
                    <form onSubmit={handleSubmit}>
                        <Label required={true}>E-mail</Label>
                        <TextField type="email" name="email" required validateOnFocusIn onChange={handleUsernameChange} />
                        <Label required={true}>Password</Label>
                        <TextField type="password" name="password" required onChange={handlePasswordChange} />
                        <PrimaryButton text='Login' type="submit" />
                    </form>
                </Stack>
            </Stack>
        </Stack>
    );
}


const mapStateToProps = (state: AppStoreState) =>
{
    return {
        isAuthenticated: state.auth.authenticated
    }
}

const mapDispatchToProps = (dispatch: ThunkDispatch) =>
{
    return {
        handleLogin: (username:string, password: string) => dispatch(loginAsync(username, password))
    }
}

type ILoginDispatchProps = ReturnType<typeof mapStateToProps> & ReturnType<typeof mapDispatchToProps>;

export default connect(mapStateToProps, mapDispatchToProps)(withRouter(Login));
