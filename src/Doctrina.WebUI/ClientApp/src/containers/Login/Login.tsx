import React, { useState } from 'react';
import { Stack, Image, ImageFit, Label, TextField, PrimaryButton } from 'office-ui-fabric-react';
import { RouteComponentProps, withRouter } from 'react-router';

export interface ILoginProps
{
    onSuccess: () => void;
}

export interface ILoginParams
{
    returnUrl?: string;
}

const Login: React.FunctionComponent<RouteComponentProps<ILoginParams> & ILoginProps> = (props) =>
{
    const [errorMessage, setErrorMessage] = useState("");
    const [showSuccess, setShowSuccess] = useState(false);
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    const handleSubmit = (event: React.FormEvent) =>
    {
        console.log(event);
        fetch('/api/login/')
            .then(response => {

            })
            .catch(reason => {
                setErrorMessage(reason);
            });
        event.preventDefault();
    }

    const handleEmailChange = (event:React.FormEvent<HTMLInputElement | HTMLTextAreaElement>, newValue:string | undefined) =>
    {
        if(newValue){
            setEmail(newValue);
        }else{
            setEmail("");
        }
    }

    const handlePasswordChange = (event:React.FormEvent<HTMLInputElement | HTMLTextAreaElement>, newValue:string | undefined) =>
    {
        if(newValue){
            setPassword(newValue);
        }else{
            setPassword("");
        }
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
                        <TextField type="email" onChange={handleEmailChange} />
                        <Label required={true}>Password</Label>
                        <TextField type="password" onChange={handlePasswordChange} />
                        <PrimaryButton text='Login' type="submit" />
                    </form>
                </Stack>
            </Stack>
        </Stack>
    );
}

export default withRouter(Login);
