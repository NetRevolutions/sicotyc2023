import { environment } from "src/environments/environment.development";
import { UserDetail } from "./user-detail.model";

const base_url = environment.base_url;

export class User {

    constructor(
        public firstName: string,
        public lastName: string,
        public userName: string,
        public email: string,
        public password?: string,
        public img?: string,
        public phoneNumber?: string,
        public roles?: string[],
        public id?: string,
        public ruc?: string,
        public userDetail?: UserDetail
    ) {}

    get imageUrl() {
        if (!this.img) {
            return `${base_url}/upload/users/no-image`;
        } else if (this.img) {
            return `${base_url}/upload/users/${this.img}`;
        } else {
            return `${base_url}/upload/users/no-image`;
        }
        
    }
}