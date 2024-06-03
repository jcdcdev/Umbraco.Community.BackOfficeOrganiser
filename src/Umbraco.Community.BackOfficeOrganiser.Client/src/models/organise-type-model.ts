import {OrganiseType} from "./organise-type.ts";

export class OrganiseTypeModel {
	value: OrganiseType;
	label: string;
	selected: boolean;
	description: string;
	constructor(value: number, label: string, description :string, selected: boolean) {
		this.value = value;
		this.label = label;
		this.description = description;
		this.selected = selected;
	}
}