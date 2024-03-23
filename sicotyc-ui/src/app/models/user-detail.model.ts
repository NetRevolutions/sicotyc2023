
export class UserDetail {
    constructor(
        public dateOfBirth: Date,
        public documentType: string,
        public documentNumber: string,
        public address: string,
        public id?: string,
        public userDetailId?: string
    ) {}
}