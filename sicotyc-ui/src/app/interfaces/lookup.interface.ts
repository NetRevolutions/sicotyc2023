export interface IRegisterLookupCodeGroup {
    lookupCodeGroupName: string,
    lookupCodes?: ILookupCode[]
};

export interface IUpdateLookupCodeGroup {
    lookupCodeGroupId?: string,
    lookupCodeGroupName: string,
    lookupCodes?: ILookupCode[]
};


export interface IRegisterLookupCode {    
    lookupCodeGroupId: string,
    lookupCodeName: string,
    lookupCodeValue: string,
    lookupCodeOrder: number
};

export interface IUpdateLookupCode {
    lookupCodeId: string,
    lookupCodeGroupId: string,
    lookupCodeName: string,
    lookupCodeValue: string,
    lookupCodeOrder: number
};

export interface ILookupCodeGroup {
    lookupCodeGroupId?: string,
    lookupCodeGroupName: string,
    lookupCodes?: ILookupCode[]
};

export interface ILookupCode {
    lookupCodeId?: string,
    lookupCodeGroupId: string,
    lookupCodeName: string,
    lookupCodeValue: string,
    lookupCodeOrder: number
};