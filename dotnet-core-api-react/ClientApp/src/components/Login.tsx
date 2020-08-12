import * as React from 'react';

const Login = () => {
    return <div>
        <button onClick={() => {
            window.location.href = "/api/Customer/login"
        }}>Login</button>
        </div>
}
export default Login