import axios from 'axios';
import authHeader from './auth-header';

const API_URL = window.origin+ '/api/';

class AuthService {
    login(username, password) {

        return axios
            .post(API_URL + '/auth/signin', {
                username: username,
                password: password
            },
                { headers: ["content-type:application/json"]})
            .then(response => {
                if (response.data!=null) {
                    localStorage.setItem('user', JSON.stringify(response.data));
                }
                return response.data;
            });
    }

    logout() {
        localStorage.removeItem('user');
    }

    register(user) {
        return axios.post(API_URL + '/Account/signup', {
            username: user.username,
            email: user.email,
            password: user.password
        }, { headers: authHeader() });
    }
}

export default new AuthService();