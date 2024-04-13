import { IEmailItem } from "./email-item.interface";

export interface IEmailMetadata {
    toAddress?: IEmailItem[],
    toCC?: IEmailItem[],
    toBCC?: IEmailItem[],
    subject?: string,
    body?: string,
    isHTML?: boolean,
    attachmentPath?: string
}