//import axios from 'axios';
//import authHeader from './auth-header';
import Menu from "../data/menu.json"

//const API_URL = window.origin+ '/api/Settings';

class MenuService {
    getMenu(){
        return Menu.data
    }
}

export default new MenuService()

