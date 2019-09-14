import React from "react";
import { AppStoreState } from '../../store/AppStore';
import { connect } from 'react-redux';

class Logout extends React.Component
{
    componentWillMount()
    {

    }

    render(){
        return (<h1>Goodbye</h1>)
    }
}

const mapStateToProps = (state: AppStoreState) => {
    return {

    };
}

export default connect(mapStateToProps)(Logout);