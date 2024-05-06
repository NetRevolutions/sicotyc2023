import { ISubMenuItem } from "./sub-menu-item.interface";

export interface IMenuItem {
    title?: string,
    icon?: string,
    submenu?: ISubMenuItem[]
}