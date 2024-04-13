import { SubMenuItem } from "./sub-menu-item.interface";

export interface MenuItem {
    title?: string,
    icon?: string,
    submenu?: SubMenuItem[]
}