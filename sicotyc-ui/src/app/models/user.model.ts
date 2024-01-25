export class User {

    constructor(
        public firstName: string,
        public lastName: string,
        public userName: string,
        public email: string,
        public password?: string,
        public img?: string,
        public roles?: string[],
        public id?: string,
    ) {}
}